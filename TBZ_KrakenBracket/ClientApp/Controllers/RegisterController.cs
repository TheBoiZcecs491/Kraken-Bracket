using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBZ.KrakenBracket.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.UM_Manager;

namespace ClientApp.Controllers
{
    
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly UserManagementManager _userManagementManager = new UserManagementManager();
        private readonly GamerDataAccess _gamerDataAccess = new GamerDataAccess();
        // PUT api/<controller>
        [Route("api/register")]
        [HttpPost]
        public IActionResult RegisterNewUser(RegistrationInput userInput)
        {
            try
            {
                
                User user = new User(userInput);
                // HACK: this is fixed in another branch, so for now this will HOPEFULLY
                // keep away any possible collisions. when that happend comment out the next 2 lines.
                Random rnd = new Random();
                user.SystemID = rnd.Next(Int32.MinValue, Int32.MaxValue);

                //HACK: due to time constraints, I realised that gamer tags need to be unique.
                GamerInfo verifyGamer = _gamerDataAccess.GetGamerInfo(new GamerInfo(null,userInput.GamerTag,0,0));
                if (verifyGamer == null)
                {
                    _userManagementManager.SingleCreateUsers(doAsUser.systemAdmin(), user);
                    _userManagementManager.updateGamerTag(user, userInput.GamerTag);
                } else user.ErrorMessage="Gamer tag is already in use";
                ContentResult serverReply = Content(user.ErrorMessage);

                switch (user.ErrorMessage)
                {
                    case "Invalid permissions":
                        serverReply.StatusCode = StatusCodes.Status401Unauthorized; break;
                    case "Password is not secured":
                    case "ID already exists":
                    case "Email already registered":
                    case "Email malformed":
                    case "Invalid names":
                    case "Gamer tag is already in use":
                        serverReply.StatusCode = StatusCodes.Status400BadRequest; break;
                    case "Email failed to register":
                        serverReply.StatusCode = StatusCodes.Status500InternalServerError; break;
                    default:
                        serverReply.StatusCode = StatusCodes.Status200OK;
                        break;
                }
                return serverReply;
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }
}
