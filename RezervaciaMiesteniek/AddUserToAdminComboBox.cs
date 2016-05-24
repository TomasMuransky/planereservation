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
                query = "Select * from Passenger;";
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query,connection);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while(rdr.Read())
                    {
                        result = "passenger id: ";
                        result += rdr["id"].ToString();
                        result += "passenger first name: ";
                        result += rdr["firstName"].ToString();
                        result += "passenger last name: ";
                        result += rdr["lastName"].ToString();

                        list.Add(result);
                    }
                }catch(MySqlException ex){ 
                
                    Console.WriteLine(ex.Message);
                }
                
            }
            return list;
        } 
    }
}
