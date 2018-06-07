using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BloodDonation.Library;
using System;

namespace BloodDonation.Repository.Core
{
    public class DbOperations
    {
        #region Methods
        public static void ExecuteCommand(string connectionString, string storedProcedureName, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;

                        command.Parameters.AddRange(parameters);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception("Database error " + e.ToString());
            }
        }

        public static IEnumerable<T> ExecuteQuery<T>(string connectionString, string storedProcedureName,
            params SqlParameter[] parameters) where T : new()
        {
            List<T> result = new List<T>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    if (parameters.Length != 0)
                        command.Parameters.AddRange(parameters);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetObjectFromReader<T>());
                        }
                    }
                }
            }
            return result;
        }
        #endregion
    }
}
