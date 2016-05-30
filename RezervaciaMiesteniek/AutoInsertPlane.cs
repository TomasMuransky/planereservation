using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervaciaMiesteniek
{
    class AutoInsertPlane
    {
        MySqlConnection connection;
        DateTime systemDate = DateTime.Now;

        public AutoInsertPlane()
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
        /***************************************************************/
        /*                                                             */
        /*                                                             */
        /*                                                             */
        /*                                                             */
        /***************************************************************/
        private bool checkIsPaneExist(DateTime future)
        {
            string result;

            string sqlQuery = "select * from plane_destinations where TimeOfDeparture like '"+future.ToString("yyyy-MM-dd")+"%';"; //dotaz
            MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
            if (OpenCon())
            {
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    result = reader["id"].ToString();
                    CloseConnection();
                    if (result != null)
                        return true;
                    else
                        return false;
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            
            if (connection != null) 
                CloseConnection();
            return false;
        }

        private void addPlanne() //nove lietadlo
        {
           
            if (OpenCon())
            {
                string query = "insert into planes(planeName) values('sovy_air_lines');";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                CloseConnection();
            }
        }
        private string getPlaneID() //zisanie id posledneho lietadla
        {
            string res = null;
            if (OpenCon())
            {
                string query = "select max(id) as id from planes;";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    res = reader["id"].ToString();
                }
                catch(MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                CloseConnection();
            }
            return res;
        }

        private void addSeats(string plane_ID) //pridane sedadiel 4x vlolane
        {
            for (int i = 1; i <= 80; i++)
            {
                if (OpenCon())
                {
                    string query = "insert into seats(planeId,seatId,passengerId,seatIsTaken) values('" + plane_ID + "','" + i.ToString() + "',0,'N');";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (MySqlException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    CloseConnection();
                }
            }
        }
        /***************/
        private void addPlaneDestination1(DateTime date, string plane_id) //pridanie prvej cesty
        {
            if (OpenCon())
            {
                string query = "INSERT INTO Plane_Destinations(planeId,TimeOfDeparture,TimeOfArriving,fromId,toId) VALUES('" + plane_id+"','"+date.ToString("yyyy-MM-dd")+ " 07:00:00','"+ date.ToString("yyyy-MM-dd") + " 08:00:00','Kosice','Bratislava');";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch(MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                CloseConnection();
                addSeats(plane_id);
            }
        }
        /***************/
        private void addPlaneDestination2(DateTime date, string plane_id) //pridanie druhej cesty
        {
            if (OpenCon())
            {
                string query = "INSERT INTO Plane_Destinations(planeId,TimeOfDeparture,TimeOfArriving,fromId,toId) VALUES('" + plane_id + "','" + date.ToString("yyyy-MM-dd") + " 07:00:00','" + date.ToString("yyyy-MM-dd") + " 08:00:00','Bratislava','Kosice');";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                CloseConnection();
                addSeats(plane_id);
            }
        }
        /***************/
        private void addPlaneDestination3(DateTime date, string plane_id) //pridanie tretej cesty
        {
            if (OpenCon())
            {
                string query = "INSERT INTO Plane_Destinations(planeId,TimeOfDeparture,TimeOfArriving,fromId,toId) VALUES('" + plane_id + "','" + date.ToString("yyyy-MM-dd") + " 19:00:00','" + date.ToString("yyyy-MM-dd") + " 20:00:00','Kosice','Bratislava');";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                CloseConnection();
                addSeats(plane_id);
            }
        }
        /***************/
        private void addPlaneDestination4(DateTime date, string plane_id) //pridanie stvrtej cesty
        {
            if (OpenCon())
            {
                string query = "INSERT INTO Plane_Destinations(planeId,TimeOfDeparture,TimeOfArriving,fromId,toId) VALUES('" + plane_id + "','" + date.ToString("yyyy-MM-dd") + " 19:00:00','" + date.ToString("yyyy-MM-dd") + " 20:00:00','Bratislava','Kosice');";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                CloseConnection();
                addSeats(plane_id);
            }
        }

        private void addNewPlane(DateTime date) //seria prikazov na pridanie do databazy
        {
            string plane_id;
            addPlanne(); //prva cesta
            plane_id = getPlaneID();
            addPlaneDestination1(date, plane_id);
            addPlanne(); //druha cesta
            plane_id = getPlaneID();
            addPlaneDestination2(date, plane_id);
            addPlanne(); //treria cesta
            plane_id = getPlaneID();
            addPlaneDestination3(date, plane_id);
            addPlanne(); //stvrta cesta
            plane_id = getPlaneID();
            addPlaneDestination4(date, plane_id);
        }


        /****************************************************************/
        public void autoInset() //jedina public metoda
        {
            DateTime future = systemDate.AddDays(30); //datum 30 dni do predu
            Console.WriteLine(getPlaneID());

            for (int i =0; i < 30; i++)
            {
                if (!checkIsPaneExist(future))
                {
                    addNewPlane(future);
                    future = future.AddDays(-1);
                }
             
            }
        }
    }
}
