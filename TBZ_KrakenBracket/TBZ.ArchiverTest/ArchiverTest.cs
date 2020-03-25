using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TBZ.ArchiverService;
using System.IO;

namespace TBZ.ArchiverTest
{
    [TestClass]
    public class ArchiverTest
    {
        string _srcDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar + "logs";
        string _endDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar + "archives";
        int _time = -1;

        [TestInitialize]
        public void PrepareLogs_Pass()
        {
            //Arrange
            bool result = true;
            string log = "2019-10-12, 23:30:11:11, Authentication, \"Invalid Claim Error\", ID_31,\n";
            log += "2019-10-25, 23:30:11:17, Registration, \"Data Store Error\", ID_32,\n";
            log += "2019-11-06, 23:13:21:50, Registration, \"Data Store Error\", ID_0001,\n";
            log += "2019-12-11, 20:53:20:07, Authorization, \"Invalid Access Error\", ID_0321,\n";
            string path = Path.Combine(_srcDir, "_logs.csv");
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
        public void CreateObject_Pass()
        {
            //Arrange
            bool result = true;
            //Act
            try
            {
                var a = new Archiver(_srcDir, _time, _endDir);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InvalidDirectory_Fail()
        {
            //Arrange
            bool result = true;
            string srcDir = "";
            string endDir = "";
            int time = 1;
            //Act
            try
            {
                var a = new Archiver(srcDir, time, endDir);
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
            int time = -2;
            //Act
            try
            {
                var a = new Archiver(_srcDir, time, _endDir);
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

            //Act
            try
            {
                var a = new Archiver(_srcDir, _time, _endDir);
                result = a.Archive();
            }
            catch (Exception)
            {
                Console.WriteLine("Failed.");
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }

        //[TestCleanup]
        //[TestMethod]
        //public void DeleteLogFile_Pass()
        //{
        //    //Arrange
        //    bool result = true;
        //    string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //    string path = Path.Combine(docPath, "_logs.csv");
        //    //Act
        //    try
        //    {
        //        File.Delete(path);
        //    }
        //    catch (Exception)
        //    {
        //        result = false;
        //    }
        //    //Assert
        //    Assert.IsTrue(result);
        //}
    }
}
