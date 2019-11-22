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
            string connectString = @"server=local; userid=root; password=password; database=cecs491testdb";
            var Database = new TBZdatabase();

            try
            {
                Database.Connects(connectString);
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
            var file = "1000";
            var result = false;
            var Database = new TBZdatabase();

            try
            {
                Database.InsertFile(file);
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
        public void Storage_Duplication_Failed()
        {
            var file = "HIx2";
            var result = false;
            var Database = new TBZdatabase();

            try
            {
                Database.InsertFile(file);
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
