using Authorization.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TBZ_Authorization.Test
{
    [TestClass]
    public class AuthorizationConfirmTest_Pass
    {
        [TestMethod]
        public void AuthorizeUser_LoggedIn_Pass()
        {
            // Arrange
            var authorizationService = new AuthorizationService();
            string email = "brian@foomail.com";
            string action = "Search For Tournament Brackets";
            bool isLoggedIn = true;
            bool result = true;

            try
            {
                //Act
                authorizationService.UserPermission(email, action, isLoggedIn);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            // Assert
            Assert.IsTrue(result);
        }
    }
}
