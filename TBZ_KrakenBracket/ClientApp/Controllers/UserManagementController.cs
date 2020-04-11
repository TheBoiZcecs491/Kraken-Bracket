using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBZ.DatabaseAccess;
using TBZ.UM_Manager;

namespace ClientApp.Controllers
{
    [Route("UserManagement/")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly UserManagementManager _userManagementManager;
        public UserManagementController(UserManagementManager userManagementManager)
        {
            _userManagementManager = userManagementManager;
        }
        [HttpPost]
        [Route("SingleCreate")]
        public ActionResult<User> SingleCreateUser(User operatedUser)
        {
            User invokingUser = new User(6, null, null, null, "84092ujIO@>>>", null, "System Admin", false, null);
            var result = _userManagementManager.SingleCreateUsers(invokingUser, operatedUser);
            return result;
        }
    }
}