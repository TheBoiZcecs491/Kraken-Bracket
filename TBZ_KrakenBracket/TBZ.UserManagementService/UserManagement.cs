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
        private string randomPassword;
        private Random random = new Random();

        static UserManagement()
        {
            _DataAccessService = new DataAccess();
            _userManagementManager = new UserManagementManager.UserManagementManager();
            
        }

        public bool SingleCreateUsers(int sysID, string password, string permission)
        {
            // TODO: have a check for password. Use Kevin's registration checker
            if (_userManagementManager.CheckPermission(permission))
            {
                stringChecker sc = new stringChecker(password);
                if (sc.isSecurePassword())
                {
                    return _DataAccessService.CreateUser(sysID, password);
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

        public bool[] BulkCreateUsers(int[] listOfIDs, string[] listOfPasswords, bool passwordCheck)
        {
            if (_userManagementManager.CheckListLength(listOfIDs))
            {
                bool[] results = new bool[listOfIDs.Length];
                int i = 0;

                // Password check is enabled
                if (passwordCheck)
                {
                    foreach (int ID in listOfIDs)
                    {
                        stringChecker sc = new stringChecker(listOfPasswords[i]);

                        // Checking if password meets requirements
                        if (sc.isSecurePassword())
                        {
                            bool temp = _DataAccessService.CreateUser(listOfIDs[i], listOfPasswords[i]);
                            results[i] = temp;
                        }
                        else
                        {
                            results[i] = false;
                        }
                    }
                    return results;
                }

                // Password check is disabled
                else
                {
                    foreach (int ID in listOfIDs)
                    {
                        bool temp = _DataAccessService.CreateUser(listOfIDs[i], listOfPasswords[i]);
                        results[i] = temp;
                    }
                    return results;
                }
            }
            else
            {
                throw new ArgumentException("Insufficient list length");
            }
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

   
        public string RandomPassword(int len)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, len)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
