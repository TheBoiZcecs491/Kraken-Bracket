﻿using MySql.Data.MySqlClient;
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

        public BracketInfo GetBracket(int bracketID)
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
                        return bracket;
                    }
                }
            }
        }
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

        public User GetUser(string email, string password)
        {
            using (conn = new MySqlConnection(CONNECTION_STRING))
            {
                string selectQuery = string.Format("SELECT * FROM user_information WHERE email='{0}'", email);
                MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                conn.Open();
                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    User user = new User();
                    reader.Read();
                    user.Password = reader.GetString("hashed_password");
                    user.Salt = reader.GetString("salt");
                    MessageSalt msalt = new MessageSalt(password, user.Salt);
                    msalt.GenerateHash(msalt);

                    if (msalt.message == user.Password)
                    {
                        return user;
                    }

                }
            }
            return null;
        }

        public BracketPlayer InsertGamerToBracket(Gamer gamer, int bracketID)
        {
            try
            {
                BracketInfo bracket = GetBracketByID(bracketID);
                if (bracket.PlayerCount >= 128)
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
            catch (Exception e)
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

        public bool InsertNewBracket(BracketInfo bracketFields)
        {
            bracketFields.BracketID = GetLatestBracketID() + 1;
            try
            {
                using (conn = new MySqlConnection(CONNECTION_STRING))
                {
                    using(MySqlCommand insertCmd = conn.CreateCommand())
                    {
                        insertCmd.CommandText = "INSERT INTO bracket_info(bracketID, bracket_name, bracketTypeID, " +
                            "number_player, game_played, gaming_platform, rules, start_date, end_date, status_code )" +
                            "VALUES(@bracketID, @bracket_name, @bracketTypeID, @number_player, @game_played, " +
                            "@gaming_platform, @rules, @start_date, @end_date, @status_code)";
                        insertCmd.Parameters.AddWithValue("@bracketID", bracketFields.BracketID);
                        insertCmd.Parameters.AddWithValue("@bracket_name", bracketFields.BracketName);
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

        public bool UpdateBracket(BracketInfo bracketFields)
        {
            if(!CheckBracketIDExistence(bracketFields.BracketID))
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
