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
        /// <param name="user">
        /// User to create
        /// </param>
        /// 
        /// <returns>
        /// True to indicate success or error if failed
        /// </returns>
        public bool SingleCreateUsers(User user)
        {
            bool temp = _DataAccessService.CreateUser(user, true);
            if (temp == true) return true;
            else throw new ArgumentException("Failed to create user with associated ID");
        }

        /// <summary>
        /// Method to create multiple users at once
        /// </summary>
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
        public List<List<User>> BulkCreateUsers(List<User> users, bool passwordCheck)
        {
            List<User> passedIDs = new List<User>();
            List<User> failedIDs = new List<User>();
            foreach (User u in users)
            {
                bool temp = _DataAccessService.CreateUser(u, passwordCheck);
                if (temp == true)
                {
                    passedIDs.Add(u);
                }
                else
                {
                    failedIDs.Add(u);
                }
            }
            return new List<List<User>> { passedIDs, failedIDs };
        }

        /// <summary>
        /// Method to delete a single user
        /// </summary>
        /// <param name="user">User to be deleted</param>
        /// <returns>
        /// True to indicate success or error if failed
        /// </returns>
        public bool SingleDeleteUser(User user)
        {
            bool temp = _DataAccessService.DeleteUser(user);
            if (temp == true) return true;
            else throw new ArgumentException("Failed to delete user with associated ID");
        }

        /// <summary>
        /// Method to delete multiple users at once
        /// </summary>
        /// 
        /// <param name="users">
        /// List of user objects to be deleted
        /// </param>
        /// 
        /// <returns>
        /// List of users that were sucessfully created and list of users who failed to be created
        /// </returns>
        public List<List<User>> BulkDeleteUsers(List<User> users)
        {
            List<User> passedIDs = new List<User>();
            List<User> failedIDs = new List<User>();
            foreach (User u in users)
            {
                bool temp = _DataAccessService.DeleteUser(u);
                if (temp == true)
                {
                    passedIDs.Add(u);
                }
                else
                {
                    failedIDs.Add(u);
                }
            }
            return new List<List<User>> { passedIDs, failedIDs };
        }
    }
}