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
        public void GetAllBrackets_Pass()
        {
            bool result = true;
            try
            {
                List<BracketInfo> actual = _tournamentBracketManager.GetAllBrackets();
                if (actual == null) result = false;
            }
            catch(Exception e)
            {
                result = false;
            }
            Assert.IsTrue(result);
        }
    }
}
