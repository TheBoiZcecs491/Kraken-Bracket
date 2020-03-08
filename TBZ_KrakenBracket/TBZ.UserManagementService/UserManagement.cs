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

        List<string> permissions = new List<string>(new string[] { "Admin", "System Admin" });

        /// <summary>
        /// Method to check account permission
        /// </summary>
        /// <param name="permission">Permisison associated with account</param>
        public void CheckPermission(string permission)
        {
            if (!permissions.Contains(permission))
            {
                throw new ArgumentException("Invalid permissions");
            }
        }

        /// <summary>
        /// Method used with bulk storing users. Checks the amount of Users and Admins the 
        /// current account wishes to store
        /// </summary>
        /// <param name="amountOfUsers">Amount of users</param>
        /// <param name="amountOfAdmins">Amount of admins</param>
        /// <param name="permission">
        /// Permisison associated with
        /// the account that is performing the operation
        /// </param>
        public void CheckAmount(int amountOfUsers, int amountOfAdmins, string permission)
        {
            if (amountOfUsers <= 0 && amountOfAdmins <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
            }
            if (amountOfAdmins > 0 && permission == "Admin")
            {
                throw new ArgumentException("Admins cannot create other admins");
            }
        }

        /// <summary>
        /// Method for checking if list length is valid
        /// </summary>
        /// <param name="list"></param>
        public void CheckListLength(int[] list)
        {
            _DataAccessService = new DataAccess();
            _userManagementManager = new UserManagementManager.UserManagementManager();
            
        }

        /// <summary>
        /// Method for creating a single user
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="accountType"></param>
        /// <param name="accountStatus"></param>
        /// <param name="permission">
        /// Permisison associated with
        /// the account that is creating the user
        /// </param>
        public void SingleCreateUsers(string firstName, string lastName,
            string email, string password, string accountType, bool accountStatus, string permission)
        {
            // TODO: have a check for password. Use Kevin's registration checker
            CheckPermission(permission);
            var dataAccess = new DataAccess();
            if (permission == "Admin" && accountType == "User" || ((permission == "System Admin" && accountType != "System Admin")))
            {
                dataAccess.StoreUser(firstName, lastName, email, password, accountType, accountStatus);
            }
            else
            {
                throw new ArgumentException("Invaid permissions");
            }
        }

        /// <summary>
        /// Method for bulk creating users
        /// </summary>
        /// <param name="amountOfUsers">Amount of regular Users</param>
        /// <param name="amountOfAdmins">Amount of Admins</param>
        /// <param name="permission">Permisison associated with
        /// the account that is creating the user</param>
        public void BulkCreateUsers(int amountOfUsers, int amountOfAdmins, string permission)
        {
            List<User> passedIDs = new List<User>();
            List<User> failedIDs = new List<User>();
            foreach(User u in users)
            {
                bool temp = _DataAccessService.CreateUser(u, passwordCheck);
                if (temp == true)
                {
                    randomPassword = RandomPassword(14);

                    // The emails will be retrieved from a list later on
                    // For now, they will be stored as null
                    dataAccess.StoreUser(null, null, null, randomPassword, "User", true);
                }
                else
                {
                    randomPassword = RandomPassword(14);
                    // The emails will be retrieved from a list later on
                    // For now, they will be stored as null
                    dataAccess.StoreUser(null, null, null, randomPassword, "User", true);
                }

                // Store admins
                for (int i = 0; i < amountOfAdmins; i++)
                {
                    randomPassword = RandomPassword(14);
                    // The emails will be retrieved from a list later on
                    dataAccess.StoreUser(null, null, null, randomPassword, "Admin", true);
                }
            }

        }

        /// <summary>
        /// Method for deleting a single user
        /// </summary>
        /// <param name="ID">ID of account to be deleted</param>
        /// <param name="permission">
        /// Permisison associated with
        /// the account that is creating the user
        /// </param>
        /// <returns>
        /// True if account is deleted.
        /// False if account is not deleted
        /// </returns>
        public bool SingleDeleteUser(int ID, string permission)
        {
            CheckPermission(permission);
            var dataAccess = new DataAccess();
            return dataAccess.DeleteUser(ID, permission);
        }

        /// <summary>
        /// Method for bulk deleting users
        /// </summary>
        /// <param name="listOfIDs"></param>
        /// <param name="permission">
        /// Permisison associated with
        /// the account that is performing the operation</param>
        /// <returns>
        /// List of booleans for each account. True if operation succeded; false if not
        /// </returns>
        public bool[] BulkDeleteUsers(int[] listOfIDs, string permission)
        {
            CheckPermission(permission);
            CheckListLength(listOfIDs);
            bool[] b = new bool[listOfIDs.Length];
            var dataAccess = new DataAccess();
            int count = 0;
            foreach (int id in listOfIDs)
            {
                bool temp = dataAccess.DeleteUser(id, permission);
                b[count] = temp;
                count++;
            }
            return b;
        }

        /// <summary>
        /// Method for enabling a single user
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="permission">
        /// Permisison associated with
        /// the account that is performing the operation
        /// </param>
        /// <returns>True if operation is successful; false if not</returns>
        public bool SingleEnableUser(int ID, string permission)
        {
            CheckPermission(permission);
            var dataAccess = new DataAccess();
            return dataAccess.EnableUser(ID, permission);
        }

        /// <summary>
        /// Method for bulk enabling users
        /// </summary>
        /// <param name="listOfIDs">List of system ID's of users to be enabled</param>
        /// <param name="permission">Permission associated with account performing operation</param>
        /// <returns></returns>
        public bool[] BulkEnableUsers(int[] listOfIDs, string permission)
        {
            CheckPermission(permission);
            CheckListLength(listOfIDs);
            bool[] b = new bool[listOfIDs.Length];
            var dataAccess = new DataAccess();
            int count = 0;
            foreach (int id in listOfIDs)
            {
                bool temp = dataAccess.EnableUser(id, permission);
                b[count] = temp;
                count++;
            }
            return b;
        }

        /// <summary>
        /// Method for disabling a single user
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="permission"></param>
        /// <returns>True if operation is successful; false if not</returns>
        public bool SingleDisableUser(int ID, string permission)
        {
            CheckPermission(permission);
            var dataAccess = new DataAccess();
            return dataAccess.DisableUser(ID, permission);
        }
        /// <summary>
        /// Method for bulk disabling users
        /// </summary>
        /// <param name="listOfIDs"></param>
        /// <param name="permission"></param>
        /// <returns>List of booleans for each account. True if operation succeded; false if not</returns>
        public bool[] BulkDisableUsers(int[] listOfIDs, string permission)
        {
            List<User> passedIDs = new List<User>();
            List<User> failedIDs = new List<User>();
            foreach (User u in users)
            {
                bool temp = _DataAccessService.DeleteUser(u);
                if (temp == true)
                {
                    passedIDs.Add(u);
                }
                else
                {
                    failedIDs.Add(u);
                }
            }
            return b;
        }

        /// <summary>
        /// Method for updating a single user
        /// </summary>
        /// <param name="sysID"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="accountType"></param>
        /// <param name="component">Attribute of user that is to be changed</param>
        /// <param name="permission">
        /// Permisison associated with
        /// the account that is performing the operation
        /// </param>
        /// <returns>True if operation is successful; false if not</returns>
        public bool SingleUpdateUser(int sysID, string firstName, string lastName,
            string email, string password, string accountType, string component, string permission)
        {
            CheckPermission(permission);
            var dataAccess = new DataAccess();
            return dataAccess.UpdateUser(sysID, firstName, lastName, email, password, accountType, component, permission);
        }

        /// <summary>
        /// Random password generator
        /// </summary>
        /// <param name="len">Length of random password</param>
        /// <returns>Random password</returns>
        public string RandomPassword(int len)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, len)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}