﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using TBZ.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.KrakenBracket.Services;
using TBZ.StringChecker;
using TBZ.UM_Manager;

namespace ClientApp.Controllers
{
    [Route("api/UserManagement")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly UserManagementManager _userManagementManager;
        private readonly AuthenticationService _authenticationService = new AuthenticationService();
        public UserManagementController(UserManagementManager userManagementManager)
        {
            _userManagementManager = userManagementManager;
        }
        [HttpPost("SingleCreate/{accountType}")]
        [Produces("application/json")]
        public IActionResult SingleCreateUser(User operatedUser, string accountType)
        {
            User invokingUser = new User(6, null, null, null, null, null, accountType, true, null);
            try
            {
                return Ok(_userManagementManager.SingleCreateUsers(invokingUser, operatedUser));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpPost("SingleDelete/{accountType}")]
        [Produces("application/json")]
        public IActionResult SingleDeleteUser(User operatedUser, string accountType)
        {
            User invokingUser = new User(0, null, null, null, null, null, accountType, true, null);
            try
            {
                return Ok(_userManagementManager.SingleDeleteUser(invokingUser, operatedUser));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut("SingleDelete/{accountType}")]
        [Produces("application/json")]
        public IActionResult SingleUpdateUser(User operatedUser, string accountType)
        {
            User invokingUser = new User(0, null, null, null, null, null, accountType, true, null);
            try
            {
                return Ok(_userManagementManager.SingleUpdateUser(invokingUser, operatedUser, "Password"));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        [Route("updateprofile")]
        public IActionResult UpdateUserProfile(ProfileUpdateInput userInput)
        {
            if (userInput == null || userInput == new ProfileUpdateInput(null,null,null,null,null,false))//this did not do its job.
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            bool passwordCheck = _authenticationService.ComparePasswords(userInput.Email, userInput.Password);
            if (!passwordCheck)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            User user = new User(userInput);//this holds the new password
            user.SystemID = _userManagementManager.getIDByEmail(userInput.Email);
            List<string> editedValues = new List<string>();
            if (userInput.NewPassword != null)
            {
                editedValues.Add("Password");
            }
            StringCheckerService firstNameChecker = new StringCheckerService(userInput.FirstName);
            StringCheckerService lastNameChecker = new StringCheckerService(userInput.LastName);
            if (!firstNameChecker.isValidName() || !lastNameChecker.isValidName())
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            editedValues.Add("FirstName");
            editedValues.Add("LastName");
            editedValues.Add("AccountStatus");
            try
            {
                foreach (string i in editedValues)
                {
                    _userManagementManager.SingleUpdateUser(doAsUser.systemAdmin(), user, i);
                }
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}