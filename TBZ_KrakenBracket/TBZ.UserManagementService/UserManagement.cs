using System;
using System.Linq;
using TBZ.DatabaseAccess;
using System.Collections.Generic;
using TBZ.UserManagementManager;
using TBZ.StringChecker;

namespace TBZ.UserManagementService
{
    public class UserManagement
    {
        private static readonly DataAccess _DataAccessService;
        private static readonly UserManagementManager.UserManagementManager _userManagementManager;

        static UserManagement()
        {
            _DataAccessService = new DataAccess();
            _userManagementManager = new UserManagementManager.UserManagementManager();
            
        }

        public bool SingleCreateUsers(User user)
        {
            bool temp = _DataAccessService.CreateUser(user, true);
            if (temp == false)
            {
                throw new ArgumentException("Failed to create user with associated ID");
            }
            return temp;
        }

        public List<List<User>> BulkCreateUsers(List<User> users, bool passwordCheck)
        {
            List<User> passedIDs = new List<User>();
            List<User> failedIDs = new List<User>();
            foreach(User u in users)
            {
                bool temp = _DataAccessService.CreateUser(u, passwordCheck);
                if (temp == true)
                {
                    passedIDs.Add(u);
                }
                else
                {
                    failedIDs.Add(u);
                }
            }
            return new List<List<User>> { passedIDs, failedIDs };
        }
    }
}
