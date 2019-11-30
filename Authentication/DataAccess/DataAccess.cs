using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Data.AccessLayer
{
    public class DataAccess
    {

        /// <summary>
        /// Method used to check if email and password used.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>
        /// True if both email and password exist. False if at least 1 does not.
        /// </returns>
        public bool GetEmailAndPassword(string email, string password)
        {
            IDictionary<string, string> userDict = new Dictionary<string, string>()
            {
                {"brian@foomail.com", "123"},
                {"test@fmail.com", "legoMyEggo123"}
            };
            string value;

            // Checks if email exists in dictionary
            if (userDict.TryGetValue(email, out value))
            {
                // Passed-in value matches password associated with email
                if (value == password)
                {
                    return true;
                }
                else if (value != password)
                {
                    if (password == null)
                    {
                        // Passed-in password is null
                        throw new ArgumentException("Password cannot be null");
                    }
                    else
                    {
                        throw new ArgumentException("Passed-in password does not match with password associated with email");
                    }
                }
            }
            else
            {
                // One or both parameters do not match fields in dictionary
                throw new ArgumentException("Passed-in email does not exist");
            }
            return false;
            
        }

        public string GetClaim(string email)
        {
            Dictionary<string, string> claims = new Dictionary<string, string>()
            {
                {"brian@foomail.com", new Claim("http://kraken-bracket.gg", "Host").ToString()},
                {"test@fmail.com", new Claim("http://kraken-bracket.gg", "Co-Host").ToString()}
            };
            string value;
            if (claims.ContainsKey(email))
            {
                claims.TryGetValue(email, out value).ToString();
                return value;
            }
            else
            {
                throw new Exception();
            }
            //List<Claim> claims = new List<Claim>();
            //claims.Add(new Claim("http://kraken-bracket.gg", "Host"));
            //claims.Add(new Claim("http://kraken-bracket.gg", "Co-Host"));
            //claims.Add(new Claim("http://kraken-bracket.gg", "SystemAdmin"));
        }

    }
}
