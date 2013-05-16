// ********************************
// <copyright file="BalloonsEngine.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
using System;
using System.Collections.Generic;
using System.Text;

namespace Balloons_Pops_game
{
    /// <summary>
    /// The Ballons-Pop-4 Engine.
    /// </summary>
    public class BalloonsEngine
    {
        /// <summary>
        /// Represents the number of the rows on the playfield.
        /// </summary>
        private readonly int fieldRows;

        /// <summary>
        /// Represents the number of the columns on the playfield.
        /// </summary>
        private readonly int fieldCols;

        /// <summary>
        /// Represents the number of the player's moves.
        /// </summary>
        private int userMoves;

        /// <summary>
        /// Represents the playfield.
        /// </summary>
        private int[,] playField;

        /// <summary>
        /// Represents the top five players chart.
        /// </summary>
        private string[,] topFive;

        /// <summary>
        /// Initializes a new instance of the <see cref="BalloonsEngine"/> class.
        /// </summary>
        /// <param name="rows">The number of the playfield's rows.</param>
        /// <param name="columns">The number of the playfield's columns.</param>
        public BalloonsEngine(int rows, int columns)
        {
            this.playField = GeneratePlayField(rows, columns);
            this.fieldRows = rows;
            this.fieldCols = columns;
            this.UserMoves = 0;
            this.topFive = new string[5, 2];
        }

        /// <summary>
        /// Gets or sets the player's moves.
        /// </summary>
        public int UserMoves
        {
            get
            {
                return this.userMoves;
            }
            set
            {
                this.userMoves = value;
            }
        }

        /// <summary>
        /// Gets the playfield's rows.
        /// </summary>
        public int FieldRows
        {
            get
            {
                return this.fieldRows;
            }
        }

        /// <summary>
        /// Gets the playfield's columns.
        /// </summary>
        public int FieldColumns
        {
            get
            {
                return fieldCols;
            }
        }

        /// <summary>
        /// Generates a two-dimensional integer array which represents the playfield.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <returns>A two-dimensional integer array with random numbers, representing the playfield.</returns>
        private int[,] GeneratePlayField(int rows, int columns)
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

        /// <summary>
        /// Check validity of the player's move input.
        /// </summary>
        /// <param name="userInput">The player's move input.</param>
        /// <returns>True if move is valid or false if move is invalid.</returns>
        public bool CheckMoveValidity(string userInput)
        {
            return (userInput.Length == 3) &&
                 (userInput[0] >= '0' && userInput[0] <= '4') &&
                 (userInput[2] >= '0' && userInput[2] <= '9') &&
                 (userInput[1] == ' ' || userInput[1] == '.' || userInput[1] == ',');
        }

        /// <summary>
        /// Generates the playfield output.
        /// </summary>
        /// <returns>Playfield output.</returns>
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

        /// <summary>
        /// Collapses playfield rows.
        /// </summary>
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

        /// <summary>
        /// Check if the player is winning the game.
        /// </summary>
        /// <returns>True if all the ballons have been popped or false if unpopped balloons remain.</returns>
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

        /// <summary>
        /// Tries to pop ballons in the current cell and its adjacents of the playfield.
        /// </summary>
        /// <param name="row">The current row of the player's move.</param>
        /// <param name="col">The current column of the player's move.</param>
        /// <returns>True if there is ballon in the current cell or false if there isn't.</returns>
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

        /// <summary>
        /// Finds and pops ballons identical to the current one.
        /// </summary>
        /// <param name="row">The current row of the player's move.</param>
        /// <param name="col">The current column of the player's move.</param>
        /// <param name="searchedItem">The current cell of the player's move.</param>
        /// <param name="direction">The possible direction.</param>
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
            }
        }

        /// <summary>
        /// Restarts the game and generates the new playfield.
        /// </summary>
        public void RestartGame()
        {
            this.playField = GeneratePlayField(5, 10);
            this.UserMoves = 0;
        }

        /// <summary>
        /// Generates Top Five chart of the winners.
        /// </summary>
        /// <returns>Top Five chart of the winners or message for empty scoreboard.</returns>
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

            if (scores.Count > 0)
            {
                scores.Sort();
                result.AppendLine("---------TOP FIVE CHART-----------");
                for (int i = 0; i < scores.Count; ++i)
                {
                    result.AppendFormat("{2}. {0} with {1} moves.\n", scores[i].Name, scores[i].Score, i + 1);
                }
                result.AppendLine("----------------------------------");
            }
            else
            {
                result.AppendLine("The scoreboard is empty.");
            }

            return result.ToString();
        }

        /// <summary>
        /// Finds index of the player's score in the score chart.
        /// </summary>
        /// <returns>Index of the player's score in score-chart array or -1 if the player's score is lower than the worse score in the chart.</returns>
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

        /// <summary>
        /// Saves the player's score in topFive[,] array.
        /// </summary>
        /// <param name="username">The name of the player.</param>
        /// <param name="place">The number of the player's moves.</param>
        public void RecordHighscore(string username, int place)
        {
            topFive[place, 0] = UserMoves.ToString();
            topFive[place, 1] = username;
        }
    }
}