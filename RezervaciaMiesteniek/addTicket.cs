using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervaciaMiesteniek
{
    class AddTicket
    {
        MySqlConnection connection;
        DateTime systemDate = DateTime.Now;

        public AddTicket()
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
        /******************************************************************************************/
        public string getPlaneID(DateTime date, string from, string time)
        {
            if (OpenCon())
            {
                try
                {
                    string query = "select * from plane_destinations where TimeOfDeparture like'"+date.ToString("yyyy-MM-dd")+" "+time+ "' && fromId like '"+from+"';";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    string result = reader["planeId"].ToString(); //zizskanie id lietala z databazy
                    CloseConnection();
                    return result;
                }
                catch(MySqlException e)
                {
                    CloseConnection();
                    Console.WriteLine(e.Message);
                }
               
            }
            return null;
        }
        /*****************************************************************************************/
        public void addTicket(DateTime date, string from, string time,string passengerID, string seatID)
        {
            DateTime dateOfbiung = DateTime.Now;
            string planeID = getPlaneID(date, from, time);  
            string query = "insert into tickets(passengerId,planeId,price,DateOfBuying,seatId) values('"+passengerID+"','"+planeID+"','60','"+dateOfbiung.ToString("yyyy-MM-dd HH:mm:ss")+"','"+seatID+"');";
            if (OpenCon())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery(); //pridanie letenky do databazy
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                CloseConnection();
            }
            string query2 = "UPDATE seats SET passengerId='"+passengerID+ "',seatIsTaken='Y' where planeId like '"+planeID+ "' && seatId like'"+seatID+"';";
            if (OpenCon())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query2, connection); 
                    cmd.ExecuteNonQuery(); // zmena boolean dodnoty sedadla, z neobsadeneho na obsadene
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                CloseConnection();
            }
        }
        /*****************************************************************************************/
        public List<string> getSeatsList(string planeID)
        {
            List<string> seatsList = new List<string>();
            if (OpenCon())
            {
                string query = "select * from seats where planeId like '"+planeID+"';";
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string[] result = new string[2];
                        result[0] = reader["seatId"].ToString();
                        result[1] = reader["seatIsTaken"].ToString(); //boolean ci je sedadlo obsdene
                       
                        if (result[1].Equals("N")) //sedadlo sa prida do zoznamu iba ked nie je obsadene
                        {
                            seatsList.Add(result[0]); //a tu je to slavne pridanie 
                        }
                    }
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                CloseConnection();
            }
            return seatsList;
        }
        
    }
}
