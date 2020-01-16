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
                    dataAccess.StoreUser("foo@gmail.com", randomPassword, "User");
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
