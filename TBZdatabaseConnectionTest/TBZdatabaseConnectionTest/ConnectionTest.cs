using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TBZdatabaseConnection;

namespace TBZdatabaseConnectionTest
{
    [TestClass]
    public class ConnectionTest
    {
        [TestMethod]
        public void Connect_failed()
        {
            var result = false;
            string connectString = "Wrong info";
            var Database = new TBZdatabase();

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
        public void Connect_pass()
        {
            var result = false;
            string connectString = @"server=localhost; userid=root; password=password; database=cecs491testdb";
            var Database = new TBZdatabase();

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
    }

}
