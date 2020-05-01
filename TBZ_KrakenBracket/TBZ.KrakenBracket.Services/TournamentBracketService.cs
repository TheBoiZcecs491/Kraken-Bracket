﻿using System;
using System.Collections.Generic;
using System.Text;
using TBZ.KrakenBracket.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;

namespace TBZ.KrakenBracket.Services
{
    public class TournamentBracketService
    {

        private readonly TournamentBracketDataAccess _tournamentBracketDataAccess = new TournamentBracketDataAccess();
        private readonly GamerDataAccess _gamerDataAccess = new GamerDataAccess();
        public bool CreateTournamentBracket(TournamentBracketDataAccess tournamentBracketDataAccess, BracketInfo bracketFields)
        {
            return true;
        }

        public bool CheckGamerExistence(Gamer gamer)
        {
            var gamerResult = _gamerDataAccess.GetGamerInfo(gamer);
            if (gamerResult == null) return false;
            else return true;
        }

        public bool CheckBracketExistenceByID(int bracketID)
        {
            return _tournamentBracketDataAccess.CheckBracketExistenceByID(bracketID);
        }

        public List<BracketInfo> GetAllBrackets()
        {
            return _tournamentBracketDataAccess.GetAllBrackets();
        }

        public BracketInfo GetBracketByID(int bracketID)
        {
            return _tournamentBracketDataAccess.GetBracketByID(bracketID);
        }

        public BracketPlayer InsertGamerToBracket(Gamer gamer, int bracketID)
        {
            return _tournamentBracketDataAccess.InsertGamerToBracket(gamer, bracketID);
        }

        public Gamer GetGamerInfoByEmail(string email)
        {
            return _gamerDataAccess.GetGamerInfoByEmail(email);
        }
    }
}
