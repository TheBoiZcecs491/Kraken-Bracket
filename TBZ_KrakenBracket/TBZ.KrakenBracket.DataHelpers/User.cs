using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TBZ.KrakenBracket.DataHelpers
{
    public class User
    {
        public int SystemID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string AccountType { get; set; }
        public bool AccountStatus { get; set; }
        public string ErrorMessage { get; set; }

        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider(); //for the salt

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

        public User(RegistrationInput input)
        {
            FirstName = input.FirstName;
            LastName = input.LastName;
            Email = input.Email;
            Password = input.Password;
            byte[] randomNumber = new byte[16];
            rngCsp.GetBytes(randomNumber);
            Salt = System.Text.Encoding.Default.GetString(randomNumber);
            AccountType = "User";
            AccountStatus = true;
        }

        public User(ProfileUpdateInput input)
        {
            FirstName = input.FirstName;
            LastName = input.LastName;
            Email = input.Email;
            Password = input.NewPassword;
            byte[] randomNumber = new byte[16];
            rngCsp.GetBytes(randomNumber);
            Salt = System.Text.Encoding.Default.GetString(randomNumber);
            AccountType = "User";
            AccountStatus = input.AccountStatus;
        }
    }
    
    public static class doAsUser
    {
        public static User systemAdmin()
        {
            return new User(0, null, null, null, null, null, "System Admin", false, null);
        }
        public static User admin()
        {
            return new User(0, null, null, null, null, null, "Admin", false, null);
        }
    }
}