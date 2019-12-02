using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TBZ_Logging;

namespace TBZ_LoggingTest
{
    [TestClass]
    public class LoggingTest
    {
        [TestMethod]
        public void Log_CreateObj_Pass()
        {
            //Arrange
            Boolean result = true;
            String path = "";
            //Act
            try
            {
                var a = new Logging(path);
            }
            catch (Exception)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Log_CreateObj_Fail()
        {
            //Arrange
            Boolean result = true;
            String path = "";
            //Act
            try
            {
                var a = new Logging(path);
            }
            catch (Exception e)
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }
    }
}
