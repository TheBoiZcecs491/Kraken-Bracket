using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.KrakenBracket.Managers;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientApp.Controllers
{
    [Route("api/events")]
    public class EventController : Controller
    {
        private readonly EventManager _eventManager = new EventManager();

        [HttpPost("createEvent")]
        [Produces("application/json")]
        public IActionResult CreateEvent(EventInfo eventObj)
        {
            return Ok(_eventManager.CreateEvent(eventObj));
        }

        public IActionResult GetAllEvent()
        {
            return Ok(_eventManager.GetAllEvents());
        }

        public IActionResult GetEventByID(int eventID)
        {
            return Ok(_eventManager.GetEventByID(eventID));
        }

        public IActionResult GetBracketsInEvent(int eventID)
        {
            return Ok(_eventManager.GetBracketsInEvent(eventID));
        }
    }


}
