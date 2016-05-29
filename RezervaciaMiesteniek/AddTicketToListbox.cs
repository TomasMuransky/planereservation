using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervaciaMiesteniek
{
    class AddTicketToListbox
    {
        MySqlConnection connection;
        

        public AddTicketToListbox()
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
        /***************************************************************************/
        /***************************************************************************/
        /***************************************************************************/
        private string getFlyInformation(string planeID)
        {
            string connectionStr = null;
            try
            {
                connectionStr = System.IO.File.ReadAllText("sqlConnectivity.conf");
            }catch
            {
                System.Windows.Forms.MessageBox.Show("file: sqlConnectivity.conf not found");
                System.Windows.Forms.Application.Exit();
            }
             
            MySqlConnection connectio2 = new MySqlConnection(connectionStr); //vytvorenie npveho pripojenia
            try
            {
                string result;
                connectio2.Open();
                string qurery = "select * from plane_destinations where planeId like'" + planeID + "';";
                MySqlCommand cmd = new MySqlCommand(qurery, connectio2);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read(); //ziskanie podrobnych infomacii o lete , odlet , smer
                result = "departure: ";

                string pom = reader["TimeOfDeparture"].ToString();
                DateTime d = DateTime.Parse(pom);
                Console.WriteLine(pom + "\n" + d.ToString()+"\n"+d.ToString("dd.MM.yyyy HH:mm:ss"));
                result += d.ToString("dd.MM.yyyy HH:mm:ss");
               
                result += " \tdirection: ";
                result += reader["fromId"].ToString();
                result += "-";
                result += reader["toId"].ToString();
                connectio2.Close();

                return result;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            if (connectio2 != null)
                connectio2.Close();
            return null;
        }
        //pridanie do listboxu
        public List<string> getTicketsList(string passengerID)
        {
            List<string> list = new List<string>();
            if (OpenCon())
            {
                string query = "select * from tickets where passengerId like'" + passengerID + "'";
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())  //nacitanie zoznamu
                    {
                        string planeID = reader["planeId"].ToString();
                        string seatID = reader["seatId"].ToString();
                        string res = getFlyInformation(planeID);
                        res += " \tseat_number: " + seatID + " \tfly_number: " + planeID;
                        list.Add(res);
                    }
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return list;
        }

    }
}
