using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TBZ.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.KrakenBracket.Services;

namespace TBZ.KrakenBracket.Managers
{
    public class AuthenticationManager
    {

        //private const string _algorithm = "HmacSHA256";
        //private const string _salt = "rz8LuOtFBXphj9WQfvFh"; //TODO: generate salt values.

        ///// <summary>
        ///// Method used to authenticate user
        ///// </summary>
        ///// 
        ///// <param name="email"></param>
        ///// <param name="password"></param>
        ///// 
        ///// <returns>
        ///// Token to be assigned to user
        ///// </returns>
        //public string AuthenticateUser(string email, string password)
        //{
        //    // Check to see if passed-in email or password is null / empty
        //    //TODO: check for white space
        //    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        //    {
        //        throw new ArgumentException("Email or password cannot be null / empty");
        //    }

        //    // Initialize DataAccess object
        //    //not nessary. just return the token.
        //    var dataAccess = new DataAccess();

        //    // Check to see if email / password combination exists in the datastore
        //    bool found = dataAccess.GetEmailAndPassword(email, password);

        //    // If the email and password combination exists in the database
        //    if (found == true)
        //    {
        //        // Retrieve hashed password
        //        string hashedPassword = GetHashedPassword(password);

        //        // Retrieve claim associated with user
        //        string claim = dataAccess.DSGetClaim(email);

        //        // Generate token to assign to user
        //        string token = GenerateToken(email, hashedPassword, claim);
        //        return token;
        //    }

        //    // The email / password combination does not exist in the datastore
        //    else
        //    {
        //        throw new ArgumentException("No results returned");
        //    }
        //}

        ///// <summary>
        ///// Method used to generate token. The token is generated based on the user's email, 
        ///// password and claim
        ///// 
        ///// Algorithm found on the following website:
        ///// http://www.primaryobjects.com/2015/05/08/token-based-authentication-for-web-service-apis-in-c-mvc-net/
        ///// 
        ///// </summary>
        ///// 
        ///// <param name="email"></param>
        ///// <param name="password"></param>
        ///// <param name="claim"></param>
        ///// 
        ///// <returns>
        ///// Token that will be assigned to the user
        ///// </returns>
        //public string GenerateToken(string email, string password, string claim)
        //{
        //    // Concatenate email, (hashed) password, and claim in a string array.
        //    string hash = string.Join(":", new string[] { email, password, claim });
        //    string hashLeft = "";
        //    string hashRight = "";
        //    using (HMAC hmac = HMACSHA256.Create(_algorithm))
        //    {
        //        hmac.Key = Encoding.UTF8.GetBytes(password);
        //        hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));
        //        hashLeft = Convert.ToBase64String(hmac.Hash);
        //        hashRight = string.Join(":", new string[] { email, claim });
        //    }
        //    string token = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, hashRight)));
        //    return token;
        //}

        ///// <summary>
        ///// Method to decode a user's token
        ///// </summary>
        ///// <param name="token"></param>
        ///// <returns>
        ///// Decoded token in the format of HashedPassword:Email:Role
        ///// </returns>
        //public string DecodeToken(string token)
        //{
        //    string key = Encoding.UTF8.GetString(Convert.FromBase64String(token));
        //    return key;
        //}

        ///// <summary>
        ///// HMAC SHA-256 algorithm to hash a password.
        ///// 
        ///// Algorithm found on the following website:
        ///// http://www.primaryobjects.com/2015/05/08/token-based-authentication-for-web-service-apis-in-c-mvc-net/
        ///// 
        ///// </summary>
        ///// <param name="password">(Unhashed) Password to be hashed</param>
        ///// <returns>Hashed password</returns>
        //public static string GetHashedPassword(string password)//needs to know what the string was encoded in.
        //{
        //    // Concatenate password and salt
        //    //dont use the ':' also maybe dont hard code that in.
        //    string key = string.Join(":", new string[] { password, _salt });
        //    using (HMAC hmac = HMACSHA256.Create(_algorithm))// new HMACSHA256() 
        //    {
        //        // Hash the key.
        //        hmac.Key = Encoding.UTF8.GetBytes(_salt);//TODO, the salt needs to be different each time.
        //        hmac.ComputeHash(Encoding.UTF8.GetBytes(key));
        //        return Convert.ToBase64String(hmac.Hash);
        //    }
        //}
        private readonly DataAccess _databaseAccess = new DataAccess();
        private readonly AuthenticationService _authenticationService = new AuthenticationService();
        public User Login(string email, string password)
        {
            User user = _databaseAccess.GetUserByEmail(email);
            if (user == null)
            {
                throw new ArgumentException();
            }
            bool passwordCheck = _authenticationService.ComparePasswords(email, password);
            if (passwordCheck) return user;
            else
            {
                throw new ArgumentException();
            }
        }
    }
}