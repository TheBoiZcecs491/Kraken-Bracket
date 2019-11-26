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
            authenticationService.AuthenticateUser("brian@fmail.com", "password");
        }
    }
}
