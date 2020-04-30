﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TBZ.KrakenBracket.DataHelpers
{
    public class EventInfo
    {
        public int EventID { get; set; }
        public string HashedUserID { get; set; }
        public string EventName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
    }

    public class EventBracketList
    {
        public int EventID { get; set; }
        public int BracketID { get; set; }
    }
}
