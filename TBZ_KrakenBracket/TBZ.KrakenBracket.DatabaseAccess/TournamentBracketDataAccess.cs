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
                        return reader.Read() ? reader.GetInt32(0) : -1; // Why is this not reading?
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: ", e);
            }
            return -1;
        }

        public bool InsertNewBracket(BracketInfo bracketFields)
        {
            bracketFields.BracketID = GetLatestBracketID();
            try
            {
                using (conn = new MySqlConnection(CONNECTION_STRING))
                {
                    // TODO: Get the last BracketID (PRIMARY KEY) needed for this insert
                    string insertQuery = string.Format("INSERT INTO bracket_info " +
                        "(bracketID, bracket_name, bracketTypeID, number_player, game_played, gaming_platform, rules, start_date, end_date )" +
                        "VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})",
                        bracketFields.BracketID, bracketFields.BracketName, bracketFields.BracketTypeID, bracketFields.PlayerCount,
                        bracketFields.GamePlayed, bracketFields.GamingPlatform, bracketFields.Rules, bracketFields.StartDate, bracketFields.EndDate);
                    MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                    MySqlDataReader reader;
                    conn.Open();
                    reader = insertCmd.ExecuteReader();
                    // insertCmd.ExecuteNonQuery();
                    conn.Close();
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
            try
            {
                using (conn = new MySqlConnection(CONNECTION_STRING))
                {
                    string updateQuery = string.Format("UPDATE bracket_info " +
                        "SET(bracket_name, bracketTypeID, number_player, game_played, gaming_platform, rules, start_date, end_date)" +
                        "VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}) WHERE bracketID = {8}",
                        bracketFields.BracketName, bracketFields.BracketTypeID, bracketFields.PlayerCount, bracketFields.GamingPlatform,
                        bracketFields.Rules, bracketFields.StartDate, bracketFields.EndDate, bracketFields.BracketID);
                    MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                    conn.Open();
                    updateCmd.ExecuteNonQuery();
                    conn.Close();
                }
                return true;
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
