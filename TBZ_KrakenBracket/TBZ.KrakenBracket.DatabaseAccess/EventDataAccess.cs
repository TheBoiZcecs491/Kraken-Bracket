using MySql.Data.MySqlClient;
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

        public EventInfo insertEvent(EventInfo eventObj)
        {
            databaseQuery.InsertEvent(eventObj);
            return eventObj;
        }

        public bool checkEventExistByID(int eventID)
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

        public EventInfo getEventByID(int eventID)
        {
            bool eventExist = checkEventExistByID(eventID);
            if (!eventExist) return null;
            else
            {
                using (conn = new MySqlConnection(DB.GetConnString()))
                {
                    string selectQuery = string.Format("SELECT * FROM event_info WHERE eventID={0}", eventID);
                    MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                    conn.Open();
                    using (MySqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        EventInfo eventObj = new EventInfo();
                        reader.Read();
                        eventObj.EventID = reader.GetInt32("eventID");
                        eventObj.HashedUserID = reader.GetString("hashedUserID");
                        eventObj.EventName = reader.GetString("event_name");
                        eventObj.Address = reader.GetString("address");
                        eventObj.Description = reader.GetString("description");
                        eventObj.StartDate = reader.GetDateTime("start_date");
                        eventObj.EndDate = reader.GetDateTime("end_date");
                        eventObj.Reason = reader.GetString("reason");
                        conn.Close();
                        return eventObj;
                    }
                }
            }
        }

        public List<EventInfo> getAllEvents()
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
                        eventObj.HashedUserID = reader.GetString("hashedUserID");
                        eventObj.EventName = reader.GetString("event_name");
                        eventObj.Address = reader.GetString("address");
                        eventObj.Description = reader.GetString("description");
                        eventObj.StartDate = reader.GetDateTime("start_date");
                        eventObj.EndDate = reader.GetDateTime("end_date");
                        eventObj.Reason = reader.GetString("reason");
                        listOfEvents.Add(eventObj);
                    }
                }
                return listOfEvents;
            }
        }

        public 

        public List<int> getAllBracktsInEvent(int eventID)
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
                return listOfBracketsInEvent;
            }
        }

    }
}
