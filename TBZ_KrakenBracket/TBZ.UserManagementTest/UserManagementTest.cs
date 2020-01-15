using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TBZ.UserManagementService;

namespace TBZ.UserManagementTest
{
    [TestClass]
    public class UserManagementTest
    {
        [TestMethod]
        public void CreateUsers_Pass()
        {
            var userManagement = new UserManagement();
            bool result = true;
            try
            {
                userManagement.CreateUsers(5);
            }
            catch(ArgumentException)
            {
                result = false;
            }
            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void CreateUsers_Fail_AmountLessThanOne()
        {
            var userManagement = new UserManagement();
            bool result = false;
            try
            {
                userManagement.CreateUsers(0);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            Assert.IsTrue(result);
        }
    }
}
