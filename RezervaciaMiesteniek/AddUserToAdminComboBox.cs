using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervaciaMiesteniek
{
    
    class AddUserToAdminComboBox
    {
        MySqlConnection connection;
        private string passengerID;

        public AddUserToAdminComboBox()
        {
            string connectionStr = "";
            try
            {
                connectionStr = System.IO.File.ReadAllText("sqlConnectivity.conf");
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("file: sqlConnectivity.conf not found!");
                System.Windows.Forms.Application.Exit();
            }
            connection = new MySqlConnection(connectionStr);
        }

        private bool OpenCon()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        public List<string> ReadUsers()
        {
            List<string> list = new List<string>();
            if(OpenCon())
            {
                string query;
                string result;
                query = "select * from passenger;";
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query,connection);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while(rdr.Read())
                    {
                        result = "passenger id: ";
                        result += rdr["id"].ToString();
                        result += " passenger first name: ";
                        result += rdr["firstName"].ToString();
                        result += " passenger last name: ";
                        result += rdr["lastName"].ToString();

                        list.Add(result);
                    }
                }catch(MySqlException ex){ 
                
                    Console.WriteLine(ex.Message);
                }
                
            }
            return list;
        } 



        public  List<string> add_ticket_to_admin_combobox(string userID)
        {
            List<string> list = new List<string>();
            if (OpenCon())
            {
                string query = "select * from tickets where passengerId like '"+userID+"';";
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string res = "ticket ID: ";
                        res += reader["id"].ToString();
                        res += " passenger ID: ";
                        res += reader["passengerId"].ToString();
                        res += " plane ID: ";
                        res += reader["planeId"].ToString();
                        res += " seat ID: ";
                        res += reader["seatId"].ToString();
                        res += " date of purchase: ";

                        string d = reader["DateOfBuying"].ToString();
                        DateTime dat = DateTime.Parse(d);
                        res += dat.ToString("dd.MM.yyyy HH:mm:ss");

                        list.Add(res);
                    }
                }
                catch(MySqlException e)
                {
                    Console.WriteLine(e.ToString());
                }
                CloseConnection();
            }
           
            return list;
        }

        public string[] get_user_info(string user_id)
        {
            string[] info = new string[10];
            if (OpenCon())
            {
                try
                {
                    string qurey = "select * from passenger where id like'"+user_id+"';";
                    MySqlCommand cmd = new MySqlCommand(qurey,connection);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    info[0] = reader["firstName"].ToString(); //[0] = First name
                    info[1] = reader["lastName"].ToString(); //[1] = Last Name
                    CloseConnection();
                    /*******************************************************************/
                    OpenCon();
                    qurey = "select * from passenger_detail where id like '"+user_id+"';";
                    MySqlCommand cmd2 = new MySqlCommand(qurey, connection);
                    MySqlDataReader reader2 = cmd2.ExecuteReader();
                    if (reader2.Read())
                    {
                        info[2] = reader2["mail"].ToString(); //[2] = mail
                        info[3] = reader2["phone"].ToString(); //[3] = phone
                    }
                    
                    CloseConnection();
                    /************************************************************************/
                    OpenCon();
                    qurey = "select * from passenger_login where passengerId like'"+user_id+"';";
                    MySqlCommand cmd3 = new MySqlCommand(qurey, connection);
                    MySqlDataReader reader3 = cmd3.ExecuteReader();
                    reader3.Read();
                    info[4] = reader3["username"].ToString(); //[4] = username
                    info[5] = reader3["password"].ToString(); //[5] = password
                    info[6] = reader3["isAdmin"].ToString(); //[6] = is_admin
                    info[7] = reader3["passengerId"].ToString(); //[7] = massenger_id
                    CloseConnection();

                }
                catch(MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
            }


            return info;//[0] = First name [1] = Last Name [2] = mail [3] = phone [4] = username [5] = password [6] = is_admin [7] = massenger_id
        }
    }
}
