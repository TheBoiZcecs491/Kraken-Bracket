using System;
using System.Linq;
using TBZ.DatabaseAccess;
using System.Collections.Generic;

namespace TBZ.UserManagementService
{
    public class UserManagement
    {
        string randomPassword;
        private Random random = new Random();

        List<string> permissions = new List<string>(new string[] { "Admin", "System Admin" });
        public void CreateUsers(int amount, string permission)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
            }
            if (permissions.Contains(permission) && permission == "Admin")
            {
                var dataAccess = new DataAccess();
                for (int i = 0; i < amount; i++)
                {
                    string randomPassword = RandomPassword(14);

                    // The emails will be retrieved from a list later on
                    dataAccess.StoreUser(null, randomPassword, "User");
                }
            }

            else if (permissions.Contains(permission) && permission == "System Admin")
            {
                var dataAccess = new DataAccess();
                int numberOfAdmins = Console.Read();
                for (int i = 0; i < amount - numberOfAdmins; i++)
                {
                    // The emails will be retrieved from a list later on
                    dataAccess.StoreUser(null, randomPassword, "User");
                }
                for (int i = 0; i < numberOfAdmins; i++)
                {
                    // The emails will be retrieved from a list later on
                    dataAccess.StoreUser(null, randomPassword, "Admin");
                }
            }
            else
            {
                throw new ArgumentException("Invalid permissions");
            }
            
        }

        public void DeleteUsers(int systemID)
        {
            var dataAccess = new DataAccess();
            bool temp = dataAccess.DeleteUser(systemID);
            if(temp == true)
            {
                Console.WriteLine("Successfully deleted");
            }
            else
            {
                throw new ArgumentException("System ID not found");
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
