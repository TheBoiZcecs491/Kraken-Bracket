using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading;
using TBZ.KrakenBracket.Services;

namespace TBZ.LoggingTest
{
    [TestClass]
    public class LoggingTest
    {
        string _endDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar + "logs";
        int _tries = 3;
        [TestMethod]
        public void CreateLoggingInstance_Pass()
        {
            //Arrange
            bool result = true;
            string dir = _endDir;
            int tries = _tries;
            //Act
            try
            {
                var l = new DataStoreLoggingService(dir, tries);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LogValidDirectory_Pass()
        {
            //Arrange
            bool result;
            string dir = _endDir;
            string op = "op";
            string msg = "";
            string id = "id";
            int tries = _tries;
            //Act
            try
            {
                var l = new DataStoreLoggingService(dir, tries);
                result = l.Log(op, msg, id);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LogInvalidDirectory_Pass()
        {
            //Arrange
            bool result;
            string dir = "logthis";
            string op = "op";
            string msg = "";
            string id = "id";
            int tries = 3;
            //Act
            try
            {
                var l = new DataStoreLoggingService(dir, tries);
                result = l.Log(op, msg, id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = false;
            }
            //Assert
            Assert.IsTrue(!result);
        }

        [TestMethod]
        public void LogInvalidTries_Fail()
        {
            //Arrange
            bool result = true;
            string dir = _endDir;
            int tries = 0;
            //Act
            try
            {
                var l = new DataStoreLoggingService(dir, tries);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = false;
            }
            //Assert
            Assert.IsTrue(!result);
        }

        [TestMethod]
        public void LogInvalidOperation_Fail()
        {
            //Arrange
            bool result;
            string dir = _endDir;
            string op = "";
            string msg = "";
            string id = "id";
            int tries = _tries;
            //Act
            try
            {
                var l = new DataStoreLoggingService(dir, tries);
                result = l.Log(op, msg, id);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(!result);
        }

        [TestMethod]
        public void LogInvalidID_Fail()
        {
            //Arrange
            bool result;
            string dir = _endDir;
            string op = "op";
            string msg = "";
            string id = "";
            int tries = _tries;
            //Act
            try
            {
                var l = new DataStoreLoggingService(dir, tries);
                result = l.Log(op, msg, id);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(!result);
        }

        [TestMethod]
        public void LogToCSV_Pass()
        {
            //Arrange
            bool result;
            string dir = _endDir;
            string op = "Registration";
            string msg = "Data Store Error";
            string id = "0001";
            int tries = _tries;
            //Act
            try
            {
                var l = new DataStoreLoggingService(dir, tries);
                result = l.Log(op, msg, id);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Log50Times_Pass()
        {
            //Arrange
            bool result = false;
            string dir = _endDir;
            string op = "Authorization";
            string msg = "Invalid Access Error";
            int tries = _tries;
            //Act
            DataStoreLoggingService l = null;
            try
            {
                l = new DataStoreLoggingService(dir, tries);
            }
            catch (Exception)
            {
                result = false;
            }
            for (int i = 0; i < 50; i++)
            {
                if (i == 17)
                {
                    op = "Authentication";
                    msg = "Invalid Claim Error";
                }
                if (i == 32)
                {
                    op = "Registration";
                    msg = "Data Store Error";
                }
                string id = i.ToString();
                try
                {
                    result = l.Log(op, msg, id);
                    Thread.Sleep(60);
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            //Assert
            Assert.IsTrue(result);
        }
    }
}