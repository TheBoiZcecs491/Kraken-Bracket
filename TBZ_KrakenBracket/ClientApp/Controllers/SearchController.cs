using Microsoft.AspNetCore.Mvc;
using TBZ.KrakenBracket.Managers;

namespace ClientApp.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly SearchManager _searchManager;

        /// <summary>
        /// A constructor for the search controller that sets the
        /// global search manager to the search manager parameter.
        /// </summary>
        /// <param name="searchManager"> An instance of search manager </param>
        public SearchController(SearchManager searchManager)
        {
            _searchManager = searchManager;
        }

        /// <summary>
        /// A method to recieve incoming search requests for Brackets
        /// </summary>
        /// <param name="search"> The search request for Brackets </param>
        /// <returns> An OkObjectResult that is recieved by the front end search service </returns>
        [HttpGet("brackets/{search}")]
        [Produces("application/json")]
        public IActionResult SearchBrackets(string search)
        {
            return Ok(_searchManager.GetBrackets(search));
        }

        /// <summary>
        /// A method to recieve incoming search requests for Events
        /// </summary>
        /// <param name="search"> The search request for Events </param>
        /// <returns> An OkObjectResult that is recieved by the front end search service </returns>
        [HttpGet("events/{search}")]
        [Produces("application/json")]
        public IActionResult SearchEvents(string search)
        {
            return Ok(_searchManager.GetEvents(search));
        }

        /// <summary>
        /// A method to recieve incoming search requests for Gamers
        /// </summary>
        /// <param name="search"> The search request for Gamers </param>
        /// <returns> An OkObjectResult that is recieved by the front end search service </returns>
        [HttpGet("gamers/{search}")]
        [Produces("application/json")]
        public IActionResult SearchGamers(string search)
        {
            return Ok(_searchManager.GetGamers(search));
        }
    }
}
