using System;
using System.Collections.Generic;
using System.Text;
using TBZ.KrakenBracket.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;

namespace TBZ.KrakenBracket.Services
{
    public class TournamentBracketService
    {

        private readonly TournamentBracketDataAccess _tournamentBracketDataAccess = new TournamentBracketDataAccess();
        public bool CreateTournamentBracket(DataAccess _TournamentDA, Bracket bracketFields)
        {
            return true;
        }
        public bool GetBracketID(string email)
        {
            bool result = true;
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
