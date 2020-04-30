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
            if ((bracketFields.BracketName.Length < 5) ||
                    (bracketFields.BracketName.Length > 75))
            {
                throw new ArgumentException("Bracket name must be between 5-75 characters");
            }
            else if (bracketFields.PlayerCount > 128)
            {
                throw new ArgumentException("No more than 128 competitors allowed");
            }
            else if(bracketFields.GamePlayed.Length > 50)
            {
                throw new ArgumentException("Game title cannot be larger than 50 characters");
            }
            else if (bracketFields.GamingPlatform.Length > 50)
            {
                throw new ArgumentException("Gaming platform cannot be larger than 50 characters");
            }
            else if (bracketFields.Rules.Length > 700)
            {
                throw new ArgumentException("Ruleset cannot exceed 700 characters");
            }
            else if (bracketFields.StartDate < DateTime.UtcNow)
            {
                throw new ArgumentException("Start date cannot be before current date");
            }
            else if (bracketFields.EndDate < DateTime.UtcNow)
            {
                throw new ArgumentException("Game title cannot be larger than 50 characters");
            }
            else
            {
                return true;
            }
        }

        public List<BracketPlayer> GetBracketPlayerInfo(string email)
        {
            var result = _tournamentBracketDataAccess.GetBracketPlayerInfo(email);
            if (result != null) return result;
            else throw new ArgumentException();
        }

        public BracketPlayer RegisterGamerIntoBracket(Gamer gamer, int bracketID)
        {
            bool checkGamerExistence = _tournamentBracketService.CheckGamerExistence(gamer);
            bool checkBracketExistence = _tournamentBracketService.CheckBracketExistenceByID(bracketID);
            if (checkGamerExistence && checkBracketExistence)
            {
                return _tournamentBracketService.InsertGamerToBracket(gamer, bracketID);
            }
            else throw new ArgumentException();
            
        }
        public bool UnregisterGamerFromBracket(int systemID, int bracketID)
        {
            var result =  _tournamentBracketDataAccess.RemoveGamerFromBracket(systemID, bracketID);
            if (result) return result;
            else throw new ArgumentException();
        }

        public List<BracketInfo> GetAllBrackets()
        {
            var result =  _tournamentBracketService.GetAllBrackets();
            if (result != null) return result;
            else throw new ArgumentException();
        }

        public BracketInfo GetBracketByID(int bracketID)
        {
            var result = _tournamentBracketService.GetBracketByID(bracketID);
            if (result != null) return result;
            else throw new ArgumentException();
        }
    }
}
