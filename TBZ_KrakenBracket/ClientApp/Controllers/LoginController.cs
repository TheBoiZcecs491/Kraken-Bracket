using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.KrakenBracket.Managers;

namespace ClientApp.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AuthenticationManager _authenticationManager = new AuthenticationManager();
        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                var resultDis = Ok(_authenticationManager.Login(user.Email, user.Password));
                return resultDis;
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
    }
}