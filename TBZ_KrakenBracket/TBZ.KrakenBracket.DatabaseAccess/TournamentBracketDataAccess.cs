using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
