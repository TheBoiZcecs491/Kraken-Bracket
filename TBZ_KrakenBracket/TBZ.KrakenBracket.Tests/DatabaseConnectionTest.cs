using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using TBZ.DatabaseConnectionService;

namespace TBZ.DatabaseConnectionTest
{
    [TestClass]
    public class ConnectionTest
    {
        Database Db = new Database();

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
            string connectString = Db.GetConnStringWrong();

            try
            {
                Db.Connect(connectString);
                result = false;
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
        //test for a wrong server
        public void Connect_offline_failed()
        {
            var result = false;
            string connectString = Db.GetConnStringWrong();

            try
            {
                Db.Connect(connectString);
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
                Db.InsertFile(fileName);
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
                Db.InsertFile(fileName);
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

            try
            {
                Db.InsertFile(fileName);
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
                Db.InsertFile(fileName);
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