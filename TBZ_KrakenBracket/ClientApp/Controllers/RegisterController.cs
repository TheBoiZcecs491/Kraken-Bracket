using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.UM_Manager;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientApp.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly UserManagementManager _userManagementManager = new UserManagementManager();
        // PUT api/<controller>
        [HttpPut]
        public IActionResult registerNewUser(RegistrationInput userInput)
        {
            try
            {
                
                User gateAdmin = new User(0, null, null, null, null, null, "System Admin", false, null);
                User user = new User(userInput);
                // HACK: this is fixed in another branch, so for now this will HOPEFULLY
                // keep away any possible collisions. when that happend comment out the next 2 lines.
                Random rnd = new Random();
                user.SystemID = rnd.Next(Int32.MinValue, Int32.MaxValue);

                var resultStat = Ok(_userManagementManager.SingleCreateUsers(gateAdmin, user));
                ContentResult serverReply = Content(user.ErrorMessage);

                switch (user.ErrorMessage)
                {
                    case "Invalid permissions":
                        serverReply.StatusCode = StatusCodes.Status401Unauthorized; break;
                    case "Password is not secured":
                        serverReply.StatusCode = StatusCodes.Status406NotAcceptable; break;
                    case "ID already exists":
                        serverReply.StatusCode = StatusCodes.Status406NotAcceptable; break;
                    case "email already registered":
                        serverReply.StatusCode = StatusCodes.Status406NotAcceptable; break;
                    case "email malformed":
                        serverReply.StatusCode = StatusCodes.Status406NotAcceptable; break;
                    case "name fields blank":
                        serverReply.StatusCode = StatusCodes.Status406NotAcceptable; break;
                    case "email failed to register":
                        serverReply.StatusCode = StatusCodes.Status500InternalServerError; break;
                    default:

                        serverReply.StatusCode = StatusCodes.Status200OK;
                        break;
                }
                return serverReply;
                //401: account wasnt made because they didnt have permission to
                //406: the registration info was not done correctly.
                //500: backend machine [B]roke
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }
        }
    }
}
