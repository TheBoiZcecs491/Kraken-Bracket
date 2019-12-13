using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TBZ.ArchivingService;
using System.IO;

namespace TBZ.ArchivingTest
{
    [TestClass]
    public class ArchivingTest
    {
        [TestInitialize]
        public void PrepLogs_Pass()
        {
            //Arrange
            bool result = true;
            string log = "2019-10-12, 23:30:11:11, Authentication, \"Invalid Claim Error\", ID_31,\n";
            log += "2019-10-25, 23:30:11:17, Registration, \"Data Store Error\", ID_32,\n";
            log += "2019-11-06, 23:13:21:50, Registration, \"Data Store Error\", ID_0001,\n";
            log += "2019-12-11, 20:53:20:07, Authorization, \"Invalid Access Error\", ID_0321,\n";
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Path.Combine(docPath, "_logs.csv");
            //Act
            try
            {
                File.AppendAllText(path, log);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void CreateObj_Pass()
        {
            //Arrange
            bool result = true;
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Path.Combine(docPath, "_logs.csv");
            int time = 1;
            //Act
            try
            {
                var a = new Archiving(path, time);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InvalidPath_Fail()
        {
            //Arrange
            bool result = true;
            string path = "";
            int time = 1;
            //Act
            try
            {
                var a = new Archiving(path, time);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(!result);
        }

        [TestMethod]
        public void InvalidTime_Fail()
        {
            //Arrange
            bool result = true;
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Path.Combine(docPath, "_logs.csv");
            int time = 0;
            //Act
            try
            {
                var a = new Archiving(path, time);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(!result);
        }

        [TestMethod]
        public void ArchiveToDest_Pass()
        {
            //Arrange
            bool result;
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Path.Combine(docPath, "_logs.csv");
            string dest = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dest += "/";
            int time = 30;
            //Act
            try
            {
                var a = new Archiving(path, time);
                result = a.Archive(dest);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }

        [TestCleanup]
        [TestMethod]
        public void DeleteLogFile_Pass()
        {
            //Arrange
            bool result = true;
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Path.Combine(docPath, "_logs.csv");
            //Act
            try
            {
                File.Delete(path);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }
    }
}
