using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TBZ.UserManagementService;

namespace TBZ.UserManagementTest
{
    [TestClass]
    public class UserManagementTest
    {
        /// <summary>
        /// Test method for creating a user as an Admin
        /// </summary>
        [DataTestMethod]
        [DataRow(100, "Brian", "Nguyen", "brian1234927@gmail.com", "Brian!!!9039", "User", true, "Admin")]
        public void SingleCreateUsers_Admin_Pass(int sysID, string firstName, string lastName, string email, string password,
            string accountType, bool accountStatus, string permission)
        {
            var userManagement = new UserManagement();
            bool result = true;
            try
            {
                userManagement.SingleCreateUsers(sysID, firstName, lastName, email, password, accountType, accountStatus, permission);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test method for creating an Admin as a System Admin
        /// </summary>
        [DataTestMethod]
        [DataRow(100, "Brian", "Nguyen", "brian1234927@gmail.com", "Brian!!!9039", "Admin", true, "System Admin")]
        public void SingleCreateUsers_SystemAdmin_Pass(int sysID, string firstName, string lastName, string email, string password,
            string accountType, bool accountStatus, string permission)
        {
            var userManagement = new UserManagement();
            bool result = true;
            try
            {
                userManagement.SingleCreateUsers(sysID, firstName, lastName, email, password, accountType, accountStatus, permission);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Fail test method where Admin attempts to create System Admin
        /// </summary>
        [DataTestMethod]
        [DataRow(100, "Brian", "Nguyen", "brian1234927@gmail.com", "Brian!!!9039", "System Admin", true, "Admin")]
        public void SingleCreateUsers_Admin_Fail_AdminCreatesSystemAdmin(int sysID, string firstName, string lastName, string email, string password,
            string accountType, bool accountStatus, string permission)
        {
            var userManagement = new UserManagement();
            bool result = false;
            try
            {
                userManagement.SingleCreateUsers(sysID, firstName, lastName, email, password, accountType, accountStatus, permission);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            catch (Exception) { }

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test method for deleting a user as a SystemAdmin
        /// </summary>
        [DataTestMethod]
        [DataRow(2, "System Admin")]
        public void SingleDeleteUsers_SystemAdmin_Pass(int sysID, string permission)
        {
            var userManagement = new UserManagement();
            bool result = true;
            try
            {
                userManagement.SingleDeleteUser(sysID, permission);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test method for deleting a user as a SystemAdmin
        /// </summary>
        [DataTestMethod]
        [DataRow(3, "Admin")]
        public void SingleDeleteUsers_Admin_Pass(int sysID, string permission)
        {
            var userManagement = new UserManagement();
            bool result = true;
            try
            {
                userManagement.SingleDeleteUser(sysID, permission);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Fail test method where Admin attempts to delete System Admin
        /// </summary>
        [DataTestMethod]
        [DataRow(1, "Admin")]
        public void SingleDeleteUsers_Admin_Fail_AdminDeletesSystemAdmin(int sysID, string permission)
        {
            var userManagement = new UserManagement();
            bool result = userManagement.SingleDeleteUser(sysID, permission);
            Assert.IsFalse(result);
        }
        /// <summary>
        /// Test method for creating users as an admin
        /// 
        /// The admin is creating 3 users and 0 admins. The admin does not have the 
        /// ability to create other admins.
        /// </summary>
        [DataTestMethod]
        [DataRow(3, 0, "Admin")]
        public void BulkCreateUsers_Admin_Pass(int amountOfUsers, int amountOfAdmins, string permission)
        {
            var userManagement = new UserManagement();
            bool result = true;
            try
            {
                userManagement.BulkCreateUsers(amountOfUsers, amountOfAdmins, permission);
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
        [DataTestMethod]
        [DataRow(5, 2, "System Admin")]
        public void BulkCreateUsers_SystemAdmin_Pass(int amountOfUsers, int amountOfAdmins, string permission)
        {
            var userManagement = new UserManagement();
            bool result = true;
            try
            {
                userManagement.BulkCreateUsers(amountOfUsers, amountOfAdmins, permission);
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
        [DataTestMethod]
        [DataRow(0, 0, "System Admin")]
        public void BulkCreateUsers_Fail_AmountLessThanOne(int amountOfUsers, int amountOfAdmins, string permission)
        {
            var userManagement = new UserManagement();
            bool result = false;
            try
            {
                userManagement.BulkCreateUsers(amountOfUsers, amountOfAdmins, permission);
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
        [DataTestMethod]
        [DataRow(3, 0, "User")]
        public void BulkCreateUsers_Fail_InvalidPermission(int amountOfUsers, int amountOfAdmins, string permission)
        {
            var userManagement = new UserManagement();
            bool result = false;
            try
            {
                userManagement.BulkCreateUsers(amountOfUsers, amountOfAdmins, permission);
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
        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5 },"User")]
        public void BulkDeleteUsers_Fail_InvalidPermission(int[] listOfIDs, string permission)
        {
            var userManagement = new UserManagement();
            bool result = false;
            // Attempting to delete any account should fail since the current account
            // is a User
            try
            {
                bool[] actual = userManagement.BulkDeleteUsers(listOfIDs, permission);
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
        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, "System Admin")]
        public void BulkDeleteUsers_SystemAdmin_Pass(int[] listOfIDs, string permission)
        {
            var userManagement = new UserManagement();
            // System ID's #1 and #2 is are system admin and admin. Rest are Users
            bool[] expected = { false, true, true, true, true };
            bool[] actual = userManagement.BulkDeleteUsers(listOfIDs, permission);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test method where the admin passes in an array of users to delete.
        /// 
        /// Note: for any ID that is not able to be deleted, the program will not throw an
        /// argument exception, but rather return false.
        /// </summary>
        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, "Admin")]
        public void BulkDeleteUsers_Admin_Pass(int[] listOfIDs, string permission)
        {
            var userManagement = new UserManagement();
            // System ID's #1 and #2 is are system admin and admin. Rest are Users
            bool[] expected = { false, false, true, true, true };
            bool[] actual = userManagement.BulkDeleteUsers(listOfIDs, permission);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Fail test method where an empty list of ID's to delete is passed in
        /// </summary>
        [DataTestMethod]
        [DataRow(new int[0], "Admin")]
        public void BulkDeleteUsers_Fail_ListIsEmpty(int[] listOfIDs, string permission)
        {
            var userManagement = new UserManagement();
            bool result = false;
            try
            {
                userManagement.BulkDeleteUsers(listOfIDs, permission);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test method for single enable a user as a SystemAdmin
        /// </summary>
        [DataTestMethod]
        [DataRow(4, "System Admin")]
        public void SingleEnableUsers_SystemAdmin_Pass(int sysID, string permission)
        {
            var userManagement = new UserManagement();
            bool result = true;
            try
            {
                userManagement.SingleEnableUser(sysID, permission);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test method for single enable a user as an Admin
        /// </summary>
        [DataTestMethod]
        [DataRow(4, "Admin")]
        public void SingleEnableUsers_Admin_Pass(int sysID, string permission)
        {
            var userManagement = new UserManagement();
            bool result = true;
            try
            {
                userManagement.SingleEnableUser(sysID, permission);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Fail test method where attempting to enable a user that is already enabled
        /// </summary>
        [DataTestMethod]
        [DataRow(3, "System Admin")]
        public void SingleEnableUsers_SystemAdmin_Fail_UserIsAlreadyEnabled(int sysID, string permission)
        {
            var userManagement = new UserManagement();
            bool result = userManagement.SingleEnableUser(sysID, permission);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test method where the system admin passes in an array of users to enable.
        /// 
        /// Note: for any ID that is not able to be enabled, the program will not throw an
        /// argument exception, but rather return false.
        /// </summary>
        [TestMethod]
        public void BulkEnableUsers_SystemAdmin_Pass()
        {
            var userManagement = new UserManagement();

            // System ID's #1 and #2 is are system admin and admin respectively. Rest are users.
            // System #4 is the only account disabled
            int[] listOfIDs =  {1, 2, 3, 4, 5};
            bool[] expected = { false, false, false, true, false };
            bool[] actual = userManagement.BulkEnableUsers(listOfIDs, "System Admin");
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test method where the admin passes in an array of users to enable.
        /// 
        /// Note: for any ID that is not able to be enabled, the program will not throw an
        /// argument exception, but rather return false.
        /// </summary>
        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, "Admin")]
        public void BulkEnableUsers_Admin_Pass(int[] listOfIDs, string permission)
        {
            var userManagement = new UserManagement();

            // System ID's #1 and #2 is are system admin and admin respectively. Rest are users.
            // System #4 is the only account disabled
            bool[] expected = { false, false, false, true, false };
            bool[] actual = userManagement.BulkEnableUsers(listOfIDs, permission);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test method where the system admin passes in an array of users to disable.
        /// 
        /// Note: for any ID that is not able to be disabled, the program will not throw an
        /// argument exception, but rather return false.
        /// </summary>
        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, "System Admin")]
        public void BulkDisableUsers_SystemAdmin_Pass(int[] listOfIDs, string permission)
        {
            var userManagement = new UserManagement();

            // System ID's #1 and #2 is are system admin and admin respectively. Rest are users.
            // System #4 is the only account disabled
            bool[] expected = { false, true, true, false, true };
            bool[] actual = userManagement.BulkDisableUsers(listOfIDs, permission);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test method where the admin passes in an array of users to disable.
        /// 
        /// Note: for any ID that is not able to be disabled, the program will not throw an
        /// argument exception, but rather return false.
        /// </summary>
        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, "Admin")]
        public void BulkDisableUsers_Admin_Pass(int[] listOfIDs, string permission)
        {
            var userManagement = new UserManagement();

            // System ID's #1 and #2 is are system admin and admin respectively. Rest are users.
            // System #4 is the only account disabled
            bool[] expected = { false, false, true, false, true };
            bool[] actual = userManagement.BulkDisableUsers(listOfIDs, "Admin");
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Fail test method where an empty list of ID's to enable is passed in
        /// </summary>
        [DataTestMethod]
        [DataRow(new int[0], "Admin")]
        public void BulkEnableUsers_Fail_ListIsEmpty(int[] listOfIDs, string permission)
        {
            var userManagement = new UserManagement();
            bool result = false;
            try
            {
                userManagement.BulkEnableUsers(listOfIDs, permission);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test method for updating a single user as a System Admin
        /// 
        /// In this case, the system admin is updating another admin's email
        /// </summary>
        [TestMethod]
        public void SingleUpdateUser_SystemAdmin_Pass()
        {
            var userManagement = new UserManagement();
            // (int sysID, string firstName, string lastName,
            // string email, string password, string accountType, string component, string permission)
            bool result = userManagement.SingleUpdateUser(2, null, null, "brian12345@gmail.com", "Brian3809{340@@", null, "Email", "System Admin");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test method for updating a single user as an Admin
        /// 
        /// In this case, the system admin is updating another user's password
        /// </summary>
        [TestMethod]
        public void SingleUpdateUser_Admin_Pass()
        {
            var userManagement = new UserManagement();
            // (int sysID, string firstName, string lastName,
            // string email, string password, string accountType, string component, string permission)
            bool result = userManagement.SingleUpdateUser(3, null, null, "brian12345@gmail.com", "Brian3809{340@@", null, "Password", "System Admin");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Fail test method when updating a User with an insufficient password
        /// </summary>
        [TestMethod]
        public void SingleUpdateUser_SystemAdmin_Fail_InsufficientPassword()
        {
            var userManagement = new UserManagement();
            // (int sysID, string firstName, string lastName,
            // string email, string password, string accountType, string component, string permission)
            bool result;
            try
            {
                result = userManagement.SingleUpdateUser(3, null, null, "brian12345@gmail.com", "123", null, "Password", "System Admin");
            }
            catch (ArgumentException)
            {
                result = true;
            }
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Fail test method when updating a User with an insufficient email
        /// </summary>
        [TestMethod]
        public void SingleUpdateUser_SystemAdmin_Fail_InsufficientEmail()
        {
            var userManagement = new UserManagement();
            // (int sysID, string firstName, string lastName,
            // string email, string password, string accountType, string component, string permission)
            bool result;
            try
            {
                result = userManagement.SingleUpdateUser(3, null, null, "br@gmail.com", "123", null, "Password", "System Admin");
            }
            catch (ArgumentException)
            {
                result = true;
            }
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Fail test method when updating a System Admin as an Admin
        /// </summary>
        [TestMethod]
        public void SingleUpdateUser_SystemAdmin_Fail_AdminUpdatesSystemAdmin()
        {
            var userManagement = new UserManagement();
            // (int sysID, string firstName, string lastName,
            // string email, string password, string accountType, string component, string permission)
            bool result = userManagement.SingleUpdateUser(1, null, null, "brian12345@gmail.com", "Brian3809{340@@", null, "Password", "Admin");
            Assert.IsFalse(result);
        }
    }
}
