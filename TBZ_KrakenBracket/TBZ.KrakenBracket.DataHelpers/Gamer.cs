using System;
using System.Collections.Generic;
using System.Text;

namespace TBZ.KrakenBracket.DataHelpers
{
    public class Gamer
    {
        public int HashedUserID { get; set; }
        public string GamerTag { get; set; }
        public int GamerTagID { get; set; }
        public int TeamID { get; set; }
        public Gamer() { }
        public Gamer(int hashedUserID, string gamerTag, int gamerTagID, int teamID)
        {
            HashedUserID = hashedUserID;
            GamerTag = gamerTag;
            GamerTagID = gamerTagID;
            TeamID = teamID;
        }
    }
}
