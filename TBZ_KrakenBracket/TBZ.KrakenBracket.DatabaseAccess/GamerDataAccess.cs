﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TBZ.DatabaseConnectionService;
using TBZ.DatabaseQueryService;
using TBZ.KrakenBracket.DataHelpers;

namespace TBZ.KrakenBracket.DatabaseAccess
{
    public class GamerDataAccess
    {
        public GamerInfo GetGamerInfo(GamerInfo gamer)
        {
            DatabaseQuery databaseQuery = new DatabaseQuery();
            return databaseQuery.GetGamerInfo(gamer);
        }

        public List<GamerInfo> ReadGamers(string gamerRequest)
        {
            var DB = new Database();
            var listOfGamers = new List<GamerInfo>();
            using (MySqlConnection conn = new MySqlConnection(DB.GetConnString()))
            {
                string selectQuery = string.Format("SELECT * FROM gamer_info WHERE gamerTag LIKE \'%{0}%\'", gamerRequest);
                Console.WriteLine(selectQuery);
                MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                conn.Open();
                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GamerInfo gamerObj = new GamerInfo();
                        gamerObj.GamerTag = reader.GetString("gamerTag");
                        listOfGamers.Add(gamerObj);
                    }
                }
            }
            return listOfGamers;
        }
    }
}
