using Microsoft.AspNetCore.Mvc;
using TBZ.KrakenBracket.Managers;

namespace ClientApp.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        //static SearchService _searchService = new SearchService();
        private readonly SearchManager _searchManager;

        public SearchController(SearchManager searchManager)
        {
            _searchManager = searchManager;
        }

        [HttpGet("brackets/{search}")] //, pageNum, skipPage}")]
        [Produces("application/json")]
        public IActionResult SearchBrackets(string search) //, int pageNum, int skipPage)
        {
            //logger
            int pageNum = 0;
            int skipPage = 0;
            return Ok(_searchManager.GetBrackets(search, pageNum, skipPage));
        }
        [HttpGet("events/{search}")] //, pageNum, skipPage}")]
        [Produces("application/json")]
        public IActionResult SearchEvents(string search) //, int pageNum, int skipPage)
        {
            //logger
            int pageNum = 0;
            int skipPage = 0;
            return Ok(_searchManager.GetBrackets(search, pageNum, skipPage));
        }
        [HttpGet("gamers/{search}")] //, pageNum, skipPage}")]
        [Produces("application/json")]
        public IActionResult SearchGamers(string search) //, int pageNum, int skipPage)
        {
            //logger
            int pageNum = 0;
            int skipPage = 0;
            return Ok(_searchManager.GetBrackets(search, pageNum, skipPage));
        }
    }
}
