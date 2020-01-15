using System;
using System.Linq;
using TBZ.DatabaseAccess;

namespace TBZ.UserManagementService
{
    public class UserManagement
    {
        private int systemID = 0;
        string randomPassword;
        private Random random = new Random();
        public void CreateUsers(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
            }
            var dataAccess = new DataAccess();
            for (int i = 0; i < amount; i ++)
            {
                systemID++;
                randomPassword = RandomPassword(14);
                dataAccess.StoreUser(systemID, randomPassword);
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
