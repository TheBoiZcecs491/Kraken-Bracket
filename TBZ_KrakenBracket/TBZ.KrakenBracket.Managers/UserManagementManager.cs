﻿using System;
using System.Collections.Generic;
using TBZ.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.StringChecker;
using TBZ.UM_Service;
using TBZ.DatabaseQueryService;
using System.Linq.Expressions;
using TBZ.KrakenBracket.Managers;

namespace TBZ.UM_Manager
{
    public class UserManagementManager
    {
        private readonly LoggingManager _loggingManager = new LoggingManager();
        private readonly DataAccess _DataAccessService = new DataAccess();
        private readonly UserManagementService _userManagementService = new UserManagementService();

        /// <summary>
        /// Method to create one user
        /// </summary>
        /// 
        /// <param name="thisUser">
        /// User performing operation
        /// </param>
        /// 
        /// <param name="operatedUser">
        /// User to create
        /// </param>
        /// 
        /// <returns>
        /// True to indicate success or error if failed
        /// </returns>
        public User SingleCreateUsers(User invokingUser, User operatedUser)
        {
            // Check permissions for user performing operation
            bool permissionResult = _userManagementService.CheckPermission(invokingUser, operatedUser, "Create");
            if (permissionResult)
            {
                StringCheckerService emailChecker = new StringCheckerService(operatedUser.Email);
                StringCheckerService firstNameChecker = new StringCheckerService(operatedUser.FirstName);
                StringCheckerService lastNameChecker = new StringCheckerService(operatedUser.LastName);
                if (!firstNameChecker.isValidName() || !lastNameChecker.isValidName())
                    operatedUser.ErrorMessage = "Invalid names";
                else if (emailChecker.isValidEmail())
                {
                    if (_DataAccessService.GetUserByEmail(operatedUser.Email) != null)
                        operatedUser.ErrorMessage = "Email already registered";
                    else
                    {
                        _DataAccessService.CreateUser(operatedUser, true);
                        if ((_DataAccessService.GetUserByEmail(operatedUser.Email) == null) &&
                            (operatedUser.ErrorMessage == null))
                            operatedUser.ErrorMessage = "Email failed to register";
                    }
                }
                else operatedUser.ErrorMessage = "Email malformed";
            }
            else
            {
                operatedUser.ErrorMessage = "Invalid permissions";
            }
            if (!operatedUser.ErrorMessage.Equals(""))
            {
                _loggingManager.Log("User Creation", operatedUser.ErrorMessage);
            }
            else
            {
                _loggingManager.Log("User Creation", "");
            }
            return operatedUser;
        }

        /// <summary>
        /// Create multiple users at once
        /// </summary>
        /// 
        /// <param name="invokingUser">
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
        public List<List<User>> BulkCreateUsers(User invokingUser, List<User> users, bool passwordCheck)
        {
            bool sufficientLength = _userManagementService.CheckListLength(users);
            if (!sufficientLength)
            {
                throw new ArgumentException("Insufficient list length");
            }
            else
            {
                List<User> passedIDs = new List<User>();
                List<User> failedIDs = new List<User>();
                foreach (User operatedUser in users)
                {
                    // Check permissions for user performing operation
                    bool permissionCheck = _userManagementService.CheckPermission(invokingUser, operatedUser, "Create");
                    if (permissionCheck)
                    {
                        // Attempt to create user
                        bool creationResult = _DataAccessService.CreateUser(operatedUser, passwordCheck);
                        if (creationResult) passedIDs.Add(operatedUser);
                        else failedIDs.Add(operatedUser);
                    }
                    else failedIDs.Add(operatedUser);
                }
                _loggingManager.Log("User Bulk Creation", "");
                return new List<List<User>> { passedIDs, failedIDs };
            }

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
            bool permissionResult = _userManagementService.CheckPermission(thisUser, checkedUser, "Delete");
            if (permissionResult == true)
            {
                // Attempt to delete user
                bool temp = _DataAccessService.DeleteUser(checkedUser);
                if (temp == true)
                {
                    _loggingManager.Log("Delete User", "");
                    return true;
                }
                else throw new ArgumentException("Failed to delete user with associated ID");
            }
            else
            {
                _loggingManager.Log("User Deletion", "Invalid Permission Error");
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
            bool listBool = _userManagementService.CheckListLength(users);
            if (listBool == true)
            {
                List<User> passedIDs = new List<User>();
                List<User> failedIDs = new List<User>();
                foreach (User u in users)
                {
                    // Check permissions for user performing operation
                    bool permissionCheck = _userManagementService.CheckPermission(thisUser, u, "Delete");
                    if (permissionCheck == true)
                    {
                        // Attempt to delete user
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
                _loggingManager.Log("User Bulk Deletion", "");
                return new List<List<User>> { passedIDs, failedIDs };
            }
            else
            {
                _loggingManager.Log("User Bulk Deletion", "User Deletion Error");
                throw new ArgumentException("List length is insufficient");
            }
        }

        /// <summary>
        /// Method to update a single user
        /// </summary>
        /// 
        /// <param name="thisUser">
        /// User performing operation
        /// </param>
        /// 
        /// <param name="checkedUser">User to be Updated</param>
        /// <returns>
        /// True to indicate success or error if failed
        /// </returns> 
        /// 
        /// <param name="attrVal">The attribute to Update</param>
        /// <returns></returns>
        public bool SingleUpdateUser(User thisUser, User checkedUser, string attrName)
        {
            // Check permissions for user performing operation
            bool permissionResult = _userManagementService.CheckPermission(thisUser, checkedUser, "Update");
            if (permissionResult == true)
            {
                if (attrName.Equals("Password"))
                {
                    bool temp = _DataAccessService.UpdateUserPass(checkedUser, true);
                    if (temp == true) return true;
                    else throw new ArgumentException("Failed to Update user Password with associated ID");
                }
                else
                {
                    bool temp = _DataAccessService.UpdateUserAttr(checkedUser, attrName);
                    if (temp == true) return true;
                    else throw new ArgumentException("Failed to Update user with associated ID");
                }
            }
            else
            {
                throw new ArgumentException("Invalid permissions");
            }
        }

        /// <summary>
        /// Method to update multiple users at once
        /// </summary>
        /// 
        /// <param name="thisUser">
        /// User performing operation
        /// </param>
        /// 
        /// <param name="users">
        /// List of user objects to be update
        /// </param>
        /// 
        /// <returns>
        /// List of users that were sucessfully updated and list of users who failed to be updated
        /// </returns> 
        /// 
        /// <param name="attrVal">The attribute to Update for all users</param>
        /// <returns></returns>
        public List<List<User>> BulkUpdateUsers(User thisUser, List<User> users, string attrName)
        {
            bool listBool = _userManagementService.CheckListLength(users);
            if (listBool == true)
            {
                List<User> passedIDs = new List<User>();
                List<User> failedIDs = new List<User>();
                foreach (User u in users)
                {
                    // Check permissions for user performing operation
                    bool permissionCheck = _userManagementService.CheckPermission(thisUser, u, "Update");
                    if (permissionCheck == true)
                    {
                        bool temp = false;
                        if (attrName.Equals("Password"))
                        {
                            u.AccountStatus = false; //TODO: change this to an int or something,
                            /// we will need to have the statuses enabled, disabled, banned, and passChangeRequired.
                            /// our DB can handle this as it doesnt have a bool type, but we use tinyint(1) to fudge that.
                            /// intrestingly enough tinyint(1) can still store values from -128 to 127. so hence my clever idea.
                            temp = _DataAccessService.UpdateUserAttr(u, "AccountStatus");
                            /// actually no, this should NOT update the password.
                            /// instead it should disable the account,
                            /// and leave a message requireing a password change.
                        }
                        else
                        {
                            temp = _DataAccessService.UpdateUserAttr(u, attrName);
                        }
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
            else
            {
                throw new ArgumentException("List length is insufficient");
            }
        }

        public void updateGamerTag(User user, string newTag)
        {
            _DataAccessService.AssignGamerTag(user.SystemID, newTag);
        }

        public int getIDByEmail(string email)
        {
            User idExtract = _DataAccessService.GetUserByEmail(email);
            if(idExtract == null)
            {
                throw new ArgumentException("Failed to find user with associated ID");
            }
            return idExtract.SystemID;
        }
    }
}