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
        [DataRow(1, null, null, null, "84092ujIO@>>>", "User", true, null)]
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
            List<List<int>> expected = new List<List<int>>()
            {
                new List<int>(){3, 4},
                new List<int>(){ }
            };


            // Act
            List<List<uint>> actual = um.BulkCreateUsers(users, true);
            //actual.ForEach(Console.WriteLine);
            //CollectionAssert.AreEqual(expected, actual);
        }
    }
}
