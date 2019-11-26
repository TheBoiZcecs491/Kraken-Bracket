using System;
using MySql.Data.MySqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data.AccessLayer
{
    public class DataAccess
    {
        const string CONNECTION_STRING = @"Data source=localhost; Database=kraken_bracket; User ID=root; Password=Gray$cale917!!";
        public bool GetEmailAndPassword(string email, string password)
        {
            string queryString = "SELECT * FROM User WHERE Email='" + email + "' AND Password='" + password + "'";
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            MySqlCommand searchCmd = new MySqlCommand(queryString, conn);
            MySqlDataReader reader;

            int count = 0;
            conn.Open();
            reader = searchCmd.ExecuteReader();
            while (reader.Read()) { count++; }
            if (count == 1)
            {
                return true;
            }
            else
            {
                throw new ArgumentException("Email or password does not exist");
            }

        }

        [TestMethod]
        public void TestConnection()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
                conn.Open();
                Console.WriteLine("Connection successful!!!");
                conn.Close();
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }
    }
}
