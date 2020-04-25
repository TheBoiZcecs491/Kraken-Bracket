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
        public bool CheckBracketIDExistence(int bracketID)
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

        public int GetNumberOfCompetitors(int bracketID)
        {
            try
            {
                using (conn = new MySqlConnection(CONNECTION_STRING))
                {
                    string selectQuery = string.Format("SELECT number_player FROM bracket_info WHERE bracketID={0}", bracketID);
                    MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                    conn.Open();
                    using (MySqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        return reader.Read() ?  reader.GetInt32(0) : -1;
                    }
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int GetBracketStatusCode(int bracketID)
        {
            bool bracketStatus = CheckBracketIDExistence(bracketID);
            if (!bracketStatus) return -1;
            else
            {
                using (conn = new MySqlConnection(CONNECTION_STRING))
                {
                    string selectQuery = string.Format("SELECT status_code FROM bracket_info WHERE bracketID={0}", bracketID);
                    MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                    conn.Open();
                    using (MySqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        return reader.Read() ? reader.GetInt32(0) : -1;
                    }
                }
            }
        }

        public BracketInfo GetBracketByID(int bracketID)
        {
            bool bracketStatus = CheckBracketIDExistence(bracketID);
            if (!bracketStatus) return null;
            else
            {
                using (conn = new MySqlConnection(CONNECTION_STRING))
                {
                    string selectQuery = string.Format("SELECT * FROM bracket_info WHERE bracketID={0}", bracketID);
                    MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                    conn.Open();
                    using (MySqlDataReader reader = selectCmd.ExecuteReader())
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

        public List<BracketPlayer> GetBracketPlayerInfo(string email)
        {
            DatabaseQuery databaseQuery = new DatabaseQuery();
            User user = databaseQuery.GetUserInfo(email);
            string hashedUserID = databaseQuery.GetHashedUserID(user.SystemID);
            List<BracketPlayer> bracketPlayers = databaseQuery.GetBracketPlayerInfo(hashedUserID);
            return bracketPlayers;            
        }

        public User GetUser(string email, string password)
        {
            DatabaseQuery databaseQuery = new DatabaseQuery();
            User user = databaseQuery.GetUserInfo(email);
            MessageSalt messageSalt = new MessageSalt(password, user.Salt);
            messageSalt.GenerateHash(messageSalt);
            if (messageSalt.message == user.Password)
            {
                return user;
            }
            //using (conn = new MySqlConnection(CONNECTION_STRING))
            //{
            //    string selectQuery = string.Format("SELECT * FROM user_information WHERE email='{0}'", email);
            //    MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
            //    conn.Open();
            //    using (MySqlDataReader reader = selectCmd.ExecuteReader())
            //    {
            //        User user = new User();
            //        reader.Read();
            //        user.Password = reader.GetString("hashed_password");
            //        user.Salt = reader.GetString("salt");
            //        conn.Close();
            //        MessageSalt msalt = new MessageSalt(password, user.Salt);
            //        msalt.GenerateHash(msalt);

            //        if (msalt.message == user.Password)
            //        {
            //            return user;
            //        }

            //    }
            //}
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
                    Gamer tempGamer = databaseQuery.GetGamerInfo(gamer);
                    BracketPlayer bracketPlayer = new BracketPlayer();
                    bracketPlayer.BracketID = bracket.BracketID;
                    bracketPlayer.HashedUserID = tempGamer.HashedUserID;
                    databaseQuery.InsertBracketPlayer(bracketPlayer);
                    databaseQuery.IncrementBracketPlayerCount(bracket);
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
            using (conn = new MySqlConnection(CONNECTION_STRING))
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
    }
}
