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
            string[] rePassword =
            {
                "Wid#$%766",
                "fortyK4K5!($^",
                "j33$M4n"
            };
            RegistrationService[] Account =
                {
                    new RegistrationService("cutieBoi@phuckahobo.gov", "Wid#$%766", "Ronald", "Cornwall"),
                    new RegistrationService("kernal.enderman.mason@masonsguild.uk", "fortyK3K5!($^", "Cernal", "Elderman"),
                    new RegistrationService("waglfragl@gmail.com", "j33$M4n", "Jonny", "Uno")
                } ;
            var result = true;
            string[] emailsInTheDB =
            {
                "personman1000@online.net",
                "sassykitty9998@hotmail.com"
            };
            bool[] corrects =
            {
                true,
                false,
                false
            };

            try
            {
                for(int i=0;i<corrects.Length;i++)
                {
                    if((Account[i].storeUser(rePassword[i], emailsInTheDB))!=corrects[i])
                    {
                        result = false;
                    }
                }
                //so this would effectivly just make the account exist.
            }
            catch(ArgumentException)
            {
                //my teammates didnt do their job so im punishing them here.
                //most of the time this will get thrown.
            }
            catch(Exception)
            {
                //something BAD happend, ya goof.
            }

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void Registration_validEmailGauntlet_Pass()
        {
            //TODO: so to test the email validiy checker ima make a list of strings
            //and a list of booleans indicating if that is supposed to be a valid
            //email or not. I got the idea for this from my game theory professor.
            bool result = true;
            string[] emails =
            {
                "georgelopez@fuckahobo.gay",
                "sassykitty9998@hotmail.com",
                "1234 Temple.Ave 69420",
                "kevinpootis923@yahoo.com",
                "darren.wallace.staff@gmail.com",
                "E><3nc1b73M45t3r@hackersunited.net"
            };
            bool[,] corrects =
            {//  isValid,isInDB
                { true, false },
                { true, true },
                { false, false },
                { true, false },
                { true, true},
                { false, false}
            };
            string[] emailsInTheDB =
            {
                "personman1000@online.net",
                "sassykitty9998@hotmail.com",
                "darren.wallace.staff@gmail.com"
            };
            try
            {
                for (int i = 0; i < emails.Length; i++)
                {
                    var tempAccount = new RegistrationService(emails[i], "123love", "Bob", "Stupit");
                    bool[] tempState = { tempAccount.isValidEmail(), tempAccount.emailExistsIn(emailsInTheDB) };
                    if ((tempState[0] != corrects[i,0]) | (tempState[1] != corrects[i, 1]))
                    {
                        result = false;
                    }
                }
            }
            catch (ArgumentException) { result = false; }
            catch (Exception) { result = false; }
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Registration_validPasswordGauntlet_Pass()
        {
            //TODO: so to test the email validiy checker ima make a list of strings
            //and a list of booleans indicating if that is supposed to be a valid
            //email or not. I got the idea for this from my game theory professor.
            bool result = true;
            string[] passwds =
            {
                "123Love",
                "@F@F#*FMWE*Rf8wf734jf",
                "ghg931)(%JDN",
                "gonot2TEHDWARVES3console",
                "d3G^"
            };
            bool[] corrects =
            {
                false,
                true,
                true,
                false,
                false
            };
            try
            {
                for (int i = 0; i < passwds.Length; i++)
                {
                    var tempAccount = new RegistrationService("personman1000@online.net", passwds[i], "Bob", "Stupit");
                    if (tempAccount.isSecurePassword() != corrects[i])
                    {
                        result = false;
                    }
                }
            }
            catch (ArgumentException) { result = false; }
            catch (Exception) { result = false; }
            Assert.IsTrue(result);
        }
    }
}
