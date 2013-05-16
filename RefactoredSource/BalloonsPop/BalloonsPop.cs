using System;

namespace Balloons_Pops_game
{
    public class BalloonsPop
    {
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

        static string GetInput()
        {
            return Console.ReadLine().ToLower().Trim();
        }

        static int ConvertCharToInt(char input)
        {
            return input - '0';
        }

        public static void PrintIntroMessage()
        {
            Console.WriteLine(
                "Welcome to “Balloons Pops” game. Please try to pop the balloons. "
                + "Use 'top' to view the top scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
            Console.WriteLine();
        }

        private static void PrintIllegalMoveMessage()
        {
            Console.WriteLine("Illegal move: cannot pop missing ballon! Try Again!");
            Console.WriteLine();
        }

        private static void PrintMoveMessage()
        {
            Console.Write("Enter a row and column: ");
        }

        private static void PrintInvalidCommandMessage()
        {
            Console.WriteLine("Invalid move or command! Try Again!");
            Console.WriteLine();
        }

        private static void PrintExitMessage()
        {
            Console.WriteLine("So sorry you are leaving! Good bye!");
            Console.WriteLine();
        }
    }
}
