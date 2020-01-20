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

        public void CreateUsers(int amountOfUsers, int amountOfAdmins, string permission)
        {
            CheckPermission(permission);
            if (amountOfUsers <= 0 && amountOfAdmins <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
            }
            if (amountOfAdmins > 0 && permission == "Admin")
            {
                throw new ArgumentException("Admins cannot create other admins");
            }
            if (permission == "Admin")
            {
                var dataAccess = new DataAccess();
                for (int i = 0; i < amountOfUsers; i++)
                {
                    randomPassword = RandomPassword(14);

                    // The emails will be retrieved from a list later on
                    // For now, they will be stored as null
                    dataAccess.StoreUser(null, randomPassword, "User");
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
                    dataAccess.StoreUser(null, randomPassword, "User");
                }

                // Store admins
                for (int i = 0; i < amountOfAdmins; i++)
                {
                    randomPassword = RandomPassword(14);
                    // The emails will be retrieved from a list later on
                    dataAccess.StoreUser(null, randomPassword, "Admin");
                }
            }
            else
            {
                throw new ArgumentException("Invalid permissions");
            }
            
        }

        public bool[] DeleteUsers(int[] listOfIDs, string permission)
        {
            CheckPermission(permission);
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

        public bool[] EnableUsers(int[] listOfIDs, string permission)
        {
            CheckPermission(permission);
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
        public bool[] DisableUsers(int[] listOfIDs, string permission)
        {
            CheckPermission(permission);
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
        public string RandomPassword(int len)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, len)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
