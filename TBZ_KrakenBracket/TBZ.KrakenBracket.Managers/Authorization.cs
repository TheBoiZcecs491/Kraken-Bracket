using System;
using System.Text.RegularExpressions;
using TBZ.DatabaseAccess;

namespace TBZ.AuthorizationManager
{
    public class Authorization
    {
        public bool UserPermission(string email, string action, bool isLoggedIn)
        {
            Regex rgx = new Regex(action);
            var dataAccess = new DataAccess();
            bool permission = false;
            string claim = (string)dataAccess.DSGetClaim(email);
            // If user is non-registered
            if ((string.IsNullOrEmpty(email)) && (isLoggedIn == false) && claim.Contains(action))
            {
                permission = true;
            }
            // Access to users who are registered and non-registered (not logged in)
            else if ((isLoggedIn) && (claim.Contains(action)))
            {
                permission = true;
            }
            // If action is not found or claim does not match, display restriction message
            else if ((!isLoggedIn))
            {
                throw new ArgumentException("User is not logged in\n");
            }
            else
            {
                throw new ArgumentException("User is not authorized to perform: " + action);
            }
            return permission;
        }
    }
}