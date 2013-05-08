using System;
using Balloons_Pops_game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BalloonsPop.Tests
{
    [TestClass]
    public class BalloonsPopOutput
    {

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
        public void CheckMoveValidityInput18()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            bool result = game.CheckMoveValidity("1,8");

            Assert.AreEqual(true, result, "Invalid input cell!");
        }

        [TestMethod]
        public void CheckMoveValidityInput2Interval9()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            bool result = game.CheckMoveValidity("2, 9");

            Assert.AreEqual(false, result, "Invalid input cell!");
        }

        [TestMethod]
        public void CheckMoveValidityInput35()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            bool result = game.CheckMoveValidity("3.5");

            Assert.AreEqual(true, result, "Invalid input cell!");
        }

        [TestMethod]
        public void CheckMoveValidityInputRestart()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            bool result = game.CheckMoveValidity("restart");

            Assert.AreEqual(false, result, "Invalid input cell!");
        }

        [TestMethod]
        public void CheckMoveValidityInputNegativeNum()
        {
            BalloonsEngine game = new BalloonsEngine(5, 10);
            bool result = game.CheckMoveValidity("0 -1");

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

        //[TestMethod]
        //public void CheckIfWinningOutput()
        //{
        //    BalloonsEngine game = new BalloonsEngine(5, 10);
        //    game.playField = new int[,]
        //    { 
        //        {0,0,0,0,0,0,0,0,0,0},
        //        {0,0,0,0,0,0,0,0,0,0},
        //        {0,0,0,0,0,0,0,0,0,0},
        //        {0,0,0,0,0,0,0,0,0,0},
        //        {0,0,0,0,0,0,0,0,0,0},
        //    };
        //    bool result = game.CheckIfWinning();
        //
        //    Assert.AreEqual(true, result);
        //}

        //[TestMethod]
        //public void CheckIfWinningOutput()
        //{
        //    BalloonsEngine game = new BalloonsEngine(5, 10);
        //    game.playField = new int[,]
        //    { 
        //        {0,0,0,0,0,0,0,0,0,0},
        //        {0,0,1,0,0,0,0,0,0,0},
        //        {0,0,1,1,0,0,0,0,0,0},
        //        {0,0,0,0,0,0,0,4,0,0},
        //        {0,0,0,0,0,0,0,4,0,0},
        //    };
        //    bool result = game.CheckIfWinning();
        //
        //    Assert.AreEqual(false, result);
        //}
    }
}
