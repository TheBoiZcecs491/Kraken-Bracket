using System;
using System.Collections.Generic;
using System.Text;

namespace TBZ.KrakenBracket.DataHelpers
{
    public class BracketPlayer
    {
        public int BracketID { get; set; }
        public int HashedUserID { get; set; }
        public int RoleID { get; set; }
        public int Placement { get; set; }
        public int Score { get; set; }
        public BracketPlayer() { }
        public BracketPlayer(int bracketID, int hashedUserID, int roleID, int placement, int score)
        {
            BracketID = bracketID;
            HashedUserID = hashedUserID;
            RoleID = roleID;
            Placement = placement;
            Score = score;
        }
    }
}
