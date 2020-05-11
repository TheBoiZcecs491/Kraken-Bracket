using System;
using System.Collections.Generic;
using System.Text;

namespace TBZ.KrakenBracket.DataHelpers
{
    public class BracketCompetitor
    {
        public int BracketID { get; set; }
        public int Score { get; set; }
        public int Placement { get; set; }
        public string GamerTag { get; set; }
        public string HashedUserID { get; set; }

        public BracketCompetitor() { }
        public BracketCompetitor(int bracketID, int score, int placement, string gamerTag, string hashedUserID)
        {
            BracketID = bracketID;
            Score = score;
            Placement = placement;
            GamerTag = gamerTag;
            HashedUserID = hashedUserID;
        }
    }
}
