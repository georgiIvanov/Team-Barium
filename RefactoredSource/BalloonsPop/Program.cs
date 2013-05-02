using System;
using System.Collections.Generic;

namespace Balloons_Pops_game
{
    

    class Program
    {
        static byte[,] GeneratePlayField(byte rows, byte columns)
        {
            byte[,] temp = new byte[rows, columns];
            Random randNumber = new Random();
            for (byte row = 0; row < rows; row++)
            {
                for (byte column = 0; column < columns; column++)
                {
                    byte tempByte = (byte)randNumber.Next(1, 5);
                    temp[row, column] = tempByte;
                }
            }
            return temp;
        }

        static void PrintField(byte[,] matrix)
        {
            Console.Write("    ");
            for (byte column = 0; column < matrix.GetLongLength(1); column++)
            {
                Console.Write(column + " ");
            }


            Console.Write("\n   ");
            for (byte column = 0; column < matrix.GetLongLength(1) * 2 + 1; column++)
            {
                Console.Write("-");
            }

            Console.WriteLine();

            for (byte i = 0; i < matrix.GetLongLength(0); i++)
            {
                Console.Write(i + " | ");
                for (byte j = 0; j < matrix.GetLongLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        Console.Write("  ");
                        continue;
                    }



                    Console.Write(matrix[i, j] + " ");
                }
                Console.Write("| ");
                Console.WriteLine();
            }

            Console.Write("   ");
            for (byte column = 0; column < matrix.GetLongLength(1) * 2 + 1; column++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        static void PopRowsAndCols(byte[,] gameField, int row, int col, int searchedItem, int direction)
        {
            if (row < 0 || row >= gameField.GetLength(0) || col < 0 || col >= gameField.GetLength(1))
            {
                return;
            }

            if (searchedItem != gameField[row, col])
            {
                return;
            }

            gameField[row, col] = 0;

            switch (direction)
            {
                case 1: PopRowsAndCols(gameField, row - 1, col, searchedItem, direction); break;
                case 2: PopRowsAndCols(gameField, row + 1, col, searchedItem, direction); break;
                case 3: PopRowsAndCols(gameField, row, col - 1, searchedItem, direction); break;
                case 4: PopRowsAndCols(gameField, row, col + 1, searchedItem, direction); break;
                default:
                    break;
            }
        }


        static bool PopBalloons(byte[,] matrixToModify, int row, int col)
        {
            if (matrixToModify[row, col] == 0)
            {
                return true;
            }
            byte searchedTarget = matrixToModify[row, col];
            matrixToModify[row, col] = 0;

            PopRowsAndCols(matrixToModify, row, col - 1, searchedTarget, 3);
            PopRowsAndCols(matrixToModify, row, col + 1, searchedTarget, 4);
            PopRowsAndCols(matrixToModify, row - 1, col, searchedTarget, 1);
            PopRowsAndCols(matrixToModify, row + 1, col, searchedTarget, 2);
            return false;
        }

        static bool CollapseRows(byte[,] matrix)
        {
            bool isWinner = true;
            Stack<byte> stack = new Stack<byte>();

            int rows = matrix.GetLength(0);

            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                for (int i = 0; i < rows; i++)
                {
                    if (matrix[i, col] != 0)
                    {
                        isWinner = false;
                        stack.Push(matrix[i, col]);
                    }
                }
                for (int k = rows - 1; k >= 0 ; k--)
                {
                    if(stack.Count > 0)
                    {
                        matrix[k, col] = stack.Pop();
                    }
                    else
                    {
                        matrix[k, col] = 0;
                    }
                }
            }
            return isWinner;
        }

        static void sortAndPrintChartFive(string[,] tableToSort)
        {
            List<ScoreEntry> scores = new List<ScoreEntry>();

            for (int i = 0; i < 5; i++)
            {
                if (tableToSort[i, 0] == null)
                {
                    break;
                }

                scores.Add(new ScoreEntry(int.Parse(tableToSort[i, 0]), tableToSort[i, 1]));
            }

            scores.Sort();
            Console.WriteLine("---------TOP FIVE CHART-----------");
            for (int i = 0; i < scores.Count; ++i)
            {
                ScoreEntry slot = scores[i];
                Console.WriteLine("{2}.   {0} with {1} moves.", slot.Name, slot.Score, i + 1);
            }
            Console.WriteLine("----------------------------------");


        }

        static bool signIfSkilled(string[,] Chart, int points)
        {
            bool Skilled = false;
            int worstMoves = 0;
            int worstMovesChartPosition = 0;
            for (int i = 0; i < 5; i++)
            {
                if (Chart[i, 0] == null)
                {
                    Console.WriteLine("Type in your name.");
                    string tempUserName = Console.ReadLine();
                    Chart[i, 0] = points.ToString();
                    Chart[i, 1] = tempUserName;
                    Skilled = true;
                    break;
                }
            }
            if (Skilled == false)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (int.Parse(Chart[i, 0]) > worstMoves)
                    {
                        worstMovesChartPosition = i;
                        worstMoves = int.Parse(Chart[i, 0]);
                    }
                }
            }
            if (points < worstMoves && Skilled == false)
            {
                Console.WriteLine("Type in your name.");
                string tempUserName = Console.ReadLine();
                Chart[worstMovesChartPosition, 0] = points.ToString();
                Chart[worstMovesChartPosition, 1] = tempUserName;
                Skilled = true;
            }
            return Skilled;
        }

        static void Main(string[] args)
        {
            string[,] topFive = new string[5, 2];
            string userInput = string.Empty;
            byte[,] playingField = GeneratePlayField(5, 10);
            int userMoves = 0;

            PrintField(playingField);

            while (userInput != "exit")
            {
                Console.WriteLine("Enter a row and column: ");
                userInput = GetInput();

                switch (userInput)
                {
                    case "restart":
                        RestartGame(ref playingField, ref userMoves);
                        break;

                    case "top":
                        sortAndPrintChartFive(topFive); // brake in two distinct parts with output
                        break;
                    case "exit": 
                        break;

                    default:
                        if (CheckMoveValidity(userInput))
                        {
                            int userRow, userColumn;
                            userRow = ConvertCharToInt(userInput[0]);
                            userColumn = ConvertCharToInt(userInput[2]);

                            if (userRow > 4 || userRow < 0 || userColumn > 9 || userColumn < 0)
                            {
                                Console.WriteLine("Wrong input ! Try Again ! ");
                                continue;
                            }
                            

                            if (PopBalloons(playingField, userRow, userColumn))
                            {
                                Console.WriteLine("cannot pop missing ballon!");
                                continue;
                            }

                            userMoves++;
                            if (CollapseRows(playingField))
                            {
                                Console.WriteLine("Gratz ! You completed it in {0} moves.", userMoves);
                                if (signIfSkilled(topFive, userMoves))
                                {
                                    sortAndPrintChartFive(topFive);
                                }
                                else
                                {
                                    Console.WriteLine("I am sorry you are not skillful enough for TopFive chart!");
                                }
                                playingField = GeneratePlayField(5, 10);
                                userMoves = 0;
                            }
                            PrintField(playingField);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Wrong input ! Try Again ! ");
                            break;
                        }


                }
            }
            Console.WriteLine("Good Bye! ");

        }

        private static void RestartGame(ref byte[,] playingField, ref int userMoves)
        {
            playingField = GeneratePlayField(5, 10);
            PrintField(playingField);
            userMoves = 0;
        }

        private static bool CheckMoveValidity(string userInput)
        {
            return (userInput.Length == 3) &&
                 (userInput[0] >= '0' && userInput[0] <= '4') &&
                 (userInput[2] >= '0' && userInput[2] <= '9') &&
                 (userInput[1] == ' ' || userInput[1] == '.' || userInput[1] == ',');
        }

        static string GetInput()
        {
            return Console.ReadLine().ToLower().Trim();
        }

        static int ConvertCharToInt(char input)
        {
            return input - '0';
        }
    }
}
