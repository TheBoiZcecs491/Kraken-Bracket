using System;
using Data.AccessLayer;

namespace Authorization.Services
{
    public class AuthorizationService
    {
        public bool UserPermission(string email, string action, bool isLoggedIn)
        {
            var dataAccess = new DataAccess();
            bool permission = false;
            string claim = dataAccess.DSGetClaim(email);
            // Access to users who are registered and non-registered (not logged in)
            if((!isLoggedIn) || (claim == "Registered User"))
            {
                if ((action == "Search For Tournament Brackets") ||
                    (action == "Search For Event") ||
                    (action == "Search For Registered User"))
                {
                    permission = true;
                }
            }
            // Access to Hosts, Co-hosts, and competitors
            else if(isLoggedIn)
            {
                if((action == "Update Event Information") ||
                   (action == "Update Tournament Bracket Information") ||
                   (action == "Manage Tournament Bracket") ||
                   (action == "Delete Tournament Bracket and Event") ||
                   (action == "Assign Other Registered Users To Be A Co- Host"))
                {
                    if (claim == "Host" || claim == "Co-Host")
                        permission = true;
                }
                else if ((action == "Check Into A Match") ||
                         (action == "Have A Substitute"))
                {
                    if (claim == "Competitor")
                        permission = true;
                }
            }
            // If action is not found or claim does not match, display restriction message
            else
                Console.WriteLine("User is not authorized to perform: " + action);
            return permission;
        }
    }
}
