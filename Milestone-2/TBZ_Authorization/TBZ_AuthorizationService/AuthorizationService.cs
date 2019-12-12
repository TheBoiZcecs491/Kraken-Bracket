using System;
using Data.AccessLayer;

namespace TBZ_Authorization.Services
{
    /// <summary>
    /// The main method to check if the user is authorized to perform the action
    /// based on who they claim to be
    /// </summary>
    /// <param user = "email"> The email provided to receive the claim </param>
    /// <param action> The string of the action attempted to perform </param>
    /// <param logged in = "isLoggedIn"> The boolean variable to confirm if user is logged in</param>
    /// <returns> A boolean of whether or not the user is authorized to perform the action</returns>
    public class AuthorizationService
    {
        public bool UserPermission(string email, string action, bool isLoggedIn)
        {
            var dataAccess = new DataAccess();
            bool permission = false;
            string claim = dataAccess.DSGetClaim(email);
            // If user is non-registered
            if((string.IsNullOrEmpty(email)) && (isLoggedIn == false) && claim.Contains(action))
            {
                permission = true;
            }
            // Access to users who are registered and non-registered (not logged in)
            else if ((isLoggedIn) && (claim.Contains(action)))
            {
                permission = true;
            }
            // If action is not found or claim does not match, display restriction message
            else if((!isLoggedIn))
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
