using System;
using System.Collections.Generic;
using System.Text;
using TBZ.DatabaseAccess;
namespace TBZ.UserManagementManager
{
    public class UserManagementManager
    {
        public bool CheckPermission(User ThisUser, User CheckedUser, string action)
        {
            /// <summary>
            /// Compares if the user is authorized to perform an action to the checked user.
            /// System admins have all access (Create, Update, and Delete) to admins
            /// and users, admins have access to users (but not other admins),
            /// and users have access to delete their own accounts.
            /// </summary>
            /// <param name="ThisUser"> To check if user is System Admin or Admin. </param>
            /// <param name="CheckedUser"> To apply the action on specified account. </param>
            /// <param name="action"> To check if ThisUser is authorized to perform action. </param>
            /// <returns> A bool to confirm authorization. </returns>

            bool permission = false;
            // System admin level permission
            if (ThisUser.AccountType == "System Admin" && CheckedUser.AccountType != "System Admin")
            {
                if (action == "Create" ||
                   action == "Delete" ||
                   action == "Update")
                {
                    permission = true;
                }
            }
            // Admin level permission
            else if ((ThisUser.AccountType == "Admin" && CheckedUser.AccountType != "System Admin") &&
                    (ThisUser.AccountType == "Admin" && CheckedUser.AccountType != "Admin"))
            {
                if (action == "Create" ||
                   action == "Delete" ||
                   action == "Update")
                {
                    permission = true;
                }
            }
            // User level permission
            else if (ThisUser.AccountType == "User")
            {
                if ((ThisUser.SystemID == CheckedUser.SystemID) && (action == "Delete"))
                {
                    permission = true;
                }
            }
            if (permission == false)
            {
                CheckedUser.ErrorMessage = "Unable to process user; insufficient permissions";
            }
            return permission;
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
