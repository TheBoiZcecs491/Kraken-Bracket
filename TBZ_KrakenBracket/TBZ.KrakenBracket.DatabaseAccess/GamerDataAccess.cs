using System;
using System.Collections.Generic;
using System.Text;
using TBZ.DatabaseQueryService;
using TBZ.KrakenBracket.DataHelpers;

namespace TBZ.KrakenBracket.DatabaseAccess
{
    public class GamerDataAccess
    {
        /// <summary>
        /// Gets gamer info associated with user
        /// </summary>
        /// 
        /// <param name="gamer">
        /// Gamer object
        /// </param>
        /// 
        /// <returns>
        /// Gamer info
        /// </returns>
        public Gamer GetGamerInfo(Gamer gamer)
        {
            DatabaseQuery databaseQuery = new DatabaseQuery();
            return databaseQuery.GetGamerInfo(gamer);
        }

        /// <summary>
        /// Gets gamer info associated with user's email
        /// </summary>
        /// 
        /// <param name="email">
        /// User's email
        /// </param>
        /// <returns>
        /// Gamer info
        /// </returns>
        public Gamer GetGamerInfoByEmail(string email)
        {
            DatabaseQuery databaseQuery = new DatabaseQuery();
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
