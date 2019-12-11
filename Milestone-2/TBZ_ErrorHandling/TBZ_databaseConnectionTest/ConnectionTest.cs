using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using TBZ_databaseConnect;

namespace TBZ_databaseConnectionTest
{
    [TestClass]
    public class ConnectionTest
    {
        TBZ_database Database = new TBZ_database();

        [TestMethod]
        public void createFile()
        {
            //pass file
            using (StreamWriter sw = File.CreateText("MyTest.txt"))
            {
                sw.WriteLine("Pass");
            }

            //failed file dup
            using (StreamWriter sw = File.CreateText("MyTestDup.txt"))
            {
                sw.WriteLine("Hello");
            }

            //failed file full 
            using (StreamWriter sw = File.CreateText("MyTestFull.txt"))
            {
                sw.WriteLine("123456789012345678901234567890");
            }
        }

        [TestMethod]
        //test for a wrong server
        public void Connect_offline_passed()
        {
            var result = false;
            string connectString = Database.GetConnString();

            try
            {
                Database.Connect(connectString);
                result = true;
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception)
            { }
            Assert.IsTrue(result);
        }

        [TestMethod]
        //test for a wrong server
        public void Connect_offline_failed()
        {
            var result = false;
            string connectString = Database.GetConnStringWrong();

            try
            {
                Database.Connect(connectString);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            catch (Exception)
            { }
            Assert.IsTrue(result);
        }

        [TestMethod]
        //test for database duplication error
        public void Storage_Full_failed()
        {
            var fileName = "MyTestFull.txt";
            var result = false;

            try
            {
                Database.InsertFile(fileName);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            catch (Exception)
            { }
            Assert.IsTrue(result);
        }

        [TestMethod]
        //test for database duplication error
        public void Storage_Duplication_failed()
        {
            var fileName = "MyTestDup.txt";
            var result = false;

            try
            {
                Database.InsertFile(fileName);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            catch (Exception)
            { }
            Assert.IsTrue(result);
        }

        [TestMethod]
        //test for server if file exist
        public void Check_If_File_Exist_failed()
        {
            var result = false;
            var fileName = "DontExist.txt";
            var Database = new TBZ_database();

            try
            {
                Database.InsertFile(fileName);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            Assert.IsTrue(result);
        }

        [TestMethod]
        //test for database insert
        public void Check_insertFile_pass()
        {
            var result = false;
            var fileName = "MyTest.txt";

            try
            {
                Database.InsertFile(fileName);
                result = true;
            }
            catch (ArgumentException)
            {
                result = true;
            }
            Assert.IsTrue(result);
        }
    }
}
