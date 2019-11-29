using System;
using System.Collections.Generic;

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
                {"test@fmail.com", "legoMyEggo123" }
            };

            if (userDict.TryGetValue(email, out password))
            {
                return true;
            } 
            else
            {
                throw new ArgumentException("Username or password is incorrect");
            }

            //for (int i = 0; i < userDict.Count; i++)
            //{
            //    // Checks to see if the email exists in the database
            //    if (userDict.Keys.Contains(email))
            //    {
            //        if (email == userDict[i])
            //        {

            //        }
            //    }
            //}
         
        }

    }
}
