using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.KrakenBracket.Managers;
using System.Diagnostics;
using TBZ.KrakenBracket.Services;
using TBZ.KrakenBracket.DatabaseAccess;

namespace TBZ.TournamentBracketTest
{
    [TestClass]
    public class TournamentBracketTest
    {
        private readonly TournamentBracketManager _tournamentBracketManager = new TournamentBracketManager();

        private readonly TournamentBracketService _tournamentBracketService = new TournamentBracketService();

        [TestMethod]
        public void GetBracket_Pass()
        {
            bool result = true;
            try
            {
                BracketInfo actual = _tournamentBracketManager.GetBracketByID(1);
                if (actual == null) result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateTournamentBracket_Pass()
        {
            // Arrange
            BracketInfo bracketFields = new BracketInfo(2, "SoCal Regionals 2020: SFVAE Pools - 1", 1, 32, "Street Fighter V - Arcade Edition", "PS4",
                "N/A", new DateTime(2020, 11, 6), new DateTime(2020, 11, 8), 0);
            var expected = true;
            var actual = false;
            Stopwatch timer = new Stopwatch();

            try
            {
                timer.Start();
                actual = _tournamentBracketManager.CreatePermission("brian@foomail.com", "Create Tournament Bracket", true);
                actual = _tournamentBracketManager.ValidateFields(bracketFields);
                actual = _tournamentBracketService.CreateTournamentBracket(bracketFields);
                timer.Stop();
            }
            catch(ArgumentException e)
            {
                Console.WriteLine("Error message: ", e);
                actual = false;
            }
            catch (Exception) { actual = false; }
            Console.WriteLine("Elasped = {0} ms", timer.ElapsedMilliseconds);
            _tournamentBracketService.DeleteTournamentBracket(bracketFields);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateTournamentBracket_Fail_ShortBracketTitle()
        {
            // Arrange
            BracketInfo bracketFields = new BracketInfo(2, "SoCa", 1, 32, "Street Fighter V - Arcade Edition", "PS4",
                "N/A", new DateTime(2019, 11, 6), new DateTime(2019, 11, 8), 0);
            var expected = false;
            var actual = false;
            Stopwatch timer = new Stopwatch();

            try
            {
                timer.Start();
                actual = _tournamentBracketManager.CreatePermission("brian@foomail.com", "Create Tournament Bracket", true);
                actual = _tournamentBracketManager.ValidateFields(bracketFields);
                actual = _tournamentBracketService.CreateTournamentBracket(bracketFields);
                timer.Stop();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Error message: ", e);
                actual = false;
            }
            catch (Exception) { actual = false; }
            Console.WriteLine("Elasped = {0} ms", timer.ElapsedMilliseconds);
            // Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CreateTournamentBracket_Fail_ExceededMaxCompetitors()
        {
            // Arrange
            BracketInfo bracketFields = new BracketInfo(2, "SoCal Regionals 2020: SFVAE Pools - 1", 1, 129, "Street Fighter V - Arcade Edition", "PS4",
                "N/A", new DateTime(2019, 11, 6), new DateTime(2019, 11, 8), 0);
            var expected = false;
            var actual = false;
            Stopwatch timer = new Stopwatch();

            try
            {
                timer.Start();
                actual = _tournamentBracketManager.CreatePermission("brian@foomail.com", "Create Tournament Bracket", true);
                actual = _tournamentBracketManager.ValidateFields(bracketFields);
                actual = _tournamentBracketService.CreateTournamentBracket(bracketFields);
                timer.Stop();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Error message: ", e);
                actual = false;
            }
            catch (Exception) { actual = false; }
            Console.WriteLine("Elasped = {0} ms", timer.ElapsedMilliseconds);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTournamentBracket_Pass()
        {
            // Arrange
            BracketInfo bracketFields = new BracketInfo(1, "SoCal Regionals 2020: SFVAE Pools - 2", 1, 128, "Street Fighter V - Arcade Edition", "PS4",
                "N/A", new DateTime(2020, 11, 6), new DateTime(2020, 11, 8), 0);
            var expected = true;
            var actual = false;
            Stopwatch timer = new Stopwatch();

            try
            {
                timer.Start();
                actual = _tournamentBracketManager.CreatePermission("brian@foomail.com", "Create Tournament Bracket", true);
                actual = _tournamentBracketManager.ValidateFields(bracketFields);
                actual = _tournamentBracketService.UpdateTournamentBracket(bracketFields);
                timer.Stop();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Error message: ", e);
                actual = false;
            }
            catch (Exception) { actual = false; }
            Console.WriteLine("Elasped = {0} ms", timer.ElapsedMilliseconds);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTournamentBracket_Fail_UnableToFindBracket()
        {
            // Arrange
            BracketInfo bracketFields = new BracketInfo(404, "SoCal Regionals 2020: SFVAE Pools - 2", 1, 128, "Street Fighter V - Arcade Edition", "PS4",
                "N/A", new DateTime(2020, 11, 6), new DateTime(2020, 11, 8), 0);
            var expected = false;
            var actual = false;
            Stopwatch timer = new Stopwatch();

            try
            {
                timer.Start();
                actual = _tournamentBracketManager.CreatePermission("brian@foomail.com", "Create Tournament Bracket", true);
                actual = _tournamentBracketManager.ValidateFields(bracketFields);
                actual = _tournamentBracketService.UpdateTournamentBracket(bracketFields);
                timer.Stop();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Error message: ", e);
                actual = false;
            }
            catch (Exception) { actual = false; }
            Console.WriteLine("Elasped = {0} ms", timer.ElapsedMilliseconds);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteTournamentBracket_Pass()
        {
            // Arrange
            BracketInfo bracketFields = new BracketInfo(2, "SoCal Regionals 2020: SFVAE Pools - 2", 1, 128, "Street Fighter V - Arcade Edition", "PS4",
                "N/A", new DateTime(2020, 11, 6), new DateTime(2020, 11, 8), 0);
            var expected = true;
            var actual = false;
            Stopwatch timer = new Stopwatch();

            try
            {
                timer.Start();
                actual = _tournamentBracketManager.CreatePermission("brian@foomail.com", "Create Tournament Bracket", true);
                actual = _tournamentBracketManager.ValidateFields(bracketFields);
                actual = _tournamentBracketService.DeleteTournamentBracket(bracketFields);
                timer.Stop();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Error message: ", e);
                actual = false;
            }
            catch (Exception) { actual = false; }
            Console.WriteLine("Elasped = {0} ms", timer.ElapsedMilliseconds);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RegisterUserToBracket_Pass()
        {
            // Arrange
            var gamerInfo = new GamerInfo();
            var bracketID = 1;
            gamerInfo.GamerTag = "GamerTag1";
            var expected = new BracketPlayer(1, "LkKpHSN1+aOvzoj3ZrCXSIxasfWSZ5j1mJI5S3er3Vw=", 0, 0, 0, 1);
            
            // Act
            var actual = _tournamentBracketManager.RegisterGamerIntoBracket(gamerInfo, bracketID);

            // Assert
            Assert.AreEqual(expected.BracketID, actual.BracketID);
            Assert.AreEqual(expected.HashedUserID, actual.HashedUserID);
        }

        [TestMethod]
        public void RegisterUserToBracket_Fail_UserAlreadyRegisteredToBracket()
        {
            // Arrange
            var result = true;
            var gamerInfo = new GamerInfo();
            var bracketID = 1;
            gamerInfo.GamerTag = "GamerTag1";

            // Act
            try
            {
                var actual = _tournamentBracketManager.RegisterGamerIntoBracket(gamerInfo, bracketID);
                if (actual == null) result = false;
            }
            catch (ArgumentException)
            {
                result = false;
            }
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegisterUserToBracket_Fail_BracketHasEnded()
        {
            // Assert
            var result = true;
            var gamerInfo = new GamerInfo();
            var bracketID = 5;
            gamerInfo.GamerTag = "GamerTag2";

            // Act
            try
            {
                var actual = _tournamentBracketManager.RegisterGamerIntoBracket(gamerInfo, bracketID);
            }
            catch (ArgumentException)
            {
                result = false;
            }

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegisterUserToBracket_Fail_BracketHasMaximumNumberOfCompetitors()
        {
            // Assert
            var result = true;
            var gamerInfo = new GamerInfo();
            var bracketID = 6;
            gamerInfo.GamerTag = "GamerTag2";

            // Act
            try
            {
                var actual = _tournamentBracketManager.RegisterGamerIntoBracket(gamerInfo, bracketID);
            }
            catch (ArgumentException)
            {
                result = false;
            }

            // Assert
            Assert.IsFalse(result);
        }

        public void RegisterUserToBracket_Fail_BracketIsInProgress()
        {
            // Assert
            var result = true;
            var gamerInfo = new GamerInfo();
            var bracketID = 6;
            gamerInfo.GamerTag = "GamerTag2";

            // Act
            try
            {
                var actual = _tournamentBracketManager.RegisterGamerIntoBracket(gamerInfo, bracketID);
            }
            catch (ArgumentException)
            {
                result = false;
            }

            // Assert
            Assert.IsFalse(result);
        }
    }
}
