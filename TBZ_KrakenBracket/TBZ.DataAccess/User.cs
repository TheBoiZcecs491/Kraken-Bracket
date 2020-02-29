using System;
using System.Collections.Generic;
using System.Text;

namespace TBZ.DatabaseAccess
{
    public class User
    {
        public int SystemID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccountType { get; set; }
        public bool AccountStatus { get; set; }

        public string ErrorMessage { get; set; }

        public User() { }

        public User(int sysID, string fName, string lName, string email, string pass, string accntType, bool accntStatus, string errMsg)
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
}
