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
        public void GetBracketStatusCode_Pass()
        {
            BracketInfo bracketInfo = new BracketInfo(1, "Test Name", 1, 127, "Mortal Kombat 11", "Xbox One", 
                "No rules atm", new DateTime(2020, 5, 3), new DateTime(2020, 5, 3), 0);
            var expected = bracketInfo.StatusCode;
            var actual = _tournamentBracketManager.GetBracketStatusCode(bracketInfo.BracketID);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetNumberOfCompetitors_Pass()
        {
            BracketInfo bracketInfo = new BracketInfo(1, "Test Name", 1, 127, "Mortal Kombat 11", "Xbox One",
                "No rules atm", new DateTime(2020, 5, 3), new DateTime(2020, 5, 3), 0);
            var expected = bracketInfo.PlayerCount;
            var actual = _tournamentBracketManager.GetNumberOfCompetitors(bracketInfo.BracketID);
            Assert.AreEqual(expected, actual);
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
            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CreateTournamentBracket_Fail_PastCurrentDate()
        {
            // Arrange
            BracketInfo bracketFields = new BracketInfo(2, "SoCal Regionals 2020: SFVAE Pools - 1", 1, 32, "Street Fighter V - Arcade Edition", "PS4",
                "N/A", new DateTime(2019, 11, 6), new DateTime(2019, 11, 8), 0);
            var expected = false;
            var actual = false;
            Stopwatch timer = new Stopwatch();

            try
            {
                timer.Start();
                actual = _tournamentBracketManager.CreatePermission("brian@foomail.com", "Create Tournament Bracket", true);
                actual = _tournamentBracketManager.ValidateFields(bracketFields);
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
