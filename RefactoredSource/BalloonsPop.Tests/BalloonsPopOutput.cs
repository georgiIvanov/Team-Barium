using System;
using Balloons_Pops_game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace BalloonsPop.Tests
{
    [TestClass]
    public class BalloonsPopOutput
    {
        public void PopAllBalloons(BalloonsEngine game)
        {
            for (int i = 0; i < game.FieldRows; i++)
            {
                for (int j = 0; j < game.FieldColumns; j++)
                {
                    game.TryPopBalloons(i, j);
                    game.UserMoves++;
                    game.CollapseRows();
                }
            }
        }

        [TestMethod]
        public void FieldOutputLength()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            string outputField = game.FieldOutput();

            Assert.AreEqual(217, outputField.Length); 
        }

        [TestMethod]
        public void FieldOutputLengthAfterMove()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            game.TryPopBalloons(0, 0);
            game.TryPopBalloons(4, 9);
            string outputField = game.FieldOutput();

            Assert.AreEqual(217, outputField.Length);
        }

        [TestMethod]
        public void FieldOutputRestartGame()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            game.TryPopBalloons(2, 5);
            game.TryPopBalloons(4, 1);
            int outputFieldLength = game.FieldOutput().Length;
            game.RestartGame();
            int outputFieldLengthAfterRestart = game.FieldOutput().Length;

            Assert.AreEqual(outputFieldLength, outputFieldLengthAfterRestart);
        }

        [TestMethod]
        public void CheckMoveValidityInput00()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            bool result = game.CheckMoveValidity("0 0");

            Assert.AreEqual(true, result, "Invalid input cell!");
        }

        [TestMethod]
        public void CheckMoveValidityInput49()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            bool result = game.CheckMoveValidity("4 9");

            Assert.AreEqual(true, result, "Invalid input cell!");
        }

        [TestMethod]
        public void CheckMoveValidityInputNegativeNum()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            bool result = game.CheckMoveValidity("-2 -1");

            Assert.AreEqual(false, result, "Invalid input cell!");
        }

        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void CheckMoveValidityInputNull()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            game.CheckMoveValidity(null);
        }

        [TestMethod]
        public void CheckMoveValidityInputEmpty()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            bool result = game.CheckMoveValidity(string.Empty);

            Assert.AreEqual(false, result, "Invalid input cell!");
        }

        [TestMethod]
        public void TryPopBalloonsOutputTrue()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            bool result = game.TryPopBalloons(1, 2);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TryPopBalloonsOutputFalse()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            game.TryPopBalloons(0, 7);
            bool result = game.TryPopBalloons(0, 7);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CheckIfWinningOutputFalse()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            game.TryPopBalloons(2, 6);
            game.TryPopBalloons(1, 8);
            bool result = game.CheckIfWinning();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CheckIfWinningOutputTrue()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);

            PopAllBalloons(game);
            bool result = game.CheckIfWinning();

            Assert.AreEqual(true, result);
        }

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

            PopAllBalloons(game);
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

            PopAllBalloons(game);
            int placeResult = game.ChartPlaceIndex();
            if (placeResult != -1)
            {
                game.RecordHighscore("Bunny", placeResult);
            }
            game.RestartGame();

            PopAllBalloons(game);
            placeResult = game.ChartPlaceIndex();
            if (placeResult != -1)
            {
                game.RecordHighscore("Sunny", placeResult);
            }
            game.RestartGame();
            PopAllBalloons(game);
            placeResult = game.ChartPlaceIndex();

            Assert.AreEqual(2, placeResult);
        }
    }
}
