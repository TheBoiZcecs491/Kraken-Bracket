
using MySql.Data.MySqlClient;
using System;
using System.IO;

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

        public void InsertFile(string fileName)
        {
            CheckFileExist(fileName);
            if (CheckFileSize(fileName) && CheckFileDup(fileName))
            {
                //MySqlConnection conn = new MySqlConnection(@"server=localhost; userid=root; password=password; database=cecs491testdb");
                //conn.Open();
                ////comd.(insert into)
                //conn.Close();
            }
            else throw new ArgumentException("Insert Error");

            //FileInfo file = new FileInfo(fileName);
            //if (file.Length > 50)
            //{
            //    throw new ArgumentException("File size too large, not enough space in database");
            //}

            //using (StreamReader sr = File.OpenText(fileName))
            //{
            //    string s = "";
            //    s = sr.ReadLine();
            //    if (s  == "Hello")
            //    {
            //        throw new ArgumentException("duplication error");
            //    }
            //}
        }

        public bool CheckFileSize(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            if (file.Length > 50)//replace 50 with data.avalibleSpace()
            {
                throw new ArgumentException("File size too large, not enough space in database");
            }
            else return true;
        }

        public bool CheckFileDup(string fileName)
        {
            using (StreamReader sr = File.OpenText(fileName))
            {
                string s = "";
                s = sr.ReadLine();
                if (s == "Hello")
                {
                    throw new ArgumentException("Duplication error");
                }
                else return true;
            }
        }


        public bool CheckFileExist(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException("File does not exist");
            }
            else return true;

            //if (!File.Exists(path))
            //{
            //    // Create a file to write to.
            //    using (StreamWriter sw = File.CreateText(path))
            //    {
            //        sw.WriteLine("Hello");
            //    }
            //}

            //// Open the file to read from.
            //using (StreamReader sr = File.OpenText(path))
            //{
            //    string s = "";
            //    while ((s = sr.ReadLine()) != null)
            //    {
            //        Console.WriteLine(s);
            //    }
            //}

        }
    }
}
