using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TBZ.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.KrakenBracket.Services;

namespace TBZ.KrakenBracket.Managers
{
    public class AuthenticationManager
    {
        private readonly DataAccess _databaseAccess = new DataAccess();
        private readonly AuthenticationService _authenticationService = new AuthenticationService();
        public User Login(string email, string password)
        {
            User user = _databaseAccess.GetUserByEmail(email);
            if (user == null)
            {
                throw new ArgumentException();
            }
            bool passwordCheck = _authenticationService.ComparePasswords(email, password);
            if (passwordCheck) return user;
            else
            {
                throw new ArgumentException();
            }
        }
    }
}