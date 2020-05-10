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

        // Base case pass bracket
        readonly BracketInfo testBracketFields1 = new BracketInfo(2, "SoCal Regionals 2020: SFVAE Pools - 1", 1, 32,
                "Street Fighter V - Arcade Edition", "PS4", "Single Elimination", new DateTime(2020, 11, 6), new DateTime(2020, 11, 8), 0);
        // Update bracket to 128 competitors
        readonly BracketInfo testBracketFields2 = new BracketInfo(2, "SoCal Regionals 2020: SFVAE Pools - 1", 1, 128,
                "Street Fighter V - Arcade Edition", "PS4", "N/A", new DateTime(2020, 11, 6), new DateTime(2020, 11, 8), 0);
        // Fail case (short bracket name)
        readonly BracketInfo testBracketFields3 = new BracketInfo(2, "SoCa", 1, 32, "Street Fighter V - Arcade Edition", "PS4",
                "N/A", new DateTime(2019, 11, 6), new DateTime(2019, 11, 8), 0);
        // Fail case (128+ competitors)
        readonly BracketInfo testBracketFields4 = new BracketInfo(2, "SoCal Regionals 2020: SFVAE Pools - 1", 1, 129,
                "Street Fighter V - Arcade Edition", "PS4", "N/A", new DateTime(2019, 11, 6), new DateTime(2019, 11, 8), 0);
        // Fail case (nonexistent bracket ID
        readonly BracketInfo testBracketFields5 = new BracketInfo(9999, "SoCal Regionals 2020: SFVAE Pools - 1", 1, 129,
                "Street Fighter V - Arcade Edition", "PS4", "N/A", new DateTime(2019, 11, 6), new DateTime(2019, 11, 8), 0);

        readonly string testEmail1 = "brian@foomail.com";
        readonly string testClaim1 = "Create Tournament Bracket";
        readonly bool isLoggedIn = true;
        /*
        [TestInitialize]
        private void InitTestData()
        {

        }

        [TestCleanup]
        private void CleanUpData()
        {

        }
        */
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
            var expected = true;
            var actual = false;
            Stopwatch timer = new Stopwatch();

            try
            {
                timer.Start();
                actual = _tournamentBracketManager.CreatePermission(testEmail1, testClaim1, isLoggedIn);
                actual = _tournamentBracketManager.ValidateFields(testBracketFields1);
                actual = _tournamentBracketService.CreateTournamentBracket(testBracketFields1);
                timer.Stop();
            }
            catch(ArgumentException e)
            {
                Console.WriteLine("Error message: ", e);
                actual = false;
            }
            catch (Exception) { actual = false; }
            Console.WriteLine("Elasped = {0} ms", timer.ElapsedMilliseconds);
            _tournamentBracketService.DeleteTournamentBracket(testBracketFields1);
            // Assert
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(timer.ElapsedMilliseconds <= 3000);
        }

        [TestMethod]
        public void Validate_Pass()
        {
            // Arrange
            var expected = true;
            var actual = false;
            Stopwatch timer = new Stopwatch();

            try
            {
                timer.Start();
                actual = _tournamentBracketManager.ValidateFields(testBracketFields1);
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
        public void Validate_Fail_ShortBracketTitle()
        {
            // Arrange
            var expected = false;
            var actual = false;
            Stopwatch timer = new Stopwatch();

            try
            {
                timer.Start();
                actual = _tournamentBracketManager.ValidateFields(testBracketFields3);
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
        public void Validate_Fail_ExceededMaxCompetitors()
        {
            // Arrange
            var expected = false;
            var actual = false;
            Stopwatch timer = new Stopwatch();

            try
            {
                timer.Start();
                actual = _tournamentBracketManager.ValidateFields(testBracketFields4);
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
            var expected = true;
            var actual = false;
            Stopwatch timer = new Stopwatch();
            // Insert data to search and update
            _tournamentBracketService.CreateTournamentBracket(testBracketFields1);
            try
            {
                timer.Start();
                actual = _tournamentBracketManager.CreatePermission(testEmail1, testClaim1, isLoggedIn);
                actual = _tournamentBracketManager.ValidateFields(testBracketFields2);
                actual = _tournamentBracketService.UpdateTournamentBracket(testBracketFields2);
                timer.Stop();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Error message: ", e);
                actual = false;
            }
            catch (Exception) { actual = false; }
            Console.WriteLine("Elasped = {0} ms", timer.ElapsedMilliseconds);
            _tournamentBracketService.DeleteTournamentBracket(testBracketFields2);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTournamentBracket_Fail_UnableToFindBracket()
        {
            // Arrange
            var expected = false;
            var actual = false;
            Stopwatch timer = new Stopwatch();
            // Insert data to search and update
            _tournamentBracketService.CreateTournamentBracket(testBracketFields1);

            try
            {
                timer.Start();
                actual = _tournamentBracketManager.CreatePermission(testEmail1, testClaim1, isLoggedIn);
                actual = _tournamentBracketManager.ValidateFields(testBracketFields5);
                actual = _tournamentBracketService.UpdateTournamentBracket(testBracketFields5);
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
            var expected = true;
            var actual = false;
            Stopwatch timer = new Stopwatch();
            // Insert bracket to be deleted
            _tournamentBracketService.DeleteTournamentBracket(testBracketFields1);
            try
            {
                timer.Start();
                actual = _tournamentBracketManager.CreatePermission(testEmail1, testClaim1, isLoggedIn);
                actual = _tournamentBracketManager.ValidateFields(testBracketFields1);
                actual = _tournamentBracketService.DeleteTournamentBracket(testBracketFields1);
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
            var expected = new BracketPlayer(1, "LkKpHSN1+aOvzoj3ZrCXSIxasfWSZ5j1mJI5S3er3Vw=", 0, 0, 0, null, 1, null);
            
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
