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

        public bool SingleCreateUsers(int sysID, string password, string accountType, string permission)
        {
            // TODO: have a check for password. Use Kevin's registration checker
            if (_userManagementManager.CheckPermission(permission))
            {
                stringChecker sc = new stringChecker(password);
                if (sc.isSecurePassword())
                {
                    if (!(_DataAccessService.CreateUser(sysID, password, accountType)))
                    {
                        throw new ArgumentException("System ID already exists");
                    }
                    return true;
                }
                else
                {
                    throw new ArgumentException("Invalid password");
                }
            }
            else
            {
                throw new ArgumentException("Invalid permissions");
            }
        }

        public IEnumerable<List<int>> BulkCreateUsers(List<User> users, bool passwordCheck)
        {
            List<int> passedIDs = new List<int>();
            List<int> failedIDs = new List<int>();
           
            return new List<List<int>> { passedIDs, failedIDs };
        }

        public bool SingleDeleteUser(int ID, string permission) 
        {
            if (_userManagementManager.CheckPermission(permission))
            {
                return _DataAccessService.DeleteUser(ID);
            }
            else
            {
                throw new ArgumentException("Invalid permissions");
            }
        }

        public bool[] BulkDeleteUsers(int[] listOfIDs, string permission)
        {
            if (_userManagementManager.CheckPermission(permission) && _userManagementManager.CheckListLength(listOfIDs))
            {
                bool[] b = new bool[listOfIDs.Length];
                int count = 0;
                foreach (int id in listOfIDs)
                {
                    bool temp = _DataAccessService.DeleteUser(id);
                    b[count] = temp;
                    count++;
                }
                return b;
            }
            else
            {
                throw new ArgumentException("Invalid permissions");
            }
            
        }
    }
}
