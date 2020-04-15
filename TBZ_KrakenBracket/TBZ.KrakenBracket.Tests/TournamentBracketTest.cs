using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.KrakenBracket.Managers;

namespace TBZ.TournamentBracketTest
{
    [TestClass]
    public class TournamentBracketTest
    {
        private readonly TournamentBracketManager _tournamentBracketManager = new TournamentBracketManager();
        
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
        public void GetBracket_Pass()
        {
            bool result = true;
            try
            {
                BracketInfo actual = _tournamentBracketManager.GetBracket(1);
                if (actual == null) result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            Assert.IsTrue(result);
        }
        
    }
}
