using MySql.Data.MySqlClient;
using System;
using System.IO;

namespace TBZ.DatabaseConnectionService
{
    public class Database
    {
        public string GetConnString()
        {
            return @"server=localhost; userid=root; password=password; database=kraken_bracket";
        }

        public string GetConnStringWrong()
        {
            return @"wrongInfo";
        }

        public void Connect(string connectString)
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

        /// <summary>
        /// Check inserted file for exist size dup
        /// then insert file
        /// </summary>
        /// <param name="fileName"></param>
        public void InsertFile(string fileName)
        {
            CheckFileExist(fileName);
            if (CheckFileSize(fileName) && CheckFileDup(fileName) && CheckFileExist(fileName))
            {
                //MySqlConnection conn = new MySqlConnection(@"server=localhost; userid=root; password=password; database=cecs491testdb");
                //conn.Open();
                ////comd.(insert into)
                //conn.Close();
            }
            else throw new ArgumentException("Insert Error");
        }

        /// <summary>
        /// check file size
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool CheckFileSize(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            sampleDatabase db = new sampleDatabase();
            if (file.Length > db.getDataStoreSize())//replace db.getDataStoreSize()
            {
                throw new ArgumentException("Not enough space in database");
            }
            else return true;
        }

        /// <summary>
        /// Check file for dup content
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool CheckFileDup(string fileName)
        {
            sampleDatabase db = new sampleDatabase();
            using (StreamReader sr = File.OpenText(fileName))
            {
                string s = sr.ReadLine();
                if (Array.Exists(db.getSampleData(), element => element == s))
                {
                    throw new ArgumentException("Duplication error");
                }
                else return true;
            }
        }

        /// <summary>
        /// check if file exist
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
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

        public class sampleDatabase
        {
            int storageSize = 25;
            string[] sampleData = { "Hello", "Hi" };

            public int getDataStoreSize()
            {
                return this.storageSize;
            }

            public string[] getSampleData()
            {
                return this.sampleData;
            }
        }
    }
}