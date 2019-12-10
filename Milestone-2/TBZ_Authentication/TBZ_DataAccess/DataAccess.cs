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
        /// <param name="email">Email to search in the datastore</param>
        /// <param name="password">Password to search for in the datastore</param>
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
            // Checks to see if the passed-in email exists in the claims datastore
            if (claims.ContainsKey(email))
            {
                // TryGetValue(string key, out string value) - gets value associated with key.
                // The statement below will return the claims associated to the email address.
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