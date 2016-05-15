using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervaciaMiesteniek
{
    class DataBase
    {
        MySqlConnection connection;
        private string passengerID;

        public DataBase()
        {
            string connectionStr = "SERVER=localhost;DATABASE=plane;UID=root;PASSWORD=1234;";
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

        private string getPassengerID() // pomocna funkcia na ziskanie id passangera ktore sa pouzije na dalsie inserty;
        {
           
            string text = null;
            string insert0 = "select max(id) as id from passenger;";
            MySqlCommand cmd = new MySqlCommand(insert0, connection);
            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    text = reader["id"].ToString();
                }
            }
            catch(MySqlException e)
            {
                Console.WriteLine("1 "+e.Message);
            }
         
            return text;
        }

        private void insetrtPassenger( string first_name, string last_name) //insert passenger
        {
         
                try
                {
                    string insert1 = "INSERT INTO passenger(firstName, lastName) VALUES('" + first_name + "','" + last_name + "');";
                    MySqlCommand cmd1 = new MySqlCommand(insert1,connection);
                    cmd1.ExecuteNonQuery();
                }
                catch(MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
            
        }

        private void inserPassenger_detail(string email, string phone) //insetr p_details
        {
            try
            {
                string insert2 = "insert into passenger_detail(id, mail, phone) values('"+passengerID+"','"+email+"','"+phone+"');";
                MySqlCommand cmd2 = new MySqlCommand(insert2, connection);
                cmd2.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void insertPassenger_login(string login, string password) //insert login
        {
            try
            {
                string insert3 = "insert into passenger_login(passengerId, username, password,isAdmin) values('" + passengerID+"','"+login+"','"+password+"','N');";
                MySqlCommand cmd3 = new MySqlCommand(insert3, connection);
                cmd3.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void addNewRegistreation(string login, string password, string email, string phone, string first_name, string last_name) //pridanie noveho usera
        {
            if (OpenCon())
            {
                insetrtPassenger(first_name, last_name);
                CloseConnection();
                /****************/
                OpenCon();
                passengerID = getPassengerID();
                CloseConnection();
                /****************/
                OpenCon();
                Console.WriteLine("ID = " + passengerID);
                inserPassenger_detail(email, phone);
                CloseConnection();
                /****************/
                OpenCon();
                insertPassenger_login( login, password);
                CloseConnection();
            }
        }

        public bool isLoginUnique(string login) //funkcia overujuca ci je prihlasovacie meno unikatne
        {
            try
            {
                OpenCon();
                string text = null;
                string select0 = "select username from passenger_login where username = '"+login+"';";
                MySqlCommand cmd = new MySqlCommand(select0, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    text = reader["username"].ToString();
                }
                if (text != null)
                {
                    CloseConnection();
                    return false;
                }
                CloseConnection();
                return true;
            }
            catch (MySqlException e)
            {
                CloseConnection();
                return false;
            }
        }
    }
}
