﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TBZ.KrakenBracket.Services;
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
            string path = Path.Combine(_srcDir, "_log1.csv");
            log = "2019-10-12, 23:30:11:11, Registration, \"Invalid Claim Error\", ID_31,\n";
            log += "2019-10-25, 23:30:11:17, Authentication, \"Data Store Error\", ID_32,\n";
            log += "2019-11-06, 23:13:21:50, Registration, \"Data Store Error\", ID_0001,\n";
            log += "2019-12-11, 20:53:20:07, Authorization, \"Invalid Access Error\", ID_0321,\n";
            string path2 = Path.Combine(_srcDir, "_log2.csv");
            //Act
            try
            {
                File.AppendAllText(path, log);
                File.AppendAllText(path2, log);
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
                var a = new ArchiveService(_srcDir, _time, _endDir);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InvalidSourceDirectory_Pass()
        {
            //Arrange
            bool result = true;
            string srcDir = "";
            //Act
            try
            {
                var a = new ArchiveService(_srcDir, _time, _endDir);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(!result);
        }

        [TestMethod]
        public void InvalidTime_Pass()
        {
            //Arrange
            bool result = true;
            int time = -2;
            //Act
            try
            {
                var a = new ArchiveService(_srcDir, _time, _endDir);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(!result);
        }

        [TestMethod]
        public void InvalidEndDirectory_Pass()
        {
            //Arrange
            bool result = true;
            string endDir = "";
            //Act
            try
            {
                var a = new ArchiveService(_srcDir, _time, _endDir);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(!result);
        }

        [TestMethod]
        public void ArchiveToEndDirectory_Pass()
        {
            //Arrange
            bool result;

            //Act
            try
            {
                var a = new ArchiveService(_srcDir, _time, _endDir);
                result = a.Archive();
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