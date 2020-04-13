using System;
using System.Collections.Generic;
using System.Text;
using TBZ.KrakenBracket.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;

namespace TBZ.KrakenBracket.Services
{
    public class TournamentBracketService
    {
        private readonly TournamentBracketDataAccess _tournamentBracketDataAccess;
        public TournamentBracketService(TournamentBracketDataAccess tournamentBracketDataAccess)
        {
            _tournamentBracketDataAccess = tournamentBracketDataAccess;
        }
        public int CheckBracketStatusCode(int bracketID)
        {
            
            var result = _tournamentBracketDataAccess.GetBracketStatusCode(bracketID);
            return result;
        }

        public int GetNumberOfCompetitors(int bracketID)
        {
            var result = _tournamentBracketDataAccess.GetNumberOfCompetitors(bracketID);
            return result;
        }
    }
}
