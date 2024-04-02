using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataConnection
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";

        private static DataConnection connection;

        public static DataConnection Connection
        {
            get
            {
                if (connection == null)
                    connection = new DataConnection();
                return connection;
            }	 	
            private set { DataConnection.connection = value; }
        }

        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable result = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);

                    if (parameter != null)
                    {
                        // command.CommandType = CommandType.StoredProcedure;
                        string[] list = query.Split(' ');
                        int i = 0;
                        foreach (string item in list)
                        {
                            if (item.Contains('@'))
                            {
                                command.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(result);
                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
            return result;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] list = query.Split(' ');
                    int i = 0;
                    foreach (string item in list)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                result = command.ExecuteNonQuery();
                connection.Close();
            }
            return result;
        }

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object result = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] list = query.Split(' ');
                    int i = 0;
                    foreach (string item in list)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                result = command.ExecuteScalar();
                connection.Close();
            }
            return result;
        }
    }
}
