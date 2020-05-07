﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using TBZ.DatabaseConnectionService;
using TBZ.DatabaseQueryService;
using TBZ.KrakenBracket.DataHelpers;

namespace TBZ.KrakenBracket.DatabaseAccess
{

    public class EventDataAccess
    {
        DatabaseQuery databaseQuery = new DatabaseQuery();
        Database DB = new Database();
        private MySqlConnection conn;

        public EventInfo InsertEvent(EventInfo eventObj)
        {
            databaseQuery.InsertEvent(eventObj);
            return eventObj;
        }

        public EventPlayerInfo InsertEventPlayer( EventPlayerInfo eventPlayer)
        {
            databaseQuery.InsertEventPalyer(eventPlayer);
            return eventPlayer;
        }

        public bool CheckEventExistByID(int eventID)
        {
            using (conn = new MySqlConnection(DB.GetConnString()))
            {
                string selectQuery = string.Format("SELECT * FROM event_info WHERE eventID={0}", eventID);
                MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);

                conn.Open();

                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        reader.Close();
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        conn.Close();
                        return false;
                    }
                }
            }
        }

        public int getLatestID()
        {
            using (conn = new MySqlConnection(DB.GetConnString()))
            {
                string selectQuery = "SELECT * FROM event_info ORDER BY eventID DESC LIMIT 1";
                MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                conn.Open();
                int eventID;
                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    reader.Read();
                    eventID = reader.GetInt32("eventID");
                }
                conn.Close();
                return eventID;
            }
        }

        public String GetEventHost(int eventID)
        {
            int roleID = 0; //change this when host roleID changes in the database
            using (conn = new MySqlConnection(DB.GetConnString()))
            {
                string selectQuery = string.Format("SELECT gamerTag FROM event_info " +
                    "inner join event_player_info on event_info.eventID=event_player_info.eventID " +
                    "inner join gamer_info on event_player_info.hashedUserID=gamer_Info.hashedUserID " +
                    "WHERE event_info.eventID={0} and roleID={1}", eventID, roleID);
                MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                conn.Open();
                string gamerTag;
                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    reader.Read();
                    gamerTag = reader.GetString("gamerTag");
                }
                conn.Close();
                return gamerTag;
            }
        }

        public EventInfo GetEventByID(int eventID)
        {
            bool eventExist = CheckEventExistByID(eventID);
            if (!eventExist) return null;
            else
            {
                using (conn = new MySqlConnection(DB.GetConnString()))
                {
                    string selectQuery = string.Format("SELECT * FROM event_info WHERE eventID={0}", eventID);
                    MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                    conn.Open();
                    EventInfo eventObj = new EventInfo();
                    using (MySqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        reader.Read();
                        eventObj.EventID = reader.GetInt32("eventID");
                        eventObj.EventName = reader.GetString("event_name");
                        eventObj.Address = reader.GetString("address");
                        eventObj.Description = reader.GetString("event_description");
                        eventObj.StartDate = reader.GetDateTime("start_date");
                        eventObj.EndDate = reader.GetDateTime("end_date");
                        eventObj.StatusCode = reader.GetInt32("status_code");
                        eventObj.Reason = reader.GetString("reason");
                    }
                    conn.Close();
                    eventObj.Host = GetEventHost(eventID);
                    return eventObj;
                }
            }
        }

        public EventPlayerInfo GetEventPlayerInfo(int eventID, int userID)
        {
            using (conn = new MySqlConnection(DB.GetConnString()))
            {
                string selectQuery = string.Format("SELECT * FROM event_player_info WHERE eventID={0} and hashedUserID={1}", eventID, userID);
                MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                conn.Open();
                EventPlayerInfo eventObj = new EventPlayerInfo();
                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    reader.Read();
                    eventObj.EventID = reader.GetInt32("eventID");
                    eventObj.HashedUserID = reader.GetString("hashedUserID");
                    eventObj.RoleID = reader.GetInt32("roleID");
                    eventObj.Claim = reader.GetString("claim");
                    eventObj.StatusCode = reader.GetInt32("status_code");
                    eventObj.reason = reader.GetString("reason");
                }
                conn.Close();
                return eventObj;
            }
        }

        public List<EventInfo> GetAllEvents()
        {
            using (conn = new MySqlConnection(DB.GetConnString()))
            {
                string selectQuery = string.Format("SELECT * FROM event_info");
                MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                conn.Open();
                List<EventInfo> listOfEvents = new List<EventInfo>();
                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EventInfo eventObj = new EventInfo();
                        eventObj.EventID = reader.GetInt32("eventID");
                        eventObj.EventName = reader.GetString("event_name");
                        eventObj.Address = reader.GetString("address");
                        eventObj.Description = reader.GetString("event_description");
                        eventObj.StartDate = reader.GetDateTime("start_date");
                        eventObj.EndDate = reader.GetDateTime("end_date");
                        eventObj.Reason = reader.GetString("reason");
                        eventObj.Host = GetEventHost(eventObj.EventID);
                        listOfEvents.Add(eventObj);
                    }
                }
                conn.Close();
                return listOfEvents;
            }
        }

        public EventBracketList AddBracketToEvent(EventBracketList eventBracket)
        {
            databaseQuery.InsertEventBracket(eventBracket);
            return eventBracket;
        }

        public List<int> GetAllBracktsInEvent(int eventID)
        {
            using (conn = new MySqlConnection(DB.GetConnString()))
            {
                string selectQuery = string.Format("SELECT * FROM event_bracket_list WHERE eventID={0}", eventID);
                MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                conn.Open();
                List<int> listOfBracketsInEvent = new List<int>();
                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int bracketID = reader.GetInt32("bracketID");
                        listOfBracketsInEvent.Add(bracketID);
                    }
                }
                conn.Close();
                return listOfBracketsInEvent;
            }
        }

        /// <summary>
        /// Reads the events whose name contains the search request.
        /// </summary>
        /// <param name="eventRequest"> String of event request </param>
        /// <returns> A list of Events </returns>
        public List<EventInfo> ReadEvents(string eventRequest)
        {
            var DB = new Database();
            var listOfEvents = new List<EventInfo>();
            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                string selectQuery = string.Format("SELECT * FROM event_info WHERE event_name LIKE \'%{0}%\'", eventRequest);
                Console.WriteLine(selectQuery);
                MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                conn.Open();
                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EventInfo eventObj = new EventInfo();
                        eventObj.EventID = reader.GetInt32("eventID");
                        eventObj.EventName = reader.GetString("event_name");
                        eventObj.Address = reader.GetString("address");
                        eventObj.StartDate = reader.GetDateTime("start_date");
                        eventObj.EndDate = reader.GetDateTime("end_date");

                        listOfEvents.Add(eventObj);
                    }
                }
                conn.Close();
            }
            return listOfEvents;
        }
    }
}
