using System;
using System.Collections.Generic;
using TBZ.KrakenBracket.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;

namespace TBZ.KrakenBracket.Services
{
    public class SearchService
    {

        TournamentBracketDatabaseQuery _bracketDAO = new TournamentBracketDatabaseQuery();
        // EventDataAccess _eventDAO = new EventDataAccess();
        // GamerDataAccess _gamerDAO = new GamerDataAccess();

        //public SearchService(TournamentBracketDataAccess bracketDAO) // eventDAO, gamerDAO
        //{
        //    _bracketDAO = bracketDAO;
        //}

        public List<BracketInfo> GetBrackets(string bracketRequest, int pageNum, int skipPage)
        {
            return _bracketDAO.ReadBrackets(bracketRequest, pageNum, skipPage);
        }
    }
}