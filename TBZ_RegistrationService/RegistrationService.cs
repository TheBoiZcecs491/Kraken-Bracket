using System;

namespace TBZ_RegistrationService
{
    public class RegistrationService
    {

        private string email = string.Empty;
        private string passwd = string.Empty;
        private RegistrationInfo extras ;

        public RegistrationService(string x, string y, string z, string w)
        {
            this.email = x;
            this.passwd = y;
            this.extras = new RegistrationInfo(z, w);
        }

        public bool isValidEmail()
        {
            //TODO: check if the email is legit.
            // so the valid pattern is something@website.com
            // case sensitivity shouldnt matter
            // A-Z and a-z
            // 0-9
            // !#$%&'*+-/=?^_`{|}~
            // . but not at the ends
            // only use the @ character once.
            // 64 octects in length (characters)
            //HACK: this is not the officual way to do it but it works well enough

            string compRes = this.email;
            compRes.ToLower();
            bool result = true;
            char look;
            char lookPrior = '\0';
            bool foundAtSign = false;
            string validChars = "!#$%&\\*+-/=?^_`{|}qwertyuiopasdfghjklzxcvbnm.1234567890@";
            string validChars2 = "qwertyuiopasdfghjklzxcvbnm.1234567890@";

            for (int i = 0; i<compRes.Length;i++){
                look = compRes[i];
                if ((i == 0 | i==(compRes.Length-1) | lookPrior=='.') & look == '.') { result = false; break; }
                else if (foundAtSign & look == '@') { result = false; break; }
                else if (!foundAtSign & !validChars.Contains(look)) { result = false; break; }
                else if (!validChars2.Contains(look)) { result = false; break; }
                if (look == '@') { foundAtSign = true; }
                lookPrior = look;
            }
            return result;
        }

        public bool isSecurePassword()
        {
            //TODO: same deal, this is where i'd check if the password is secure enough.
            //this.passwd;
            return false;//never work
        }
        public bool matchesPasswd(string x)
        {
            return this.passwd.Equals(x);
        }

        public bool emailExistsIn(string[] x)
        {
            bool result = false;
            foreach(string i in x)
            {
                if (this.email.Equals(i)) {
                    result = true;
                }
            }
            return result;
        }

        public bool storeUser(string x, string[] y)
        {
            //TODO: this would be called to store the user in the DB.
            //and it returns a boolean to indicate if it worked or not.
            bool verify =
                this.isValidEmail() &
                this.isSecurePassword() &
                this.matchesPasswd(x) &
                this.emailExistsIn(y)
                ;
            if (verify)
            { 
                //store the user idk.
            }

            return verify;
        }
    }
}
