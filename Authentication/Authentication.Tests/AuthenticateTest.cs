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
        public void AuthenticateUser_failed()
        {
            var authenticationService = new AuthenticationService();
            authenticationService.AuthenticateUser("brian@goomail.com", "111");
        }
    }
}
