using System;
using System.Collections.Generic;
using System.Text;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.KrakenBracket.Services;

namespace TBZ.KrakenBracket.Managers
{
    public class TournamentBracketManager
    {
        private readonly TournamentBracketService _tournamentBracketService;
        public TournamentBracketManager(TournamentBracketService tournamentBracketService)
        {
            _tournamentBracketService = tournamentBracketService;
        }
        public int GetStatusCode(int bracketID)
        {
            int result = _tournamentBracketService.CheckBracketStatusCode(bracketID);
            return result;

        }
    }
}
