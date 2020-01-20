using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TBZ.UserManagementService;

namespace TBZ.UserManagementTest
{
    [TestClass]
    public class UserManagementTest
    {
        /// <summary>
        /// Test method for creating users as an admin
        /// 
        /// The admin is creating 3 users and 0 admins. The admin does not have the 
        /// ability to create other admins.
        /// </summary>
        [TestMethod]
        public void CreateUsers_Admin_Pass()
        {
            var userManagement = new UserManagement();
            bool result = true;
            try
            {
                userManagement.CreateUsers(3, 0, "Admin");
            }
            catch(ArgumentException)
            {
                result = false;
            }
            catch(Exception) { }

            Assert.IsTrue(result);
        }


        /// <summary>
        /// Test method for creating accounts as system admin
        /// 
        /// The system admin is creating 5 users and 2 admins
        /// </summary>
        [TestMethod]
        public void CreateUsers_SystemAdmin_Pass()
        {
            var userManagement = new UserManagement();
            bool result = true;
            try
            {
                userManagement.CreateUsers(5, 2, "System Admin");
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Fail test method where the CreateUsers method is invoked but no amount of accounts
        /// is specified.
        /// </summary>
        [TestMethod]
        public void CreateUsers_Fail_AmountLessThanOne()
        {
            var userManagement = new UserManagement();
            bool result = false;
            try
            {
                userManagement.CreateUsers(0, 0, "System Admin");
            }
            catch (ArgumentException)
            {
                result = true;
            }
            catch (Exception) { }

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Fail test method where the user attempts to create accounts but does not have
        /// the necessary permissions
        /// </summary>
        [TestMethod]
        public void CreateUsers_Fail_InvalidPermission()
        {
            var userManagement = new UserManagement();
            bool result = false;
            try
            {
                userManagement.CreateUsers(3, 0, "User");
            }
            catch (ArgumentException)
            {
                result = true;
            }
            catch (Exception) { }

            Assert.IsTrue(result);
        }


        /// <summary>
        /// Fail test method where the user attempts to delete accounts but does not have
        /// the necessary permissions
        /// </summary>
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

        /// <summary>
        /// Test method where the system admin passes in an array of users to delete.
        /// 
        /// Note: for any ID that is not able to be deleted, the program will not throw an
        /// argument exception, but rather return false.
        /// </summary>
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


        /// <summary>
        /// Test method where the admin passes in an array of users to delete.
        /// 
        /// Note: for any ID that is not able to be deleted, the program will not throw an
        /// argument exception, but rather return false.
        /// </summary>
        [TestMethod]
        public void DeleteUsers_Admin_Pass()
        {
            var userManagement = new UserManagement();
            // System ID's #1 and #2 is are system admin and admin. Rest are Users
            int[] listOfIDs = { 1, 2, 3, 4, 5 };
            bool[] expected = { false, true, true, true, true };
            bool[] actual = userManagement.DeleteUsers(listOfIDs, "System Admin");
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test method where the admin passes in an array of users to enable.
        /// 
        /// Note: for any ID that is not able to be enabled, the program will not throw an
        /// argument exception, but rather return false.
        /// </summary>
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

        /// <summary>
        /// Test method where the admin passes in an array of users to disable.
        /// 
        /// Note: for any ID that is not able to be disabled, the program will not throw an
        /// argument exception, but rather return false.
        /// </summary>
        [TestMethod]
        public void DisableUsers_SystemAdmin_Pass()
        {
            var userManagement = new UserManagement();

            // System ID's #1 and #2 is are system admin and admin respectively. Rest are users.
            // System #4 is the only account disabled
            int[] listOfIDs = { 1, 2, 3, 4, 5 };
            bool[] expected = { false, true, true, false, true };
            bool[] actual = userManagement.DisableUsers(listOfIDs, "System Admin");
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
