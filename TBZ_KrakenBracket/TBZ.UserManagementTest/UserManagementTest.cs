using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TBZ.DatabaseAccess;
using TBZ.UM_Manager;

//these two are for my reset button
using MySql.Data.MySqlClient;
using TBZ.DatabaseConnectionService;

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
        /// Method to clean data from database
        /// </summary>
        public void ResetDB()
        {
            var DB = new Database();

            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                using (MySqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandText = "TRUNCATE TABLE `user_information`";
                    conn.Open();
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
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

            // User to insert into DB
            User user = new User(sysID, fName, lName, email, password, salt, accntType, accountStatus, errMsg);

            // User to perform operation
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
            ResetDB();
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

            // User to insert into DB
            User user = new User(sysID, fName, lName, email, password, salt, accntType, accountStatus, errMsg);

            // User performing operation
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
            ResetDB();
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

            // User to insert into DB
            User user = new User(sysID, fName, lName, email, password, salt, accntType, accountStatus, errMsg);

            // User performing action
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
            ResetDB();
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

            // User to insert into DB
            User user = new User(sysID, fName, lName, email, password, salt, accntType, accountStatus, errMsg);

            // User performing operation
            User thisUser = new User(103, fName, lName, email, password, salt, "System Admin", true, null);

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
            ResetDB();
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

            // Users to insert into DB
            User u1 = new User(3, null, null, null, "8*3kmmrMropongig", null, "User", false, null);
            User u2 = new User(4, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "User", false, null);

            // User performing operation
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

            ResetDB();

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

            // Users to insert into DB
            User u1 = new User(1, null, null, null, "password", null, "User", false, null);
            User u2 = new User(2, null, null, null, "123", null, "User", false, null);
            User u3 = new User(3, null, null, null, "", null, "User", false, null);
            User u4 = new User(4, null, null, null, null, null, "User", false, null);
            User u5 = new User(5, null, null, null, "bad", null, "User", false, null);
            User u6 = new User(6, null, null, null, "brian", null, "User", false, null);

            // User performing operation
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
            ResetDB();
        }

        [TestMethod]
        public void BulkCreateUsers_Fail_InvalidPermissions()
        {
            // Arrange
            List<User> users = new List<User>();

            // Initializing User objects to test

            // Users to insert into DB
            User u1 = new User(1, null, null, null, "password", null, "System Admin", true, null);
            User u2 = new User(1, null, null, null, "123", null, "System Admin", true, null);
            User u3 = new User(1, null, null, null, "", null, "System Admin", true, null);

            // User performing operation
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
            ResetDB();
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

            // Users to insert into DB
            User u1 = new User(1, null, null, null, "password", null, "User", false, null);
            User u2 = new User(1, null, null, null, "123", null, "User", false, null);
            User u3 = new User(1, null, null, null, "", null, "User", false, null);
            User u4 = new User(1, null, null, null, null, null, "User", false, null);
            User u5 = new User(1, null, null, null, "bad", null, "User", false, null);
            User u6 = new User(1, null, null, null, "brian", null, "User", false, null);

            // User performing operation
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
            ResetDB();
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

            // Users to insert into DB
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

            // User performing operation
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
            ResetDB();
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

            // User to delete from DB
            User user = new User(sysID, fName, lName, email, password, salt, accntType, accountStatus, errMsg);

            // User performing operation
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
            ResetDB();
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

            // User to delete from DB
            User u1 = new User(1, null, null, null, "password", null, "User", false, null);

            // User performing operation
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
            ResetDB();
        }

        [TestMethod]
        public void SingleDeleteUser_Fail_InvalidPermissions()
        {
            // Arrange
            bool result = true;

            // Initializing User objects to test

            // User to delete from DB
            User u1 = new User(1, null, null, null, "password", null, "System Admin", true, null);

            // User performing operation
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
            ResetDB();
        }

        /// <summary>
        /// Test method to bulk delete users
        /// </summary>
        [TestMethod]
        public void BulkDeleteUsers_Pass()
        {
            // Arrange
            List<User> users = new List<User>();

            // Users to delete from DB
            User u1 = new User(1, null, null, null, "password", null, "User", false, null);
            User u2 = new User(2, null, null, null, "123", null, "User", false, null);
            User u3 = new User(3, null, null, null, "", null, "User", false, null);
            User u4 = new User(4, null, null, null, null, null, "User", false, null);
            User u5 = new User(5, null, null, null, "bad", null, "User", false, null);
            User u6 = new User(6, null, null, null, "brian", null, "User", false, null);

            // User performing operation
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
            ResetDB();
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

            // Users to delete from DB
            User u1 = new User(1, null, null, null, "password", null, "User", false, null);
            User u2 = new User(2, null, null, null, "123", null, "User", false, null);
            User u3 = new User(3, null, null, null, "", null, "User", false, null);
            User u4 = new User(4, null, null, null, null, null, "User", false, null);
            User u5 = new User(5, null, null, null, "bad", null, "User", false, null);
            User u6 = new User(6, null, null, null, "brian", null, "User", false, null);

            // User performing operation
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
            ResetDB();
        }

        /// <summary>
        /// Fail test method where attempting to bulk delete users fail because of 
        /// invalid permissions
        /// ALSO also: I realised this doesnt clear the test DB when it finishes,
        /// that is screwing up the next test BulkDeleteUsers_Fail_SystemIDsDoNotExist
        /// </summary>
        [TestMethod]
        public void BulkDeleteUsers_Fail_InvalidPermissions()
        {
            // Arrange
            List<User> users = new List<User>();
   
            // Populate admins in DB
            User u1 = new User(1, null, null, null, "password", null, "Admin", true, null);
            User u2 = new User(2, null, null, null, "123", null, "Admin", true, null);
            User u3 = new User(3, null, null, null, "", null, "Admin", true, null);
            User u4 = new User(4, null, null, null, null, null, "Admin", true, null);
            User u5 = new User(5, null, null, null, "bad", null, "Admin", true, null);
            User u6 = new User(6, null, null, null, "brian", null, "Admin", true, null);
            User thisUser = new User(1111, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "System Admin", true, null);

            users.Add(u1);
            users.Add(u2);
            users.Add(u3);
            users.Add(u4);
            users.Add(u5);
            users.Add(u6);

            _userManagementManager.BulkCreateUsers(thisUser, users, false);


            List<List<User>> expected = new List<List<User>>()
            {
                new List<User>() {}, // Passed ID's
                users // Failed ID's
            };

            Stopwatch sw = new Stopwatch();

            sw.Start();
            // An admin will attempt to bulk delete other admins
            List<List<User>> actual = _userManagementManager.BulkDeleteUsers(u6, users);
            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);

            // Assert
            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);
            ResetDB();
        }


        /// <summary>
        /// Test method to Update a single user
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
        public void SingleUpdateUser_Pass(uint sysID, string fName, string lName, string email,
            string password, string salt, string accntType, bool accountStatus, string errMsg)
        {
            // Arrange

            // Initializing User objects to test

            // User to update in DB
            User user = new User(sysID, fName, lName, email, password, salt, accntType, accountStatus, errMsg);

            // User performing operation
            User thisUser = new User(109, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "System Admin", true, null);
            var um = new UserManagementManager();
            bool result;

            // Act
            um.SingleCreateUsers(thisUser, user);
            try
            {
                string nameChange = "Bob";
                user.FirstName = nameChange; //attempts to change the user's name to Bob
                result = um.SingleUpdateUser(thisUser, user, "FirstName"); //then pushes the update
                //TODO: wait... shouldnt we test to see IF our DB is changing values?
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { result = false; }

            // Assert
            Assert.IsTrue(result);
            ResetDB();
        }

        [TestMethod]
        public void SingleUpdateUser_Fail_InvalidPermissions()
        {
            // Arrange
            bool result = true;

            // Initializing User objects to test

            // User to delete from DB
            User u1 = new User(1, null, null, null, "password", null, "System Admin", true, null);

            // User performing operation
            User thisUser = new User(111, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "Admin", true, null);

            Stopwatch sw = new Stopwatch();

            _userManagementManager.SingleCreateUsers(u1, thisUser);

            // Act
            sw.Start();
            try
            {
                u1.FirstName = "Barry"; // lets see if this works
                result = _userManagementManager.SingleUpdateUser(thisUser, u1, "FirstName");
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
            ResetDB();
        }

        /// <summary>
        /// Fail test method where attempting to Update a user fails because
        /// the system ID is not found
        /// </summary>
        [TestMethod]
        public void SingleUpdateUser_Fail_SystemIDDoesNotExist()
        {
            // Arrange
            bool result = true;

            // Initializing User objects to test

            // User to delete from DB
            User u1 = new User(1, null, null, null, "password", null, "User", false, null);

            // User performing operation
            User thisUser = new User(110, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "System Admin", true, null);

            Stopwatch sw = new Stopwatch();

            // Act
            sw.Start();
            try
            {
                u1.FirstName = "Raul";
                result = _userManagementManager.SingleUpdateUser(thisUser, u1, "FirstName");
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
            ResetDB();
        }



        /// <summary>
        /// Test method to bulk Update users
        /// </summary>
        [TestMethod]
        public void BulkUpdateUsers_Pass()
        {
            // Arrange
            List<User> users = new List<User>();

            // Users to update in DB
            User u1 = new User(1, null, null, null, "password", null, "User", false, null);
            User u2 = new User(2, null, null, null, "123", null, "User", false, null);
            User u3 = new User(3, null, null, null, "", null, "User", false, null);
            User u4 = new User(4, null, null, null, null, null, "User", false, null);
            User u5 = new User(5, null, null, null, "bad", null, "User", false, null);
            User u6 = new User(6, null, null, null, "brian", null, "User", false, null);

            // User performing operation
            User thisUser = new User(112, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "System Admin", true, null);
            users.Add(u1);
            users.Add(u2);
            users.Add(u3);
            users.Add(u4);
            users.Add(u5);
            users.Add(u6);

            var um = new UserManagementManager();

            // Act
            um.BulkCreateUsers(thisUser, users, false);
            List<List<User>> expected = new List<List<User>>()
            {
                users, // Passed ID's
                new List<User>() {} // Failed ID's
            };


            string nameChange = "Bob";
            foreach (User u in users)
            {
                u.FirstName = nameChange;
            }
            List<List<User>> actual = um.BulkUpdateUsers(thisUser, users, "FirstName");

            // Assert
            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);
            ResetDB();
        }

        /// <summary>
        /// Fail test method where attempting to bulk update users fail because
        /// the system ID's do not exist
        /// </summary>
        [TestMethod]
        public void BulkDUpdateUsers_Fail_SystemIDsDoNotExist()
        {
            // Arrange
            List<User> users = new List<User>();

            // Users to delete from DB
            User u1 = new User(1, null, null, null, "password", null, "User", false, null);
            User u2 = new User(2, null, null, null, "123", null, "User", false, null);
            User u3 = new User(3, null, null, null, "", null, "User", false, null);
            User u4 = new User(4, null, null, null, null, null, "User", false, null);
            User u5 = new User(5, null, null, null, "bad", null, "User", false, null);
            User u6 = new User(6, null, null, null, "brian", null, "User", false, null);

            // User performing operation
            User thisUser = new User(113, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "System Admin", true, null);
            users.Add(u1);
            u1.FirstName = "Joe";
            users.Add(u2);
            u2.FirstName = "Barry";
            users.Add(u3);
            u3.FirstName = "Snerp";
            users.Add(u4);
            u4.FirstName = "Suzy";
            users.Add(u5);
            u5.FirstName = "Brulee";
            users.Add(u6);
            u6.FirstName = "Carl";


            List<List<User>> expected = new List<List<User>>()
            {
                new List<User>() {}, // Passed ID's
                users // Failed ID's
            };

            Stopwatch sw = new Stopwatch();

            // Act
            sw.Start();
            List<List<User>> actual = _userManagementManager.BulkUpdateUsers(thisUser, users, "FirstName");
            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);

            // Assert
            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);
            ResetDB();
        }

        /// <summary>
        /// Fail test method where attempting to bulk update users fail because of 
        /// invalid permissions
        /// </summary>
        [TestMethod]
        public void BulkUpdateUsers_Fail_InvalidPermissions()
        {
            // Arrange
            List<User> users = new List<User>();

            // Populate admins in DB
            User u1 = new User(1, null, null, null, "password", null, "Admin", true, null);
            User u2 = new User(2, null, null, null, "123", null, "Admin", true, null);
            User u3 = new User(3, null, null, null, "", null, "Admin", true, null);
            User u4 = new User(4, null, null, null, null, null, "Admin", true, null);
            User u5 = new User(5, null, null, null, "bad", null, "Admin", true, null);
            User u6 = new User(6, null, null, null, "brian", null, "Admin", true, null);
            User thisUser = new User(1111, null, null, null, "meMEeiaj093QNGEJOW~~~", null, "System Admin", true, null);

            users.Add(u1);
            users.Add(u2);
            users.Add(u3);
            users.Add(u4);
            users.Add(u5);
            users.Add(u6);

            _userManagementManager.BulkCreateUsers(thisUser, users, false);

            u1.FirstName = "John";
            u2.FirstName = "Lyana";
            u3.FirstName = "Gregory";
            u4.FirstName = "Fred";
            u5.FirstName = "Kyle";
            u6.FirstName = "Scoffle";


            List<List<User>> expected = new List<List<User>>()
            {
                new List<User>() {}, // Passed ID's
                users // Failed ID's
            };

            Stopwatch sw = new Stopwatch();

            sw.Start();
            // An admin will attempt to bulk delete other admins
            List<List<User>> actual = _userManagementManager.BulkUpdateUsers(u6, users, "FirstName");
            sw.Stop();
            Console.WriteLine("Elapsed = {0} ms", sw.ElapsedMilliseconds);

            // Assert
            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);
            ResetDB();
        }


    }

}