﻿using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Data.AccessLayer
{
    public class DataAccess
    {

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
            IDictionary<string, string> userDict = new Dictionary<string, string>()
            {
                {"brian@foomail.com", "123"},
                {"test@fmail.com", "legoMyEggo123"}
            };
            string value;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Email or password cannot be null / empty");
            }

            // Checks if email exists in dictionary
            if (userDict.TryGetValue(email, out value))
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
        public string DSGetClaim(string email)
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
        }

    }
}