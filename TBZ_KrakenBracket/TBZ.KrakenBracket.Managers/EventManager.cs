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
            eventObj.EventID = _eventDataAccess.getLatestID();

            eventPlayer.EventID = eventObj.EventID;
            eventPlayer.HashedUserID = eventObj.Host;
            eventPlayer.RoleID = 0;

            _eventDataAccess.InsertEventPlayer(eventPlayer);
            
            return eventObj;
        }

        public List<EventInfo> GetAllEvents()
        {
            return _eventDataAccess.GetAllEvents();
        }

        public object GetEventByID(int eventID)
        {
            return _eventDataAccess.GetEventByID(eventID);
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

    }
}
