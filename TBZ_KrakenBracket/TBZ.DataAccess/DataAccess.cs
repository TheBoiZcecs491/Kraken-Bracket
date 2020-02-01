using System;
using System.Collections.Generic;
using System.Linq;
using TBZ.RegistrationService;

namespace TBZ.DatabaseAccess
{
    public class DataAccess
    {
        // List of users and their passwords
        Dictionary<string, string> userDict = new Dictionary<string, string>()
        {
            {"brian@foomail.com", "123"},
            {"test@fmail.com", "legoMyEggo123"}
        };

        // TODO: remove hardcoded values later
        List<User> users = new List<User>()
        {
            new User
            {
                SystemID = 1,
                FirstName = null,
                LastName = null,
                Email = "foomail@gmail.com",
                Password = "4fweu2fwr",
                AccountType = "System Admin",
                AccountStatus = true
            },
            new User
            {
                SystemID = 2,
                FirstName = null,
                LastName = null,
                Email = "test139@gmail.com",
                Password = "A3[favrw23",
                AccountType = "Admin",
                AccountStatus = true
            },
            new User
            {
                SystemID = 3,
                FirstName = null,
                LastName = null,
                Email = "test901@gmail.com",
                Password = "[r4pl323][",
                AccountType = "User",
                AccountStatus = true
            },
            new User
            {
                SystemID = 4,
                FirstName = null,
                LastName = null,
                Email = "test12241@gmail.com",
                Password = "[r4pl323][",
                AccountType = "User",
                AccountStatus = false
            },
            new User
            {
                SystemID = 5,
                FirstName = null,
                LastName = null,
                Email = "test391@gmail.com",
                Password = "[r4pl323][",
                AccountType = "User",
                AccountStatus = true
            }
        };

        // Check the user's action that was added according to the role(s) they claim to be,
        // then check the if that action is in the list of actions they can perform
        Dictionary<string, List<string>> permissions = new Dictionary<string, List<string>>()
        {
            {"Host", new List<string>(){"Update Event Information",
                                        "Update Tournament Bracket Information",
                                        "Manage Tournament Bracket",
                                        "Delete Tournament Bracket and Event",
                                        "Assign Other Registered Users To Be A Co-Host"} },
            {"Co-Host", new List<string>(){ "Update Event Information", "Update Tournament Bracket Information",
                                            "Manage Tournament Bracket", "Delete Tournament Bracket and Event"} },
            {"Competitor", new List<string>(){ "Check Into A Match", "Have A Substitute" } },
            {"Registered User", new List<string>(){ "Search For Tournament Brackets",
                                                    "Search For Event", "Search For Registered User" } },
        };

        // Have these test users with actions assigned
        Dictionary<string, List<string>> userActions = new Dictionary<string, List<string>>()
        {
            {"brian@foomail.com", new List<string>(){"Update Event Information",
                                        "Update Tournament Bracket Information",
                                        "Manage Tournament Bracket",
                                        "Delete Tournament Bracket and Event",
                                        "Assign Other Registered Users To Be A Co-Host",
                                        "Search For Tournament Brackets",
                                        "Search For Event", "Search For Registered User"} },
            {"test@fmail.com", new List<string>(){ "Check Into A Match", "Have A Substitute",
                                                    "Search For Tournament Brackets",
                                                    "Search For Event", "Search For Registered User"} },
            {"", new List<string>(){ "Search For Tournament Brackets","Search For Event",
                                        "Search For Registered User"} }
        };

        /// <summary>
        /// Method used to check if email and password used.
        /// </summary>
        ///
        /// <param name="email">Email to search in the datastore</param>
        /// <param name="password">Password to search for in the datastore</param>
        ///
        /// <returns>
        /// True if both email and password exist. False if at least 1 does not.
        /// </returns>
        public bool GetEmailAndPassword(string email, string password)
        {
            // Checks if email exists in dictionary
            if (userDict.TryGetValue(email, out string value))
            {
                // Passed-in value matches password associated with email
                if (value == password)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method used to get claim associated with user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        ///
        public string DSGetClaim(string email)
        {
            // Checks to see if the passed-in email exists in the claims datastore
            if (userActions.ContainsKey(email))
            {
                // TryGetValue(string key, out string value) - gets value associated with key.
                // The statement below will return the claims associated to the email address.
                userActions.TryGetValue(email, out List<string> value).ToString();
                // Convert the list of claims into an array
                string claimCollection = string.Join(",", value.ToArray());
                return claimCollection;
            }
            else
            {
                throw new Exception();
            }
        }
        private int systemID = 0;

        public User UserExists(int systemID)
        {
            if (users.Exists(x => x.SystemID == systemID))
            {
                int index = users.FindIndex(x => x.SystemID == systemID);
                User u = users[index];
                return u;
            }
            return null;
        }
        public void StoreUser(int sysID, string firstName, string lastName,
            string email, string password, string accountType, bool accountStatus)
        {
            bool flag = true;
            while (flag)
            {
                if (users.Exists(x => x.SystemID == systemID))
                {
                    systemID++;
                }
                else
                {
                    flag = false;
                }
            }
            users.Add(new User
            {
                SystemID = systemID,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                AccountType = accountType,
                AccountStatus = accountStatus
            });
        }

        public bool DeleteUser(int systemID, string permission)
        {
            if (permission == "System Admin")
            {
                if(users.Exists(x => x.SystemID == systemID) )
                {
                    int index = users.FindIndex(x => x.SystemID == systemID);
                    User user = users[index];
                    if (user.AccountType != "System Admin")
                    {
                        var itemToRemove = users.Single(r => r.SystemID == systemID);

                        // Expected to return true
                        return users.Remove(itemToRemove);
                    }
                }
            }

            else if (permission == "Admin")
            {
                if (users.Exists(x => x.SystemID == systemID))
                {
                    int index = users.FindIndex(x => x.SystemID == systemID);
                    User user = users[index];
                    if (user.AccountType == "User")
                    {
                        var itemToRemove = users.Single(r => r.SystemID == systemID);

                        // Expected to return true
                        return users.Remove(itemToRemove);
                    }
                }
            }
            return false;
        }

        public bool EnableUser(int systemID, string permission)
        {
            if (permission == "System Admin")
            {
                if (users.Exists(x => x.SystemID == systemID))
                {
                    int index = users.FindIndex(x => x.SystemID == systemID);
                    User user = users[index];
                    if (user.AccountType != "System Admin" && user.AccountStatus == false)
                    {
                        user.AccountStatus = true;
                        return true;
                    }
                }
            }

            else if (permission == "Admin")
            {
                if (users.Exists(x => x.SystemID == systemID))
                {
                    int index = users.FindIndex(x => x.SystemID == systemID);
                    User user = users[index];
                    if (user.AccountType == "User" && user.AccountStatus == false)
                    {
                        user.AccountStatus = true;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool DisableUser(int systemID, string permission)
        {
            if (permission == "System Admin")
            {
                if (users.Exists(x => x.SystemID == systemID))
                {
                    int index = users.FindIndex(x => x.SystemID == systemID);
                    User user = users[index];
                    if (user.AccountType != "System Admin" && user.AccountStatus == true)
                    {
                        user.AccountStatus = false;
                        return true;
                    }
                }
            }

            else if (permission == "Admin")
            {
                if (users.Exists(x => x.SystemID == systemID))
                {
                    int index = users.FindIndex(x => x.SystemID == systemID);
                    User user = users[index];
                    if (user.AccountType == "User" && user.AccountStatus == true)
                    {
                        user.AccountStatus = false;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool UpdateFirstName(int sysID, string firstName, string permission)
        {
            User user = UserExists(sysID);
            if (user == null)
            {
                return false;
            }
            else
            {
                // TODO: have a first name checker and make sure the current account doesn't edit themselves
                if ((user.AccountType == "User" && permission == "Admin") 
                    || (user.AccountType != "System Admin" && permission == "System Admin"))
                {
                    user.FirstName = firstName;
                    return true;
                }
                return false;
            }

        }

        public bool UpdateLastName(int sysID, string lastName)
        {
            User user = UserExists(sysID);
            if (user == null)
            {
                return false;
            }
            else
            {
                // TODO: have a last name checker
                user.LastName = lastName;
                return true;
            }

        }

        public bool UpdateEmail(int sysID, string email)
        {
            User user = UserExists(sysID);
            if (user == null)
            {
                return false;
            }
            else
            {
                // TODO: have an email checker
                user.Email = email;
                return true;
            }
        }

        public bool UpdatePassword(int sysID, string password)
        {
            User user = UserExists(sysID);
            if (user == null)
            {
                return false;
            }
            else
            {
                // TODO: have a password checker
                user.Password = password;
                return true;
            }
        }


        // Is Account Type part of user account or user profile ?
        public bool UpdateAccountType(int sysID, string accountType, string permission)
        {
            User user = UserExists(sysID);
            if (user == null)
            {
                return false;
            }
            else
            {
                // TODO: have an account type checker
                user.AccountType = accountType;
                return true;
            }
        }
    }
}
