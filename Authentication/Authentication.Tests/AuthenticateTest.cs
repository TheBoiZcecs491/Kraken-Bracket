using Authentication.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Authentication.Tests
{
    [TestClass]
    public class AuthenticateTest
    {
        [TestMethod]
        public void AuthenticateUser_Pass()
        {
            var authenticationService = new AuthenticationService();
            authenticationService.AuthenticateUser("brian@foomail.com", "123");
        }

        [TestMethod]
        public void AuthenticateUser_Fail_EmailDoesNotExist()
        {
            var authenticationService = new AuthenticationService();
            authenticationService.AuthenticateUser("brian@goomail.com", "111");
        }

        [TestMethod]
        public void AuthenticateUser_Fail_PasswordDoesNotMatchWithEmail()
        {
            var authenticationService = new AuthenticationService();
            authenticationService.AuthenticateUser("brian@foomail.com", "uihfruiwe");
        }

        [TestMethod]
        public void AuthenticateUser_Fail_NullPassword()
        {
            var authenticationService = new AuthenticationService();
            authenticationService.AuthenticateUser("brian@foomail.com", null);
        }

        [TestMethod]
        public void AuthenticateUser_Fail_NullEmail()
        {
            var authenticationService = new AuthenticationService();
            authenticationService.AuthenticateUser(null, "123");
        }

        [TestMethod]
        public void AuthenticateUser_Fail_NullParameters()
        {
            var authenticationService = new AuthenticationService();
            authenticationService.AuthenticateUser(null, null);
        }
    }
}
