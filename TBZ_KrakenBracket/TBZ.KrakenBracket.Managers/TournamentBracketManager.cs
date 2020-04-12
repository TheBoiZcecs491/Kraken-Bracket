using System;
using System.Collections.Generic;
using System.Text;
using TBZ.AuthorizationManager;
using TBZ.DatabaseAccess;
using TBZ.KrakenBracket.Services;

namespace TBZ.KrakenBracket.Managers
{
    class TournamentBracketManager
    {
        private readonly TournamentBracketService _TournamentBracketService = new TournamentBracketService();
        private readonly DataAccess TournamentDA = new DataAccess();
        public bool TournamentPermission(string email, string action, bool isLoggedIn)
        {
            Authorization AuthZ = new Authorization();
            return AuthZ.UserPermission(email, action, isLoggedIn);
        }
    }
}
