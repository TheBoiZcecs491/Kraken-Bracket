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
        /// Retrieves latest bracket ID to assign to new bracket
        /// </summary>
        /// <returns> integer bracketID </returns>
        public int GetLatestBracketID()
        {
            try
            {
                using (conn = new MySqlConnection(CONNECTION_STRING))
                {
                    string selectQuery = string.Format(
                        "SELECT MAX(bracketID) FROM bracket_info");
                    MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                    conn.Open();
                    using (MySqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        return reader.Read() ? reader.GetInt32(0) : -1;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: ", e);
            }
            return -1;
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
        public BracketPlayer InsertGamerToBracket(GamerInfo gamer, BracketInfo bracket)
        {
            try
            {
                DatabaseQuery databaseQuery = new DatabaseQuery();
                TournamentBracketDatabaseQuery tournamentBracketDatabaseQuery = new TournamentBracketDatabaseQuery();
                GamerInfo tempGamer = databaseQuery.GetGamerInfo(gamer);
                BracketPlayer bracketPlayer = new BracketPlayer();
                bracketPlayer.BracketID = bracket.BracketID;
                bracketPlayer.HashedUserID = tempGamer.HashedUserID;
                bracketPlayer.StatusCode = 1;
                bracketPlayer.Claim = null;
                bracketPlayer.Reason = null;
                bool insertionResult = tournamentBracketDatabaseQuery.InsertBracketPlayer(bracketPlayer);
                if (insertionResult)
                {
                    tournamentBracketDatabaseQuery.UpdateBracketPlayerCount(bracket.BracketID, 1);
                    return bracketPlayer;
                }
                return null;
            }

            catch (Exception e)
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

        /// <summary>
        /// Retrieves latest bracket ID to increment and assign.
        /// Performs MySQL query to insert bracket with provided fields
        /// </summary>
        /// <param name="bracketFields"></param> provided bracket fields
        /// <returns> boolean to indicate a successful/unsuccessful insert to database </returns>
        public bool InsertNewBracket(BracketInfo bracketFields)
        {
            bracketFields.BracketID = GetLatestBracketID() + 1;
            try
            {
                using (conn = new MySqlConnection(CONNECTION_STRING))
                {
                    using (MySqlCommand insertCmd = conn.CreateCommand())
                    {
                        insertCmd.CommandText = "INSERT INTO bracket_info(bracketID, bracket_name, host, bracketTypeID, " +
                            "number_player, game_played, gaming_platform, rules, start_date, end_date, status_code )" +
                            "VALUES(@bracketID, @bracket_name, @host, @bracketTypeID, @number_player, @game_played, " +
                            "@gaming_platform, @rules, @start_date, @end_date, @status_code)";
                        insertCmd.Parameters.AddWithValue("@bracketID", bracketFields.BracketID);
                        insertCmd.Parameters.AddWithValue("@bracket_name", bracketFields.BracketName);
                        insertCmd.Parameters.AddWithValue("@host", bracketFields.Host);
                        insertCmd.Parameters.AddWithValue("@bracketTypeID", bracketFields.BracketTypeID);
                        insertCmd.Parameters.AddWithValue("@number_player", bracketFields.PlayerCount);
                        insertCmd.Parameters.AddWithValue("@game_played", bracketFields.GamePlayed);
                        insertCmd.Parameters.AddWithValue("@gaming_platform", bracketFields.GamingPlatform);
                        insertCmd.Parameters.AddWithValue("@rules", bracketFields.Rules);
                        insertCmd.Parameters.AddWithValue("@start_date", bracketFields.StartDate);
                        insertCmd.Parameters.AddWithValue("@end_date", bracketFields.EndDate);
                        insertCmd.Parameters.AddWithValue("@status_code", bracketFields.StatusCode);
                        conn.Open();
                        insertCmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false; // unsuccessful insert
            }
        }

        /// <summary>
        /// Updates the bracket using the provided bracket data,
        /// bases the search on the bracket ID assigned for MySQL query
        /// </summary>
        /// <param name="bracketFields"></param> Updated field(s) to update the query
        /// <returns> boolean to indicate successful/unsuccessful update to database </returns>
        public bool UpdateBracket(BracketInfo bracketFields)
        {
            if (!CheckBracketExistenceByID(bracketFields.BracketID))
                return false;
            try
            {
                using (conn = new MySqlConnection(CONNECTION_STRING))
                {
                    using (MySqlCommand updateCmd = conn.CreateCommand())
                    {
                        updateCmd.CommandText = "UPDATE bracket_info " +
                            "SET " +
                            "bracket_name = @bracket_name, " +
                            "bracketTypeID = @bracketTypeID, " +
                            "number_player = @number_player, " +
                            "game_played = @game_played, " +
                            "gaming_platform = @gaming_platform, " +
                            "rules = @rules, " +
                            "start_date = @start_date, " +
                            "end_date = @end_date " +
                            "WHERE bracketID = @bracketID";
                        updateCmd.Parameters.AddWithValue("@bracketID", bracketFields.BracketID);
                        updateCmd.Parameters.AddWithValue("@bracket_name", bracketFields.BracketName);
                        updateCmd.Parameters.AddWithValue("@bracketTypeID", bracketFields.BracketTypeID);
                        updateCmd.Parameters.AddWithValue("@number_player", bracketFields.PlayerCount);
                        updateCmd.Parameters.AddWithValue("@game_played", bracketFields.GamePlayed);
                        updateCmd.Parameters.AddWithValue("@gaming_platform", bracketFields.GamingPlatform);
                        updateCmd.Parameters.AddWithValue("@rules", bracketFields.Rules);
                        updateCmd.Parameters.AddWithValue("@start_date", bracketFields.StartDate);
                        updateCmd.Parameters.AddWithValue("@end_date", bracketFields.EndDate);
                        conn.Open();
                        updateCmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false; // unsuccessful update
            }
        }

        /// <summary>
        /// Deletes bracket with provided bracket info for the MySQL query
        /// </summary>
        /// <param name="bracketFields"></param> Uses the bracket ID in the BracketField object
        /// <returns> boolean to indicate successful/unsuccessful delete </returns>
        public bool DeleteBracket(BracketInfo bracketFields)
        {
            try
            {
                using (conn = new MySqlConnection(CONNECTION_STRING))
                {
                    string deleteQuery = string.Format("DELETE FROM bracket_info WHERE bracketID = {0}", bracketFields.BracketID);
                    MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn);
                    conn.Open();
                    deleteCmd.ExecuteNonQuery();
                    conn.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false; // unsuccessful delete
            }
        }
    }
}
