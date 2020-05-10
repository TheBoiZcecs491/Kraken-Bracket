using System;
using System.Collections.Generic;
using System.Text;

namespace TBZ.KrakenBracket.DataHelpers
{
    public class ProfileUpdateInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public bool AccountStatus { get; set; }

        public ProfileUpdateInput() { }

        public ProfileUpdateInput(string fName, string lName, string email, string pass, string newPassword, bool accountStatus)
        {
            FirstName = fName;
            LastName = lName;
            Email = email;
            Password = pass;
            NewPassword = newPassword;
            AccountStatus = accountStatus;
        }
    }
}
