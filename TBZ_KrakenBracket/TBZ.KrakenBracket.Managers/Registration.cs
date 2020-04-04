using System;
using TBZ.RegistrationInfoDataHelper;

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
        //these two values are needed for login.
        private string email;
        public string Email
        {
            get
            {
                return email;
            }
        }
        private string passwd;
        public string Password
        {
            get
            {
                return passwd;
            }
        }

        private RegistrationInfo extras;
        public RegistrationInfo Extras
        {
            get
            {
                return extras;
            }
        }

        public Registration(string x, string y, string z, string w)
        {
            this.email = x;
            this.passwd = y;
            this.extras = new RegistrationInfo(z, w);
            if (!this.isValidEmail())
            {
                throw new ArgumentException("invalid email syntax");
            }
            if (!this.isSecurePassword())
            {
                throw new ArgumentException("password lacks needed criteria");
            }


        }

        public bool isValidEmail()
        {
            // checks if the email is legit.
            // so the valid pattern is something@website.com
            // case sensitivity shouldnt matter
            // A-Z and a-z
            // 0-9
            // !#$%&'*+-/=?^_`{|}~
            // . but not at the ends
            // only use the @ character once.
            // 64 octects in length (characters)
            //HACK: this is not the officual way to do it but it works well enough
            // it does some basic level syntax checking on an entered email.

            string compRes = this.email.ToLower();
            //compRes.ToLower();
            bool result = true;
            char look;
            char lookPrior = '\0';//this has to be initalized or else it cant compile
            bool foundAtSign = false;

            // this loop passes over the input string once and checks if each character would work there.
            // as soon as it find a violation in the string it breaks and returns a False value.
            for (int i = 0; i < compRes.Length; i++)
            {
                look = compRes[i];
                // is this the 2nd '.' in a row?
                if ((i == 0 | i == (compRes.Length - 1) | lookPrior == '.') & look == '.') { result = false; break; }
                // did we find another '@' sign?
                else if (foundAtSign & look == Constants.emailDelim) { result = false; break; }
                // are we in the name of the email, and is the character correct?
                else if (!foundAtSign & !Constants.validChars.Contains(look)) { result = false; break; }
                // are we in the domain of the email, and is the character correct?
                else if (!Constants.validChars2.Contains(look)) { result = false; break; }
                // did we find the '@' sign, for the first time?
                if (look == Constants.emailDelim) { foundAtSign = true; }
                // store the last character checked, used in the first bit.
                lookPrior = look;
            }
            // check if the email is too long, and THEN return the status.
            return (result & compRes.Length <= Constants.emailMaxLength);
        }

        public bool isSecurePassword()
        {
            // okay here is the thing
            // trying to create an algorithm that checks for a secure password a bit too complex.
            // it can be cone but for now im using a simple character requierment.
            //HACK: im just going to check if the password can meet the 4 criteria.
            string compRes = this.passwd;

            //status bools
            bool hasLower = false;
            bool hasUpper = false;
            bool hasNumber = false;
            bool hasSpecial = false;

            // do we have a lowerCase character?
            foreach (char i in Constants.lowercaseChars)
            {
                if (compRes.Contains(i)) { hasLower = true; break; }
            }
            // do we have an UPPERCASE character?
            foreach (char i in Constants.uppercaseChars)
            {
                if (compRes.Contains(i)) { hasUpper = true; break; }
            }
            // do we have a number?
            foreach (char i in Constants.numberChars)
            {
                if (compRes.Contains(i)) { hasNumber = true; break; }
            }
            // do we have a special printable character?
            foreach (char i in Constants.specialChars)
            {
                if (compRes.Contains(i)) { hasSpecial = true; break; }
            }
            // check if the password is too long, and return the status
            return (hasLower & hasUpper & hasNumber & hasSpecial & (compRes.Length >= 8 & compRes.Length <= Constants.passwdMaxLength));
        }
        public bool matchesPasswd(string x)
        {
            //self explanitory, it tells you if the entered string matches this objects stored password.
            return this.passwd.Equals(x);
        }

        public bool emailExistsIn(string[] x)
        {
            // this tells you if the objects stored email exists in the provided array of strings.
            bool result = false;
            foreach (string i in x)
            {
                if (this.email.Equals(i))
                {
                    result = true;
                }
            }
            return result;
        }

        public bool storeUser(string x, string[] y)
        {
            //this would be called to store the user in the DB.
            //and it returns a boolean to indicate if it worked or not.
            //bool cond1 = this.isValidEmail();
            //bool cond2 = this.isSecurePassword();
            if (!this.matchesPasswd(x))
            {
                throw new ArgumentException("passwords do not match");
            }
            if (this.emailExistsIn(y))
            {
                throw new ArgumentException("email exists in the list");
            }

            bool verify;
            // store the user idk. this is where an server query would go.
            // it should pass along the needed info found in this class.
            verify = true;

            return verify;
            // if this object's parameters are correct, it returns true.
            //TODO: function can return false if the server response denys the request.
        }
    }
}