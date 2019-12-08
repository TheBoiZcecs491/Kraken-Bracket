using System;
using Data.AccessLayer;

namespace AuthorizationService
{
    public class AuthorizationService
    {
        public bool UserPermission(string email)
        {
            var dataAccess = new DataAccess();
            string permissionLevel;
            string claim = dataAccess.DSGetClaim(email);

        }
    }
}
