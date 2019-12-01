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
            //this.email;
            return false;//never work
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
                    break;
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
