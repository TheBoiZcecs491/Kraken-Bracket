using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TBZ.UserManagementService;

namespace TBZ.UserManagementTest
{
    [TestClass]
    public class UserManagementTest
    {
        [TestMethod]
        public void CreateUsers_Admin_Pass()
        {
            var userManagement = new UserManagement();
            bool result = true;
            try
            {
                userManagement.CreateUsers(3, "Admin");
            }
            catch(ArgumentException)
            {
                result = false;
            }
            catch(Exception) { }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateUsers_SystemAdmin_Pass()
        {
            var userManagement = new UserManagement();
            bool result = true;
            try
            {
                userManagement.CreateUsers(10, "System Admin");
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateUsers_Fail_AmountLessThanOne()
        {
            var userManagement = new UserManagement();
            bool result = false;
            try
            {
                userManagement.CreateUsers(0, "System Admin");
            }
            catch (ArgumentException)
            {
                result = true;
            }
            catch (Exception) { }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteUsers_Pass()
        {
            var userManagement = new UserManagement();
            bool result = true;
            try
            {
                userManagement.DeleteUsers(1);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            Assert.IsTrue(result);
        }
    }
}
