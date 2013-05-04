using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Balloons_Pops_game
{
    class BalloonsEngine
    {
        int fieldRows, fieldCols, userMoves;
        int[,] playField;
        string[,] topFive;

        public BalloonsEngine(int rows, int columns)
        {
            playField = GeneratePlayField(rows, columns);
            fieldRows = rows;
            fieldCols = columns;
            userMoves = 0;
            topFive = new string[5, 2];
        }

        public int UserMoves
        {
            get
            {
                return userMoves;
            }
            set
            {
                this.userMoves = value;
            }
        }
        public int FieldRows
        {
            get
            {
                return fieldRows;
            }
        }
        public int FieldColumns
        {
            get
            {
                return fieldCols;
            }
        }

        int[,] GeneratePlayField(int rows, int columns)
        {
            int[,] playField = new int[rows, columns];
            Random randNumber = new Random();
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    playField[row, column] = randNumber.Next(1, 5);
                }
            }
            return playField;
        }

        public bool CheckMoveValidity(string userInput)
        {
            return (userInput.Length == 3) &&
                 (userInput[0] >= '0' && userInput[0] <= '4') &&
                 (userInput[2] >= '0' && userInput[2] <= '9') &&
                 (userInput[1] == ' ' || userInput[1] == '.' || userInput[1] == ',');
        }

        public string FieldOutput()
        {
            StringBuilder result = new StringBuilder();
            result.Append("    ");
            for (int column = 0; column < fieldCols; column++)
            {
                result.Append(column + " ");
            }

            result.Append("\n   ");
            for (int column = 0; column < fieldCols * 2 + 1; column++)
            {
                result.Append("-");
            }

            result.AppendLine();

            for (int i = 0; i < fieldRows; i++)
            {
                result.Append(i + " | ");
                for (int j = 0; j < fieldCols; j++)
                {
                    if (playField[i, j] == 0)
                    {
                        result.Append("  ");
                        continue;
                    }

                    result.Append(playField[i, j] + " ");
                }
                result.AppendLine("| ");
            }

            result.Append("   ");
            for (int column = 0; column < fieldCols * 2 + 1; column++)
            {
                result.Append("-");
            }

            result.AppendLine();
            return result.ToString();
        }

        public void CollapseRows()
        {
            Stack<int> stack = new Stack<int>();

            for (int col = 0; col < fieldCols; col++)
            {
                for (int i = 0; i < fieldRows; i++)
                {
                    if (playField[i, col] != 0)
                    {
                        stack.Push(playField[i, col]);
                    }
                }

                for (int k = fieldRows - 1; k >= 0; k--)
                {
                    if (stack.Count > 0)
                    {
                        playField[k, col] = stack.Pop();
                    }
                    else
                    {
                        playField[k, col] = 0;
                    }
                }
            }
        }

        public bool CheckIfWinning()
        {
            for (int i = 0; i < fieldRows; i++)
            {
                for (int j = 0; j < fieldCols; j++)
                {
                    if (playField[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool TryPopBalloons(int row, int col)
        {
            if (playField[row, col] == 0)
            {
                return false;
            }

            int searchedTarget = playField[row, col];
            playField[row, col] = 0;

            PopRowsAndCols(row, col - 1, searchedTarget, 3);
            PopRowsAndCols(row, col + 1, searchedTarget, 4);
            PopRowsAndCols(row - 1, col, searchedTarget, 1);
            PopRowsAndCols(row + 1, col, searchedTarget, 2);
            return true;
        }

        private void PopRowsAndCols(int row, int col, int searchedItem, int direction)
        {
            if (row < 0 || row >= fieldRows || col < 0 || col >= fieldCols)
            {
                return;
            }

            if (searchedItem != playField[row, col])
            {
                return;
            }

            playField[row, col] = 0;

            switch (direction)
            {
                case 1: PopRowsAndCols(row - 1, col, searchedItem, direction); break;
                case 2: PopRowsAndCols(row + 1, col, searchedItem, direction); break;
                case 3: PopRowsAndCols(row, col - 1, searchedItem, direction); break;
                case 4: PopRowsAndCols(row, col + 1, searchedItem, direction); break;
                default:
                    break;
            }
        }

        public void RestartGame()
        {
            playField = GeneratePlayField(5, 10);
            userMoves = 0;
        }

        public string GenerateChart()
        {
            StringBuilder result = new StringBuilder();
            List<ScoreEntry> scores = new List<ScoreEntry>();

            for (int i = 0; i < 5; i++)
            {
                if (topFive[i, 0] == null)
                {
                    break;
                }

                scores.Add(new ScoreEntry(int.Parse(topFive[i, 0]), topFive[i, 1]));
            }

            scores.Sort();
            result.AppendLine("---------TOP FIVE CHART-----------");
            for (int i = 0; i < scores.Count; ++i)
            {
                result.AppendFormat("{2}. {0} with {1} moves.\n", scores[i].Name, scores[i].Score, i + 1);
            }
            result.AppendLine("----------------------------------");

            return result.ToString();
        }

        public int ChartPlaceIndex()
        {
            int userPlace = -1;
            for (int i = 0; i < topFive.GetLength(0); i++)
            {
                if (topFive[i, 0] == null)
                {
                    userPlace = i;
                    break;
                }
            }

            if (userPlace == -1)
            {
                int worstScore = int.MinValue;
                for (int i = 0; i < topFive.GetLength(0); i++)
                {
                    int currentScore = int.Parse(topFive[i, 0]);
                    if (currentScore > worstScore && userMoves < currentScore)
                    {
                        worstScore = currentScore;
                        userPlace = i;
                    }
                }
            }

            return userPlace;
        }

        public void signIfSkilled(string username, int place)
        {
            topFive[place, 0] = userMoves.ToString();
            topFive[place, 1] = username;
        }
    }
}
