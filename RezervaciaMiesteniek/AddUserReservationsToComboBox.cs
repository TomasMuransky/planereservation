using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervaciaMiesteniek
{
    class AddUserReservationsToComboBox
    {
            MySqlConnection connection;
            private string passengerID;

            public AddUserReservationsToComboBox()
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

        public List<string> ReadUserReservations()
        {
            List<string> list1 = new List<string>();
            if (OpenCon())
            {
                string query;
                string result;
                query = "Select * from Tickets;";
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        result = "ticket id: ";
                        result += rdr["id"].ToString();
                        result += " passenger id: ";
                        result += rdr["passengerId"].ToString();
                        result += " plane id: ";
                        result += rdr["planeId"].ToString();
                        result += "  ticket price: ";
                        result += rdr["price"].ToString();
                        result += "  ticket buying time: ";
                        result += rdr["DateOfBuying"].ToString();
                        result += " seatId: ";
                        result += rdr["seatId"].ToString();

                        list1.Add(result);
                    }
                }
                catch (MySqlException ex)
                {

                    Console.WriteLine(ex.Message);
                }

            }
            return list1;
        }

    }
 }


