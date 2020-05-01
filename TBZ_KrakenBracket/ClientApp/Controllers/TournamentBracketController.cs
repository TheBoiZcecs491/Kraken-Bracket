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


        [HttpGet("{bracketID}")]
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
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{bracketID}/unregister/{systemID}")]
        [Produces("application/json")]
        public IActionResult UnregisterGamerFromBracket(int bracketID, int systemID)
        {
            try
            {
                return Ok(_tournamentBracketManager.UnregisterGamerFromBracket(systemID, bracketID));
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{email}/bracketPlayerInfo")]
        [Produces("application/json")]
        public IActionResult GetBracketPlayerInfo(string email)
        {
            try
            {
                return Ok(_tournamentBracketManager.GetBracketPlayerInfo(email));
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{email}/gamerInfo")]
        [Produces("application/json")]
        public IActionResult GetGamerInfo(string email)
        {
            try
            {
                return Ok(_tournamentBracketManager.GetGamerInfoByEmail(email));
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
    }
