using System;
using System.Collections.Generic;
using TBZ.KrakenBracket.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.KrakenBracket.Services;


namespace TBZ.KrakenBracket.Managers
{
    public class SearchManager
    {
        private readonly SearchService _searchService = new SearchService();
        //public SearchManager(SearchService searchService) //logging
        //{
        //    _searchService = searchService;
        //}

        public List<BracketInfo> GetBrackets(string bracketSearch, int pageNum, int skipPage)
        {
            return _searchService.GetBrackets(bracketSearch, pageNum, skipPage);
        }
    }
}
