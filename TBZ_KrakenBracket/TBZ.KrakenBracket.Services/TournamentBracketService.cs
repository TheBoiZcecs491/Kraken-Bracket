using System;
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
        public bool CreateTournamentBracket(BracketInfo bracketFields)
        {
            return _tournamentBracketDataAccess.InsertNewBracket(bracketFields);
        }

        public bool UpdateTournamentBracket(BracketInfo bracketFields)
        {
            return _tournamentBracketDataAccess.UpdateBracket(bracketFields);
        }

        public bool DeleteTournamentBracket(BracketInfo bracketFields)
        {
            return _tournamentBracketDataAccess.DeleteBracket(bracketFields);
        }

        /// <summary>
        /// Check gamer object's existence in the database. Used for
        /// registering for bracket
        /// </summary>
        ///
        /// <param name="gamer">
        /// Gamer object to be searched for
        /// </param>
        ///
        /// <returns>
        /// Boolean indicating success or fail in finding Gamer object
        /// </returns>
        public bool CheckGamerExistence(GamerInfo gamer)
        {
            var gamerResult = _gamerDataAccess.GetGamerInfo(gamer);
            if (gamerResult == null) return false;
            else return true;
        }

        /// <summary>
        /// Checks if bracket exists by its bracket ID
        /// </summary>
        ///
        /// <param name="bracketID">
        /// Bracket ID associated with bracket
        /// </param>
        ///
        /// <returns>
        /// Boolean indicating success or fail in finding bracket
        /// </returns>
        public bool CheckBracketExistenceByID(int bracketID)
        {
            return _tournamentBracketDataAccess.CheckBracketExistenceByID(bracketID);
        }

        /// <summary>
        /// Returns all brackets stored in the database
        /// </summary>
        ///
        /// <returns>
        /// All brackets stored in the database
        /// </returns>
        public List<BracketInfo> GetAllBrackets()
        {
            return _tournamentBracketDataAccess.GetAllBrackets();
        }

        /// <summary>
        /// Gets a specific bracket by its ID
        /// </summary>
        ///
        /// <param name="bracketID">
        /// Bracket ID to be used to fetch bracket objectr
        /// </param>
        ///
        /// <returns>
        /// Bracket object associated with bracket ID
        /// </returns>
        public BracketInfo GetBracketByID(int bracketID)
        {
            return _tournamentBracketDataAccess.GetBracketByID(bracketID);
        }

        /// <summary>
        /// Registers gamer into a bracket
        /// </summary>
        ///
        /// <param name="gamer">
        /// User's Gamer info
        /// </param>
        ///
        /// <param name="bracketID">
        /// The bracket ID to reference the bracket that the user is being
        /// registered to
        /// </param>
        ///
        /// <returns>
        /// BracketPlayer object if registration is successful
        /// </returns>
        public BracketPlayer InsertGamerToBracket(GamerInfo gamer, BracketInfo bracket)
        {
            return _tournamentBracketDataAccess.InsertGamerToBracket(gamer, bracket);
        }

        /// <summary>
        /// Gets gamer info by user's email
        /// </summary>
        ///
        /// <param name="email">
        /// User's email
        /// </param>
        ///
        /// <returns>
        /// Gamer object associated with user
        /// </returns>
        public GamerInfo GetGamerInfoByEmail(string email)
        {
            return _gamerDataAccess.GetGamerInfoByEmail(email);
        }

        /// <summary>
        /// Unregisters gamer from bracket
        /// </summary>
        ///
        /// <param name="systemID">
        /// System ID associated with user
        /// </param>
        ///
        /// <param name="bracketID">
        /// Bracket ID associated with bracket
        /// </param>
        ///
        /// <returns>
        /// Boolean indicated success or fail in unregistration
        /// </returns>
        public bool UnregisterGamerFromBracket(int systemID, int bracketID)
        {
            return _tournamentBracketDataAccess.UnregisterGamerFromBracket(systemID, bracketID);
        }
    }
}
