using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
namespace TBZ.DatabaseAccess
{
    public class DataAccess
    {
        const string CONNECTION_STRING = @"Data source=localhost; Database=kraken_bracket; User ID=root; Password=Gray$cale917!!";
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

        /// <summary>
        /// Method to insert user into database
        /// </summary>
        /// <param name="sysID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CreateUser(int sysID, string password)
        {
            try
            {
                string query = "INSERT INTO User(System_ID, User_Password) VALUES('" + sysID + "', '" + password + "')";
                conn = new MySqlConnection(CONNECTION_STRING);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUser(int systemID)
        {
            try
            {
                string query = "DELETE FROM User WHERE System_ID=" + systemID;
                conn = new MySqlConnection(CONNECTION_STRING);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
