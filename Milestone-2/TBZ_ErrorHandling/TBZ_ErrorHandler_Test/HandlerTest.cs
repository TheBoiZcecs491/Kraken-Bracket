using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TBZ_ErrorHandler;

namespace TBZ_ErrorHandler_Test
{
    [TestClass]
    public class HandlerTest
    {
        Authn authnTest = new Authn();
        Authz authzTest = new Authz();
        Logging logTest = new Logging();
        Database DBTest = new Database();

        [TestMethod]
        public void Everything_Pass()
        {
            var result = false;

            try
            {
                authnTest.Login(true);
                authzTest.Authorize(true);
                logTest.Log(true);
                DBTest.Connection(true);
                result = true;
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception)
            {
                result = false;
                //should not happen.
            }
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Authn_Failed()
        {
            var result = false;

            try
            {
                authnTest.Login(false);
                authzTest.Authorize(true);
                logTest.Log(true);
                DBTest.Connection(true);
                result = false;
            }
            catch (ArgumentException)
            {
                result = true;
            }
            catch (Exception)
            {
                result = false;
                //should not happen.
            }
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Unexpected_catch()
        {
            var result = false;

            try
            {
                authnTest.Login(true);
                authzTest.Authorize(true);
                logTest.Log(true);
                DBTest.Store("sample.txt");
                result = true;
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception)
            {
                result = true;
                //sample message an unexpected error has occur
                //Come back at another time
            }
            Assert.IsTrue(result);
        }
    }
}
