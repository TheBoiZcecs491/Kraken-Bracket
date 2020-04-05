using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TBZ.AuthorizationManager;

namespace TBZ.AuthorizationTest
{
    [TestClass]
    public class AuthorizationTests
    {
        [TestMethod]
        public void AuthorizeUser_LoggedIn_Pass()
        {
            // Arrange
            var authorizationService = new Authorization();
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
            var authorizationService = new Authorization();
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
            var authorizationService = new Authorization();
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
            var authorizationService = new Authorization();
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

        [TestMethod]
        public void AuthorizeUser_NonRegistered_Action_Pass()
        {
            // Arrange
            var authorizationService = new Authorization();
            string email = "";
            string action = "Search For Tournament Brackets";
            bool isLoggedIn = false;
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
        public void AuthorizeUser__NonRegistered_Action_Fail()
        {
            // Arrange
            var authorizationService = new Authorization();
            string email = "";
            string action = "Update Tournament Bracket";
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
    }
}