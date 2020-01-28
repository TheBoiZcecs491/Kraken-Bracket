using System;
using System.Collections.Generic;
using System.Text;

namespace TBZ.UserManagementService
{
    class UserManagementManager
    {
        List<string> permissions = new List<string>(new string[] { "Admin", "System Admin" });

        public bool CheckPermission(string permission)
        {
            if (permissions.Contains(permission))
            {
                return true;
            }
            return false;
        }
        public bool CheckAmount(int amountOfUsers, int amountOfAdmins, string permission)
        {
            if (amountOfUsers <= 0 && amountOfAdmins <= 0)
            {
                return false;
            }
            if (amountOfAdmins > 0 && permission == "Admin")
            {
                return false;
            }
            return true;
        }

        public bool CheckListLength(int[] list)
        {
            if (list.Length < 1)
            {
                return false;
            }
            return true;
        }
        public bool SingleCreateCheck(string permission, string accountType)
        {
            if (permission == "Admin" && accountType == "User" || ((permission == "System Admin" && accountType != "System Admin")))
            {
                return true;
            }
            return false;
        }
    }
}
