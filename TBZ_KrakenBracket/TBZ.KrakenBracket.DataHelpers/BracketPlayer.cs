using System;
using System.Collections.Generic;
using System.Text;

namespace TBZ.KrakenBracket.DataHelpers
{
    public class BracketPlayer
    {
        public int BracketID { get; set; }
        public string HashedUserID { get; set; }
        public int RoleID { get; set; }
        public int Placement { get; set; }
        public int Score { get; set; }
        public int StatusCode { get; set; }
        public BracketPlayer() { }
        public BracketPlayer(int bracketID, string hashedUserID, int roleID, int placement, int score)
        {
            BracketID = bracketID;
            HashedUserID = hashedUserID;
            RoleID = roleID;
            Placement = placement;
            Score = score;
        }
    }
}
