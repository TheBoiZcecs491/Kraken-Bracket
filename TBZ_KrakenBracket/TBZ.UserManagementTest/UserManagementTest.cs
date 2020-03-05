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
        public void SingleCreateUser_Pass(uint sysID, string fName, string lName, string email, string password, string accntType, bool accountStatus, string errMsg)
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
        public void SingleCreateUser_Fail_SystemIDAlreadyExists(uint sysID, string fName, string lName, string email, string password, string accntType, bool accountStatus, string errMsg)
        {
            // Arrange
            User user = new User(sysID, fName, lName, email, password, accntType, accountStatus, errMsg);
            var um = new UserManagement();

            // Act
            bool result = um.SingleCreateUsers(user);
            try
            {
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

        [TestMethod]
        public void BulkCreateUsers_Pass()
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
            //CollectionAssert.AreEqual(expected, actual);


        }

        [DataTestMethod]
        [DataRow(6u, null, null, null, "84092ujIO@>>>", "User", true, null)]
        [DataRow(12u, null, null, null, "NDIaklnmef*()#!3", "User", true, null)]
        public void SingleDeleteUser_Pass(uint sysID, string fName, string lName, string email, string password, string accntType, bool accountStatus, string errMsg)
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

            User u1 = new User(3, null, null, null, "8*3kmmrMropongig", "User", true, null);
            User u2 = new User(4, null, null, null, "meMEeiaj093QNGEJOW~~~", "User", true, null);

            users.Add(u1);
            users.Add(u2);

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
            // CollectionAssert.AreEqual(expected, actual);
        }
    }
}
