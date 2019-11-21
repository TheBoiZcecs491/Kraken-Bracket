using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TBZdatabaseConnection;

namespace TBZdatabaseConnectionTest
{
    [TestClass]
    public class ConnectionTest
    {
        [TestMethod]
        //test for wrong input
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
        //test for correct input
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

        [TestMethod]
        //test for a wrong server
        public void Connect_offline()
        {
            var result = false;
            string connectString = @"server=localhost; userid=root; password=password; database=cecs491testdb";
            var Database = new TBZdatabase();

            try
            {
                Database.Connects(connectString);
                result = true;
            }
            catch (ArgumentException)
            {
                result = true;
            }
            catch (Exception)
            { }
            Assert.IsTrue(result);
        }
    }

}
