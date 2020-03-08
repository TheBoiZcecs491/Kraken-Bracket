using System;
using System.Linq;
using TBZ.DatabaseAccess;
using System.Collections.Generic;
using TBZ.UserManagementManager;
using TBZ.StringChecker;

namespace TBZ.UserManagementService
{
    public class UserManagement
    {
        private static readonly DataAccess _DataAccessService;
        private static readonly UserManagementManager.UserManagementManager _userManagementManager;

        static UserManagement()
        {
            _DataAccessService = new DataAccess();
            _userManagementManager = new UserManagementManager.UserManagementManager();

        }

        /// <summary>
        /// Method to create one user
        /// </summary>
        /// 
        /// <param name="thisUser">
        /// User performing operation
        /// </param>
        /// 
        /// <param name="checkedUser">
        /// User to create
        /// </param>
        /// 
        /// <returns>
        /// True to indicate success or error if failed
        /// </returns>
        public bool SingleCreateUsers(User thisUser, User checkedUser)
        {

            // Check permissions for user performing operation
            bool permissionResult = _userManagementManager.CheckPermission(thisUser, checkedUser, "Create");
            if (permissionResult == true)
            {
                // Attempt to create user
                bool temp = _DataAccessService.CreateUser(checkedUser, true);
                if (temp == true) return true;
                else throw new ArgumentException("Failed to create user with associated ID");
            }
            else
            {
                throw new ArgumentException("Invalid permissions");
            }
        }

        /// <summary>
        /// Method to create multiple users at once
        /// </summary>
        /// 
        /// <param name="thisUser">
        /// User performing operation
        /// </param>
        /// 
        /// <param name="users">
        /// List of user objects to be created
        /// </param>
        /// 
        /// <param name="passwordCheck">
        /// Option to use password check for each user object
        /// </param>
        /// 
        /// <returns>
        /// List of users that were sucessfully created and list of users who failed to be created
        /// </returns>
        public List<List<User>> BulkCreateUsers(User thisUser, List<User> users, bool passwordCheck)
        {
            List<User> passedIDs = new List<User>();
            List<User> failedIDs = new List<User>();
            foreach (User u in users)
            {
                // Check permissions for user performing operation
                bool permissionCheck = _userManagementManager.CheckPermission(thisUser, u, "Create");
                if (permissionCheck == true)
                {
                    // Attempt to create user
                    bool temp = _DataAccessService.CreateUser(u, passwordCheck);
                    if (temp == true)
                    {
                        // Creation successful; store user in passed ID's
                        passedIDs.Add(u);
                    }
                    else
                    {
                        // Deletion failed; store user in failed ID's
                        failedIDs.Add(u);
                    }
                }
                else
                {
                    // Permission check failed; store user in failed ID's
                    failedIDs.Add(u);
                }
            }
            return new List<List<User>> { passedIDs, failedIDs };
        }

        /// <summary>
        /// Method to delete a single user
        /// </summary>
        /// 
        /// <param name="thisUser">
        /// User performing operation
        /// </param>
        /// 
        /// <param name="checkedUser">User to be deleted</param>
        /// <returns>
        /// True to indicate success or error if failed
        /// </returns>
        public bool SingleDeleteUser(User thisUser, User checkedUser)
        {
            // Check permissions for user performing operation
            bool permissionResult = _userManagementManager.CheckPermission(thisUser, checkedUser, "Delete");
            if (permissionResult == true)
            {
                bool temp = _DataAccessService.DeleteUser(checkedUser);
                if (temp == true) return true;
                else throw new ArgumentException("Failed to delete user with associated ID");
            }
            else
            {
                throw new ArgumentException("Invalid permissions");
            }
        }

        /// <summary>
        /// Method to delete multiple users at once
        /// </summary>
        /// 
        /// <param name="thisUser">
        /// User performing operation
        /// </param>
        /// 
        /// <param name="users">
        /// List of user objects to be deleted
        /// </param>
        /// 
        /// <returns>
        /// List of users that were sucessfully created and list of users who failed to be created
        /// </returns>
        public List<List<User>> BulkDeleteUsers(User thisUser, List<User> users)
        {
            List<User> passedIDs = new List<User>();
            List<User> failedIDs = new List<User>();
            foreach (User u in users)
            {
                // Check permissions for user performing operation
                bool permissionCheck = _userManagementManager.CheckPermission(thisUser, u, "Delete");
                if (permissionCheck == true)
                {
                    bool temp = _DataAccessService.DeleteUser(u);
                    if (temp == true)
                    {
                        // Deletion successful; store user in passed ID's
                        passedIDs.Add(u);
                    }
                    else
                    {
                        // Deletion failed; store user in failed ID's
                        failedIDs.Add(u);
                    }
                }
                else
                {
                    // Permission check failed; store user in failed ID's
                    failedIDs.Add(u);
                }
            }
            return new List<List<User>> { passedIDs, failedIDs };
        }
    }
}