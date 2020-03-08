using System;
using System.Collections.Generic;
using System.Text;

namespace TBZ.DatabaseAccess
{
    public class User
    {
        public uint SystemID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccountType { get; set; }
        public bool AccountStatus { get; set; }

        public string ErrorMessage { get; set; }

        public User() { }

        public User(uint sysID, string fName, string lName, string email, string pass, string accntType, bool accntStatus, string errMsg)
        {
            SystemID = sysID;
            FirstName = fName;
            LastName = lName;
            Email = email;
            Password = pass;
            AccountType = accntType;
            AccountStatus = accntStatus;
            ErrorMessage = errMsg;

        }
    }

    public class Gamer
    {
        public int HashedUserID { get; set; }
        public string GamerTag { get; set; }
        public int GamerTagID { get; set; }
        public int TeamID { get; set; }
    }

    public class Bracket
    {
        public int BracketID { get; set; }
        public string BracketName { get; set; }
        public int BracketTypeID { get; set; }
        public int NumberPlayer { get; set; }
        public string GamePlayed { get; set; }
        public string GamingPlatform { get; set; }
        public string Rules { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class BracketPlayer
    {
        public int BracketID { get; set; }
        public int HashedUserID { get; set; }
        public int RoleID { get; set; }
        public int Placement { get; set; }
        public int Score { get; set; }
    }

    public class Event
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }


}
