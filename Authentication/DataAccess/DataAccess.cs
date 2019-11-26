using System;
using MySql.Data.MySqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data.AccessLayer
{
    public class DataAccess
    {
        const string CONNECTION_STRING = @"Data source=localhost; Database=kraken_bracket; User ID=root; Password=Gray$cale917!!";

        /// <summary>
        /// Method used to check if email and password used.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>
        /// True if both email and password exist. False if at least 1 does not.
        /// </returns>
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
            conn.Close();
            if (count == 1)
            {
                return true;
            }
            else
            {
                throw new ArgumentException("Email or password does not exist");
            }
        }

        /// <summary>
        /// Method used to test the MySQL connection of the database.
        /// </summary>
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
