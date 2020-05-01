using System;
using System.Collections.Generic;
using TBZ.KrakenBracket.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.KrakenBracket.Services;


namespace TBZ.KrakenBracket.Managers
{
    public class SearchManager
    {
        //These must be static ???????

        static readonly TournamentBracketDatabaseQuery _tournamentBracketDatabaseQuery = new TournamentBracketDatabaseQuery();
        static readonly EventDataAccess _eventDataAccess = new EventDataAccess();
        static readonly GamerDataAccess _gamerDataAccess = new GamerDataAccess();
        private readonly SearchService _searchService = new SearchService(_tournamentBracketDatabaseQuery, _eventDataAccess, _gamerDataAccess);

        public List<BracketInfo> GetBrackets(string bracketSearch, int pageNum, int skipPage)
        {
            return _searchService.GetBrackets(bracketSearch, pageNum, skipPage);
        }
        public List<EventInfo> GetEvents(string bracketSearch, int pageNum, int skipPage)
        {
            return _searchService.GetEvents(bracketSearch, pageNum, skipPage);
        }

        public List<GamerInfo> GetGamers(string bracketSearch, int pageNum, int skipPage)
        {
            return _searchService.GetGamers(bracketSearch, pageNum, skipPage);
        }
    }
}
