using System;
using System.Collections.Generic;
using System.Text;

namespace TBZ.RegistrationService
{
    public class RegistrationInfo
    {
        //because the need for extra info, such as a first and last name,
        //is not actually required to make the account registration work.
        //I desided to make this its own class just in case we wanted to
        //add extra account registration requirements.
        private string fName;
        public string FirstName
        {
            get
            {
                return fName;
            }
        }
        private string lName;
        public string LastName
        {
            get
            {
                return lName;
            }
        }

        public RegistrationInfo(string x, string y)
        {
            this.fName = x;
            this.lName = y;
        }
    }   
}
