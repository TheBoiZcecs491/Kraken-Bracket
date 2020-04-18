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
        public IActionResult GetBracketNumberOfCompetitors(int bracketID)
        {
            var result = _tournamentBracketManager.GetBracketStatusCode(bracketID);
            if (result == 0)
            {
                return Ok(_tournamentBracketManager.GetNumberOfCompetitors(bracketID));
            }
            else
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
        }

        [HttpGet("{bracketID}")]
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

    }
}