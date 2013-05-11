using System;
using Balloons_Pops_game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BalloonsPop.Tests
{
    [TestClass]
    public class ScoreEntryTest
    {
        [TestMethod]
        public void Score11CompareToScore15()
        {
            ScoreEntry bunny = new ScoreEntry(11, "Bunny");
            ScoreEntry satan = new ScoreEntry(15, "Satan");
            int actualResult = bunny.CompareTo(satan);

            Assert.AreEqual(-1, actualResult);
        }

        [TestMethod]
        public void Score7CompareToScore7()
        {
            ScoreEntry bunny = new ScoreEntry(7, "Bunny");
            ScoreEntry satan = new ScoreEntry(7, "Satan");
            int actualResult = bunny.CompareTo(satan);

            Assert.AreEqual(0, actualResult);
        }
    }
}
