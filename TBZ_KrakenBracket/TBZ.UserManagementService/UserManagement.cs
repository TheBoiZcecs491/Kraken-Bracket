using System;
using System.Linq;
using TBZ.DatabaseAccess;
using System.Collections.Generic;

namespace TBZ.UserManagementService
{
    public class UserManagement
    {
        private string randomPassword;
        private Random random = new Random();

        List<string> permissions = new List<string>(new string[] { "Admin", "System Admin" });

        public void CheckPermission(string permission)
        {
            if (!permissions.Contains(permission))
            {
                throw new ArgumentException("Invalid permissions");
            }
        }

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

        public void CheckListLength(int[] list)
        {
            if (list.Length < 1)
            {
                throw new ArgumentException("Length of list cannot be less than 1");
            }
        }

        public void SingleCreateUsers(int sysID, string firstName, string lastName, 
            string email, string password, string accountType, bool accountStatus, string permission)
        {
            // TODO: have a check for password. Use Kevin's registration checker
            CheckPermission(permission);
            var dataAccess = new DataAccess();
            if (permission == "Admin" && accountType == "User" || ((permission == "System Admin" && accountType != "System Admin")))
            {
                dataAccess.StoreUser(sysID, firstName, lastName, email, password, accountType, accountStatus);
            }
            else
            {
                throw new ArgumentException("Invaid permissions");
            }
        }

        public void BulkCreateUsers(int amountOfUsers, int amountOfAdmins, string permission)
        {
            CheckPermission(permission);
            CheckAmount(amountOfUsers, amountOfAdmins, permission);
            if (permission == "Admin")
            {
                var dataAccess = new DataAccess();
                for (int i = 0; i < amountOfUsers; i++)
                {
                    randomPassword = RandomPassword(14);

                    // The emails will be retrieved from a list later on
                    // For now, they will be stored as null
                    dataAccess.StoreUser(0, null, null, null, randomPassword, "User", true);
                }
            }

            else if (permission == "System Admin")
            {
                var dataAccess = new DataAccess();

                // Store regular users
                for (int i = 0; i < amountOfUsers; i++)
                {
                    randomPassword = RandomPassword(14);
                    // The emails will be retrieved from a list later on
                    // For now, they will be stored as null
                    dataAccess.StoreUser(0, null, null, null, randomPassword, "User", true);
                }

                // Store admins
                for (int i = 0; i < amountOfAdmins; i++)
                {
                    randomPassword = RandomPassword(14);
                    // The emails will be retrieved from a list later on
                    dataAccess.StoreUser(0, null, null, null, randomPassword, "Admin", true);
                }
            }
            
        }

        public bool SingleDeleteUser(int ID, string permission) 
        {
            CheckPermission(permission);
            var dataAccess = new DataAccess();
            return dataAccess.DeleteUser(ID, permission);
        }

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

        public bool SingleEnableUser(int ID, string permission)
        {
            CheckPermission(permission);
            var dataAccess = new DataAccess();
            return dataAccess.EnableUser(ID, permission);
        }
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

        public bool SingleDisableUser(int ID, string permission)
        {
            CheckPermission(permission);
            var dataAccess = new DataAccess();
            return dataAccess.DisableUser(ID, permission);
        }
        public bool[] BulkDisableUsers(int[] listOfIDs, string permission)
        {
            CheckPermission(permission);
            CheckListLength(listOfIDs);
            bool[] b = new bool[listOfIDs.Length];
            var dataAccess = new DataAccess();
            int count = 0;
            foreach (int id in listOfIDs)
            {
                bool temp = dataAccess.DisableUser(id, permission);
                b[count] = temp;
                count++;
            }
            return b;
        }

        public bool SingleUpdateUser(int sysID, string firstName, string lastName,
            string email, string password, string accountType, string component, string permission)
        {
            CheckPermission(permission);
            var dataAccess = new DataAccess();
            return dataAccess.UpdateUser(sysID, firstName, lastName, email, password, accountType, component, permission);
        }

        public string RandomPassword(int len)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, len)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
