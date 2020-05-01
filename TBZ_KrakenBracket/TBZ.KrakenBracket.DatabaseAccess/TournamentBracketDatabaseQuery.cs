using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using TBZ.DatabaseConnectionService;
using TBZ.KrakenBracket.DataHelpers;

namespace TBZ.KrakenBracket.DatabaseAccess
{
    public class TournamentBracketDatabaseQuery
    {
        public void IncrementBracketPlayerCount(BracketInfo bracket)
        {
            var DB = new Database();

            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                using (MySqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandText = "UPDATE bracket_info SET number_player = number_player + 1 WHERE bracketID=@BracketID";
                    comm.Parameters.AddWithValue("@BracketID", bracket.BracketID);
                    conn.Open();
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        public void DecrementBracketPlayerCount(int bracketID)
        {
            var DB = new Database();

            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                using (MySqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandText = "UPDATE bracket_info SET number_player = number_player - 1 WHERE bracketID=@BracketID";
                    comm.Parameters.AddWithValue("@BracketID", bracketID);
                    conn.Open();
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        public BracketInfo GetBracketInfo(int bracketID)
        {
            var DB = new Database();

            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                using (MySqlCommand comm = conn.CreateCommand())
                {

                    comm.CommandText = "SELECT * FROM bracket_info WHERE bracketID=@BracketID";
                    comm.Parameters.AddWithValue("@BracketID", bracketID);
                    conn.Open();
                    using (MySqlDataReader reader = comm.ExecuteReader())
                    {
                        BracketInfo bracket = new BracketInfo();
                        reader.Read();
                        bracket.BracketID = reader.GetInt32("bracketID");
                        bracket.BracketName = reader.GetString("bracket_name");
                        bracket.BracketTypeID = reader.GetInt32("bracketTypeID");
                        bracket.PlayerCount = reader.GetInt32("number_player");
                        bracket.GamePlayed = reader.GetString("game_played");
                        bracket.GamingPlatform = reader.GetString("gaming_platform");
                        bracket.Rules = reader.GetString("rules");
                        bracket.StartDate = reader.GetDateTime("start_date");
                        bracket.EndDate = reader.GetDateTime("end_date");
                        bracket.StatusCode = reader.GetInt32("status_code");
                        conn.Close();
                        return bracket;
                    }
                }
            }
        }
        public List<BracketPlayer> GetBracketPlayerInfo(string hashedUserID)
        {
            var DB = new Database();
            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                string selectQuery2 = string.Format("SELECT * FROM bracket_player_info WHERE hashedUserID='{0}'", hashedUserID);
                MySqlCommand selectCmd = new MySqlCommand(selectQuery2, conn);
                conn.Open();
                using (MySqlDataReader reader0 = selectCmd.ExecuteReader())
                {
                    List<BracketPlayer> bracketPlayers = new List<BracketPlayer>();
                    while (reader0.Read())
                    {
                        BracketPlayer bracketPlayer = new BracketPlayer();
                        bracketPlayer.BracketID = reader0.GetInt32("bracketID");
                        bracketPlayer.HashedUserID = reader0.GetString("hashedUserID");
                        bracketPlayer.RoleID = reader0.GetInt32("roleID");
                        bracketPlayer.Placement = reader0.GetInt32("placement");
                        bracketPlayer.Score = reader0.GetInt32("score");
                        bracketPlayers.Add(bracketPlayer);
                    }
                    conn.Close();
                    return bracketPlayers;
                }
            }
        }
        public List<BracketInfo> GetAllBrackets()
        {
            var DB = new Database();
            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                string selectQuery = string.Format("SELECT * FROM bracket_info");
                MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                conn.Open();
                List<BracketInfo> listOfBrackets = new List<BracketInfo>();
                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BracketInfo bracket = new BracketInfo();
                        bracket.BracketID = reader.GetInt32("bracketID");
                        bracket.BracketName = reader.GetString("bracket_name");
                        bracket.BracketTypeID = reader.GetInt32("bracketTypeID");
                        bracket.PlayerCount = reader.GetInt32("number_player");
                        bracket.GamePlayed = reader.GetString("game_played");
                        bracket.GamingPlatform = reader.GetString("gaming_platform");
                        bracket.Rules = reader.GetString("rules");
                        bracket.StartDate = reader.GetDateTime("start_date");
                        bracket.EndDate = reader.GetDateTime("end_date");
                        bracket.StatusCode = reader.GetInt32("status_code");
                        listOfBrackets.Add(bracket);
                    }
                }
                return listOfBrackets;
            }
        }

        public List<BracketInfo> ReadBrackets(string bracketRequest, int pageNum, int skipPage)
        {
            var DB = new Database();
            var listOfBrackets = new List<BracketInfo>();
            var negativeSkipPage = skipPage < 0;
            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                string selectQuery = string.Format("SELECT * FROM bracket_info WHERE bracket_name LIKE \'%{0}%\'", bracketRequest);
                Console.WriteLine(selectQuery);
                MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                conn.Open();
                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BracketInfo bracket = new BracketInfo();
                        bracket.BracketID = reader.GetInt32("bracketID");
                        bracket.BracketName = reader.GetString("bracket_name");
                        bracket.BracketTypeID = reader.GetInt32("bracketTypeID");
                        bracket.PlayerCount = reader.GetInt32("number_player");
                        bracket.GamePlayed = reader.GetString("game_played");
                        bracket.GamingPlatform = reader.GetString("gaming_platform");
                        bracket.Rules = reader.GetString("rules");
                        bracket.StartDate = reader.GetDateTime("start_date");
                        bracket.EndDate = reader.GetDateTime("end_date");
                        bracket.StatusCode = reader.GetInt32("status_code");
                        listOfBrackets.Add(bracket);
                    }
                }
            }
            return listOfBrackets;
        }
    }
}