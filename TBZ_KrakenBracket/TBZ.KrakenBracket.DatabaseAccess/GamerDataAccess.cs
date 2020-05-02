using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TBZ.DatabaseConnectionService;
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
        public GamerInfo GetGamerInfo(GamerInfo gamer)
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
        public GamerInfo GetGamerInfoByEmail(string email)
        {
            DatabaseQuery databaseQuery = new DatabaseQuery();
            User user = databaseQuery.GetUserInfo(email);
            if (user != null)
            {

                string hashedUserID = databaseQuery.GetHashedUserID(user.SystemID);
                GamerInfo gamer = databaseQuery.GetGamerInfoByHashedID(hashedUserID);
                return gamer;
            }
            else throw new ArgumentException();
        }

        public List<GamerInfo> ReadGamers(string gamerRequest)
        {
            var DB = new Database();
            var listOfGamers = new List<GamerInfo>();
            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                string selectQuery = string.Format("SELECT * FROM gamer_info WHERE gamerTag LIKE \'%{0}%\'", gamerRequest);
                Console.WriteLine(selectQuery);
                MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                conn.Open();
                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GamerInfo gamerObj = new GamerInfo();
                        gamerObj.GamerTag = reader.GetString("gamerTag");
                        listOfGamers.Add(gamerObj);
                    }
                }
            }
            return listOfGamers;
        }
    }
}
