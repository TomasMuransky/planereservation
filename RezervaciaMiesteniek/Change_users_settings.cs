﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RezervaciaMiesteniek
{
    class Change_users_settings
    {

        MySqlConnection connection;
        private string passengerID;

        public Change_users_settings()
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

        public bool check_password(string userID, string password)
        {
            try
            {
                OpenCon();
                string text = null;
                string select0 = "select * from passenger_login where passengerId = '" + userID + "';";
                MySqlCommand cmd = new MySqlCommand(select0, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    text = reader["password"].ToString();
                }
                if (text.Equals(password))
                {
                    CloseConnection();
                    return true;
                }
                else
                {
                    Console.WriteLine("zadali ste zle heslo");
                }
                CloseConnection();
                return false;
            }
            catch (MySqlException e)
            {
                CloseConnection();
                return false;
            }
        }

        public void change_password(string userID, string password)
        {
            if (OpenCon())
            {
                try
                {
                    string insert4 = "update passenger_login set password = '" + password + "' where passengerId = '" + userID + "';";
                    MySqlCommand cmd4 = new MySqlCommand(insert4, connection);
                    cmd4.ExecuteNonQuery();
                    CloseConnection();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e.Message);
                    if (connection != null)
                        CloseConnection();
                }
            }
        }

        public void change_phone(string userID, string phone)
        {
            if (OpenCon())
            {
                try
                {
                    string insert4 = "update passenger_detail set phone = '" + phone + "' where id = '" + userID + "';";
                    MySqlCommand cmd4 = new MySqlCommand(insert4, connection);
                    cmd4.ExecuteNonQuery();
                    CloseConnection();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e.Message);
                    if (connection != null)
                        CloseConnection();
                }
            }
        }

        public void change_mail(string userID, string mail)
        {
            if (OpenCon())
            {
                try
                {
                    string insert4 = "update passenger_detail set mail = '" + mail + "' where id = '" + userID + "';";
                    MySqlCommand cmd4 = new MySqlCommand(insert4, connection);
                    cmd4.ExecuteNonQuery();
                    CloseConnection();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e.Message);
                    if (connection != null)
                        CloseConnection();
                }
            }
        }

    }
}
