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

        /// <summary>
        /// Method used to authenticate user
        /// </summary>
        /// 
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// 
        /// <returns>
        /// Token to be assigned to user
        /// </returns>
        public string AuthenticateUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Email or password cannot be null / empty");
            }
            var dataAccess = new DataAccess();
            bool found = dataAccess.GetEmailAndPassword(email, password);
            string claim;
            string token;
            if (found == true)
            {
                claim = dataAccess.DSGetClaim(email);
                token = GenerateToken(email, password, claim);
                return token;
            }
            else
            {
                throw new ArgumentException("No results returned");
            }
        }

        /// <summary>
        /// Method used to generate token. The token is generated based on the user's email, 
        /// password and claim
        /// 
        /// Algorithm found on the following website:
        /// http://www.primaryobjects.com/2015/05/08/token-based-authentication-for-web-service-apis-in-c-mvc-net/
        /// 
        /// </summary>
        /// 
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="claim"></param>
        /// 
        /// <returns>
        /// Token that will be assigned to the user
        /// </returns>
        public string GenerateToken(string email, string password, string claim)
        {
            // NOTE: string.Join() concatenates all elements in a string array.
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