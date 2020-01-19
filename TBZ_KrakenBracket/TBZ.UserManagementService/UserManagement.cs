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

        public void CreateUsers(int amount, string permission)
        {
            CheckPermission(permission);
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
            }
            if (permission == "Admin")
            {
                var dataAccess = new DataAccess();
                for (int i = 0; i < amount; i++)
                {
                    randomPassword = RandomPassword(14);

                    // The emails will be retrieved from a list later on
                    dataAccess.StoreUser(null, randomPassword, "User");
                }
            }

            else if (permission == "System Admin")
            {
                var dataAccess = new DataAccess();

                // FIXME: Need to find another way to specify the number of admins
                int numberOfAdmins = 1;

                // Store regular users
                for (int i = 0; i < amount - numberOfAdmins; i++)
                {
                    randomPassword = RandomPassword(14);
                    // The emails will be retrieved from a list later on
                    dataAccess.StoreUser(null, randomPassword, "User");
                }

                // Store admins
                for (int i = 0; i < numberOfAdmins; i++)
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

        public void DeleteUsers(int[] listOfIDs, string permission)
        {
            CheckPermission(permission);
            var dataAccess = new DataAccess();
            foreach (int id in listOfIDs)
            {
                bool temp = dataAccess.DeleteUser(id, permission);
            }
        }

        public void EnableUsers(int[] listOfIDs, string permission)
        {
            CheckPermission(permission);
            var dataAccess = new DataAccess();
            foreach (int id in listOfIDs)
            {
                bool temp = dataAccess.EnableUser(id, permission);
            }
        }
        public void DisableUsers(int[] listOfIDs, string permission)
        {
            CheckPermission(permission);
            var dataAccess = new DataAccess();
            foreach (int id in listOfIDs)
            {
                bool temp = dataAccess.DisableUser(id, permission);
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
