using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.AccessLayer
{
    public class DataAccess
    {
        // List of users and their passwords
        Dictionary<string, string> userDict = new Dictionary<string, string>()
        {
            {"brian@foomail.com", "123"},
            {"test@fmail.com", "legoMyEggo123"}
        };

        // Initialize dictionary with email (string) as keys and claims (key) as the values
        Dictionary<string, string> claims = new Dictionary<string, string>()
        {
            {"brian@foomail.com", "Host" },
            {"test@fmail.com", "Co-host" },
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
        public string DSGetClaim(string email)
        {
            // var userPrincipal = new ClaimsPrincipal(new[] { brianIdentity }).ToString();
            if (claims.ContainsKey(email))
            {
                // TryGetValue(string key, out string value) - gets value associated with key.
                // The statement below will return the claims associated to the email address.
                claims.TryGetValue(email, out List<string> value).ToString();
                // Convert the list of claims into an array
                string claimCollection = string.Join(",", value.ToArray());
                return claimCollection;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}