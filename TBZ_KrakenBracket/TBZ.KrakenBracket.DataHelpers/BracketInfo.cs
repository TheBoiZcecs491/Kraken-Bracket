using System;
using System.Collections.Generic;
using System.Text;

namespace TBZ.KrakenBracket.DataHelpers
{
    public class BracketInfo
    {
        public int BracketID { get; set; }
        public string BracketName { get; set; }
        public int BracketTypeID { get; set; }
        public int PlayerCount { get; set; }
        public string GamePlayed { get; set; }
        public string GamingPlatform { get; set; }
        public string Rules { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusCode { get; set; }

        public BracketInfo() { }
        public BracketInfo(int bracketID, string bracketName, int bracketTypeID, int playerCount, string gamePlayed, string gamingPlatform,
            string rules, DateTime startDate, DateTime endDate, int statusCode)
        {
            BracketID = bracketID;
            BracketName = bracketName;
            BracketTypeID = bracketTypeID;
            PlayerCount = playerCount;
            GamePlayed = gamePlayed;
            GamingPlatform = gamingPlatform;
            Rules = rules;
            StartDate = startDate;
            EndDate = endDate;
            StatusCode = statusCode;
        }
    }
}
