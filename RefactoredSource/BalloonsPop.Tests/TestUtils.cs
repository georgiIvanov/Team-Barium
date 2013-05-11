using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;
using Balloons_Pops_game;

namespace BalloonsPop.Tests
{
    class TestUtils
    {
        static string filePath;
        static int expectedRow, expectedCol;

        public static bool CheckPlayFields(int[,] gameField, int[,] expectedOutput)
        {
            bool valuesAreEqual = true;
            for (int i = 0; i < expectedRow && valuesAreEqual == true; i++)
            {
                for (int j = 0; j < expectedCol; j++)
                {
                    if (gameField[i, j] != expectedOutput[i, j])
                    {
                        valuesAreEqual = false;
                        break;
                    }
                }
            }
            return valuesAreEqual;
        }

        public static void FileName(string name)
        {
            filePath = string.Format("../../TestData/{0}", name);
        }

        public static int[,] GetExpectedOutput()
        {
            filePath = filePath.Replace("Input", "Output");

            return GetFieldFromXML(expectedRow, expectedCol);
        }

        public static int[,] GenerateFieldShim(BalloonsEngine game, int row, int col)
        {
            expectedRow = row;
            expectedCol = col;

            return GetFieldFromXML(row, col);
        }

        private static int[,] GetFieldFromXML(int row, int col)
        {
            XDocument testFile = XDocument.Load(filePath);
            int[,] playField = new int[row, col];
            int fieldRow = 0;

            foreach (var item in testFile.Descendants("row"))
            {
                string[] cellValues = item.Value.Split();
                for (int i = 0; i < col; i++)
                {
                    playField[fieldRow, i] = int.Parse(cellValues[i]);
                }
                fieldRow++;
            }

            return playField;
        }

        public static void PopAllBalloons(BalloonsEngine game)
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
    }
}
