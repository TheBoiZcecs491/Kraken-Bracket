using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using TBZ.DatabaseAccess;
using TBZ.DatabaseConnectionService;
using TBZ.HashingService;
using TBZ.KrakenBracket.DataHelpers;
//using static TBZ.Manager.Hashing.ManagerHashing;

namespace TBZ.DatabaseQueryService
{
    public class DatabaseQuery
    {
        Dictionary<string, string> tables = new Dictionary<string, string>()
        {
            {"bracket_info","bracket_info(bracketID, bracket_name, bracketTypeID, number_player) VALUES(@bracketID, @bracket_name, @bracketTypeID, @number_player)"},
            {"bracket_player_info","bracket_player_info(bracketID, hashedUserID, roleID) VALUES(@bracketID, @hashedUserID, @roleID)"},
            {"bracket_type","bracket_type(bracketTypeID, bracket_type) VALUES(@bracketTypeID, @bracket_type)"},
            {"event_bracket_list","event_bracket_list(eventID, bracketID) VALUES(@eventID, @bracketID)"},
            {"event_info","event_info(eventID, event_name) VALUES(@eventID, @event_name)"},
            {"role_type","role_type(roleID, role_type) VALUES(@roleID, @role_type)"},
            {"team_info","team_info(teamID, team_name) VALUES(@teamID, @team_name)" },
            {"team_list", "team_list(teamID, hashedUserID) VALUES(@teamID, @hashedUserID)"},
            {"gamer_info", "gamer_info(hashedUserID, gamerTag, gamerTagID, teamID) VALUES(@hashedUserID, @gamerTag, @gamerTagID, @teamID)" },
            {"user_information", "user_information(userID, email, hashed_password, salt, fname, lname) VALUES(@userID, @email, @hashed_password, @salt, @fname, @lname" },
            {"userid", "userid(userID, hashed_userID) VALUES(@userID, @hashed_userID"}
        };

        public bool TableExist(string tableName)
        {
            if (!tables.ContainsKey(tableName))
            {
                throw new ArgumentException("Table does not exist");
            }
            return true;
        }

        public void InsertUserAcc(User tempUser)
        {

            var DB = new Database();

            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                using (MySqlCommand comm = conn.CreateCommand())
                {
                    MessageSalt msalt = new MessageSalt(tempUser.Password, tempUser.Salt);
                    msalt.GenerateHash(msalt);
                    tempUser.Password = msalt.message;
                    tempUser.Salt = msalt.salt;
                    comm.CommandText = "INSERT INTO user_information(userID, email, hashed_password, salt, fname, lname, account_type) " +
                    "VALUES(@userID, @email, @hashed_password, @salt, @fname, @lname, @account_type)";

                    comm.Parameters.AddWithValue("@userID", tempUser.SystemID);
                    comm.Parameters.AddWithValue("@email", tempUser.Email);
                    comm.Parameters.AddWithValue("@hashed_password", tempUser.Password);
                    comm.Parameters.AddWithValue("@salt", tempUser.Salt);
                    comm.Parameters.AddWithValue("@fname", tempUser.FirstName);
                    comm.Parameters.AddWithValue("@lname", tempUser.LastName);
                    comm.Parameters.AddWithValue("@account_type", tempUser.AccountType);

                    //comm.Parameters.AddWithValue("@userID", tempUser.SystemID);
                    //comm.Parameters.AddWithValue("@hashed_userID", tempUser.SystemID);
                    conn.Open();
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }

        }

        public void InsertGamerInfo(Gamer tempGamer)
        {
            var DB = new Database();

            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                using (MySqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandText = "INSERT INTO gamer_info(hashedUserID, gamerTag, gamerTagID, teamID) VALUES(@hashedUserID, @gamerTag, @gamerTagID, @teamID)";

                    comm.Parameters.AddWithValue("@hashedUserID", tempGamer.HashedUserID);
                    comm.Parameters.AddWithValue("@gamerTag", tempGamer.GamerTag);
                    comm.Parameters.AddWithValue("@gamerTagID", tempGamer.GamerTagID);
                    comm.Parameters.AddWithValue("@teamID", tempGamer.TeamID);

                    conn.Open();
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void InsertBracketInfo(BracketInfo tempBracket)
        {
            var DB = new Database();

            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                using (MySqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandText = "INSERT INTO bracket_info(bracketID, bracket_name, bracketTypeID, number_player) VALUES(@bracketID, @bracket_name, @bracketTypeID, @number_player)";

                    comm.Parameters.AddWithValue("@bracketID", tempBracket.BracketID);
                    comm.Parameters.AddWithValue("@bracket_name", tempBracket.BracketName);
                    comm.Parameters.AddWithValue("@bracketTypeID", tempBracket.BracketTypeID);
                    comm.Parameters.AddWithValue("@number_player", tempBracket.PlayerCount);
                    conn.Open();
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void InsertBracketPlayer(BracketPlayer tempBracket)
        {
            var DB = new Database();

            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                using (MySqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandText = "INSERT INTO bracket_player_info(bracketID, hashedUserID, roleID) VALUES(@bracketID, @hashedUserID, @roleID)";

                    comm.Parameters.AddWithValue("@bracketID", tempBracket.BracketID);
                    comm.Parameters.AddWithValue("@hashedUserID", tempBracket.HashedUserID);
                    comm.Parameters.AddWithValue("@roleID", tempBracket.RoleID);
                    conn.Open();
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void InsertEvent(Event tempEvent)
        {
            var DB = new Database();

            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                using (MySqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandText = "INSERT INTO event_info(eventID, event_name) VALUES(@eventID, @event_name)";

                    comm.Parameters.AddWithValue("@eventID", tempEvent.EventID);
                    comm.Parameters.AddWithValue("@event_Name", tempEvent.EventName);

                    conn.Open();
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void DeleteUser(int deleteValue)
        {

            var DB = new Database();

            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                using (MySqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandText = "DELETE FROM user_information WHERE userID= @Value";
                    comm.Parameters.AddWithValue("@Value", deleteValue);
                    conn.Open();
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }

        }

        public void UpdateQuery(string tableName, string columnName, string updateValue, string variable, string value)
        {
            var DB = new Database();

            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                using (MySqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandText = "UPDATE " + tableName + " SET " + columnName + " = '" + updateValue + "'" + " WHERE " + variable + " = " + value;
                    //comm.Parameters.AddWithValue("@value", value);
                    //comm.Parameters.AddWithValue("@updateValue", updateValue);
                    conn.Open();//ya bongus u needed to open the connection
                    comm.ExecuteNonQuery();
                    //TODO: okay b/c I was having trubbs with trying to get this to work. ONLY TO REALIZE THAT BOIO FORGOT TO PUT IN comm.Open()
                    //now, this is not the kosher way to handle these update sql commands, so I will need to come back and fix that.
                    //I should use those comm.Parameters which I commented out due to string format paranoia.
                    conn.Close();
                }
            }
        }

    }
}