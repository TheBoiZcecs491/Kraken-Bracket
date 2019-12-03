using Data.AccessLayer;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Authentication.Services
{
    public class AuthenticationService
    {
        private const string _algorithm = "HmacSHA256";
        private const string _salt = "rz8LuOtFBXphj9WQfvFh";

        public void AuthenticateUser(string email, string password)
        {
            try
            {
                var dataAccess = new DataAccess();
                bool found = dataAccess.GetEmailAndPassword(email, password);
                if (found == true)
                {
                    string claim = GetClaim(email);
                    string token = GenerateToken(email, password, claim);
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

        public string GetClaim(string email)
        {
            var dataAccess = new DataAccess();
            string claim = dataAccess.DSGetClaim(email);
            return claim;

        }
        public string GenerateToken(string email, string password, string claim)
        {
            string hash = string.Join(":", new string[] { email, password, claim });
            string hashLeft = "";
            string hashRight = "";
            using (HMAC hmac = HMACSHA256.Create(_algorithm))
            {
                hmac.Key = Encoding.UTF8.GetBytes(password);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));
                hashLeft = Convert.ToBase64String(hmac.Hash);
                hashRight = string.Join(":", new string[] { email, claim });
            }
            string token = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, hashRight)));
            return token;
        }
    }
}
