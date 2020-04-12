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
        public BracketInfo GetStatusCode(BracketInfo bracket)
        {
            int bracketID = _tournamentBracketService.CheckBracketStatusCode(bracket);
            switch (bracketID)
            {
                case 0:
                    return bracket;
                case 1:
                    return bracket;
                case 2:
                    return bracket;
                default:
                    return null;
            }

        }
    }
}
