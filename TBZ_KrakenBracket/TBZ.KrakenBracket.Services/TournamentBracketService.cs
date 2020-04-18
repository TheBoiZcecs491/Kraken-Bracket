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
        public bool CreateTournamentBracket(BracketInfo bracketFields)
        {
            return _tournamentBracketDataAccess.InsertNewBracket(bracketFields);
        }

        public bool UpdateTournamentBracket(BracketInfo bracketFields)
        {
            return _tournamentBracketDataAccess.UpdateBracket(bracketFields);
        }

        public bool DeleteTournamentBracket(BracketInfo bracketFields)
        {
            return _tournamentBracketDataAccess.DeleteBracket(bracketFields);
        }
        public bool GetBracketID(string email)
        {
            bool result = true;
            return result;
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
        public BracketInfo GetTournamentBracket(int bracketID)
        {
            return _tournamentBracketDataAccess.GetBracket(bracketID);
        }
    }
}
