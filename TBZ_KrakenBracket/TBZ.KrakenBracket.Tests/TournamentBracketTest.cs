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
        public void GetBracket_Pass()
        {
            bool result = true;
            try
            {
                User user = _tournamentBracketManager.GetUser("user1@krakenbracket.com", "Pass1");
                if (user == null) result = false;
            }
            catch(Exception e)
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
    }
}
