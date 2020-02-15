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
        [DataRow(100,  "Brian!!!9039", "Admin")]
        public void SingleCreateUsers_Admin_Pass(int sysID, string password, string permission)
        {
            var userManagement = new UserManagement();
            bool result;
            try
            {
                result = userManagement.SingleCreateUsers(sysID,  password, permission);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            Assert.IsTrue(result);
        }

     
    }
}
