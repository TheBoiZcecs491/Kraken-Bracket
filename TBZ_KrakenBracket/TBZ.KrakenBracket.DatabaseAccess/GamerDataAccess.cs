using System;
using System.Collections.Generic;
using System.Text;
using TBZ.DatabaseQueryService;
using TBZ.KrakenBracket.DataHelpers;

namespace TBZ.KrakenBracket.DatabaseAccess
{
    public class GamerDataAccess
    {
        public Gamer GetGamerInfo(Gamer gamer)
        {
            DatabaseQuery databaseQuery = new DatabaseQuery();
            return databaseQuery.GetGamerInfo(gamer);
        }

        public Gamer GetGamerInfoByEmail(string email)
        {
            DatabaseQuery databaseQuery = new DatabaseQuery();
            TournamentBracketDatabaseQuery tournamentBracketDatabaseQuery = new TournamentBracketDatabaseQuery();
            User user = databaseQuery.GetUserInfo(email);
            if (user != null)
            {

                string hashedUserID = databaseQuery.GetHashedUserID(user.SystemID);
                Gamer gamer = databaseQuery.GetGamerInfoByHashedID(hashedUserID);
                return gamer;
            }
            else throw new ArgumentException();
        }
    }
}
