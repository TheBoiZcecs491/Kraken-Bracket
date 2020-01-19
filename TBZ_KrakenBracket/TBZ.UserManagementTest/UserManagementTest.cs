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
        public void DeleteUsers_Fail_InvalidPermission()
        {
            var userManagement = new UserManagement();
            bool result = false;
            // Attempting to delete any account should fail since the current account
            // is a User
            int[] listOfIDs = { 1, 2, 3, 4, 5 };
            try
            {
                bool[] actual = userManagement.DeleteUsers(listOfIDs, "User");
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
            // System ID's #1 and #2 is are system admin and admin. Rest are Users
            int[] listOfIDs = { 1, 2, 3, 4, 5 };
            bool[] expected = { false, true, true, true, true };
            bool[] actual = userManagement.DeleteUsers(listOfIDs, "System Admin");
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteUsers_Admin_Pass()
        {
            var userManagement = new UserManagement();
            // System ID's #1 and #2 is are system admin and admin. Rest are Users
            int[] listOfIDs = { 1, 2, 3, 4, 5 };
            bool[] expected = { false, false, true, true, true };
            bool[] actual = userManagement.DeleteUsers(listOfIDs, "System Admin");
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EnableUsers_SystemAdmin_Pass()
        {
            var userManagement = new UserManagement();

            // System ID's #1 and #2 is are system admin and admin respectively. Rest are users.
            // System #4 is the only account disabled
            int[] listOfIDs =  {1, 2, 3, 4, 5};
            bool[] expected = { false, false, false, true, false };
            bool[] actual = userManagement.EnableUsers(listOfIDs, "System Admin");
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DisableUsers_SystemAdmin_Pass()
        {
            var userManagement = new UserManagement();

            // System ID's #1 and #2 is are system admin and admin respectively. Rest are users.
            // System #4 is the only account disabled
            int[] listOfIDs = { 1, 2, 3, 4, 5 };
            bool[] expected = { true, true, true, false, true };
            bool[] actual = userManagement.DisableUsers(listOfIDs, "System Admin");
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
