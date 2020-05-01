using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using TBZ.DatabaseQueryService;
using TBZ.HashingService;
using TBZ.KrakenBracket.DataHelpers;

namespace TBZ.KrakenBracket.DatabaseAccess
{
    public class TournamentBracketDataAccess
    {
        const string CONNECTION_STRING = @"server=localhost; userid=root; password=Gray$cale917!!; database=kraken_bracket";
        private MySqlConnection conn;

        /// <summary>
        /// Searches the database for a bracket by its ID
        /// </summary>
        /// 
        /// <param name="bracketID">
        /// Bracket ID associated with bracket
        /// </param>
        /// 
        /// <returns>
        /// Boolean indicating success or fail
        /// </returns>
        public bool CheckBracketExistenceByID(int bracketID)
        {
            try
            {
                using (conn = new MySqlConnection(CONNECTION_STRING))
                {
                    string selectQuery = string.Format("SELECT * FROM bracket_info WHERE bracketID={0}", bracketID);
                    MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);

                    conn.Open();

                    using (MySqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        int count = 0;
                        while (reader.Read())
                        {
                            count++;
                        }
                        reader.Close();
                        conn.Close();
                        return (count == 1);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Gets a specific bracket by its ID
        /// </summary>
        /// 
        /// <param name="bracketID">
        /// Bracket ID to be used to fetch bracket objectr
        /// </param>
        /// 
        /// <returns>
        /// Bracket object associated with bracket ID
        /// </returns>
        public BracketInfo GetBracketByID(int bracketID)
        {
            bool bracketStatus = CheckBracketExistenceByID(bracketID);
            if (!bracketStatus) return null;
            else
            {
                TournamentBracketDatabaseQuery tournamentBracketDatabaseQuery = new TournamentBracketDatabaseQuery();
                BracketInfo bracket = tournamentBracketDatabaseQuery.GetBracketInfo(bracketID);
                return bracket;
            }
        }

        /// <summary>
        /// Unregisters gamer from bracket
        /// </summary>
        /// 
        /// <param name="systemID">
        /// System ID associated with user
        /// </param>
        /// 
        /// <param name="bracketID">
        /// Bracket ID associated with bracket
        /// </param>
        /// 
        /// <returns>
        /// Boolean indicated success or fail in unregistration
        /// </returns>
        public bool UnregisterGamerFromBracket(int systemID, int bracketID)
        {
            /*
             Status codes

            2 - bracket is in progress
            1 - bracket not in progress and has already completed
            0 - bracket not in progress and has not begun
             */
            try
            {
                DatabaseQuery databaseQuery = new DatabaseQuery();
                TournamentBracketDatabaseQuery tournamentBracketDatabaseQuery = new TournamentBracketDatabaseQuery();
                string hashedUserID = databaseQuery.GetHashedUserID(systemID);
                BracketInfo bracket = tournamentBracketDatabaseQuery.GetBracketInfo(bracketID);
                if(bracket.StatusCode == 2)
                {
                    return tournamentBracketDatabaseQuery.DisqualifyGamerFromBracket(bracketID, hashedUserID);
                }
                else if(bracket.StatusCode == 0)
                {
                    bool removeResult = tournamentBracketDatabaseQuery.RemoveGamerFromBracket(hashedUserID, bracketID);
                    bool updateResult = tournamentBracketDatabaseQuery.UpdateBracketPlayerCount(bracketID, 0);
                    if (removeResult && updateResult) return true;
                }
                return false;
                
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets bracket player info by user's email
        /// </summary>
        /// 
        /// <param name="email">
        /// Email associated with user
        /// </param>
        /// 
        /// <returns>
        /// List of user's BracketPlayer info
        /// </returns>
        public List<BracketPlayer> GetBracketPlayerInfo(string email)
        {
            DatabaseQuery databaseQuery = new DatabaseQuery();
            TournamentBracketDatabaseQuery tournamentBracketDatabaseQuery = new TournamentBracketDatabaseQuery();
            User user = databaseQuery.GetUserInfo(email);
            if(user != null)
            {
                string hashedUserID = databaseQuery.GetHashedUserID(user.SystemID);
                List<BracketPlayer> bracketPlayers = tournamentBracketDatabaseQuery.GetBracketPlayerInfo(hashedUserID);
                return bracketPlayers;
            }
            return null;
                  
        }

        /// <summary>
        /// Inserts gamer into bracket
        /// </summary>
        /// 
        /// <param name="gamer">
        /// Gamer object to be inserted
        /// </param>
        /// 
        /// <param name="bracketID">
        /// BracketID associated with bracket, where the gamer will be inserted
        /// </param>
        /// 
        /// <returns>
        /// BracketPlayer object if insertion successful; null if not
        /// </returns>
        public BracketPlayer InsertGamerToBracket(Gamer gamer, int bracketID)
        {
            try
            {
                BracketInfo bracket = GetBracketByID(bracketID);
                if(bracket.PlayerCount >= 128)
                {
                    return null;
                }
                else
                {
                    DatabaseQuery databaseQuery = new DatabaseQuery();
                    TournamentBracketDatabaseQuery tournamentBracketDatabaseQuery = new TournamentBracketDatabaseQuery();
                    Gamer tempGamer = databaseQuery.GetGamerInfo(gamer);
                    BracketPlayer bracketPlayer = new BracketPlayer();
                    bracketPlayer.BracketID = bracket.BracketID;
                    bracketPlayer.HashedUserID = tempGamer.HashedUserID;
                    bracketPlayer.StatusCode = 1;
                    tournamentBracketDatabaseQuery.InsertBracketPlayer(bracketPlayer);
                    tournamentBracketDatabaseQuery.UpdateBracketPlayerCount(bracket.BracketID, 1);
                    return bracketPlayer;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Returns all brackets stored in the database
        /// </summary>
        /// 
        /// <returns>
        /// All brackets stored in the database
        /// </returns>
        public List<BracketInfo> GetAllBrackets()
        {
            TournamentBracketDatabaseQuery tournamentBracketDatabaseQuery = new TournamentBracketDatabaseQuery();
            return tournamentBracketDatabaseQuery.GetAllBrackets();
        }
    }
}
