using Data.AccessLayer;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Authentication.Services
{
    public class AuthenticationService
    {
        private const string _algorithm = "HmacSHA256";
        //private const string _salt = "rz8LuOtFBXphj9WQfvFh";
        //that is brian's stupid idea of a salt generator. just fart on the keyboard and see what we get.
        

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
            // Check to see if passed-in email or password is null / empty
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Email or password cannot be null / empty");
            }

            // Initialize DataAccess object
            var dataAccess = new DataAccess();

            // Check to see if email / password combination exists in the datastore
            bool found = dataAccess.GetEmailAndPassword(email, password);
            
            // If the email and password combination exists in the database
            if (found == true)
            { 
                // Retrieve hashed password
                string hashedPassword = GetHashedPassword(password);

                // Retrieve claim associated with user
                string claim = dataAccess.DSGetClaim(email);

                // Generate token to assign to user
                string token = GenerateToken(email, hashedPassword, claim);
                return token;
            }

            // The email / password combination does not exist in the datastore
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
            // Concatenate email, (hashed) password, and claim in a string array.
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

        /// <summary>
        /// Method to decode a user's token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>
        /// Decoded token in the format of HashedPassword:Email:Role
        /// </returns>
        public string DecodeToken(string token)
        {
            string key = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            return key;
        }

        /// <summary>
        /// HMAC SHA-256 algorithm to hash a password.
        /// 
        /// Algorithm found on the following website:
        /// http://www.primaryobjects.com/2015/05/08/token-based-authentication-for-web-service-apis-in-c-mvc-net/
        /// 
        /// </summary>
        /// <param name="password">(Unhashed) Password to be hashed</param>
        /// <returns>Hashed password</returns>
        public static string GetHashedPassword(string password)
        {
            // Concatenate password and salt
            //TODO: actually properly salt it with the stuff I found out about above
            //because Brian has this setup to use strings I have to do a convert here, I might change this tho.
            string saltASCII = Encoding.ASCII.GetString(makeRando(3)); //no idea if this even works lul
            string key = string.Join(":", new string[] { password, saltASCII });
            using (HMAC hmac = HMACSHA256.Create(_algorithm))
            {
                // Hash the key.
                hmac.Key = Encoding.UTF8.GetBytes(saltASCII);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));
                return Convert.ToBase64String(hmac.Hash);
                //so waiet, the result contains both the salt value and the key rite? as a b64 string?
            }
        }

        public static byte[] makeRando(int x)
        {
            /*
            Okay because Brian doesnt know how to generate real kosher salt.
            I guess I'll have to do it. though to be fair I didn't know until just now.
            basically this generates an array of random bytes of a choice amount.
             */
            byte[] saltTemp = new byte[x];
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            rngCsp.GetBytes(saltTemp);
            return saltTemp;
    }
    }
}