using System;
using System.Collections.Generic;
using System.Text;
using TBZ.DatabaseQueryService;
using TBZ.KrakenBracket.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;

namespace TBZ.KrakenBracket.Managers
{
    public class EventManager
    {
        private readonly LoggingManager _loggingManager = new LoggingManager();
        private readonly EventDataAccess _eventDataAccess = new EventDataAccess();
        private readonly TournamentBracketDataAccess _tournamentBracketDataAccess = new TournamentBracketDataAccess();

        public EventInfo CreateEvent(EventInfo eventObj)
        {
            try
            {
                EventPlayerInfo eventPlayer = new EventPlayerInfo();
                //TODO: change to systemID to hash id
                //eventPlayer.hashedUserID = databaseQuery.GetHashedUserID(Int32.Parse(eventObj.Host));
                _eventDataAccess.InsertEvent(eventObj);
                eventObj.EventID = _eventDataAccess.GetLatestID();

                eventPlayer.EventID = eventObj.EventID;
                eventPlayer.HashedUserID = eventObj.Host;
                eventPlayer.RoleID = 0;
                eventPlayer.Claim = "";

                _eventDataAccess.InsertEventPlayer(eventPlayer);
                _loggingManager.Log("Event Creation", "");

                return eventObj;
            } catch (Exception e)
            {
                _loggingManager.Log("Event Creation", "Data Store Error");
                throw e;
            }
        }

        public EventInfo UpdateEvent(EventInfo eventObj)
        {
            return _eventDataAccess.UpdateEvent(eventObj);
        }

        public List<EventInfo> GetAllEvents()
        {
            List<EventInfo> events = new List<EventInfo>();
            events = _eventDataAccess.GetAllEvents();
            foreach( EventInfo eventObj in events)
            {
                eventObj.Host = _eventDataAccess.GetEventHost(eventObj.EventID);
            }
            return events;
        }

        public List<EventInfo> GetAllCurrentEvents()
        {
            List<EventInfo> events = new List<EventInfo>();
            DateTime currentTime = DateTime.Now;
            currentTime.ToString("yyyyMMddTHH:mm:ss");
            events = _eventDataAccess.GetAllEvents();
            foreach (EventInfo eventObj in events)
            {
                if (eventObj.StatusCode == 0)
                {
                    events.Remove(eventObj);
                }
                if (eventObj.StatusCode == 2)
                {
                    if(eventObj.StartDate < currentTime)
                    {
                        eventObj.StatusCode = 1;
                    }
                    else
                    {
                        events.Remove(eventObj);
                    }
                }
            }
            foreach (EventInfo eventObj in events)
            {
                eventObj.Host = _eventDataAccess.GetEventHost(eventObj.EventID);
            }
            return events;
        }

        public object AddGamerToEvent(int eventID, string hashedUserID)
        {
            EventPlayerInfo eventPlayer = new EventPlayerInfo();
            eventPlayer.EventID = eventID;
            eventPlayer.HashedUserID = hashedUserID;
            eventPlayer.RoleID = 1;
            eventPlayer.Claim = "";

            return _eventDataAccess.AddGamerToEvent(eventPlayer);
        }

        public object CheckEventPlayer(int eventID, string hashUserID)
        {
            return _eventDataAccess.CheckEventPlayer(eventID, hashUserID);
        }

        public object GetEventByID(int eventID)
        {
            var EventByID = _eventDataAccess.GetEventByID(eventID);
            return EventByID;
        }

        public object GetEventPlayer(int eventID)
        {
            return _eventDataAccess.GetAllEventPlayerInfoByID(eventID);
        }

        public object GetEventHost(int eventID)
        {
            return _eventDataAccess.GetEventHost(eventID);
        }

        public object GetEventPlayerByID(int eventID, int playerID)
        {
            return _eventDataAccess.GetEventPlayerInfo(eventID, playerID);
        }

        public EventBracketList AddBracketToEvent(EventBracketList eventBracket)
        {
            return _eventDataAccess.AddBracketToEvent(eventBracket);
        }

        public object RemoveGamerFromEvent(int eventID, string hashedUserID)
        {
            return _eventDataAccess.RemoveEventPlayer(eventID, hashedUserID);
        }

        public List<BracketInfo> GetBracketsInEvent(int eventID)
        {
            List<int> BracketIDs = _eventDataAccess.GetAllBracktsInEvent(eventID);
            List<BracketInfo> listOfBrackts = new List<BracketInfo>();
            BracketInfo Bracket = new BracketInfo();
            foreach (int id in BracketIDs)
            {
                Bracket = _tournamentBracketDataAccess.GetBracketByID(id);
                listOfBrackts.Add(Bracket);
            }
            return listOfBrackts;
        }

        public List<GamerInfo> GetEventCompetitors(int eventID)
        {
            return _eventDataAccess.GetAllEventCompetitorsBy(eventID);

        }
    }
}
