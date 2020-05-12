using MySql.Data.MySqlClient;
using System;
using TBZ.DatabaseConnectionService;

namespace TBZ.KrakenBracket.DatabaseAccess
{
    public class DataStore
    {
        public bool LogDataStore(string time, string operation, string msg, string id)
        {
            try
            {
                var DB = new Database();
                using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
                {
                    using (MySqlCommand comm = conn.CreateCommand())
                    {
                        comm.CommandText = "INSERT INTO table_logs VALUES(@time, @operation, @errorMessage, @hashedUserID)";
                        comm.Parameters.AddWithValue("@time", time);
                        comm.Parameters.AddWithValue("@operation", operation);
                        comm.Parameters.AddWithValue("@errorMessage", msg);
                        comm.Parameters.AddWithValue("@hashedUserID", id);
                        conn.Open();
                        comm.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
            }
        }
            catch (Exception e)
            {
                return false;
            }
}
    }
}
