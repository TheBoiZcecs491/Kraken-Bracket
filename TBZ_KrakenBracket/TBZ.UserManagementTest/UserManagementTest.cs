using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
        public void CreateUsers_Fail_InvalidPermission()
        {
            var userManagement = new UserManagement();
            bool result = false;
            try
            {
                userManagement.CreateUsers(3, "User");
            }
            catch (ArgumentException)
            {
                result = true;
            }
            catch (Exception) { }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteUsers_SystemAdmin_Pass()
        {
            var userManagement = new UserManagement();
            bool result = true;

            // System ID #2 is an admin. Rest are Users
            List<int> listOfIDs = new List<int>() {2, 3, 4, 5};
            try
            {
                userManagement.DeleteUsers(listOfIDs, "System Admin");
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteUsers_Admin_Pass()
        {
            var userManagement = new UserManagement();
            bool result = true;

            // System ID #2 is an admin. Rest are Users
            List<int> listOfIDs = new List<int>() {2, 3, 4, 5 };
            try
            {
                userManagement.DeleteUsers(listOfIDs, "Admin");
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteUsers_Admin_Fail_UserIsAdmin()
        {
            var userManagement = new UserManagement();
            bool result = true;

            // System ID #2 is an admin
            List<int> listOfIDs = new List<int>() {2};
            try
            {
                userManagement.DeleteUsers(listOfIDs, "Admin");
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteUsers_Admin_Fail_UserIsSystemAdmin()
        {
            var userManagement = new UserManagement();
            bool result = true;

            // System ID #2 is an admin
            List<int> listOfIDs = new List<int>() { 1 };
            try
            {
                userManagement.DeleteUsers(listOfIDs, "Admin");
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
