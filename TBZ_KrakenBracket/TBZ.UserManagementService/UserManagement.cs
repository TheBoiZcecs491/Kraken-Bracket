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
                throw new ArgumentException("");
            }
            return temp;
        }

        public List<List<int>> BulkCreateUsers(List<User> users, bool passwordCheck)
        {
            List<int> passedIDs = new List<int>();
            List<int> failedIDs = new List<int>();
            foreach(User u in users)
            {
                bool temp = _DataAccessService.CreateUser(u, passwordCheck);
                if (temp == true)
                {
                    passedIDs.Add(u.SystemID);
                }
                else
                {
                    failedIDs.Add(u.SystemID);
                }
            }
            return new List<List<int>> { passedIDs, failedIDs };
        }
    }
}
