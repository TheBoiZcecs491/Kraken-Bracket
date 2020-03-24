using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TBZ.DatabaseAccess;
using TBZ.UM_Manager;

namespace TBZ.UserManagementTest
{
    [TestClass]
    public class UserManagementTest
    {
        private static readonly UserManagementManager _userManagementManager;
        static UserManagementTest()
        {
            _userManagementManager = new UserManagementManager();
        }
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
        [DataRow(6u, null, null, null, "84092ujIO@>>>", null, "User", false, null)]
        [DataRow(12u, null, null, null, "NDIaklnmef*()#!3", null, "User", false, null)]
        public void SingleCreateUser_Pass(uint sysID, string fName, string lName, string email,
            string password, string salt, string accntType, bool accountStatus, string errMsg)
        {
            // Arrange

            // Initializing User objects to test
            User user = new User(sysID, fName, lName, email, password, salt, accntType, accountStatus, errMsg);
            User thisUser = new User(114, fName, lName, email, password, null, "System Admin", true, null);
            
            bool result = false;

            Stopwatch sw = new Stopwatch();

            // Act
            sw.Start();
            try
            {
                // System admin creates an admin
                result = _userManagementManager.SingleCreateUsers(thisUser, user);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }
            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);

            // Assert
            Assert.IsTrue(result);

            // Delete user to clean database
            _userManagementManager.SingleDeleteUser(thisUser, user);
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
        [DataRow(6u, null, null, null, "84092ujIO@>>>", null, "User", false, null)]
        [DataRow(12u, null, null, null, "NDIaklnmef*()#!3", null, "User", false, null)]
        public void SingleCreateUser_Fail_SystemIDAlreadyExists(uint sysID, string fName, string lName, string email,
            string password, string salt, string accntType, bool accountStatus, string errMsg)
        {
            // Arrange

            // Initializing User objects to test
            User user = new User(sysID, fName, lName, email, password, salt, accntType, accountStatus, errMsg);
            User thisUser = new User(101, fName, lName, email, password, null, "System Admin", true, null);

            Stopwatch sw = new Stopwatch();

            bool result = true;

            // Creating a user
            _userManagementManager.SingleCreateUsers(thisUser, user);

            // Act
            sw.Start();
            try
            {
                // Creating the exact same user with the same system ID
                result = _userManagementManager.SingleCreateUsers(thisUser, user);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);

            // Assert
            Assert.IsFalse(result);

            // Delete user to clean database
            _userManagementManager.SingleDeleteUser(thisUser, user);
        }

        /// <summary>
        /// Fail test method where attempting to create user fails because of invalid permissions
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
        [DataRow(6u, null, null, null, "84092ujIO@>>>", null, "System Admin", true, null)]
        public void SingleCreateUser_Fail_InvalidPermissions(uint sysID, string fName, string lName, string email,
            string password, string salt, string accntType, bool accountStatus, string errMsg)
        {
            // Arrange

            // Initializing User objects to test
            User user = new User(sysID, fName, lName, email, password, salt, accntType, accountStatus, errMsg);
            User thisUser = new User(102, fName, lName, email, password, null, "Admin", true, null);

            Stopwatch sw = new Stopwatch();

            bool result = true;

            // Act
            sw.Start();
            try
            {
                result = _userManagementManager.SingleCreateUsers(thisUser, user);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);

            // Assert
            Assert.IsFalse(result);
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
        [DataRow(6u, null, null, null, "password", null, "User", false, null)]
        [DataRow(12u, null, null, null, "123", null, "User", false, null)]
        public void SingleCreateUser_Fail_InsufficientPassword(uint sysID, string fName, string lName, string email,
            string password, string salt, string accntType, bool accountStatus, string errMsg)
        {
            // Arrange

            // Initializing User objects to test
            User user = new User(sysID, fName, lName, email, password, salt, accntType, accountStatus, errMsg);
            User thisUser = new User(103, fName, lName, email, password, salt, "System Admin", true, null);

            Stopwatch sw = new Stopwatch();

            bool result = true;

            // Act
            sw.Start();
            try
            {
                // Creating the exact same user with the same system ID
                result = _userManagementManager.SingleCreateUsers(thisUser, user);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);

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

            // Initializing User objects to test
            User u1 = new User(3, null, null, null, "8*3kmmrMropongig", null, "User", false, null);
            User u2 = new User(4, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "User", false, null);
            User thisUser = new User(104, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "System Admin", true, null);

            users.Add(u1);
            users.Add(u2);

            Stopwatch sw = new Stopwatch();

            List<List<User>> expected = new List<List<User>>()
            {
                users, // Passed ID's
                new List<User>() {} // Failed ID's
            };

            // Act
            sw.Start();
            List<List<User>> actual = _userManagementManager.BulkCreateUsers(thisUser, users, true);
            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);


            // Assert
            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);

            // Delete users to clean up database
            foreach (User u in users)
            {
                _userManagementManager.SingleDeleteUser(thisUser, u);
            }
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

            // Initializing User objects to test
            User u1 = new User(1, null, null, null, "password", null, "User", false, null);
            User u2 = new User(2, null, null, null, "123", null, "User", false, null);
            User u3 = new User(3, null, null, null, "", null, "User", false, null);
            User u4 = new User(4, null, null, null, null, null, "User", false, null);
            User u5 = new User(5, null, null, null, "bad", null, "User", false, null);
            User u6 = new User(6, null, null, null, "brian", null, "User", false, null);

            User thisUser = new User(105, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "System Admin", true, null);

            users.Add(u1);
            users.Add(u2);
            users.Add(u3);
            users.Add(u4);
            users.Add(u5);
            users.Add(u6);

            
            List<List<User>> expected = new List<List<User>>()
            {
                new List<User>(){ }, // Passed ID's
                users // Failed ID's
            };

            Stopwatch sw = new Stopwatch();

            // Act
            sw.Start();
            List<List<User>> actual = _userManagementManager.BulkCreateUsers(thisUser, users, true);
            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);

            // Assert
            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);
        }

        [TestMethod]
        public void BulkCreateUsers_Fail_InvalidPermissions()
        {
            // Arrange
            List<User> users = new List<User>();

            // Initializing User objects to test
            User u1 = new User(1, null, null, null, "password", null, "System Admin", true, null);
            User u2 = new User(1, null, null, null, "123", null, "System Admin", true, null);
            User u3 = new User(1, null, null, null, "", null, "System Admin", true, null);

            User thisUser = new User(106, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "Admin", true, null);

            users.Add(u1);
            users.Add(u2);
            users.Add(u3);


            
            List<List<User>> expected = new List<List<User>>()
            {
                new List<User>(){  }, // Passed ID's
                users // Failed ID's
            }; 
            
            Stopwatch sw = new Stopwatch();

            // Act
            sw.Start();
            List<List<User>> actual = _userManagementManager.BulkCreateUsers(thisUser, users, false);
            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);

            // Assert
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

            // Initializing User objects to test
            User u1 = new User(1, null, null, null, "password", null, "User", false, null);
            User u2 = new User(1, null, null, null, "123", null, "User", false, null);
            User u3 = new User(1, null, null, null, "", null, "User", false, null);
            User u4 = new User(1, null, null, null, null, null, "User", false, null);
            User u5 = new User(1, null, null, null, "bad", null, "User", false, null);
            User u6 = new User(1, null, null, null, "brian", null, "User", false, null);

            User thisUser = new User(107, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "System Admin", true, null);

            users.Add(u1);
            users.Add(u2);
            users.Add(u3);
            users.Add(u4);
            users.Add(u5);
            users.Add(u6);

            
            List<List<User>> expected = new List<List<User>>()
            {
                new List<User>(){ u1 }, // Passed ID's
                new List<User>() { u2, u3, u4, u5, u6 } // Failed ID's
            };

            Stopwatch sw = new Stopwatch();

            // Act
            sw.Start();
            List<List<User>> actual = _userManagementManager.BulkCreateUsers(thisUser, users, false);
            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);

            // Assert
            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);

            // Delete user to clean database
            _userManagementManager.SingleDeleteUser(thisUser, u1);
        }

        /// <summary>
        /// Test method to bulk create users with no password check
        /// </summary>
        [TestMethod]
        public void BulkCreateUsers_NoPasswordCheck_Pass()
        {
            // Arrange
            List<User> users = new List<User>();

            // Initializing User objects to test
            User u1 = new User(1, null, null, null, "password", null, "User", false, null);
            User u2 = new User(2, null, null, null, "123", null, "User", false, null);
            User u3 = new User(3, null, null, null, "", null, "User", false, null);
            User u4 = new User(4, null, null, null, null, null, "User", false, null);
            User u5 = new User(5, null, null, null, "bad", null, "User", false, null);
            User u6 = new User(6, null, null, null, "brian", null, "User", false, null);

            users.Add(u1);
            users.Add(u2);
            users.Add(u3);
            users.Add(u4);
            users.Add(u5);
            users.Add(u6);

            User thisUser = new User(108, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "System Admin", true, null);

            
            List<List<User>> expected = new List<List<User>>()
            {
                users, // Passed ID's
                new List<User>() {} // Failed ID's
            };

            Stopwatch sw = new Stopwatch();

            // Act
            sw.Start();
            List<List<User>> actual = _userManagementManager.BulkCreateUsers(thisUser, users, false);
            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);

            // Assert
            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);

            // Delete users to clean database
            foreach (User u in users)
            {
                _userManagementManager.SingleDeleteUser(thisUser, u);
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
        [DataRow(6u, null, null, null, "84092ujIO@>>>", null, "User", false, null)]
        [DataRow(12u, null, null, null, "NDIaklnmef*()#!3", null, "User", false, null)]
        public void SingleDeleteUser_Pass(uint sysID, string fName, string lName, string email,
            string password, string salt, string accntType, bool accountStatus, string errMsg)
        {
            // Arrange

            // Initializing User objects to test
            User user = new User(sysID, fName, lName, email, password, salt, accntType, accountStatus, errMsg);
            User thisUser = new User(109, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "System Admin", true, null);
            
            bool result = false;

            // Act
            _userManagementManager.SingleCreateUsers(thisUser, user);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                result = _userManagementManager.SingleDeleteUser(thisUser, user);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }
            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);

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

            // Initializing User objects to test
            User u1 = new User(1, null, null, null, "password", null, "User", false, null);
            User thisUser = new User(110, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "System Admin", true, null);

            Stopwatch sw = new Stopwatch();

            // Act
            sw.Start();
            try
            {
                result = _userManagementManager.SingleDeleteUser(thisUser, u1);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }
            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SingleDeleteUser_Fail_InvalidPermissions()
        {
            // Arrange
            bool result = true;

            // Initializing User objects to test
            User u1 = new User(1, null, null, null, "password", null, "System Admin", true, null);
            User thisUser = new User(111, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "Admin", true, null);

            Stopwatch sw = new Stopwatch();

            _userManagementManager.SingleCreateUsers(u1, thisUser);

            // Act
            sw.Start();
            try
            {
                result = _userManagementManager.SingleDeleteUser(thisUser, u1);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }
            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);

            // Assert
            Assert.IsFalse(result);

            _userManagementManager.SingleDeleteUser(u1, thisUser);
        }

        /// <summary>
        /// Test method to bulk delete users
        /// </summary>
        [TestMethod]
        public void BulkDeleteUsers_Pass()
        {
            // Arrange
            List<User> users = new List<User>();

            User u1 = new User(1, null, null, null, "password", null, "User", false, null);
            User u2 = new User(2, null, null, null, "123", null, "User", false, null);
            User u3 = new User(3, null, null, null, "", null, "User", false, null);
            User u4 = new User(4, null, null, null, null, null, "User", false, null);
            User u5 = new User(5, null, null, null, "bad", null, "User", false, null);
            User u6 = new User(6, null, null, null, "brian", null, "User", false, null);
            User thisUser = new User(112, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "System Admin", true, null);
            users.Add(u1);
            users.Add(u2);
            users.Add(u3);
            users.Add(u4);
            users.Add(u5);
            users.Add(u6);

            Stopwatch sw = new Stopwatch();

            // Act
            _userManagementManager.BulkCreateUsers(thisUser, users, false);
            List<List<User>> expected = new List<List<User>>()
            {
                users, // Passed ID's
                new List<User>() {} // Failed ID's
            };

            sw.Start();
            List<List<User>> actual = _userManagementManager.BulkDeleteUsers(thisUser, users);
            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);

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

            User u1 = new User(1, null, null, null, "password", null, "User", false, null);
            User u2 = new User(2, null, null, null, "123", null, "User", false, null);
            User u3 = new User(3, null, null, null, "", null, "User", false, null);
            User u4 = new User(4, null, null, null, null, null, "User", false, null);
            User u5 = new User(5, null, null, null, "bad", null, "User", false, null);
            User u6 = new User(6, null, null, null, "brian", null, "User", false, null);
            User thisUser = new User(113, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "System Admin", true, null);
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

            Stopwatch sw = new Stopwatch();

            // Act
            sw.Start();
            List<List<User>> actual = _userManagementManager.BulkDeleteUsers(thisUser, users);
            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);

            // Assert
            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);
        }

        /// <summary>
        /// Fail test method where attempting to bulk delete users fail because of 
        /// invalid permissions
        /// </summary>
        [TestMethod]
        public void BulkDeleteUsers_Fail_InvalidPermissions()
        {
            // Arrange
            List<User> users1 = new List<User>();
            List<User> users2 = new List<User>();

            User u1 = new User(1, null, null, null, "password", null, "Admin", true, null);
            User u2 = new User(2, null, null, null, "123", null, "Admin", true, null);
            User u3 = new User(3, null, null, null, "", null, "Admin", true, null);
            User u4 = new User(4, null, null, null, null, null, "Admin", true, null);
            User u5 = new User(5, null, null, null, "bad", null, "Admin", true, null);
            User u6 = new User(6, null, null, null, "brian", null, "Admin", true, null);
            User thisUser1 = new User(1111, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "System Admin", true, null);

            User thisUser2 = new User(7, null, null, null, "brian", null, "Admin", true, null);
            User u8 = new User(8, null, null, null, "brian", null, "System Admin", true, null);
            User u9 = new User(9, null, null, null, "brian", null, "System Admin", true, null);
            User u10 = new User(10, null, null, null, "brian", null, "System Admin", true, null);
            User u11 = new User(11, null, null, null, "brian", null, "System Admin", true, null);

            users1.Add(u1);
            users1.Add(u2);
            users1.Add(u3);
            users1.Add(u4);
            users1.Add(u5);
            users1.Add(u6);

            _userManagementManager.BulkCreateUsers(thisUser1, users1, false);

            users2.Add(u8);
            users2.Add(u9);
            users2.Add(u10);
            users2.Add(u11);

            List<List<User>> expected = new List<List<User>>()
            {
                new List<User>() {}, // Passed ID's
                users2 // Failed ID's
            };

            Stopwatch sw = new Stopwatch();

            sw.Start();
            List<List<User>> actual = _userManagementManager.BulkDeleteUsers(thisUser2, users2);
            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);

            // Assert
            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);
        }
    }
}