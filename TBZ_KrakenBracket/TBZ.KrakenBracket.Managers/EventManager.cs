﻿using System;
using System.Collections.Generic;
using System.Text;
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
            return _eventDataAccess.insertEvent(eventObj);
        }

        public List<EventInfo> GetAllEvents()
        {
            return _eventDataAccess.GetAllEvents();
        }

        public object GetEventByID(int eventID)
        {
            return _eventDataAccess.GetEventByID(eventID);
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
            foreach(int id in BracketIDs)
            {
                Bracket = _tournamentBracketDataAccess.GetBracketByID(id);
                listOfBrackts.Add(Bracket);
            }
            return listOfBrackts;
        }

    }
}