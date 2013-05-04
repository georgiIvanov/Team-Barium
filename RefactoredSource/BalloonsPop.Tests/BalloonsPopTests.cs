using System;
using Balloons_Pops_game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BalloonsPop.Tests
{
    [TestClass]
    public class BalloonsPopTests
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
    }
}
