using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Utils
{
    public static class DBManager
    {
        private static string connectionString = "server=localhost;user=root;database=world;port=3306;password=******";

        private static void CreateCommand(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LogProvider.AddLog($"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetString(2)}");
                    }
                }
            }
        }
    }
}
