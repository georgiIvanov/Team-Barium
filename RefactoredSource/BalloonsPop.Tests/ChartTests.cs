using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Balloons_Pops_game;

namespace BalloonsPop.Tests
{
    [TestClass]
    public class ChartTests
    {
        [TestMethod]
        public void GenerateChartOutputWithoutTop()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            game.TryPopBalloons(2, 6);
            string result = game.GenerateChart();

            StringBuilder expectedResult = new StringBuilder();
            expectedResult.AppendLine("The scoreboard is empty.");

            Assert.AreEqual(expectedResult.ToString(), result);
        }

        [TestMethod]
        public void GenerateChartOutputWithTop()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);

            TestUtils.PopAllBalloons(game);
            int place = game.ChartPlaceIndex();
            string result = string.Empty;
            if (place != -1)
            {
                game.RecordHighscore("Bunny", place);
                result = game.GenerateChart();
            }

            StringBuilder expectedResult = new StringBuilder();
            expectedResult.AppendLine("---------TOP FIVE CHART-----------");
            expectedResult.AppendFormat("{2}. {0} with {1} moves.\n", "Bunny", game.UserMoves, 1);
            expectedResult.AppendLine("----------------------------------");

            Assert.AreEqual(expectedResult.ToString(), result);
        }

        [TestMethod]
        public void ChartPlaceIndexOutput()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);

            TestUtils.PopAllBalloons(game);
            int placeResult = game.ChartPlaceIndex();
            if (placeResult != -1)
            {
                game.RecordHighscore("Bunny", placeResult);
            }
            game.RestartGame();

            TestUtils.PopAllBalloons(game);
            placeResult = game.ChartPlaceIndex();
            if (placeResult != -1)
            {
                game.RecordHighscore("Sunny", placeResult);
            }
            game.RestartGame();
            TestUtils.PopAllBalloons(game);
            placeResult = game.ChartPlaceIndex();

            Assert.AreEqual(2, placeResult);
        }

        [TestMethod]
        public void ChartPlacementAfter5()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            game.UserMoves = 5;
            for (int i = 0; i < 5; i++)
            {
                game.UserMoves++;
                game.RecordHighscore(i.ToString(), game.ChartPlaceIndex());
            }

            game.UserMoves = 1;
            game.RecordHighscore("FirstPlace", game.ChartPlaceIndex());

            StringBuilder expectedBuilder = new StringBuilder();
            expectedBuilder.AppendLine("---------TOP FIVE CHART-----------");
            expectedBuilder.AppendFormat("{2}. {0} with {1} moves.\n", "FirstPlace", 1, 1);
            for (int i = 2; i <= 5; i++)
            {
                expectedBuilder.AppendFormat("{2}. {0} with {1} moves.\n", (i - 2).ToString(), i + 4, i);
            }

            expectedBuilder.AppendLine("----------------------------------");

            Assert.AreEqual(expectedBuilder.ToString(), game.GenerateChart());
        }
    }
}
