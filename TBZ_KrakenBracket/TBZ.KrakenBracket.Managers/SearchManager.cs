﻿using System;
using System.Collections.Generic;
using TBZ.KrakenBracket.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.KrakenBracket.Services;


namespace TBZ.KrakenBracket.Managers
{
    interface ISearchManager
    {
        List<BracketInfo> GetBrackets(string bracketRequest);
        List<EventInfo> GetEvents(string eventRequest);
        List<GamerInfo> GetGamers(string gamerRequest);
    }
    public class SearchManager : ISearchManager
    {
        readonly LoggingManager _loggingManager = new LoggingManager();
        readonly TournamentBracketDatabaseQuery _tournamentBracketDatabaseQuery = new TournamentBracketDatabaseQuery();
        readonly EventDataAccess _eventDataAccess = new EventDataAccess();
        readonly GamerDataAccess _gamerDataAccess = new GamerDataAccess();
        readonly SearchService _searchService;

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
            try
            {
                List<BracketInfo> results = _searchService.GetBrackets(bracketRequest);
                _ = _loggingManager.Log("Search", "");
                return results;
            }
            catch (Exception e)
            {
                _ = _loggingManager.Log("Search", "Data Store Error");
                throw e;
            }
        }

        /// <summary>
        /// A method to recieve Events from the search service
        /// </summary>
        /// <param name="eventRequest"> The search request for events </param>
        /// <returns> A List of Events </returns>
        public List<EventInfo> GetEvents(string eventRequest)
        {
            try
            {
                List<EventInfo> results = _searchService.GetEvents(eventRequest);
                _ = _loggingManager.Log("Search", "");
                return results;
            }
            catch (Exception e)
            {
                _ = _loggingManager.Log("Search", "Data Store Error");
                throw e;
            }
        }

        /// <summary>
        /// A method to recieve Gamers from the search service.
        /// </summary>
        /// <param name="gamerRequest"> The search request for gamers </param>
        /// <returns> A List of Gamers </returns>
        public List<GamerInfo> GetGamers(string gamerRequest)
        {
            try
            {
                List<GamerInfo> results = _searchService.GetGamers(gamerRequest);
                _ = _loggingManager.Log("Search", "");
                return results;
            }
            catch (Exception e)
            {
                _ = _loggingManager.Log("Search", "Data Store Error");
                throw e;
            }
        }
    }

    public class SearchManagerNoDB : ISearchManager
    {
        SearchServiceNoDB _searchServiceNoDB;
        public SearchManagerNoDB(List<BracketInfo> mockBrackets, List<EventInfo> mockEvents, List<GamerInfo> mockGamers)
        {
            _searchServiceNoDB = new SearchServiceNoDB(mockBrackets, mockEvents, mockGamers);
        }
        public List<BracketInfo> GetBrackets(string bracketRequest)
        {
           return _searchServiceNoDB.GetBrackets(bracketRequest);
        }

        public List<EventInfo> GetEvents(string eventRequest)
        {
            return _searchServiceNoDB.GetEvents(eventRequest);
        }

        public List<GamerInfo> GetGamers(string gamerRequest)
        {
            return _searchServiceNoDB.GetGamers(gamerRequest);
        }
    }

}
