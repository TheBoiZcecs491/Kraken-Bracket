using System;
using TBZ_StringChecker;


namespace TBZ_RegistrationService
{
    public class RegistrationService
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

        public RegistrationService(string x, string y, string z, string w)
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

        //TODO: use String checker for this part.
        public bool isValidEmail()
        {
            StringChecker dis = new StringChecker(this.email);
            return dis.isValidEmail();
            //the code that origonaly handled this was moved to a new module
        }

        //TODO: same story as the above TODO.
        public bool isSecurePassword()
        {
            StringChecker dis = new StringChecker(this.passwd);
            return dis.isSecurePassword();
            //the code that origonaly handled this was moved to a new module
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
