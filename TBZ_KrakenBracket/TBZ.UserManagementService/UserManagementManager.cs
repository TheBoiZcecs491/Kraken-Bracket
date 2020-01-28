using System;
using System.Collections.Generic;
using System.Text;

namespace TBZ.UserManagementService
{
    class UserManagementManager
    {
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
    }
}
