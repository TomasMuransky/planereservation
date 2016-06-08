using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervaciaMiesteniek
{
    class RemoveReservation
    {
        MySqlConnection connection;

        public RemoveReservation()
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
        /********************************************************************/
        private void deleteTicket(string passengerID, string planeID, string seatID)
        {
            if (OpenCon())
            {
                string query = "delete from tickets where passengerId like '"+passengerID+"' && planeId like '"+planeID+"' && seatId like '"+seatID+"';";
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

        private void updateSeat(string passengerID, string planeID, string seatID)
        {
            if (OpenCon())
            {
                string query = "update seats set passengerId='0',seatIsTaken='N' where  seatId like '"+seatID+"' && planeId like '"+planeID+"' && passengerId like '"+passengerID+"';";
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void removeReservation(string ticketInfo,string passengerID)
        {
            string[] parts = ticketInfo.Split(' '); //[6] = seatID, [8] = planeID
            for (int i = 0; i < parts.Length; i++)
            {
                Console.WriteLine(parts[i]);
            }
            deleteTicket(passengerID, parts[8], parts[6]);
            updateSeat(passengerID, parts[8], parts[6]);
        }
    }
}
