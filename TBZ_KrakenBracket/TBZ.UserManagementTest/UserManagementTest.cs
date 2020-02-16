using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TBZ.UserManagementService;

namespace TBZ.UserManagementTest
{
    [TestClass]
    public class UserManagementTest
    {
        /// <summary>
        /// Single creating users as admin
        /// </summary>
        /// <param name="sysID">System ID</param>
        /// <param name="password">Password</param>
        /// <param name="permission">Permission</param>
        [DataTestMethod]
        [DataRow(1, "398h389289hNU(F", "Admin")]
        [DataRow(2000,  "Brian!!!9039", "Admin")]
        [DataRow(619, "89wf[8@negjJH", "Admin")]
        public void SingleCreateUsers_Admin_Pass(int sysID, string password, string permission)
        {
            // Arrange
            var userManagement = new UserManagement();
            bool result = false;

            // Act
            try
            {
                result = userManagement.SingleCreateUsers(sysID, password, permission);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception){ } 

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Single create users fail because of insufficient password
        /// </summary>
        /// <param name="sysID"></param>
        /// <param name="password"></param>
        /// <param name="permission"></param>
        [DataTestMethod]
        [DataRow(199, "password", "Admin")]
        public void SingleCreateUsers_Admin_Fail_InsufficientPasswords(int sysID, string password, string permission)
        {
            // Arrange
            var userManagement = new UserManagement();
            bool result;

            // Act
            try
            {
                result = userManagement.SingleCreateUsers(sysID, password, permission);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            catch (Exception)
            {
                result = true;
            }

            // Assert
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow(1, "398h389289hNU(F", "Admin")]
        public void SingleCreateUsers_Admin_Fail_SystemIDAlreadyExists(int sysID, string password, string permission)
        {
            // Arrange
            var userManagement = new UserManagement();
            bool result;

            // Act
            try
            {
                result = userManagement.SingleCreateUsers(sysID, password, permission);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Bulk creating users with no password check
        /// </summary>
        /// <param name="listOfIDs"></param>
        /// <param name="listOfPasswords"></param>
        /// <param name="passwordCheck">boolean to enforce password check or not</param>
        [DataTestMethod]
        [DataRow(new int[] { 100, 200, 300 }, new string[] {" ", "password" , "123" }, false)]
        public void BulkCreateUsers_NoPasswordCheck_Pass(int[] listOfIDs, string[] listOfPasswords, bool passwordCheck)
        {
            // Arrange
            var userManagement = new UserManagement();
            bool[] expected = { true, true, true };
            bool[] actual;

            // Act
            actual = userManagement.BulkCreateUsers(listOfIDs, listOfPasswords, passwordCheck);
            
            // Actual
            CollectionAssert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Bulk creating users with password check enabled
        /// </summary>
        /// <param name="listOfIDs"></param>
        /// <param name="listOfPasswords"></param>
        /// <param name="passwordCheck"></param>
        [DataTestMethod]
        [DataRow(new int[] { 400, 500, 600 }, new string[] { "BridgesIHaveBurned392!", "JIO389fkqfg4//", "w894uhiM<>" }, true)]
        public void BulkCreateUsers_PasswordCheck_Pass(int[] listOfIDs, string[] listOfPasswords, bool passwordCheck)
        {
            // Arrange
            var userManagement = new UserManagement();
            bool[] expected = { true, true, true };
            bool[] actual;

            // Act
            actual = userManagement.BulkCreateUsers(listOfIDs, listOfPasswords, passwordCheck);

            // Actual
            CollectionAssert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Fail test method when bulk creating users fail because of insufficient passwords
        /// </summary>
        /// <param name="listOfIDs"></param>
        /// <param name="listOfPasswords"></param>
        /// <param name="passwordCheck"></param>
        [DataTestMethod]
        [DataRow(new int[] { 23, 24, 25}, new string[] { " ", "password", "123" }, true)]
        public void BulkCreateUsers_PasswordCheck_Fail_InsufficientPasswords(int[] listOfIDs, 
            string[] listOfPasswords, bool passwordCheck)
        {
            // Arrange
            var userManagement = new UserManagement();
            bool[] expected = { false, false, false };
            bool[] actual;

            // Act
            actual = userManagement.BulkCreateUsers(listOfIDs, listOfPasswords, passwordCheck);

            // Actual
            CollectionAssert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Single deleting users
        /// </summary>
        /// <param name="sysID"></param>
        /// <param name="permission"></param>
        [DataTestMethod]
        [DataRow(100, "Admin")]
        public void SingleDeleteUser_Pass(int sysID, string permission)
        {
            // Arrange
            var userManagement = new UserManagement();
            bool result = false;

            // Act
            try
            {
                result = userManagement.SingleDeleteUser(sysID, permission);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Fail test method where attempting to delete a system ID that does not exist
        /// </summary>
        /// <param name="sysID"></param>
        /// <param name="permission"></param>
        [DataTestMethod]
        [DataRow(93828, "Admin")]
        public void SingleDeleteUser_Fail_SystemIDDoesNotExist(int sysID, string permission)
        {
            // Arrange
            var userManagement = new UserManagement();
            bool result = true;

            // Act
            try
            {
                result = userManagement.SingleDeleteUser(sysID, permission);
            }
            catch (ArgumentException) 
            {
                result = true;
            }
            catch (Exception) { }

            // Assert
            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DataRow(new int[] { 400, 500, 600}, "Admin")]
        public void BulkDeleteUsers_Pass(int[] listOfIDs, string permission)
        {
            var userManagement = new UserManagement();
            bool[] expected = { true, true, true };
            bool[] actual;

            // Act
            actual = userManagement.BulkDeleteUsers(listOfIDs, permission);

            // Actual
            CollectionAssert.AreEqual(actual, expected);
        }
    }
}
