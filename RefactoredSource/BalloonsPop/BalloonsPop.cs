// ********************************
// <copyright file="BalloonsPop.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
using System;

namespace Balloons_Pops_game
{
    /// <summary>
    /// The main class of the console game Ballons-Pop.
    /// </summary>
    public class BalloonsPop
    {
        /// <summary>
        /// The entry point of the program.
        /// </summary>
        static void Main(string[] args)
        {
            string userInput = string.Empty;

            BalloonsEngine game = new BalloonsEngine(5, 10);

            PrintIntroMessage();
            Console.WriteLine(game.FieldOutput());

            while (userInput != "exit")
            {
                PrintMoveMessage();
                userInput = GetInput();

                ExecuteCommand(game, userInput);
            }
        }

        /// <summary>
        /// Executes a command from a predefined set of commands or breaks if invalid command.
        /// </summary>
        /// <param name="game">Instace of the BalloonsEngine class.</param>
        /// <param name="command">User input command to execute.</param>
        private static void ExecuteCommand(BalloonsEngine game, string command)
        {
            switch (command)
            {
                case "restart":
                    Console.WriteLine();
                    PrintIntroMessage();
                    game.RestartGame();
                    Console.WriteLine(game.FieldOutput());
                    break;

                case "top":
                    Console.WriteLine(game.GenerateChart());
                    break;

                case "exit":
                    PrintExitMessage();
                    break;

                default:
                    if (game.CheckMoveValidity(command))
                    {
                        ProcessMove(command, game);
                    }
                    else
                    {
                        PrintInvalidCommandMessage();
                    }

                    break;
            }
        }

        /// <summary>
        /// Processes the move entered by the player.
        /// </summary>
        /// <param name="userInput">Row and column of the move.</param>
        /// <param name="game">Instace of the BalloonsEngine class.</param>
        private static void ProcessMove(string userInput, BalloonsEngine game)
        {
            int userRow, userColumn;
            userRow = ConvertCharToInt(userInput[0]);
            userColumn = ConvertCharToInt(userInput[2]);

            if (!game.TryPopBalloons(userRow, userColumn))
            {
                PrintIllegalMoveMessage();
            }

            game.UserMoves++;
            game.CollapseRows();

            if (game.CheckIfWinning())
            {
                Console.WriteLine(game.FieldOutput());
                Console.WriteLine("Congratulations! You popped all baloons in {0} moves.", game.UserMoves);

                int place = game.ChartPlaceIndex();
                if (place != -1)
                {
                    Console.WriteLine("Please enter your name for the top scoreboard: ");
                    string username = Console.ReadLine();
                    game.RecordHighscore(username, place);
                    Console.WriteLine(game.GenerateChart());
                }
                else
                {
                    Console.WriteLine("Game Over!");
                    Console.WriteLine(game.GenerateChart());
                }

                game.RestartGame();
            }

            Console.WriteLine(game.FieldOutput());
        }

        /// <summary>
        /// Reads input data from the console.
        /// </summary>
        /// <returns>Trimmed input data to lowercase.</returns>
        static string GetInput()
        {
            return Console.ReadLine().ToLower().Trim();
        }

        /// <summary>
        /// Convert char character to integer number.
        /// </summary>
        /// <param name="input">The character.</param>
        /// <returns>Integer number;</returns>
        static int ConvertCharToInt(char input)
        {
            return input - '0';
        }

        /// <summary>
        /// Prints intro message to the console.
        /// </summary>
        public static void PrintIntroMessage()
        {
            Console.WriteLine(
                "Welcome to “Balloons Pops” game. Please try to pop the balloons. "
                + "Use 'top' to view the top scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
            Console.WriteLine();
        }

        /// <summary>
        /// Prints illegal move message to the console.
        /// </summary>
        private static void PrintIllegalMoveMessage()
        {
            Console.WriteLine("Illegal move: cannot pop missing ballon! Try Again!");
            Console.WriteLine();
        }

        /// <summary>
        /// Prints message to the console for entering row and column of the move.
        /// </summary>
        private static void PrintMoveMessage()
        {
            Console.Write("Enter a row and column: ");
        }

        /// <summary>
        /// Print message for invalid command to the console.
        /// </summary>
        private static void PrintInvalidCommandMessage()
        {
            Console.WriteLine("Invalid move or command! Try Again!");
            Console.WriteLine();
        }

        /// <summary>
        /// Print exit message and info for Ballons-Pop game to the console.
        /// </summary>
        private static void PrintExitMessage()
        {
            Console.WriteLine("\nSo sorry you are leaving! Good bye!\n");
            Console.WriteLine("Balloons-Pop-4 game\nVersion 1.0\n" + "Copyright (c) 2013 Telerik Academy. All rights reserved.\n");
        }
    }
}
