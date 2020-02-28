using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using TBZ.DatabaseConnectionService;

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
            {"team_list", "team_list(teamID, hashedUserID) VLAUES(@teamID, @hashedUserID)"},
            {"gamer_info", "gamer_info(hashedUserID, gamerTag, gamerTagID, teamID) VALUES(@hashedUserID, @gamerTag, @gamerTagID, @teamID)" },
            {"user_information", "user_information(userID, email, hashed_password, salt, fname, lname) VALUES(@userID, @email, @hashed_password, @salt, @fname, @lname" }
        };

        public bool TableExist(string tableName)
        {
            if (!tables.ContainsKey(tableName))
            {
                throw new ArgumentException("Table does not exist");
            }
            return true;
        }

        public void InsertQuery(string tableName, List<string> insertList)
        {
            var DB = new Database();
            MySqlConnection conn = new MySqlConnection(DB.GetConnString());
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = "INSERT INTO " + tables[tableName];

            int i = Array.IndexOf(tables.Keys.ToArray(), tableName);
            switch (i)
            {
                case 1:
                    comm.Parameters.AddWithValue("@bracketID", insertList[0]);
                    comm.Parameters.AddWithValue("@bracket_name", insertList[1]);
                    comm.Parameters.AddWithValue("@bracketTypeID", insertList[2]);
                    comm.Parameters.AddWithValue("@number_player", insertList[3]);
                    break;

                case 2:
                    comm.Parameters.AddWithValue("@bracketID", insertList[0]);
                    comm.Parameters.AddWithValue("@hashedUserID", insertList[1]);
                    comm.Parameters.AddWithValue("@roleID", insertList[2]);
                    break;

                case 3:
                    comm.Parameters.AddWithValue("@bracketID", insertList[0]);
                    comm.Parameters.AddWithValue("@bracket_type", insertList[1]);
                    break;

                case 4:
                    comm.Parameters.AddWithValue("@eventID", insertList[0]);
                    comm.Parameters.AddWithValue("@bracketID", insertList[1]);
                    break;

                case 5:
                    comm.Parameters.AddWithValue("@eventID", insertList[0]);
                    comm.Parameters.AddWithValue("@event_name", insertList[1]);
                    break;

                case 6:
                    comm.Parameters.AddWithValue("@roleID", insertList[0]);
                    comm.Parameters.AddWithValue("@role_type", insertList[1]);
                    break;

                case 7:
                    comm.Parameters.AddWithValue("@teamID", insertList[0]);
                    comm.Parameters.AddWithValue("@team_name", insertList[1]);
                    break;

                case 8:
                    comm.Parameters.AddWithValue("@teamID", insertList[0]);
                    comm.Parameters.AddWithValue("@hashehUserID", insertList[1]);
                    break;

                case 9:
                    comm.Parameters.AddWithValue("@hashedUserID", insertList[0]);
                    comm.Parameters.AddWithValue("@gamerTag", insertList[1]);
                    comm.Parameters.AddWithValue("@gamerTagID", insertList[2]);
                    comm.Parameters.AddWithValue("@teamID", insertList[3]);
                    break;

                case 10:
                    comm.Parameters.AddWithValue("@userID", insertList[0]);
                    comm.Parameters.AddWithValue("@email", insertList[1]);
                    comm.Parameters.AddWithValue("@hashed_password", insertList[2]);
                    comm.Parameters.AddWithValue("@salt", insertList[3]);
                    comm.Parameters.AddWithValue("@fname", insertList[4]);
                    comm.Parameters.AddWithValue("@lname", insertList[5]);
                    break;

                default:
                    break;
            }
            comm.ExecuteNonQuery();
            conn.Close();
        }

        public void DeleteQuery(string tableName, string columnName, string deleteValue)
        {
            var DB = new Database();
            MySqlConnection conn = new MySqlConnection(DB.GetConnString());
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = "DELETE FROM " + tableName + "WHERE " + columnName + "= @Value";
            comm.Parameters.AddWithValue("@Value", deleteValue);
            comm.ExecuteNonQuery();
            conn.Close();

        }

        public void UpdateQuery(string tableName, string columnName, string updateValue, string variable, string value)
        {
            var DB = new Database();
            MySqlConnection conn = new MySqlConnection(DB.GetConnString());
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = "UPDATE " + tableName + "SET " + columnName + "= " + updateValue + "WHERE" + variable + "= @value";
            comm.Parameters.AddWithValue("@value", value);
            comm.ExecuteNonQuery();
            conn.Close();
        }

    }
}
