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
    [Route("api/brackets")]
    [ApiController]
    public class TournamentBracketController : ControllerBase
    {
        private readonly TournamentBracketManager _tournamentBracketManager;
        public TournamentBracketController(TournamentBracketManager tournamentBracketManager)
        {
            _tournamentBracketManager = tournamentBracketManager;
        }

        [Produces("application/json")]
        public IActionResult GetBracketStatusCode(int bracketID)
        {
            try
            {
                return Ok(_tournamentBracketManager.GetBracketStatusCode(bracketID));
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpGet("competitors/{bracketID}")]
        [Produces("application/json")]
        public IActionResult GetBracketByID(int bracketID)
        {
            try
            {
                return Ok(_tournamentBracketManager.GetBracketByID(bracketID));
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAllBrackets()
        {
            try
            {
                return Ok(_tournamentBracketManager.GetAllBrackets());
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{bracketID}/register/{gamer}")]
        [Produces("application/json")]
        public IActionResult RegisterGamerIntoBracket(int bracketID, Gamer gamer)
        {
            try
            {
                return Ok(_tournamentBracketManager.RegisterGamerIntoBracket(gamer, bracketID));
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("login")]
        public IActionResult LoginUser(User user)
        {
            try
            {
                return Ok(_tournamentBracketManager.GetUser(user.Email, user.Password));
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("createBracket/{bracketInfo}")]
        [Produces("application/json")]
        public IActionResult CreateBracket(BracketInfo bracketInfo)
        {
            try
            {
                return Ok(_tournamentBracketManager.ValidCreateBracket(bracketInfo));
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}