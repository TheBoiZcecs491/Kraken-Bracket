using System;
using System.Collections.Generic;
using System.Text;

namespace TBZ.KrakenBracket.DataHelpers
{
    public class RegistrationInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string GamerTag { get; set; }

        public RegistrationInput() { }

        public RegistrationInput(string fName, string lName, string email, string pass, string gamerTag)
        {
            FirstName = fName;
            LastName = lName;
            Email = email;
            Password = pass;
            GamerTag = gamerTag;
        }
    }
}
