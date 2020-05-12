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
    [Route("api/events")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly EventManager _eventManager;

        public EventController(EventManager eventManager)
         {
            _eventManager = eventManager;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAllEvent()
        {
            return Ok(_eventManager.GetAllEvents());
        }

        [HttpPost("createEvent")]
        [Produces("application/json")]
        public IActionResult CreateEvent(EventInfo eventInfo)
        {
            return Ok(_eventManager.CreateEvent(eventInfo));
        }

        [HttpPost("updateEvent")]
        [Produces("application/json")]
        public IActionResult UpdateEvent(EventInfo eventInfo)
        {
            return Ok(_eventManager.UpdateEvent(eventInfo));
        }

        [HttpGet("{eventID}")]
        [Produces("application/json")]
        public IActionResult GetEventByID(int eventID)
        {
            return Ok(_eventManager.GetEventByID(eventID));
        }

        [HttpPost("AddEventBracket")]
        [Produces("application/json")]
        public IActionResult AddEventBracket(EventBracketList eventBracket)
        {
            return Ok(_eventManager.AddBracketToEvent(eventBracket));
        }

        [HttpPost("{eventID}/register/{hashedUserID}")]
        [Produces("application/json")]
        public IActionResult AddEventPlayer(int eventID, string hashedUserID)
        {
            return Ok(_eventManager.AddGamerToEvent(eventID, hashedUserID));
        }

        [HttpPost("{eventID}/statusRegistration/{hashedUserID}")]
        [Produces("application/json")]
        public IActionResult CheckEventPlayer(int eventID, string hashUserID)
        {
            return Ok(_eventManager.CheckEventPlayer(eventID, hashUserID));
        }


        [HttpGet("GetBracketEvent/{eventID}")]
        [Produces("application/json")]
        public IActionResult GetBracketsInEvent(int eventID)
        {
            return Ok(_eventManager.GetBracketsInEvent(eventID));
        }

        [HttpGet("GetEventHost/{eventID}")]
        [Produces("application/json")]
        public IActionResult GetEventHost(int eventID)
        {
            return Ok(_eventManager.GetEventHost(eventID));
        }

        [HttpGet("GetEventInfo/{eventID}")]
        [Produces("application/json")]
        public IActionResult GetEventInfo(int eventID)
        {
            return Ok(_eventManager.GetEventPlayer(eventID));
        }

        [HttpGet("GetEventInfo/{eventID}/competitors")]
        [Produces("application/json")]
        public IActionResult GetEventCompetitors(int eventID)
        {
            return Ok(_eventManager.GetEventCompetitors(eventID));
        }

        [HttpDelete("{eventID}/unregister/{hashedUserID}")]
        [Produces("application/json")]
        public IActionResult RemoveEventPlayer(int eventID, string hashedUserID)
        {
            return Ok(_eventManager.RemoveGamerFromEvent(eventID, hashedUserID));
        }

    }
}
