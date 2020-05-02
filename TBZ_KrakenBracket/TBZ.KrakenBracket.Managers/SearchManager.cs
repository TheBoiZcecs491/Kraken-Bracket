using System;
using System.Collections.Generic;
using TBZ.KrakenBracket.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.KrakenBracket.Services;


namespace TBZ.KrakenBracket.Managers
{
    public class SearchManager
    {
        readonly TournamentBracketDatabaseQuery _tournamentBracketDatabaseQuery = new TournamentBracketDatabaseQuery();
        readonly EventDataAccess _eventDataAccess = new EventDataAccess();
        readonly GamerDataAccess _gamerDataAccess = new GamerDataAccess();
        private readonly SearchService _searchService;

        /// <summary>
        /// A constructor for the search manager that initializes
        /// the search service with the DAOs that were initialized.
        /// </summary>
        public SearchManager()
        {
            _searchService = new SearchService(_tournamentBracketDatabaseQuery, _eventDataAccess, _gamerDataAccess);
        }

        /// <summary>
        /// A method to recieve Brackets from the search service
        /// </summary>
        /// <param name="bracketRequest"> The search request for brackets </param>
        /// <returns> A List of Brackets </returns>
        public List<BracketInfo> GetBrackets(string bracketRequest)
        {
            return _searchService.GetBrackets(bracketRequest);
        }

        /// <summary>
        /// A method to recieve Events from the search service
        /// </summary>
        /// <param name="eventRequest"> The search request for events </param>
        /// <returns> A List of Events </returns>
        public List<EventInfo> GetEvents(string eventRequest)
        {
            return _searchService.GetEvents(eventRequest);
        }

        /// <summary>
        /// A method to recieve Gamers from the search service.
        /// </summary>
        /// <param name="gamerRequest"> The search request for gamers </param>
        /// <returns> A List of Gamers </returns>
        public List<GamerInfo> GetGamers(string gamerRequest)
        {
            return _searchService.GetGamers(gamerRequest);
        }
    }
}
