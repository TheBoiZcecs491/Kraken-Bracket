using System;
using System.Collections.Generic;
using TBZ.KrakenBracket.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;

namespace TBZ.KrakenBracket.Services
{
    interface ISearchService {
        List<BracketInfo> GetBrackets(string bracketRequest);
        List<EventInfo> GetEvents(string eventRequest);
        List<GamerInfo> GetGamers(string gamerRequest);
    }

    public class SearchService : ISearchService
    {
        TournamentBracketDatabaseQuery _bracketDAO = new TournamentBracketDatabaseQuery();
        EventDataAccess _eventDAO = new EventDataAccess();
        GamerDataAccess _gamerDAO = new GamerDataAccess();

        /// <summary>
        /// A constructor for the search service.
        /// </summary>
        /// <param name="bracketDAO"> A bracket data access object </param>
        /// <param name="eventDAO"> An event data access object </param>
        /// <param name="gamerDAO"> A gamer data access object </param>
        public SearchService(TournamentBracketDatabaseQuery bracketDAO, EventDataAccess eventDAO, GamerDataAccess gamerDAO)
        {
            _bracketDAO = bracketDAO;
            _eventDAO = eventDAO;
            _gamerDAO = gamerDAO;
        }

        /// <summary>
        /// A method to recieve Brackets from the Bracket DAO.
        /// </summary>
        /// <param name="bracketRequest"> The search request for brackets </param>
        /// <returns> A List of Brackets </returns>
        public List<BracketInfo> GetBrackets(string bracketRequest)
        {
            return _bracketDAO.ReadBrackets(bracketRequest);
        }

        /// <summary>
        /// A method to recieve Events from the Event DAO.
        /// </summary>
        /// <param name="eventRequest"> The search request for events </param>
        /// <returns> A List of Events </returns>
        public List<EventInfo> GetEvents(string eventRequest)
        {
            return _eventDAO.ReadEvents(eventRequest);
        }

        /// <summary>
        /// A method to recieve Gamers from the Gamer DAO.
        /// </summary>
        /// <param name="gamerRequest"> The search request for gamers </param>
        /// <returns> A List of Gamers </returns>
        public List<GamerInfo> GetGamers(string gamerRequest)
        {
            return _gamerDAO.ReadGamers(gamerRequest);
        }
    }

    public class SearchServiceNoDB : ISearchService
    {
        readonly List<BracketInfo> _mockBracketData;
        readonly List<EventInfo> _mockEventData;
        readonly List<GamerInfo> _mockGamerData;

        public SearchServiceNoDB(List<BracketInfo> mockBracketData, List<EventInfo> mockEventData, List<GamerInfo> mockGamerData)
        {
            _mockBracketData = mockBracketData;
            _mockEventData = mockEventData;
            _mockGamerData = mockGamerData;
        }

        public List<BracketInfo> GetBrackets(string bracketRequest)
        {
            List<BracketInfo> brackets = new List<BracketInfo>();
            foreach(BracketInfo bracketInfo in _mockBracketData)
            {
                if (bracketInfo.BracketName.Contains(bracketRequest))
                {
                    brackets.Add(bracketInfo);
                }
            }
            return brackets;
        }

        public List<EventInfo> GetEvents(string eventRequest)
        {
            List<EventInfo> events = new List<EventInfo>();
            foreach (EventInfo eventInfo in _mockEventData)
            {
                if (eventInfo.EventName.Contains(eventRequest))
                {
                    events.Add(eventInfo);
                }
            }
            return events;
        }

        public List<GamerInfo> GetGamers(string gamerRequest)
        {
            List<GamerInfo> gamers = new List<GamerInfo>();
            foreach (GamerInfo gamerInfo in _mockGamerData)
            {
                if (gamerInfo.GamerTag.Contains(gamerRequest))
                {
                    gamers.Add(gamerInfo);
                }
            }
            return gamers;
        }
    }

}
