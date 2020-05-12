using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TBZ.KrakenBracket.DataHelpers;
using TBZ.KrakenBracket.Managers;
namespace TBZ.KrakenBracket.Tests
{
    [TestClass]
    public class SearchTest
    {
        readonly BracketInfo testBracket1 = new BracketInfo("The Bracket");
        readonly BracketInfo testBracket2 = new BracketInfo("EVO 2020");
        readonly BracketInfo testBracket3 = new BracketInfo("EVO 2019");
        readonly List<BracketInfo> _brackets = new List<BracketInfo>();
        readonly EventInfo testEvent1 = new EventInfo("The Event");
        readonly EventInfo testEvent2 = new EventInfo("Year 2020");
        readonly EventInfo testEvent3 = new EventInfo("Year 2019");
        readonly List<EventInfo> _events = new List<EventInfo>();
        readonly GamerInfo testGamer1 = new GamerInfo("The Gamer", 1, "US");
        readonly GamerInfo testGamer2 = new GamerInfo("Gamer1", 1, "US");
        readonly GamerInfo testGamer3 = new GamerInfo("Gamer2", 1, "US");
        readonly List<GamerInfo> _gamers = new List<GamerInfo>();
        public SearchManagerNoDB _searchManager;

        [TestInitialize]
        public void PopulateDataLists()
        {
            _brackets.Add(testBracket1);
            _brackets.Add(testBracket2);
            _brackets.Add(testBracket3);
            _events.Add(testEvent1);
            _events.Add(testEvent2);
            _events.Add(testEvent3);
            _gamers.Add(testGamer1);
            _gamers.Add(testGamer2);
            _gamers.Add(testGamer3);
        }

        [TestMethod]
        public void SearchBracketsForOneResult_Pass() {
            //Arrange
            var expected = new List<string>()
            {
                "The Bracket"
            };
            var actual = new List<string>();
            //Act
            try
            {
                _searchManager = new SearchManagerNoDB(_brackets,_events,_gamers);
                List<BracketInfo> results = _searchManager.GetBrackets("The Bracket");
                foreach (BracketInfo bracketInfo in results)
                {
                    if (!bracketInfo.BracketName.Equals(""))
                    {
                        actual.Add(bracketInfo.BracketName);
                    }
                }
            }
            catch { }
            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SearchEventsForMultipleResults_Pass() {
            //Arrange
            var expected = new List<string>
            {
                "Year 2020",
                "Year 2019"
            };
            var actual = new List<string>();
            //Act
            try
            {
                _searchManager = new SearchManagerNoDB(_brackets, _events, _gamers);
                List<EventInfo> results = _searchManager.GetEvents("Year");
                foreach (EventInfo eventInfo in results)
                {
                    if (!eventInfo.EventName.Equals(""))
                    {
                        actual.Add(eventInfo.EventName);
                    }
                }
            }
            catch { }
            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SearchGamersWithNoResults_Pass() {
            //Arrange
            var expected = new List<string>();
            var actual = new List<string>();
            //Act
            try
            {
                _searchManager = new SearchManagerNoDB(_brackets, _events, _gamers);
                List<GamerInfo> results = _searchManager.GetGamers("No Request");
                foreach (GamerInfo gamerInfo in results)
                {
                    if (!gamerInfo.GamerTag.Equals(""))
                    {
                        actual.Add(gamerInfo.GamerTag);
                    }
                }
            }
            catch { }
            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
