using System;
using Data.AccessLayer;

namespace AuthorizationService
{
    public class AuthorizationService
    {
        public bool UserPermission(string email, string action, bool isLoggedIn)
        {
            var dataAccess = new DataAccess();
            bool permission = false;
            string claim = dataAccess.DSGetClaim(email);
            if(isLoggedIn)
            {
                if((action == "Update Event Information") ||
                   (action == "Update Tournament Bracket Information") ||
                   (action == "Manage Tournament Bracket") ||
                   (action == "Delete Tournament Bracket and Event") ||
                   (action == "Assign Other Registered Users To Be A Co- Host"))
                {
                    if (claim == "Host" || claim == "Co-Host")
                        permission = true;
                    return permission;
                }

            }
            else
            {
                Console.WriteLine("User is not authorized to perform: " + action);
                return permission;
            }

        }
    }
}
