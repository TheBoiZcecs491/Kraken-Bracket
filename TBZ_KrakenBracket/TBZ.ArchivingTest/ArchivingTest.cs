using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TBZ.ArchivingService;
using System.IO;

namespace TBZ.ArchiivngTest
{
    [TestClass]
    public class ArchivingTest
    {
        [TestMethod]
        public void CreateObj_Pass()
        {
            //Arrange
            Boolean result = true;
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Path.Combine(docPath, "logs.csv");
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
            Boolean result = true;
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
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InvalidTime_Fail()
        {
            //Arrange
            Boolean result = true;
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Path.Combine(docPath, "logs.csv");
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
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ArchiveToDest_Pass()
        {
            //Arrange
            Boolean result;
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Path.Combine(docPath, "logs.csv");
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
    }
}
