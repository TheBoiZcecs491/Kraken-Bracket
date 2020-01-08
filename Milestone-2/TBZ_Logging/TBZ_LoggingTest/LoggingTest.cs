using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading;
using TBZ.LoggingService;

namespace TBZ.LoggingTest
{
    [TestClass]
    public class LoggingTest
    {
        [TestMethod]
        public void CreateLoggingInstance_Pass()
        {
            //Arrange
            bool result = true;
            string path = "output.csv";
            int tries = 3;
            //Act
            try
            {
                var l = new Logging(path, tries);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LogValidPath_Pass()
        {
            //Arrange
            bool result;
            string path = "output.csv";
            string op = "op";
            string msg = "";
            string id = "id";
            int tries = 3;
            //Act
            try
            {
                var l = new Logging(path, tries);
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
        public void LogInvalidPathType_Fail()
        {
            //Arrange
            bool result;
            string path = "output.txt";
            string op = "op";
            string msg = "";
            string id = "id";
            int tries = 3;
            //Act
            try
            {
                var l = new Logging(path, tries);
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
            bool result;
            string path = "output.csv";
            string op = "op";
            string msg = "";
            string id = "id";
            int tries = 0;
            //Act
            try
            {
                var l = new Logging(path, tries);
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
        public void LogInvalidOperation_Fail()
        {
            //Arrange
            bool result;
            string path = "output.csv";
            string op = "";
            string msg = "";
            string id = "id";
            int tries = 3;
            //Act
            try
            {
                var l = new Logging(path, tries);
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
            string path = "output.csv";
            string op = "op";
            string msg = "";
            string id = "";
            int tries = 3;
            //Act
            try
            {
                var l = new Logging(path, tries);
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
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Path.Combine(docPath, "logs.csv");
            string op = "Registration";
            string msg = "Data Store Error";
            string id = "0001";
            int tries = 3;
            //Act
            try
            {
                var l = new Logging(path, tries);
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
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Path.Combine(docPath, "logs.csv");
            string op = "Authorization";
            string msg = "Invalid Access Error";
            string id = "0321";
            int tries = 3;
            //Act
            Logging l = null;
            try
            {
                l = new Logging(path, tries);
            }
            catch (Exception)
            {
                result = false;
            }
            for (int i = 0; i < 50; i++)
            {
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
