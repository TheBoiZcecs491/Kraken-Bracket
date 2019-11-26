using Data.AccessLayer;
using System;

namespace Authentication.Services
{
    public class AuthenticationService
    {
        public void AuthenticateUser(string email, string password)
        {
            var dataAccess = new DataAccess();
            dataAccess.GetEmailAndPassword(email, password);
        }
    }
}
