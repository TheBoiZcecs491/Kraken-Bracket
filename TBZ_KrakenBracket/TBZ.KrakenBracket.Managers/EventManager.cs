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
        private readonly EventDataAccess _eventDataAccess = new EventDataAccess();
        private readonly TournamentBracketDataAccess _tournamentBracketDataAccess = new TournamentBracketDataAccess();

        public EventInfo CreateEvent(EventInfo eventObj)
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
            
            return eventObj;
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

        public object GetEventByID(int eventID)
        {
            var EventByID = _eventDataAccess.GetEventByID(eventID);
            return EventByID;
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
