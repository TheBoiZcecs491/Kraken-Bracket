using System;
using System.Collections.Generic;
using System.Text;

namespace TBZ_RegistrationService
{
    class RegistrationInfo
    {
        //because the need for extra info, such as a first and last name,
        //is not actually required to make the account registration work.
        //I desided to make this its own class just in case we wanted to
        //add extra account registration requirements.
        private string fName = string.Empty;
        private string lName = string.Empty;

        public RegistrationInfo(string x, string y)
        {
            this.fName = x;
            this.lName = y;
        }
    }
}
