using System;
using System.Collections.Generic;
using System.Text;
namespace TBZ.UserManagementManager
{
    public class UserManagementManager
    {
        public bool CheckPermission(string permission)
        {
            if (permission == "System Admin" || permission == "Admin")
            {
                return true;
            }
            return false;
        }
        public bool CheckListLength(int[] list)
        {
            if (list.Length < 1)
            {
                return false;
            }
            return true;
        }
    }

}
