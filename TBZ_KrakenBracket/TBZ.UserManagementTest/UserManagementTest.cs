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
        [TestMethod]
        public void BulkCreateUsers_Pass()
        {
            List<User> users = new List<User>();
            User u1 = new User();
            u1.SystemID = 1;
            u1.Password = "80ewjf0wNUFEIA@";
            u1.AccountType = "User";

            User u2 = new User();
            u2.SystemID = 2;
            u2.Password = "80ewjf0wNUFEIA@";
            u2.AccountType = "User";

            users.Add(u1);
            users.Add(u2);

            var um = new UserManagement();
            um.BulkCreateUsers(users, true);
        }
    }
}
