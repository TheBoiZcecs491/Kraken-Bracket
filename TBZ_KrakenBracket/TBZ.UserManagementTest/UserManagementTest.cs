using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TBZ.DatabaseAccess;
using TBZ.UserManagementService;

namespace TBZ.UserManagementTest
{
    [TestClass]
    public class UserManagementTest
    {

        /// <summary>
        /// Test method to single create a user
        /// </summary>
        /// <param name="sysID"></param>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="accntType"></param>
        /// <param name="accountStatus"></param>
        /// <param name="errMsg"></param>
        [DataTestMethod]
        [DataRow(6u, null, null, null, "84092ujIO@>>>", "User", true, null)]
        [DataRow(12u, null, null, null, "NDIaklnmef*()#!3", "User", true, null)]
        public void SingleCreateUser_Pass(uint sysID, string fName, string lName, string email,
            string password, string accntType, bool accountStatus, string errMsg)
        {
            // Arrange
            User user = new User(sysID, fName, lName, email, password, accntType, accountStatus, errMsg);
            User thisUser = new User(100, fName, lName, email, password, "System Admin", true, null);
            var um = new UserManagement();
            bool result;

            // Act
            try
            {
                result = um.SingleCreateUsers(thisUser, user);
            }
            catch (Exception)
            {
                result = false;
            }

            // Assert
            Assert.IsTrue(result);

            // Delete user to clean database
            um.SingleDeleteUser(thisUser, user);
        }

        /// <summary>
        /// Fail test method when attempting to create a user whose system ID already exists
        /// </summary>
        /// <param name="sysID"></param>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="accntType"></param>
        /// <param name="accountStatus"></param>
        /// <param name="errMsg"></param>
        [DataTestMethod]
        [DataRow(6u, null, null, null, "84092ujIO@>>>", "User", true, null)]
        [DataRow(12u, null, null, null, "NDIaklnmef*()#!3", "User", true, null)]
        public void SingleCreateUser_Fail_SystemIDAlreadyExists(uint sysID, string fName, string lName, string email,
            string password, string accntType, bool accountStatus, string errMsg)
        {
            // Arrange
            User user = new User(sysID, fName, lName, email, password, accntType, accountStatus, errMsg);
            User thisUser = new User(100, fName, lName, email, password, "System Admin", true, null);
            var um = new UserManagement();

            // Act
            bool result;

            // Creating a user
            um.SingleCreateUsers(thisUser, user);
            try
            {
                // Creating the exact same user with the same system ID
                result = um.SingleCreateUsers(thisUser, user);
            }
            catch (Exception)
            {
                result = false;
            }

            // Assert
            Assert.IsFalse(result);

            // Delete user to clean database
            um.SingleDeleteUser(thisUser, user);
        }

        /// <summary>
        /// Fail test method where attempting to create a user whose password
        /// does not meet requirements
        /// </summary>
        /// <param name="sysID"></param>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="accntType"></param>
        /// <param name="accountStatus"></param>
        /// <param name="errMsg"></param>
        [DataTestMethod]
        [DataRow(6u, null, null, null, "password", "User", true, null)]
        [DataRow(12u, null, null, null, "123", "User", true, null)]
        public void SingleCreateUser_Fail_InsufficientPassword(uint sysID, string fName, string lName, string email,
            string password, string accntType, bool accountStatus, string errMsg)
        {
            // Arrange
            User user = new User(sysID, fName, lName, email, password, accntType, accountStatus, errMsg);
            User thisUser = new User(100, fName, lName, email, password, "System Admin", true, null);
            var um = new UserManagement();
            bool result;

            // Act
            try
            {
                // Creating the exact same user with the same system ID
                result = um.SingleCreateUsers(thisUser, user);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { result = true; }

            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test method to bulk create users with password check enabled
        /// </summary>
        [TestMethod]
        public void BulkCreateUsers_PasswordCheck_Pass()
        {
            // Arrange
            List<User> users = new List<User>();

            User u1 = new User(3, null, null, null, "8*3kmmrMropongig", "User", true, null);
            User u2 = new User(4, null, null, null, "meMEeiaj093QNGEJOW~~~", "User", true, null);
            User thisUser = new User(100, null, null, null, "meMEeiaj093QNGEJOW~~~", "System Admin", true, null);
            users.Add(u1);
            users.Add(u2);

            var um = new UserManagement();
            List<List<User>> expected = new List<List<User>>()
            {
                users, // Passed ID's
                new List<User>() {} // Failed ID's
            };

            // Act
            List<List<User>> actual = um.BulkCreateUsers(thisUser, users, true);
            foreach (User u in users)
            {
                um.SingleDeleteUser(thisUser, u);
            }

            // Assert
            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);
        }

        /// <summary>
        /// Fail test method where bulk creating users fail because
        /// of insufficient passwords when password check is enabled
        /// </summary>
        [TestMethod]
        public void BulkCreateUsers_PasswordCheck_Fail_InsufficientPasswords()
        {
            // Arrange
            List<User> users = new List<User>();

            User u1 = new User(1, null, null, null, "password", "User", true, null);
            User u2 = new User(2, null, null, null, "123", "User", true, null);
            User u3 = new User(3, null, null, null, "", "User", true, null);
            User u4 = new User(4, null, null, null, null, "User", true, null);
            User u5 = new User(5, null, null, null, "bad", "User", true, null);
            User u6 = new User(6, null, null, null, "brian", "User", true, null);

            User thisUser = new User(100, null, null, null, "meMEeiaj093QNGEJOW~~~", "System Admin", true, null);

            users.Add(u1);
            users.Add(u2);
            users.Add(u3);
            users.Add(u4);
            users.Add(u5);
            users.Add(u6);

            var um = new UserManagement();
            List<List<User>> expected = new List<List<User>>()
            {
                new List<User>(){ }, // Passed ID's
                users // Failed ID's
            };

            // Act
            List<List<User>> actual = um.BulkCreateUsers(thisUser, users, true);

            // FIXME: error that element 0 on both collections do not match
            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);
        }

        /// <summary>
        /// Fail test method where attempting to bulk create users fail
        /// because a redundant system ID is found
        /// </summary>
        [TestMethod]
        public void BulkCreateUsers_Fail_SystemIDAlreadyExist()
        {
            // Arrange
            List<User> users = new List<User>();

            User u1 = new User(1, null, null, null, "password", "User", true, null);
            User u2 = new User(1, null, null, null, "123", "User", true, null);
            User u3 = new User(1, null, null, null, "", "User", true, null);
            User u4 = new User(1, null, null, null, null, "User", true, null);
            User u5 = new User(1, null, null, null, "bad", "User", true, null);
            User u6 = new User(1, null, null, null, "brian", "User", true, null);

            User thisUser = new User(100, null, null, null, "meMEeiaj093QNGEJOW~~~", "System Admin", true, null);

            users.Add(u1);
            users.Add(u2);
            users.Add(u3);
            users.Add(u4);
            users.Add(u5);
            users.Add(u6);

            var um = new UserManagement();
            List<List<User>> expected = new List<List<User>>()
            {
                new List<User>(){ u1 }, // Passed ID's
                new List<User>() { u2, u3, u4, u5, u6 } // Failed ID's
            };

            // Act
            List<List<User>> actual = um.BulkCreateUsers(thisUser, users, false);

            // FIXME: error that element 0 on both collections do not match
            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);
            um.SingleDeleteUser(thisUser, u1);
        }

        /// <summary>
        /// Test method to bulk create users with no password check
        /// </summary>
        [TestMethod]
        public void BulkCreateUsers_NoPasswordCheck_Pass()
        {
            // Arrange
            List<User> users = new List<User>();

            User u1 = new User(1, null, null, null, "password", "User", true, null);
            User u2 = new User(2, null, null, null, "123", "User", true, null);
            User u3 = new User(3, null, null, null, "", "User", true, null);
            User u4 = new User(4, null, null, null, null, "User", true, null);
            User u5 = new User(5, null, null, null, "bad", "User", true, null);
            User u6 = new User(6, null, null, null, "brian", "User", true, null);

            users.Add(u1);
            users.Add(u2);
            users.Add(u3);
            users.Add(u4);
            users.Add(u5);
            users.Add(u6);

            User thisUser = new User(100, null, null, null, "meMEeiaj093QNGEJOW~~~", "System Admin", true, null);

            var um = new UserManagement();
            List<List<User>> expected = new List<List<User>>()
            {
                users, // Passed ID's
                new List<User>() {} // Failed ID's
            };

            // Act
            List<List<User>> actual = um.BulkCreateUsers(thisUser, users, false);

            // Assert
            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);

            // Delete users to clean database
            foreach (User u in users)
            {
                um.SingleDeleteUser(thisUser, u);
            }
        }

        /// <summary>
        /// Test method to delete a single user
        /// </summary>
        /// <param name="sysID"></param>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="accntType"></param>
        /// <param name="accountStatus"></param>
        /// <param name="errMsg"></param>
        [DataTestMethod]
        [DataRow(6u, null, null, null, "84092ujIO@>>>", "User", true, null)]
        [DataRow(12u, null, null, null, "NDIaklnmef*()#!3", "User", true, null)]
        public void SingleDeleteUser_Pass(uint sysID, string fName, string lName, string email,
            string password, string accntType, bool accountStatus, string errMsg)
        {
            // Arrange
            User user = new User(sysID, fName, lName, email, password, accntType, accountStatus, errMsg);
            User thisUser = new User(100, null, null, null, "meMEeiaj093QNGEJOW~~~", "System Admin", true, null);
            var um = new UserManagement();
            bool result;

            // Act
            um.SingleCreateUsers(thisUser, user);
            try
            {
                result = um.SingleDeleteUser(thisUser, user);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { result = false; }

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Fail test method where attempting to delete a user fails because
        /// the system ID is not found
        /// </summary>
        [TestMethod]
        public void SingleDeleteUser_Fail_SystemIDDoesNotExist()
        {
            // Arrange
            bool result = true;
            User u1 = new User(1, null, null, null, "password", "User", true, null);
            User thisUser = new User(100, null, null, null, "meMEeiaj093QNGEJOW~~~", "System Admin", true, null);
            var um = new UserManagement();
            // Act
            try
            {
                result = um.SingleDeleteUser(thisUser, u1);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test method to bulk delete users
        /// </summary>
        [TestMethod]
        public void BulkDeleteUsers_Pass()
        {
            // Arrange
            List<User> users = new List<User>();

            User u1 = new User(1, null, null, null, "password", "User", true, null);
            User u2 = new User(2, null, null, null, "123", "User", true, null);
            User u3 = new User(3, null, null, null, "", "User", true, null);
            User u4 = new User(4, null, null, null, null, "User", true, null);
            User u5 = new User(5, null, null, null, "bad", "User", true, null);
            User u6 = new User(6, null, null, null, "brian", "User", true, null);
            User thisUser = new User(100, null, null, null, "meMEeiaj093QNGEJOW~~~", "System Admin", true, null);
            users.Add(u1);
            users.Add(u2);
            users.Add(u3);
            users.Add(u4);
            users.Add(u5);
            users.Add(u6);

            var um = new UserManagement();

            // Act
            um.BulkCreateUsers(thisUser, users, false);
            List<List<User>> expected = new List<List<User>>()
            {
                users, // Passed ID's
                new List<User>() {} // Failed ID's
            };

            List<List<User>> actual = um.BulkDeleteUsers(users);

            // Assert
            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);
        }

        /// <summary>
        /// Fail test method where attempting to bulk delete users fail because
        /// the system ID's do not exist
        /// </summary>
        [TestMethod]
        public void BulkDeleteUsers_Fail_SystemIDsDoNotExist()
        {
            // Arrange
            List<User> users = new List<User>();

            var um = new UserManagement();

            User u1 = new User(1, null, null, null, "password", "User", true, null);
            User u2 = new User(2, null, null, null, "123", "User", true, null);
            User u3 = new User(3, null, null, null, "", "User", true, null);
            User u4 = new User(4, null, null, null, null, "User", true, null);
            User u5 = new User(5, null, null, null, "bad", "User", true, null);
            User u6 = new User(6, null, null, null, "brian", "User", true, null);

            users.Add(u1);
            users.Add(u2);
            users.Add(u3);
            users.Add(u4);
            users.Add(u5);
            users.Add(u6);

            List<List<User>> expected = new List<List<User>>()
            {
                new List<User>() {}, // Passed ID's
                users // Failed ID's
            };

            // Act
            List<List<User>> actual = um.BulkDeleteUsers(users);

            // Assert
            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);
        }
    }
}