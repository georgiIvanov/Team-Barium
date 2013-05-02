using System;
using System.Collections.Generic;

namespace Balloons_Pops_game
{
    public struct structOfRow : IComparable<structOfRow>
    {

        public int Value;
        public string Name;
        public structOfRow(int value, string name)
        {

            Value = value;
            Name = name;
        }

        public int CompareTo(structOfRow other)
        {
            return Value.CompareTo(other.Value);
        }
    }
    class Program
    {
        static byte[,] gen(byte rows, byte columns)
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
        static void checkLeft(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row;
            int newColumn = column - 1;
            try
            {
                if (matrix[newRow, newColumn] == searchedItem)
                {
                    matrix[newRow, newColumn] = 0; checkLeft(matrix, newRow, newColumn, searchedItem);
                }
                else return;
            }
            catch (IndexOutOfRangeException)
            { return; }

        }
        static void checkRight(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row;
            int newColumn = column + 1;
            try
            {
                if (matrix[newRow, newColumn] == searchedItem)
                {
                    matrix[newRow, newColumn] = 0;
                    checkRight(matrix, newRow, newColumn, searchedItem);
                }
                else return;
            }
            catch (IndexOutOfRangeException)
            { return; }

        }
        static void checkUp(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row + 1;
            int newColumn = column;
            try
            {
                if (matrix[newRow, newColumn] == searchedItem)
                {
                    matrix[newRow, newColumn] = 0;
                    checkUp(matrix, newRow, newColumn, searchedItem);
                }
                else return;
            }
            catch (IndexOutOfRangeException)
            { return; }
        }

        static void checkDown(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row - 1;
            int newColumn = column;
            try
            {
                if (matrix[newRow, newColumn] == searchedItem)
                {
                    matrix[newRow, newColumn] = 0;
                    checkDown(matrix, newRow, newColumn, searchedItem);
                }
                else return;
            }
            catch (IndexOutOfRangeException)
            { return; }

        }
        static bool change(byte[,] matrixToModify, int rowAtm, int columnAtm)
        {
            if (matrixToModify[rowAtm, columnAtm] == 0)
            {
                return true;
            }
            byte searchedTarget = matrixToModify[rowAtm, columnAtm];
            matrixToModify[rowAtm, columnAtm] = 0;
            checkLeft(matrixToModify, rowAtm, columnAtm, searchedTarget);
            checkRight(matrixToModify, rowAtm, columnAtm, searchedTarget);


            checkUp(matrixToModify, rowAtm, columnAtm, searchedTarget);
            checkDown(matrixToModify, rowAtm, columnAtm, searchedTarget);
            return false;
        }

        static bool doit(byte[,] matrix)
        {
            bool isWinner = true;
            Stack<byte> stek = new Stack<byte>();
            int columnLenght = matrix.GetLength(0);
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < columnLenght; i++)
                {
                    if (matrix[i, j] != 0)
                    {
                        isWinner = false;
                        stek.Push(matrix[i, j]);
                    }
                }
                for (int k = columnLenght - 1; (k >= 0); k--)
                {
                    try
                    {
                        matrix[k, j] = stek.Pop();
                    }
                    catch (Exception)
                    {
                        matrix[k, j] = 0;
                    }
                }
            }
            return isWinner;
        }

        static void sortAndPrintChartFive(string[,] tableToSort)
        {

            List<structOfRow> klasirane = new List<structOfRow>();

            for (int i = 0; i < 5; i++)
            {
                if (tableToSort[i, 0] == null)
                {
                    break;
                }

                klasirane.Add(new structOfRow(int.Parse(tableToSort[i, 0]), tableToSort[i, 1]));

            }

            klasirane.Sort();
            Console.WriteLine("---------TOP FIVE CHART-----------");
            for (int i = 0; i < klasirane.Count; ++i)
            {
                structOfRow slot = klasirane[i];
                Console.WriteLine("{2}.   {0} with {1} moves.", slot.Name, slot.Value, i + 1);
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
            byte[,] playingField = gen(5, 10);
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
                            

                            if (change(playingField, userRow, userColumn))
                            {
                                Console.WriteLine("cannot pop missing ballon!");
                                continue;
                            }
                            userMoves++;
                            if (doit(playingField))
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
                                playingField = gen(5, 10);
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
            playingField = gen(5, 10);
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
