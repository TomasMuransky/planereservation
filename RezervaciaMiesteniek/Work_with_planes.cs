using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervaciaMiesteniek
{
    class Work_with_planes
    {
        MySqlConnection connection;

        public Work_with_planes()
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

        public List<string> add_plane_list_listbox()
        {
            List<string> list = new List<string>();
            string query = "select * from plane_destinations";

            if (OpenCon())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query,connection);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string res = "fly ID: ";
                        res += reader["planeId"].ToString();
                        res += " departure: ";

                        DateTime date = DateTime.Parse(reader["TimeOfDeparture"].ToString());
                        res += date.ToString("dd.MM.yyyy HH:mm:ss");
                        res += " landing: ";
                        date = DateTime.Parse(reader["TimeOfArriving"].ToString());
                        res += date.ToString("dd.MM.yyyy HH:mm:ss");
                        res += " from: ";
                        res += reader["fromId"].ToString();
                        res += " to: ";
                        res += reader["toId"].ToString();
                        list.Add(res);
                    }
                }
                catch
                {

                }
                CloseConnection();
            }
            return list;
        }

        public int get_count_of_reservated_seats(string plane_id)
        {
            int count =0;
            string query = "select * from seats where planeId like '"+plane_id+"' && seatIsTaken like'Y';";
            if (OpenCon())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        count++;
                    }
                }
                catch
                {

                }
                CloseConnection();
            }

            return count;
        }

        public void remove_fly(string plane_id)
        {
            if (OpenCon())
            {
                string query = "delete from tickets where planeId like'"+plane_id+"';";
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query,connection);
                    cmd.ExecuteNonQuery();
                    CloseConnection();
                    /***********************************/
                    OpenCon();
                    query = "delete from plane_destinations where planeId like'" + plane_id + "';";
                    MySqlCommand cmd2 = new MySqlCommand(query, connection);
                    cmd2.ExecuteNonQuery();
                    CloseConnection();
                    /***********************************/
                    OpenCon();
                    query = "delete from seats where planeId like'" + plane_id + "';";
                    MySqlCommand cmd3 = new MySqlCommand(query, connection);
                    cmd3.ExecuteNonQuery();
                    CloseConnection();
                    /***********************************/
                    OpenCon();
                    query = "delete from planes where id like'" + plane_id + "';";
                    MySqlCommand cmd4 = new MySqlCommand(query, connection);
                    cmd4.ExecuteNonQuery();
                    CloseConnection();
                }
                catch
                {

                }
            }
        }

        public void remove_reservation_from_admin(string passenger_id,string ticket_id, string plane_id, string seat_id)
        {
            if (OpenCon())
            {
                try
                {
                    string query = "delete from tickets where id like '"+ticket_id+"';";
                    MySqlCommand cmd = new MySqlCommand(query,connection);
                    cmd.ExecuteNonQuery();
                    CloseConnection();
                    /*******************************************************/
                    OpenCon();
                    query = "update seats set passengerId='0',seatIsTaken='N' where planeId like'"+plane_id+"' && seatId like'"+seat_id+"' && passengerId like '"+passenger_id+"';";
                    MySqlCommand cmd2 = new MySqlCommand(query, connection);
                    cmd2.ExecuteNonQuery();
                    CloseConnection();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
