using System;
using System.Collections.Generic;
using System.Text;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.KrakenBracket.Services;

namespace TBZ.KrakenBracket.Managers
{
    public class TournamentBracketManager
    {
        private readonly TournamentBracketService _tournamentBracketService = new TournamentBracketService();
        
        public int GetBracketStatusCode(int bracketID)
        {
            int result = _tournamentBracketService.CheckBracketStatusCode(bracketID);
            return result;

        }

        public int GetNumberOfCompetitors(int bracketID)
        {
            int result = _tournamentBracketService.GetNumberOfCompetitors(bracketID);
            return result;
        }
    }
}
