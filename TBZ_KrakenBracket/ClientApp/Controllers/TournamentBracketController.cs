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
        private TournamentBracketManager TournamentBracketManager { get; } 

        public TournamentBracketController(TournamentBracketManager tournamentBracketManager)
        {
            TournamentBracketManager = tournamentBracketManager;
        }

        [HttpGet("{bracketID}")]
        [Produces("application/json")]
        public IActionResult GetBracketByID(int bracketID)
        {

            try
            {
                return Ok(TournamentBracketManager.GetBracketByID(bracketID));
            }
            catch (ArgumentException)
            {
                // Bracket was not found
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch
            {
                // Generic error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAllBrackets()
        {
            try
            {
                return Ok(TournamentBracketManager.GetAllBrackets());
            }
            catch (ArgumentException)
            {
                // Brackets not found
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch
            {
                // Generic error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{bracketID}/register/{gamer}")]
        [Produces("application/json")]
        public IActionResult RegisterGamerIntoBracket(int bracketID, GamerInfo gamer)
        {
            try
            {
                return Ok(TournamentBracketManager.RegisterGamerIntoBracket(gamer, bracketID));
            }
            catch (ArgumentException)
            {
                // Registering unsuccessful
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch
            {
                // Generic error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{bracketID}/unregister/{systemID}")]
        [Produces("application/json")]
        public IActionResult UnregisterGamerFromBracket(int bracketID, int systemID)
        {
            try
            {
                return Ok(TournamentBracketManager.UnregisterGamerFromBracket(systemID, bracketID));
            }
            catch (ArgumentException)
            {
                /// Unregistering unsuccessful
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch
            {
                // Generic error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{email}/bracketPlayerInfo")]
        [Produces("application/json")]
        public IActionResult GetBracketPlayerInfo(string email)
        {
            try
            {
                return Ok(TournamentBracketManager.GetBracketPlayerInfo(email));
            }
            catch (ArgumentException)
            {
                // Bracket player info not found
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch
            {
                // Generic error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{email}/gamerInfo")]
        [Produces("application/json")]
        public IActionResult GetGamerInfo(string email)
        {
            try
            {
                return Ok(TournamentBracketManager.GetGamerInfoByEmail(email));
            }
            catch (ArgumentException)
            {
                // Gamer not found
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch
            {
                // Generic error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("createBracket/")]
        [Produces("application/json")]
        public IActionResult CreateBracket(BracketInfo bracketInfo)
        {
            try
            {
                return Ok(TournamentBracketManager.ValidateCreateBracket(bracketInfo));
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

         [HttpGet("{bracketID}/competitorInfo")]
        [Produces("application/json")]
        public IActionResult GetCompetitorListByBracketID(int bracketID)
        {
            try
            {
                return Ok(TournamentBracketManager.GetCompetitorListByBracketID(bracketID));
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
        [HttpPut("updateBracket/")]
        [Produces("application/json")]
        public IActionResult UpdateBracketInformation(BracketInfo bracketInfo)
        {
            try
            {
                return Ok(TournamentBracketManager.UpdateBracketInformation(bracketInfo));
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
         [HttpPost("{bracketID}/competitorInfo/update")]
        [Produces("application/json")]
        public IActionResult UpdateBracketStanding(int bracketID, BracketCompetitor bracketCompetitor)
        {
            try
            {
                return Ok(TournamentBracketManager.UpdateBracketStanding(bracketID, bracketCompetitor));
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

        [HttpPut("deleteBracket/")]
        [Produces("application/json")]
        public IActionResult DeleteBracket(BracketInfo bracketInfo)
        {
            try
            {
                return Ok(TournamentBracketManager.DeleteBracket(bracketInfo));
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
