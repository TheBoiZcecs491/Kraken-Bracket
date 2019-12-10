using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Data.AccessLayer
{
    public class DataAccess
    {
        Dictionary<string, string> userDict = new Dictionary<string, string>()
        {
            {"brian@foomail.com", "123"},
            {"test@fmail.com", "legoMyEggo123"}
        };
        Dictionary<string, string> claims = new Dictionary<string, string>()
        {
            {"brian@foomail.com", new Claim("http://kraken-bracket.gg", "Host").ToString()},
            {"test@fmail.com", new Claim("http://kraken-bracket.gg", "Registered User").ToString()},
        };


        /// <summary>
        /// Method used to check if email and password used.
        /// </summary>
        /// 
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// 
        /// <returns>
        /// True if both email and password exist. False if at least 1 does not.
        /// </returns>
        public bool GetEmailAndPassword(string email, string password)
        {
            

            // Checks if email exists in dictionary
            if (userDict.TryGetValue(email, out string value))
            {
                // Passed-in value matches password associated with email
                if (value == password)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method used to get claim associated with user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// 

        // FIXME: Research if this is how to assign claims
        public string DSGetClaim(string email)
        {
            if (claims.ContainsKey(email))
            {
                // TryGetValue(string key, out string value) - gets value associated with key.
                // The statement below will return the claim associated to the email address.
                claims.TryGetValue(email, out string value).ToString();
                return value;
            }
            else
            {
                throw new Exception();
            }
        }

    }
}