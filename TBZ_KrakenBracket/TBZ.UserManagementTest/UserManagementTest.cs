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
        [DataTestMethod]
        [DataRow(6u, null, null, null, "84092ujIO@>>>", "User", true, null)]
        [DataRow(12u, null, null, null, "NDIaklnmef*()#!3", "User", true, null)]
        public void SingleCreateUser_Pass(uint sysID, string fName, string lName, string email,
            string password, string accntType, bool accountStatus, string errMsg)
        {
            // Arrange
            User user = new User(sysID, fName, lName, email, password, accntType, accountStatus, errMsg);
            var um = new UserManagement();
            bool result;

            // Act
            try
            {
                result = um.SingleCreateUsers(user);
            }
            catch (Exception)
            {
                result = false;
            }

            // Assert
            Assert.IsTrue(result);

            // Delete user to clean database
            um.SingleDeleteUser(user);
        }

        [DataTestMethod]
        [DataRow(6u, null, null, null, "84092ujIO@>>>", "User", true, null)]
        [DataRow(12u, null, null, null, "NDIaklnmef*()#!3", "User", true, null)]
        public void SingleCreateUser_Fail_SystemIDAlreadyExists(uint sysID, string fName, string lName, string email, 
            string password, string accntType, bool accountStatus, string errMsg)
        {
            // Arrange
            User user = new User(sysID, fName, lName, email, password, accntType, accountStatus, errMsg);
            var um = new UserManagement();

            // Act
            bool result;

            // Creating a user
            um.SingleCreateUsers(user);
            try
            {
                // Creating the exact same user with the same system ID
                result = um.SingleCreateUsers(user);
            }
            catch (Exception)
            {
                result = false;
            }

            // Assert
            Assert.IsFalse(result);

            // Delete user to clean database
            um.SingleDeleteUser(user);
        }

        [DataTestMethod]
        [DataRow(6u, null, null, null, "password", "User", true, null)]
        [DataRow(12u, null, null, null, "123", "User", true, null)]
        public void SingleCreateUser_Fail_InsufficientPassword(uint sysID, string fName, string lName, string email,
            string password, string accntType, bool accountStatus, string errMsg)
        {
            // Arrange
            User user = new User(sysID, fName, lName, email, password, accntType, accountStatus, errMsg);
            var um = new UserManagement();
            bool result;

            // Act
            try
            {
                // Creating the exact same user with the same system ID
                result = um.SingleCreateUsers(user);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { result = true; }

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BulkCreateUsers_PasswordCheck_Pass()
        {
            // Arrange
            List<User> users = new List<User>();

            User u1 = new User(3, null, null, null, "8*3kmmrMropongig", "User", true, null);
            User u2 = new User(4, null, null, null, "meMEeiaj093QNGEJOW~~~", "User", true, null);

            users.Add(u1);
            users.Add(u2);

            var um = new UserManagement();
            List<List<User>> expected = new List<List<User>>()
            {
                users, // Passed ID's
                new List<User>() {} // Failed ID's
            };

            // Act
            List<List<User>> actual = um.BulkCreateUsers(users, true);
            um.SingleDeleteUser(u1);
            um.SingleDeleteUser(u2);
            // FIXME: error that element 0 on both collections do not match
            CollectionAssert.AreEqual(expected[0], actual[0]);
        }

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
            List<List<User>> actual = um.BulkCreateUsers(users, true);

            // FIXME: error that element 0 on both collections do not match
            CollectionAssert.AreEqual(expected[1], actual[1]);
        }

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

            var um = new UserManagement();
            List<List<User>> expected = new List<List<User>>()
            {
                users, // Passed ID's
                new List<User>() {} // Failed ID's
            };

            // Act
            List<List<User>> actual = um.BulkCreateUsers(users, false);

            // FIXME: error that element 0 on both collections do not match
            CollectionAssert.AreEqual(expected[0], actual[0]);

            // Delete users to clean database
            foreach (User u in users)
            {
                um.SingleDeleteUser(u);
            }
        }

        [DataTestMethod]
        [DataRow(6u, null, null, null, "84092ujIO@>>>", "User", true, null)]
        [DataRow(12u, null, null, null, "NDIaklnmef*()#!3", "User", true, null)]
        public void SingleDeleteUser_Pass(uint sysID, string fName, string lName, string email,
            string password, string accntType, bool accountStatus, string errMsg)
        {
            // Arrange
            User user = new User(sysID, fName, lName, email, password, accntType, accountStatus, errMsg);
            var um = new UserManagement();
            bool result;

            // Act
            um.SingleCreateUsers(user);
            try
            {
                result = um.SingleDeleteUser(user);
            }
            catch (Exception)
            {
                result = false;
            }

            // Assert
            Assert.IsTrue(result);
        }

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

            users.Add(u1);
            users.Add(u2);
            users.Add(u3);
            users.Add(u4);
            users.Add(u5);
            users.Add(u6);

            var um = new UserManagement();

            // Act
            um.BulkCreateUsers(users, false);
            List<List<User>> expected = new List<List<User>>()
            {
                users, // Passed ID's
                new List<User>() {} // Failed ID's
            };

            List<List<User>> actual = um.BulkDeleteUsers(users);

            // Assert
            // FIXME: error that element 0 on both collections do not match
            CollectionAssert.AreEqual(expected[0], actual[0]);
        }
    }
}
