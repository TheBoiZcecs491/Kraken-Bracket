using System;
using System.Collections.Generic;
using System.Text;

namespace TBZ.KrakenBracket.DataHelpers
{
    public class User
    {
        public int SystemID { get; set; } //IMMUTABLE
        public string FirstName { get; set; } //attr
        public string LastName { get; set; } //attr
        public string Email { get; set; } // update email thingy
        public string Password { get; set; } // securely update pass
        public string Salt { get; set; } // also part of the above
        public string AccountType { get; set; } //if ur admin or not
        public bool AccountStatus { get; set; } //enable/disable account
        public string ErrorMessage { get; set; } //something borked

        //TODO: make all these attributes, or most of them,
        // into a data structure that can have multiple atributes
        // defined by a string. the jist is that I should be able to
        // have only one method here that can update a value based on
        // any arbitrary variable name, defined by a string.

        public User() { }

        public User(int sysID, string fName, string lName, string email, string pass, string salt, string accntType, bool accntStatus, string errMsg)
        {
            SystemID = sysID;
            FirstName = fName;
            LastName = lName;
            Email = email;
            Password = pass;
            Salt = salt;
            AccountType = accntType;
            AccountStatus = accntStatus;
            ErrorMessage = errMsg;

        }
    }
}