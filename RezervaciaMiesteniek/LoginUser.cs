using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervaciaMiesteniek
{
    class LoginUser
    {
        MySqlConnection connection;

        public LoginUser()
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
        /*******************funkcia overujuca platnost vstupnych****************************************/
        public bool loginVerification(string login, string password) 
        {
            if (password == null || login == null)
                return false;
            OpenCon();
            string text = null;
            string select0 = "select * from passenger_login where username = '"+login+"';";
            try
            {
                MySqlCommand cmd = new MySqlCommand(select0, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                text = reader["password"].ToString();
            }
            catch(MySqlException r)
            {
                CloseConnection();
                return false;
            }

            if (text.Equals(password))
            {
                CloseConnection();
                return true;
            }
            CloseConnection();
            return false;
        }
        /*********************************************************/
        public string getUserId(string login)
        {
            OpenCon();
            string text = null;
            string select0 = "select * from passenger_login where username = '" + login + "';";
            try
            {
                MySqlCommand cmd = new MySqlCommand(select0, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                text = reader["passengerId"].ToString();
                CloseConnection();
                return text;
            }
            catch (MySqlException r)
            {
                CloseConnection();
                return null;
            }
        }
        /**************************************************************/
        public string[] getUserName(string id)
        {

            string[] name = new string[2];

            OpenCon();
            string select0 = "select * from passenger where id = '" + id + "';";
            try
            {
                MySqlCommand cmd = new MySqlCommand(select0, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                name[0] = reader["firstName"].ToString();
                name[1] = reader["lastName"].ToString();
                CloseConnection();
                return name;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                CloseConnection();
                return null;
            }

        }
        /*************************************************/
        public bool isAdmin(string login)
        {
            return true;
        }
    }
}
