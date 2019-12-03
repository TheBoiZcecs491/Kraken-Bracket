using Authentication.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Authentication.Tests
{
    [TestClass]
    public class AuthenticateTest
    {
        [TestMethod]
        public void AuthenticateUser_Pass()
        {
            // Arrange
            var authenticationService = new AuthenticationService();
            string email = "brian@foomail.com";
            string password = "123";
            bool result = true;

            try
            {
                // Act
                authenticationService.AuthenticateUser(email, password);
            }
            catch (ArgumentException ae)
            {
                if (ae.Message == "Email or password is incorrect")
                {
                    result = false;
                }
            }
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AuthenticateUser_Fail_EmailDoesNotExist()
        {
            // Arrange
            var authenticationService = new AuthenticationService();
            string email = "nonexistent@foomail.com";
            string password = "123";
            bool result = false;

            try
            {
                // Act
                authenticationService.AuthenticateUser(email, password);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            // Assert 
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AuthenticateUser_Fail_PasswordDoesNotMatchWithEmail()
        {
            // Arrange
            var authenticationService = new AuthenticationService();
            string email = "brian@foomail.com";
            string password = "111111111";
            bool result = false;
            try
            {
                // Act
                authenticationService.AuthenticateUser(email, password);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            // Assert 
            Assert.IsTrue(result);
        }
        
    

        [TestMethod]
        public void AuthenticateUser_Fail_NullPassword()
        {
            // Arrange
            var authenticationService = new AuthenticationService();
            string email = "brian@foomail.com";
            string password = null;
            bool result = false;
            try
            {
                // Act
                authenticationService.AuthenticateUser(email, password);
            }
            catch (ArgumentException)
            {
                result = true;
            }
       
            // Assert 
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AuthenticateUser_Fail_NullEmail()
        {
            // Arrange
            var authenticationService = new AuthenticationService();
            string email = null;
            string password = "123";
            bool result = false;
            try
            {
                // Act
                authenticationService.AuthenticateUser(email, password);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            // Assert 
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AuthenticateUser_Fail_NullParameters()
        {
            // Arrange
            var authenticationService = new AuthenticationService();
            string email = null;
            string password = null;
            bool result = false;
            try
            {
                // Act
                authenticationService.AuthenticateUser(email, password);
            }
            catch (ArgumentException)
            {
                result = true;
            }
            // Assert 
            Assert.IsTrue(result);
        }
    }
}
