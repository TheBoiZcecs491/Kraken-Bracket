using System;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TBZ_StringChecker
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

        //regex stuffs
        public const string DomainNormalizer = @"(@)(.+)$";
        public const string emailPattern =
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$"
            ;
    }
    public class StringChecker
    {
        //Okay so this is where ima write my string checker
        //the jist is that this will centralise all the algos that govern string validation
        //umm... I should learn the rulez first. XDD
        private string theString;
        public string FirstName
        {
            get
            {
                return theString;
            }
        }
        public StringChecker(string x)
        {
            this.theString = x;
        }
        //so my plan here is to make this a rather simple object. you create it out of any string.
        //and then from there you can use the included methods to anaylize it.
        //most of them will just return a boolean.

        public bool isEmpty()
        {
            return this.theString.CompareTo("") == 0;
            //I THINK that is how this work, but this is an example of how I plan to do this.
        }

        //anyway ima just copy pasta the methods I had in Registration service.
        //for now, but at some point I wana make this extra shiny.
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

            string compRes = this.theString.ToLower();
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
            string compRes = this.theString;

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

        public bool isValidEmail2()
        {
            //me writing the email checker, part 2 electric boogalo.
            //also I MAY have nicked this from https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
            //dont worry ima make it look like I did NOT copy it enterly.
            string copied = this.theString;
            //is it NOT empty?
            if (!string.IsNullOrWhiteSpace(copied) & !(copied.Length>Constants.emailMaxLength))
            {
                try
                {
                    copied = Regex.Replace(
                        copied,
                        Constants.DomainNormalizer,
                        this.DomainMapper,
                        RegexOptions.None,
                        TimeSpan.FromMilliseconds(200)
                        );
                }
                catch (RegexMatchTimeoutException e) { return false; }
                catch (ArgumentException e) { return false; }
                try
                {
                    return Regex.IsMatch(
                        copied,
                        Constants.emailPattern,
                        RegexOptions.IgnoreCase,
                        TimeSpan.FromMilliseconds(250)
                        );
                }
                catch (RegexMatchTimeoutException) { return false; }

            }
            else { return false; }

        }
        private string DomainMapper(Match match)
        {
            // Use IdnMapping class to convert Unicode domain names.
            var idn = new IdnMapping();

            // Pull out and process domain name (throws ArgumentException on invalid)
            var domainName = idn.GetAscii(match.Groups[2].Value);

            return match.Groups[1].Value + domainName;
        }
    }
}