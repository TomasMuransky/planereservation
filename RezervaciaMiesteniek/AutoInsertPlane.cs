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




        /****************************************************************/
        public void autoInset()
        {
            systemDate = systemDate.AddDays(30);
            systemDate = systemDate.AddDays(-8).AddHours(5);
            Console.WriteLine(systemDate.ToString());
        }
    }
}
