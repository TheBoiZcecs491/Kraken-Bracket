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
            return Ok(_tournamentBracketManager.GetBracketStatusCode(bracketID));
        }
        
        [HttpGet("competitors/{bracketID}")]
        [Produces("application/json")]
        public IActionResult GetBracketByID(int bracketID)
        {
            return Ok(_tournamentBracketManager.GetBracketByID(bracketID));
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAllBrackets()
        {
            return Ok(_tournamentBracketManager.GetAllBrackets());
        }

        [HttpPost("{bracketID}/register/{gamer}")]
        [Produces("application/json")]
        public IActionResult RegisterGamerIntoBracket(int bracketID, Gamer gamer)
        {
            return Ok(_tournamentBracketManager.RegisterGamerIntoBracket(gamer, bracketID));
        }
        [HttpPost("login")]
        public IActionResult LoginUser(User user)
        {
            return Ok(_tournamentBracketManager.GetUser(user.Email, user.Password));
        }
    }
}