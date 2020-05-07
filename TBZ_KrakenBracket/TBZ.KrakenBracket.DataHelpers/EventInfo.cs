using System;
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
        public int StatusCode { get; set; }
        public string Reason { get; set; }
        public int NumOfBrackets { get; set; }
        public string Host { get; set; }

        public EventInfo() {}
        public EventInfo(string eventName)
        {
            EventName = eventName;
        }
        public EventInfo( int eventID, string eventName, String description, DateTime startDate, DateTime endDate, int statusCode, String reason, String host ) 
        {
            EventID = eventID;
            EventName = eventName;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            StatusCode = statusCode;
            Reason = reason;
            Host = host;
        }
        public EventInfo(string eventName, String description, DateTime startDate, DateTime endDate, String host)
        {
            EventName = eventName;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Host = host;
        }
    }

    public class EventBracketList
    {
        public int EventID { get; set; }
        public int BracketID { get; set; }
    }

    public class EventPlayerInfo
    {
        public int EventID { get; set; }
        public string HashedUserID { get; set; }
        public int RoleID { get; set; }
        public string Claim { get; set; }
        public int StatusCode { get; set; }
        public string reason { get; set; }
    }
}
