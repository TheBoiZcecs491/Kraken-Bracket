using System;
using System.Linq;
using TBZ.DatabaseAccess;
using System.Collections.Generic;

namespace TBZ.UserManagementService
{
    public class UserManagement
    {
        private static readonly DataAccess _DataAccessService;
        private static readonly UserManagementManager _userManagementManager;
        private string randomPassword;
        private Random random = new Random();

        static UserManagement()
        {
            _DataAccessService = new DataAccess();
            _userManagementManager = new UserManagementManager();
        }

        public void SingleCreateUsers(int sysID, string firstName, string lastName, 
            string email, string password, string accountType, bool accountStatus, string permission)
        {
            // TODO: have a check for password. Use Kevin's registration checker
            if (_userManagementManager.CheckPermission(permission))
            {
                if (_userManagementManager.SingleCreateCheck(permission, accountType))
                {
                    _DataAccessService.StoreUser(sysID, firstName, lastName, email, password, accountType, accountStatus);
                }
                else
                {
                    throw new ArgumentException("Invalid permissions");
                }
            }
            else
            {
                throw new ArgumentException("Invalid permissions");
            }
        }

        public void BulkCreateUsers(int amountOfUsers, int amountOfAdmins, string permission)
        {
            if (_userManagementManager.CheckPermission(permission) && 
                _userManagementManager.CheckAmount(amountOfUsers, amountOfAdmins, permission))
            {
                if (permission == "Admin")
                {
                    for (int i = 0; i < amountOfUsers; i++)
                    {
                        randomPassword = RandomPassword(14);

                        // The emails will be retrieved from a list later on
                        // For now, they will be stored as null
                        _DataAccessService.StoreUser(0, null, null, null, randomPassword, "User", true);
                    }
                }

                else if (permission == "System Admin")
                {
                    // Store regular users
                    for (int i = 0; i < amountOfUsers; i++)
                    {
                        randomPassword = RandomPassword(14);
                        // The emails will be retrieved from a list later on
                        // For now, they will be stored as null
                        _DataAccessService.StoreUser(0, null, null, null, randomPassword, "User", true);
                    }

                    // Store admins
                    for (int i = 0; i < amountOfAdmins; i++)
                    {
                        randomPassword = RandomPassword(14);
                        // The emails will be retrieved from a list later on
                        _DataAccessService.StoreUser(0, null, null, null, randomPassword, "Admin", true);
                    }
                }
            }
            else
            {
                throw new ArgumentException("Invalid permissions");
            }
        }

        public bool SingleDeleteUser(int ID, string permission) 
        {
            if (_userManagementManager.CheckPermission(permission))
            {
                return _DataAccessService.DeleteUser(ID, permission);
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
                    bool temp = _DataAccessService.DeleteUser(id, permission);
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

        public bool SingleEnableUser(int ID, string permission)
        {
            if (_userManagementManager.CheckPermission(permission))
            {
                return _DataAccessService.EnableUser(ID, permission);
            }
            else
            {
                throw new ArgumentException("Invalid permissions");
            }
        }
        public bool[] BulkEnableUsers(int[] listOfIDs, string permission)
        {
            if (_userManagementManager.CheckPermission(permission) && _userManagementManager.CheckListLength(listOfIDs))
            {
                bool[] b = new bool[listOfIDs.Length];
                int count = 0;
                foreach (int id in listOfIDs)
                {
                    bool temp = _DataAccessService.EnableUser(id, permission);
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

        public bool SingleDisableUser(int ID, string permission)
        {
            if (_userManagementManager.CheckPermission(permission))
            {
                return _DataAccessService.DisableUser(ID, permission);
            }
            else
            {
                throw new ArgumentException("Invalid permissions");
            }
        }
        public bool[] BulkDisableUsers(int[] listOfIDs, string permission)
        {
            if (_userManagementManager.CheckPermission(permission) && _userManagementManager.CheckListLength(listOfIDs))
            {
                bool[] b = new bool[listOfIDs.Length];
                int count = 0;
                foreach (int id in listOfIDs)
                {
                    bool temp = _DataAccessService.DisableUser(id, permission);
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

        public bool SingleUpdateUserFirstName(int sysID, string firstName, string permission)
        {
            if (_userManagementManager.CheckPermission(permission))
            {
                return _DataAccessService.UpdateFirstName(sysID, firstName);
            }
            else
            {
                throw new ArgumentException("Invalid permissions");
            }
        }

        public bool SingleUpdateUserLastName(int sysID, string lastName, string permission)
        {
            if (_userManagementManager.CheckPermission(permission))
            {
                return _DataAccessService.UpdateLastName(sysID, lastName);
            }
            else
            {
                throw new ArgumentException("Invalid permissions");
            }
        }

        public bool SingleUpdateUserEmail(int sysID, string email, string permission)
        {
            if (_userManagementManager.CheckPermission(permission))
            {
                return _DataAccessService.UpdateEmail(sysID, email);
            }
            else
            {
                throw new ArgumentException("Invalid permissions");
            }
        }

        public bool SingleUpdateUserPassword(int sysID, string password, string permission)
        {
            if (_userManagementManager.CheckPermission(permission))
            {
                return _DataAccessService.UpdatePassword(sysID, password);
            }
            else
            {
                throw new ArgumentException("Invalid permissions");
            }
        }
        public bool SingleUpdateUserAccountType(int sysID, string accountType, string permission)
        {
            if (_userManagementManager.CheckPermission(permission))
            {
                return _DataAccessService.UpdateAccountType(sysID, accountType, permission);
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
