
using MySql.Data.MySqlClient;
using System;

namespace TBZdatabaseConnection
{
    public class TBZdatabase
    {
        public void Connect(string connectString)
        {
            if (connectString != @"server=localhost; userid=root; password=password; database=cecs491testdb")
            {
                throw new ArgumentException("Wrong info");
            }
            MySqlConnection conn = new MySqlConnection(connectString);
            conn.Open();
            Console.WriteLine("Connection Open");
            //Console.ReadKey();
            conn.Close();
            Console.WriteLine("Connection Closed");
            //Console.ReadKey();

        }

        public void Connects(string connectString)
        {
            MySqlConnection conn = new MySqlConnection(connectString);
            try
            {
                conn.Open();
                Console.WriteLine("Connection Open");
                //Console.ReadKey();
            }
            catch (Exception)
            {
                throw new ArgumentException("Server offline");
            }
            finally
            {
                conn.Close();
                Console.WriteLine("Connection Closed");
                //Console.ReadKey();
            }
        }
    }
}
