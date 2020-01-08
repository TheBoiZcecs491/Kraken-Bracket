using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TBZ_RegistrationService;

namespace TBZ_registration.Tests
{
    [TestClass]
    public class registrationTest
    {
        [TestMethod]
        public void Registration_validEmailAndPass_Pass()
        {
            //this is a rudementary test to see if the system can detect failed account creations.
            //TODO, come up with more test variables that take weird cases into account.

            //arrange

            //each user's attempt at retyping the password
            string[] rePassword =
            {
                "Wid#$%766",
                "fortyK4K5!($^",
                "j33$M4n"
            };
            //each user's account creation info

            string[] userNames =
            {
                "cutieBoi@aol.com",
                "kernal.enderman.mason@masonsguild.uk",
                "waglfragl@gmail.com"
            };

            string[] userPasswds =
            {
                "Wid#$%766",
                "fortyK3K5!($^",
                "j33$M4n"
            };

            string[] userFNames =
            {
                "Ronald",
                "Cernal",
                "Jonny",
            };

            string[] userLNames =
            {
                "Cornwall",
                "Elderman",
                "Uno"
            };

            var result = true;

            //emails in our test DB
            string[] emailsInTheDB =
            {
                "personman1000@online.net",
                "sassykitty9998@hotmail.com"
            };

            //the expected results of each test.
            bool[] corrects =
            {
                true,
                false,
                false
            };

            //act

            //run each test
            try
            {
                RegistrationService Account;
                for(int i=0;i<corrects.Length;i++)
                {
                    try
                    {
                        Account = new RegistrationService(userNames[i], userPasswds[i], userFNames[i], userLNames[i]);
                        Account.storeUser(rePassword[i], emailsInTheDB);
                        if (corrects[i] != true) result = false;
                    }
                    catch (ArgumentException)
                    {
                        if (corrects[i] != false) result = false; 
                    }
                    
                }
            }
            catch (Exception) { result = false; }

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Registration_validEmailGauntlet_Pass()
        {
            //so to test the email validiy checker ima make a list of strings
            //and a list of booleans indicating if that is supposed to be a valid
            //email or not. I got the idea for this from my game theory professor.

            //arrange

            bool result = true;

            // a list of emails people might type in.
            string[] emails =
            {
                "georgelopez@aol.com",
                "sassykitty9998@hotmail.com",
                "1234 Temple.Ave 69420",
                "kevinpootis923@yahoo.com",
                "darren.wallace.staff@gmail.com",
                "E><3nc1b73M45t3r@hackersunited.net"
            };

            //expected test results
            bool[] corrects =
            {//should this email work?
                true,
                false,
                false,
                true,
                false,
                false
            };

            //emails in our test DB
            string[] emailsInTheDB =
            {
                "personman1000@online.net",
                "sassykitty9998@hotmail.com",
                "darren.wallace.staff@gmail.com"
            };

            //act

            //run each test
            try
            {
                for (int i = 0; i < corrects.Length; i++)
                {
                    try
                    {
                        var Account = new RegistrationService(emails[i], "Wid#$%766", "Bob", "Simon");
                        Account.storeUser("Wid#$%766", emailsInTheDB);
                        if (corrects[i] != true) result = false;
                    }
                    catch (ArgumentException)
                    {
                        if (corrects[i] != false) result = false;
                    }

                }
            }
            catch (Exception) { result = false; }

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Registration_validPasswordGauntlet_Pass()
        {
            //so to test the email validiy checker ima make a list of strings
            //and a list of booleans indicating if that is supposed to be a valid
            //email or not. I got the idea for this from my game theory professor.

            //arrange

            bool result = true;

            //list of passwords users might type in
            string[] passwds =
            {
                "123Love",
                "@F@F#*FMWE*Rf8wf734jf",
                "ghg931)(%JDN",
                "gonot2TEHDWARVES3console",
                "d3G^"
            };

            //expected results.
            bool[] corrects =
            {
                false,
                true,
                true,
                false,
                false
            };

            //act

            //run the tests
            try
            {
                for (int i = 0; i < passwds.Length; i++)
                {
                    try
                    {
                        var tempAccount = new RegistrationService("personman1000@online.net", passwds[i], "Bob", "Simon");
                        if (corrects[i] != true) result = false;
                    }
                    catch (ArgumentException)
                    {
                        if (corrects[i] != false) result = false;
                    }
                }
            }
            catch (Exception) { result = false; }

            //assert
            Assert.IsTrue(result);
        }
        [TestMethod]

        public void getMethods_Pass()
        {
            //this was a aftertought, its an implementation of all the get() methods.

            bool result = false;

            //arrange

            //someones registration info
            string usrEmail = "personman1000@online.net";
            string usrPasswd = "Wid#$%766";
            string usrFName = "Bob";
            string usrLName = "Simon";

            //act

            //check if each get returns back the same value
            try
            {
                var tempAccount = new RegistrationService(usrEmail, usrPasswd, usrFName, usrLName);
                result = (usrFName == tempAccount.Extras.FirstName);
                result = (usrLName == tempAccount.Extras.LastName);
                result = (usrEmail == tempAccount.Email);
                result = (usrPasswd == tempAccount.Password);
            }
            catch (ArgumentException) { result = false; }
            catch (Exception) { result = false; }
            
            //assert
            Assert.IsTrue(result);
        }
    }
}
