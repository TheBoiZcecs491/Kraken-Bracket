using System;
using System.Collections.Generic;
using System.Text;
using TBZ.AuthorizationManager;
using TBZ.KrakenBracket.DatabaseAccess;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.KrakenBracket.Services;

namespace TBZ.KrakenBracket.Managers
{
    public class TournamentBracketManager
    {
        private readonly TournamentBracketService _tournamentBracketService = new TournamentBracketService();
        private readonly TournamentBracketDataAccess _tournamentBracketDataAccess = new TournamentBracketDataAccess();

        /// <summary>
        /// Checks to see if user is authorized to create a tournament bracket.
        /// User only needs to be a registered user and logged in to do so.
        /// </summary>
        /// <param name="email"> User email. </param>
        /// <param name="action"> Desired action to perform. </param>
        /// <param name="isLoggedIn"> Whether or not the user is logged in. </param>
        /// <returns> A boolean to grant permission. </returns>
        public bool CreatePermission(string email, string action, bool isLoggedIn)
        {
            Authorization AuthZ = new Authorization();
            return AuthZ.UserPermission(email, action, isLoggedIn);
        }

        /// <summary>
        /// Validates the bracket fields given according to business rules.
        /// </summary>
        /// <param name="bracketFields"> Fields passed from controller layer. </param>
        /// <returns> A boolean to confirm that fields align with business rules. </returns>
        public bool ValidateFields(BracketInfo bracketFields)
        {
            if (bracketFields == null)
                throw new ArgumentNullException();

            if ((bracketFields.BracketName == null) ||
                (bracketFields.BracketName.Length < 5) ||
                    (bracketFields.BracketName.Length > 75))
            {
                throw new ArgumentException("Bracket name must be between 5-75 characters");
            }
            else if ((bracketFields.MaxCapacity < 2) ||
                    (bracketFields.MaxCapacity > 128))
            {
                throw new ArgumentException("Only 2-128 competitors allowed");
            }
            else if((bracketFields.GamePlayed == null) ||
                    (bracketFields.GamePlayed.Length > 50))
            {
                throw new ArgumentException("Game title cannot be larger than 50 characters");
            }
            else if ((bracketFields.GamingPlatform == null) ||
                    (bracketFields.GamingPlatform.Length > 50))
            {
                throw new ArgumentException("Gaming platform cannot be larger than 50 characters");
            }
            else if (bracketFields.Rules != null && bracketFields.Rules.Length > 700)
            {
                throw new ArgumentException("Ruleset cannot exceed 700 characters");
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Checks the validate fields method and confirms the bracket creation
        /// </summary>
        /// <param name="bracketInfo"></param> bracket fields to be validated and sent
        /// <returns></returns>
        public bool ValidateCreateBracket(BracketInfo bracketInfo)
        {
            if (ValidateFields(bracketInfo))
                return _tournamentBracketService.CreateTournamentBracket(bracketInfo);
            else
                return false;
        }

        public bool DeleteBracket(BracketInfo bracketInfo)
        {
            return _tournamentBracketService.DeleteTournamentBracket(bracketInfo);
        }

        /// <summary>
        /// Gets user's bracket player info
        /// </summary>
        ///
        /// <param name="email">
        /// User's email
        /// </param>
        ///
        /// <returns>
        /// List of their bracket player info.
        /// This list tells what brackets the user has been registered to
        /// </returns>
        public List<BracketPlayer> GetBracketPlayerInfo(string email)
        {
            var result = _tournamentBracketDataAccess.GetBracketPlayerInfo(email);
            if (result != null) return result;
            else throw new ArgumentException();
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
        public BracketPlayer RegisterGamerIntoBracket(GamerInfo gamer, int bracketID)
        {
            bool checkGamerExistence = _tournamentBracketService.CheckGamerExistence(gamer);
            bool checkBracketExistence = _tournamentBracketService.CheckBracketExistenceByID(bracketID);
            if (!(checkGamerExistence && checkBracketExistence))
            {
                throw new ArgumentException();
            }
            else
            {
                var bracket = _tournamentBracketService.GetBracketByID(bracketID);
                if (!(bracket.StatusCode == 0 && bracket.PlayerCount < 128))
                {
                    throw new ArgumentException();
                }
                return _tournamentBracketService.InsertGamerToBracket(gamer, bracket);
            }
        }

        public bool UpdateBracketInformation(BracketInfo bracketInfo)
        {
            if(bracketInfo == null)
            {
                throw new ArgumentException();
            }
            else
            {
                return _tournamentBracketService.UpdateTournamentBracket(bracketInfo);
            }
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
            var result =  _tournamentBracketService.GetGamerInfoByEmail(email);
            if (result != null) return result;
            else throw new ArgumentException();
        }

        /// <summary>
        /// Unregister gamer from bracket
        /// </summary>
        ///
        /// <param name="systemID">
        /// The user's system ID
        /// </param>
        ///
        /// <param name="bracketID">
        /// Bracket ID to reference the bracket that the user
        /// is unregistering from
        /// </param>
        ///
        /// <returns>
        /// Boolean to indicate success or fail unregistration
        /// </returns>
        public bool UnregisterGamerFromBracket(int systemID, int bracketID)
        {
            
            bool checkBracketExistence = _tournamentBracketService.CheckBracketExistenceByID(bracketID);
            if (!checkBracketExistence)
            {
                throw new ArgumentException();
            }
            else
            {
                var result = _tournamentBracketService.UnregisterGamerFromBracket(systemID, bracketID);
                if (!result) throw new ArgumentException();
                return result;
            }
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
            var result =  _tournamentBracketService.GetAllBrackets();
            if (result != null) return result;
            else throw new ArgumentException();
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
            var result = _tournamentBracketService.GetBracketByID(bracketID);
            if (result != null) return result;
            else throw new ArgumentException();
        }
    }
}
