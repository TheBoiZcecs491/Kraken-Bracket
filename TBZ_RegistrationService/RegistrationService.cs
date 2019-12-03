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
            //check if the email is legit.
            // so the valid pattern is something@website.com
            // case sensitivity shouldnt matter
            // A-Z and a-z
            // 0-9
            // !#$%&'*+-/=?^_`{|}~
            // . but not at the ends
            // only use the @ character once.
            // 64 octects in length (characters)
            //HACK: this is not the officual way to do it but it works well enough

            string compRes = this.email.ToLower();
            //compRes.ToLower();
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
            //same deal, this is where i'd check if the password is secure enough.
            //HACK: im just going to check if the password can meet the 4 criteria.
            string compRes = this.passwd;
            string lowercaseChars = "qwertyuiopasdfghjklzxcvbnm";
            string uppercaseChars = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string numberChars = "1234567890";
            string specialChars = " -=[];\',./\\`~!@#$%^&*()_+{}|:\"<>?";
            bool hasLower = false;
            bool hasUpper = false;
            bool hasNumber = false;
            bool hasSpecial = false;

            foreach(char i in lowercaseChars)
            {
                if (compRes.Contains(i)) { hasLower = true; break; }
            }
            foreach (char i in uppercaseChars)
            {
                if (compRes.Contains(i)) { hasUpper = true; break; }
            }
            foreach (char i in numberChars)
            {
                if (compRes.Contains(i)) { hasNumber = true; break; }
            }
            foreach (char i in specialChars)
            {
                if (compRes.Contains(i)) { hasSpecial = true; break; }
            }
            
            return (hasLower&hasUpper&hasNumber&hasSpecial&(compRes.Length>=8));
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
            //this would be called to store the user in the DB.
            //and it returns a boolean to indicate if it worked or not.
            bool cond1 = this.isValidEmail();
            bool cond2 = this.isSecurePassword();
            bool cond3 = this.matchesPasswd(x);
            bool cond4 = this.emailExistsIn(y);
            bool verify = cond1&cond2&cond3&!cond4;
            if (verify)
            { 
                //store the user idk.
            }

            return verify;
        }
    }
}
