using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.RegistrationManager;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientApp.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : Controller
    {
        // PUT api/<controller>
        [HttpPut]
        public IActionResult registerNewUser(User user)
        {
            try
            {
                Registration registrationInstance = new Registration();
                var resultStat = Ok(registrationInstance.selfRegister(user));
                //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Snerp\Downloads\log.txt", true)){file.WriteLine(user.ErrorMessage);}
                //I used this when I still had that confusing problem that prevented the debugger from starting.
                //I fixed the problem but im keeping this here for reference.
                switch (user.ErrorMessage)
                {
                    case "Invalid permissions":
                        return StatusCode(StatusCodes.Status401Unauthorized);
                    case "Password is not secured":
                        return StatusCode(StatusCodes.Status406NotAcceptable);
                    case "ID already exists":
                        return StatusCode(StatusCodes.Status406NotAcceptable);
                    case "email already registered":
                        return StatusCode(StatusCodes.Status406NotAcceptable);
                    case "email malformed":
                        return StatusCode(StatusCodes.Status406NotAcceptable);
                    case "name fields blank":
                        return StatusCode(StatusCodes.Status406NotAcceptable);
                    case "email failed to register":
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    default:
                        return resultStat;
                }
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
