using Balloons_Pops_game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.QualityTools.Testing.Fakes;
using Balloons_Pops_game.Fakes;
using System.Xml.Linq;
using System.Reflection;

namespace BalloonsPop.Tests
{
    [TestClass]
    public class DataDrivenTests
    {
        [TestMethod]
        public void PopFirstRowAndCol()
        {
            using (ShimsContext.Create())
            {
                BalloonsEngine game = new BalloonsEngine(0, 0);

                ShimBalloonsEngine.AllInstances.GeneratePlayFieldInt32Int32
                = (game1, x, y) => TestUtils.GenerateFieldShim(game, 5, 10);

                TestUtils.FileName("01.AllOnesInput.xml");
                game = new BalloonsEngine(5, 10);
                game.TryPopBalloons(0, 0);

                int[,] gameField = (int[,])typeof(BalloonsEngine).GetField("playField", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(game);
                int[,] expectedOutput = TestUtils.GetExpectedOutput();

                bool valuesAreEqual = TestUtils.CheckFieldsEquality(gameField, expectedOutput);

                Assert.AreEqual(valuesAreEqual, true);
            }
        }

        [TestMethod]
        public void PopSecondRowAndCol()
        {
            using (ShimsContext.Create())
            {
                BalloonsEngine game = new BalloonsEngine(0, 0);

                ShimBalloonsEngine.AllInstances.GeneratePlayFieldInt32Int32
                = (game1, x, y) => TestUtils.GenerateFieldShim(game, 5, 10);

                TestUtils.FileName("02.AllOnesInput.xml");
                game = new BalloonsEngine(5, 10);
                game.TryPopBalloons(2, 2);
                game.CollapseRows();

                int[,] gameField = (int[,])typeof(BalloonsEngine).GetField("playField", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(game);
                int[,] expectedOutput = TestUtils.GetExpectedOutput();

                bool valuesAreEqual = TestUtils.CheckFieldsEquality(gameField, expectedOutput);

                Assert.AreEqual(valuesAreEqual, true);
            }
        }

        


    }
}