using System;
using System.Security.Cryptography;
using TBZ.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.StringChecker;
using TBZ.UM_Manager;

namespace TBZ.RegistrationManager
{
    static class Constants
    {
        //these are used in string comparisons to check for email validiation
        public const string validChars = "!#$%&\\*+-/=?^_`{|}qwertyuiopasdfghjklzxcvbnm.1234567890@";
        public const string validChars2 = "qwertyuiopasdfghjklzxcvbnm.1234567890@";

        //more comparision strings used to check password requirements
        public const string lowercaseChars = "qwertyuiopasdfghjklzxcvbnm";
        public const string uppercaseChars = "QWERTYUIOPASDFGHJKLZXCVBNM";
        public const string numberChars = "1234567890";
        public const string specialChars = " -=[];\',./\\`~!@#$%^&*()_+{}|:\"<>?";
        //the email '@' sign.
        public const char emailDelim = '@';
        public const int emailMaxLength = 200;
        public const int passwdMaxLength = 2000;
    }
    public class Registration
    {

        private readonly UserManagementManager _userManagementManager = new UserManagementManager();
        private readonly DataAccess _databaseAccess = new DataAccess();
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider(); //for the salt

        public User selfRegister(User user)
        {
            User gateAdmin = new User(0, null, null, null, null, null, "System Admin", false, null);
            user.AccountStatus = true;
            user.AccountType = "User";
            byte[] randomNumber = new byte[16];
            rngCsp.GetBytes(randomNumber);
            user.Salt = System.Text.Encoding.Default.GetString(randomNumber);
            //HACK: this is fixed in another branch, so for now this will HOPEFULLY
            // keep away any possible collisions. when that happend comment out the next 2 lines.
            Random rnd = new Random();
            user.SystemID = rnd.Next(Int32.MinValue, Int32.MaxValue);
            StringCheckerService stringChecker = new StringCheckerService(user.Email);
            if (user.FirstName.Equals("") | user.LastName.Equals("")) user.ErrorMessage = "name fields blank";
            else if (stringChecker.isValidEmail2())
            {
                if (isEmailAlreadyRegistered(user.Email)) user.ErrorMessage = "email already registered";
                else
                {
                    _userManagementManager.SingleCreateUsers(gateAdmin, user);
                    if (!isEmailAlreadyRegistered(user.Email)&&user.ErrorMessage.Equals("")) user.ErrorMessage = "email failed to register";
                }
            }
            else user.ErrorMessage = "email malformed";
            return user;
        }

        public bool isEmailAlreadyRegistered(string email)
        {
            return _databaseAccess.GetUserByEmail(email) != null;
        }
    }
}