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

        public bool RemoveGamerFromBracket(int systemID, int bracketID)
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
                    databaseQuery.RemoveGamerFromBracket(hashedUserID, bracketID);
                    tournamentBracketDatabaseQuery.DecrementBracketPlayerCount(bracketID);
                    return true;
                }
                return false;
                
            }
            catch
            {
                return false;
            }
        }

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
                    databaseQuery.InsertBracketPlayer(bracketPlayer);
                    tournamentBracketDatabaseQuery.IncrementBracketPlayerCount(bracket);
                    return bracketPlayer;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public List<BracketInfo> GetAllBrackets()
        {
            TournamentBracketDatabaseQuery tournamentBracketDatabaseQuery = new TournamentBracketDatabaseQuery();
            return tournamentBracketDatabaseQuery.GetAllBrackets();
        }
    }
}
