
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TBZ_Authorization.Services;

namespace TBZ_Authorization.Test
{
    [TestClass]
    public class AuthorizationTests
    {
        [TestMethod]
        public void AuthorizeUser_LoggedIn_Pass()
        {
            // Arrange
            var authorizationService = new AuthorizationService();
            string email = "test@fmail.com";
            string action = "Check Into A Match";
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

        [TestMethod]
        public void AuthorizeUser_NotLoggedIn_Fail()
        {
            // Arrange
            var authorizationService = new AuthorizationService();
            string email = "test@fmail.com";
            string action = "Check Into A Match";
            bool isLoggedIn = false;
            bool result = false;

            try
            {
                //Act
                authorizationService.UserPermission(email, action, isLoggedIn);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            catch (Exception) { }

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AuthorizeUser_Permission_Pass()
        {
            // Arrange
            var authorizationService = new AuthorizationService();
            string email = "test@fmail.com";
            string action = "Check Into A Match";
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

        [TestMethod]
        public void AuthorizeUser_Permission_Fail()
        {
            // Arrange
            var authorizationService = new AuthorizationService();
            string email = "test@fmail.com";
            string action = "Update Tournament Bracket";
            bool isLoggedIn = true;
            bool result = false;

            try
            {
                //Act
                authorizationService.UserPermission(email, action, isLoggedIn);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            catch (Exception) { }

            // Assert
            Assert.IsTrue(result);
        }
    }
}
