using Data.AccessLayer;
using System;

namespace Authentication.Services
{
    public class AuthenticationService
    {
        public void AuthenticateUser(string email, string password)
        {
            try
            {
                var dataAccess = new DataAccess();
                bool found = dataAccess.GetEmailAndPassword(email, password);
                if (found == true)
                {
                    GetClaim(email);
                }
                else
                {

                }
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae);
            }
            catch (Exception) { }
        }

        public void GetClaim(string email)
        {
            var dataAccess = new DataAccess();

        }
        public string GenerateToken(string email, string password, string claim)
        {
            string temp = "";

            return temp;
        }
    }
}
