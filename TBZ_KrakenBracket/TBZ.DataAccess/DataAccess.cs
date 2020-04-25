using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using TBZ.DatabaseQueryService;
using TBZ.StringChecker;

namespace TBZ.DatabaseAccess
{
    public class DataAccess
    {
        const string CONNECTION_STRING = @"server=localhost; userid=root; password=password; database=tbz_krackenbracket";
        private MySqlConnection conn;

        // List of users and their passwords
        Dictionary<string, string> userDict = new Dictionary<string, string>()
        {
            {"brian@foomail.com", "123"},
            {"test@fmail.com", "legoMyEggo123"}
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
        /// Method used to get claim associated with user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        ///
        public object DSGetClaim(string email)
        {
            // Checks to see if the passed-in email exists in the claims datastore
            if (userActions.ContainsKey(email))
            {
                // TryGetValue(string key, out string value) - gets value associated with key.
                // The statement below will return the claims associated to the email address.
                userActions.TryGetValue(email, out List<string> value).ToString();
                // Convert the list of claims into an array
                string claimCollection = string.Join(",", value.ToArray());
                return (object)claimCollection;
            }
            else
            {
                throw new Exception();
            }
        }

        public bool CheckIDExistence(uint sysID)
        {
            try
            {
                using (conn = new MySqlConnection(CONNECTION_STRING))
                {
                    string selectQuery = string.Format("SELECT * FROM user_information WHERE userID={0}", sysID);
                    MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);

                    conn.Open();

                    using (MySqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        int count = 0;
                        while (reader.Read())
                        {
                            count++;
                        }
                        reader.Close();
                        conn.Close();
                        return (count == 1);
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Method to insert user into database
        /// </summary>
        /// <param name="sysID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CreateUser(User user, bool passwordCheck)
        {
            try
            {
                bool result = CheckIDExistence(user.SystemID);

                // ID is not found, so it is safe to proceed
                if (result == false)
                {
                    // Password check is enabled
                    if (passwordCheck == true)
                    {
                        StringCheckerService sc = new StringCheckerService(user.Password);

                        // Password is secured
                        if (sc.isSecurePassword())
                        {
                            DatabaseQuery dq = new DatabaseQuery();
                            dq.InsertUserAcc(user);
                            return true;
                        }

                        // Password is not secured
                        else
                        {
                            user.ErrorMessage = "Password is not secured";
                            return false;
                        }
                    }

                    // Password check is disabled
                    else
                    {
                        DatabaseQuery dq = new DatabaseQuery();
                        dq.InsertUserAcc(user);
                        return true;
                    }
                }
                else
                {
                    user.ErrorMessage = "System ID already exists";
                    return false;
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                user.ErrorMessage = e.ToString();
                return false;
            }
            catch (Exception e)
            {
                user.ErrorMessage = e.ToString();
                return false;
            }
        }

        public bool DeleteUser(User user)
        {
            try
            {
                bool result = CheckIDExistence(user.SystemID);
                if (result == true)
                {
                    DatabaseQuery dq = new DatabaseQuery();
                    dq.DeleteUser(user.SystemID);
                    return true;
                }
                else
                {
                    user.ErrorMessage = "System ID not found";
                    return false;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                user.ErrorMessage = e.ToString();
                return false;
            }
            catch (Exception e)
            {
                user.ErrorMessage = e.ToString();
                return false;
            }
        }
    }
}