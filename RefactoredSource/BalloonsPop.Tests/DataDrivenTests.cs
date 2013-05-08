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

        [TestMethod]
        public void PopFourthRowAndFirstCol()
        {
            using (ShimsContext.Create())
            {
                BalloonsEngine game = new BalloonsEngine(0, 0);

                ShimBalloonsEngine.AllInstances.GeneratePlayFieldInt32Int32
                = (game1, x, y) => TestUtils.GenerateFieldShim(game, 5, 10);

                TestUtils.FileName("03.AllOnesInput.xml");
                game = new BalloonsEngine(5, 10);
                game.TryPopBalloons(4, 1);

                int[,] gameField = (int[,])typeof(BalloonsEngine).GetField("playField", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(game);
                int[,] expectedOutput = TestUtils.GetExpectedOutput();

                bool valuesAreEqual = TestUtils.CheckFieldsEquality(gameField, expectedOutput);

                Assert.AreEqual(valuesAreEqual, true);
            }
        }

        [TestMethod]
        public void PopFourthRowAndNinthCol()
        {
            using (ShimsContext.Create())
            {
                BalloonsEngine game = new BalloonsEngine(0, 0);

                ShimBalloonsEngine.AllInstances.GeneratePlayFieldInt32Int32
                = (game1, x, y) => TestUtils.GenerateFieldShim(game, 5, 10);

                TestUtils.FileName("04.AllOnesInput.xml");
                game = new BalloonsEngine(5, 10);
                game.TryPopBalloons(4, 9);
                game.CollapseRows();

                int[,] gameField = (int[,])typeof(BalloonsEngine).GetField("playField", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(game);
                int[,] expectedOutput = TestUtils.GetExpectedOutput();

                bool valuesAreEqual = TestUtils.CheckFieldsEquality(gameField, expectedOutput);

                Assert.AreEqual(valuesAreEqual, true);
            }
        }

        [TestMethod]
        public void PopEmpty()
        {
            using (ShimsContext.Create())
            {
                BalloonsEngine game = new BalloonsEngine(0, 0);

                ShimBalloonsEngine.AllInstances.GeneratePlayFieldInt32Int32
                = (game1, x, y) => TestUtils.GenerateFieldShim(game, 5, 10);

                TestUtils.FileName("04.AllOnesOutput.xml");
                game = new BalloonsEngine(5, 10);
                game.TryPopBalloons(4, 9);
                game.CollapseRows();

                int[,] gameField = (int[,])typeof(BalloonsEngine).GetField("playField", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(game);
                int[,] expectedOutput = TestUtils.GetExpectedOutput();

                bool valuesAreEqual = TestUtils.CheckFieldsEquality(gameField, expectedOutput);

                Assert.AreEqual(valuesAreEqual, true);
            }
        }

        [TestMethod]
        public void PopFirstRowAndFifthCol()
        {
            using (ShimsContext.Create())
            {
                BalloonsEngine game = new BalloonsEngine(0, 0);

                ShimBalloonsEngine.AllInstances.GeneratePlayFieldInt32Int32
                = (game1, x, y) => TestUtils.GenerateFieldShim(game, 5, 10);

                TestUtils.FileName("05.AllOnesInput.xml");
                game = new BalloonsEngine(5, 10);
                game.TryPopBalloons(1, 5);

                int[,] gameField = (int[,])typeof(BalloonsEngine).GetField("playField", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(game);
                int[,] expectedOutput = TestUtils.GetExpectedOutput();

                bool valuesAreEqual = TestUtils.CheckFieldsEquality(gameField, expectedOutput);

                Assert.AreEqual(valuesAreEqual, true);
            }
        }

        [TestMethod]
        public void PopThirdRowAndFourthCol()
        {
            using (ShimsContext.Create())
            {
                BalloonsEngine game = new BalloonsEngine(0, 0);

                ShimBalloonsEngine.AllInstances.GeneratePlayFieldInt32Int32
                = (game1, x, y) => TestUtils.GenerateFieldShim(game, 5, 10);

                TestUtils.FileName("06.AllOnesInput.xml");
                game = new BalloonsEngine(5, 10);
                game.TryPopBalloons(3, 4);

                int[,] gameField = (int[,])typeof(BalloonsEngine).GetField("playField", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(game);
                int[,] expectedOutput = TestUtils.GetExpectedOutput();

                bool valuesAreEqual = TestUtils.CheckFieldsEquality(gameField, expectedOutput);

                Assert.AreEqual(valuesAreEqual, true);
            }
        }
    }
}