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
    }
}
