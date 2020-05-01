using System;
using System.Collections.Generic;
using TBZ.KrakenBracket.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;

namespace TBZ.KrakenBracket.Services
{
    public class SearchService
    {

        TournamentBracketDatabaseQuery _bracketDAO = new TournamentBracketDatabaseQuery();
        EventDataAccess _eventDAO = new EventDataAccess();
        GamerDataAccess _gamerDAO = new GamerDataAccess();

        public SearchService(TournamentBracketDatabaseQuery bracketDAO, EventDataAccess eventDAO, GamerDataAccess gamerDAO)
        {
            _bracketDAO = bracketDAO;
            _eventDAO = eventDAO;
            _gamerDAO = gamerDAO;
        }

        public List<BracketInfo> GetBrackets(string bracketRequest, int pageNum, int skipPage)
        {
            return _bracketDAO.ReadBrackets(bracketRequest, pageNum, skipPage);
        }
        public List<EventInfo> GetEvents(string bracketRequest, int pageNum, int skipPage)
        {
            return _eventDAO.ReadEvents(bracketRequest, pageNum, skipPage);
        }
        public List<GamerInfo> GetGamers(string bracketRequest, int pageNum, int skipPage)
        {
            return _gamerDAO.ReadGamers(bracketRequest, pageNum, skipPage);
        }
    }
}
