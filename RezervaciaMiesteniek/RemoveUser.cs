using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervaciaMiesteniek
{
    class RemoveUser
    {
        MySqlConnection connection;

        public RemoveUser()
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

        private bool OpenCon() //ovorenie spojenia
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

        private bool CloseConnection() //uzavretie pripojeniea
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

        private void deletePassenger(string passengerID, string firstName, string lastName, string mail, string phone, string username, string password, string isAdmin)
        {
            if (OpenCon())
            {
                string query = "delete from Passenger where id like '" + passengerID + "' && firstName like '" + firstName + "' && lastName like '" + lastName + "';";
                string query2 = "delete from Passenger_detail where mail like '" + mail + "' && phone like '" + phone + "';";
                string query3 = "delete from Passenger_login where passengerId like '" + passengerID + "' && username like '" + username + "' && password like '" + password + "' && isAdmin like '" + isAdmin + "';";
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                CloseConnection();
            }
        }

        public void removeUser()
        {
            
        }
    }
}
