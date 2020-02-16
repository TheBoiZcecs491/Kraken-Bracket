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
        [DataRow(2000,  "Brian!!!9039", "Admin")]
        [DataRow(619, "89wf[8@negjJH", "Admin")]
        public void SingleCreateUsers_Admin_Pass(int sysID, string password, string permission)
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
                result = false;
            }
            catch (Exception) 
            { 
                result = false; 
            }

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
            catch (MySql.Data.MySqlClient.MySqlException)
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
        [DataRow(new int[] { 1, 2, 3 }, new string[] {" ", "password" , "123" }, false)]
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
        [DataRow(new int[] { 4, 5, 6 }, new string[] { "BridgesIHaveBurned392!", "JIO389fkqfg4//", "w894uhiM<>" }, true)]
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


        [DataTestMethod]
        [DataRow(1, "Admin")]
        public void SingleDeleteUser_Pass(int sysID, string permission)
        {
            // Arrange
            var userManagement = new UserManagement();
            bool result;

            try
            {
                result = userManagement.SingleDeleteUser(sysID, permission);
            }
            catch (Exception)
            {
                result = false;
            }

            Assert.IsTrue(result);


        }
    }
}
