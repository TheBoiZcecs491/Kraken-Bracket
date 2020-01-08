using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TBZ.AuthenticationService;

namespace TBZ.AuthenticationTest
{
    [TestClass]
    public class AuthenticateTest
    {
        /// <summary>
        /// Test case where credentials are valid
        /// </summary>
        [TestMethod]
        public void AuthenticateUser_Pass()
        {
            // Arrange
            var authenticationService = new Authentication();
            string email = "kevin@foomail.com";
            string password = "123";
            bool result = true;

            try
            {
                // Act
                authenticationService.AuthenticateUser(email, password);
                //um... what is this set to?
                //also this isnt reproducable.
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (Exception) { }

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test case where the email passed in does not exist
        /// </summary>
        [TestMethod]
        public void AuthenticateUser_Fail_EmailDoesNotExist()
        {
            // Arrange
            var authenticationService = new Authentication();
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
            catch (Exception) { }
            // Assert 
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test case where email passed in exists in the database, but the password passed-in does not
        /// match the password associated with the email in the database
        /// </summary>
        [TestMethod]
        public void AuthenticateUser_Fail_PasswordDoesNotMatchWithEmail()
        {
            // Arrange
            var authenticationService = new Authentication();
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
            catch (Exception) { }
            // Assert 
            Assert.IsTrue(result);
        }


        /// <summary>
        /// Test case where no password is passed in
        /// </summary>
        [TestMethod]
        public void AuthenticateUser_Fail_NullPassword()
        {
            // Arrange
            var authenticationService = new Authentication();
            string email = "brian@foomail.com";
            string password = string.Empty;
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
            catch (Exception) { }

            // Assert 
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test case where no email is passed in
        /// </summary>
        [TestMethod]
        public void AuthenticateUser_Fail_NullEmail()
        {
            // Arrange
            var authenticationService = new Authentication();
            string email = string.Empty;
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
            catch (Exception) { }
            // Assert 
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test case where neither email nor password is passed in
        /// </summary>
        [TestMethod]
        public void AuthenticateUser_Fail_NullParameters()
        {
            // Arrange
            var authenticationService = new Authentication();
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
            catch (Exception) { }
            // Assert 
            Assert.IsTrue(result);
        }
    }
}
