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

        internal Gamer GetGamerInfoByHashedID(string hashedUserID)
        {
            try
            {
                var DB = new Database();

                using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
                {
                    using (MySqlCommand comm = conn.CreateCommand())
                    {
                        comm.CommandText = "SELECT * FROM gamer_info WHERE hashedUserID=@HashedUserID";
                        comm.Parameters.AddWithValue("@HashedUserID", hashedUserID);
                        conn.Open();
                        using (MySqlDataReader reader = comm.ExecuteReader())
                        {
                            Gamer gamer = new Gamer();
                            reader.Read();
                            gamer.GamerTag = reader.GetString("gamerTag");
                            gamer.GamerTagID = reader.GetInt32("gamerTagID");
                            gamer.HashedUserID = reader.GetString("hashedUserID");
                            conn.Close();
                            return gamer;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

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

       
        

        public void InsertEvent(EventInfo tempEvent)
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
                    conn.Open();
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
       
        public Gamer GetGamerInfo(Gamer gamer)
        {
            try
            {
                var DB = new Database();

                using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
                {
                    using (MySqlCommand comm = conn.CreateCommand())
                    {
                        comm.CommandText = "SELECT * FROM gamer_info WHERE gamerTag=@GamerTag";
                        comm.Parameters.AddWithValue("@GamerTag", gamer.GamerTag);
                        conn.Open();
                        using (MySqlDataReader reader = comm.ExecuteReader())
                        {
                            reader.Read();
                            gamer.GamerTag = reader.GetString("gamerTag");
                            gamer.GamerTagID = reader.GetInt32("gamerTagID");
                            gamer.HashedUserID = reader.GetString("hashedUserID");
                            conn.Close();
                            return gamer;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
      
        public User GetUserInfo(string email)
        {
            try
            {
                var DB = new Database();

                using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
                {
                    using (MySqlCommand comm = conn.CreateCommand())
                    {
                        comm.CommandText = "SELECT * FROM user_information WHERE email=@Email";
                        comm.Parameters.AddWithValue("@Email", email);
                        conn.Open();
                        using (MySqlDataReader reader = comm.ExecuteReader())
                        {
                            User user = new User();
                            reader.Read();
                            user.SystemID = reader.GetInt32("userID");
                            user.Email = reader.GetString("email");
                            user.FirstName = reader.GetString("fname");
                            user.LastName = reader.GetString("lname");
                            user.Password = reader.GetString("hashed_password");
                            user.Salt = reader.GetString("salt");
                            user.AccountType = reader.GetString("account_type");
                            user.AccountStatus = reader.GetBoolean("account_status");
                            conn.Close();
                            return user;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        public string GetHashedUserID(int systemID)
        {
            var DB = new Database();
            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                // Retrieve the system ID
                string selectQuery0 = string.Format("SELECT * FROM userid WHERE userID={0}", systemID);
                MySqlCommand selectCmd = new MySqlCommand(selectQuery0, conn);
                conn.Open();
                using (MySqlDataReader reader0 = selectCmd.ExecuteReader())
                {
                    reader0.Read();
                    string hashedUserID = reader0.GetString("hashed_userID");
                    conn.Close();
                    return hashedUserID;
                }
            }
        }

      
    }
}