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
<<<<<<< HEAD
                        res += " date of purchase: ";
=======
                        res += " date of buying: ";
>>>>>>> c9378c63b02a4c695ec1514d789b522d2a6e5ae2

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
    }
}
