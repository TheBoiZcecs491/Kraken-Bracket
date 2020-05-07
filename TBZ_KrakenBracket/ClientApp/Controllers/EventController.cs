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

        [HttpGet("{eventID}")]
        [Produces("application/json")]
        public IActionResult GetEventByID(int eventID)
        {
            return Ok(_eventManager.GetEventByID(eventID));
        }

        [HttpPost("addEventBracket")]
        [Produces("application/json")]
        public IActionResult addEventBracket(EventBracketList eventBracket)
        {
            return Ok(_eventManager.AddBracketToEvent(eventBracket));
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

    }
}
