using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.KrakenBracket.Managers;
using TBZ.UM_Manager;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientApp.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly UserManagementManager _userManagementManager = new UserManagementManager();
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider(); //for the salt
        // PUT api/<controller>
        [HttpPut]
        public IActionResult Login(User user)
        {
            //temp system admin to make new users
            User gateAdmin = new User(0, null, null, null, null, null, "System Admin", false, null);
            user.AccountStatus = true;
            user.AccountType = "User";
            byte[] randomNumber = new byte[16];
            rngCsp.GetBytes(randomNumber);
            user.Salt = System.Text.Encoding.Default.GetString(randomNumber);
            //HACK: this is fixed in another branch, so for now this will HOPEFULLY
            // keep away any possible collisions. when that happend comment out the next 2 lines.
            Random rnd = new Random();
            user.SystemID = rnd.Next(Int32.MinValue,Int32.MaxValue);
            try
            {
                var resultStat = Ok(_userManagementManager.SingleCreateUsers(gateAdmin, user));
                //using (System.IO.StreamWriter file =
                //    new System.IO.StreamWriter(@"C:\Users\Snerp\Downloads\shit.txt", true))
                //{
                //    file.WriteLine(user.Email);
                //    file.WriteLine(user.Password);
                //    file.WriteLine(user.FirstName);
                //    file.WriteLine(user.LastName);
                //    file.WriteLine(user.ErrorMessage);
                //}
                if(user.ErrorMessage != "")
                {
                    //print the error to their screen.
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                else
                {
                    return resultStat;
                    //also log the user in, why not?
                    //var resultDis = Ok(_authenticationManager.Login(user.Email, user.Password));
                    //return resultDis;
                }
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }
}
