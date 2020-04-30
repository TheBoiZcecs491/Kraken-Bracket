using System;
using System.Collections.Generic;
using System.Text;
using TBZ.DatabaseAccess;

namespace TBZ.KrakenBracket.Services
{
    public class AuthenticationService
    {
        private readonly DataAccess _dataAccess = new DataAccess();
        public bool ComparePasswords(string email, string password)
        {
            return _dataAccess.ComparePasswords(email, password);
        }
    }
}
