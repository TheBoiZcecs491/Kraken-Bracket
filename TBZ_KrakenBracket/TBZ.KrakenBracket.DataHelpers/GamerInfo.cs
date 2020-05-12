namespace TBZ.KrakenBracket.DataHelpers
{
    public class GamerInfo
    {
        public string HashedUserID { get; set; }
        public string GamerTag { get; set; }
        public int GamerTagID { get; set; }
        public int TeamID { get; set; }
        public int BracketCount { get; set; }
        public string Region { get; set; }
        public GamerInfo() { }
        public GamerInfo(string gamerTag, int bracketCount, string region)
        {
            GamerTag = gamerTag;
            BracketCount = bracketCount;
            Region = region;
        }
        public GamerInfo(string hashedUserID, string gamerTag, int gamerTagID, int teamID)
        {
            HashedUserID = hashedUserID;
            GamerTag = gamerTag;
            GamerTagID = gamerTagID;
            TeamID = teamID;
        }
    }
}
