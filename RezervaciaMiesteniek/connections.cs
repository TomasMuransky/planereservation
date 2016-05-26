using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace plane_reservation_xml
{
    class Connection
    {
        public static string connectionString { get; set; }
        public static string tableName { get; set; }
        protected string status { get; set; }
        protected SqlConnection connection;
        protected SqlCommand command;
        protected SqlDataReader dataReader;

        protected DataTable LoadDataWithAllColums()
        {
            DataTable data = null;
            try
            {
                connection = new SqlConnection(connectionString);
                string query = string.Format("select * from passenger, passenger_detail, passenger_login");
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        dataReader = command.ExecuteReader();
                        data = new DataTable();
                        data.Load(dataReader);
                    }
                }
                return data;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Load all data error");
            }
        }
}
