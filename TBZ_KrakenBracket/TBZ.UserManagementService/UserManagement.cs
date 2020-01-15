using System;
using System.Linq;
using TBZ.DatabaseAccess;

namespace TBZ.UserManagementService
{
    public class UserManagement
    {
        private int systemID = 0;
        private Random random = new Random();
        public void CreateUsers(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
            }
            for (int i = 0; i < amount; i ++)
            {
                systemID++;
                // TODO: store user and their password
            }
        }

        public string RandomString(int len)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, len)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
