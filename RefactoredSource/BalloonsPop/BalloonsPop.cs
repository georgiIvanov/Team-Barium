using System;

namespace Balloons_Pops_game
{
    class BalloonsPop
    {
        static void Main(string[] args)
        {
            string userInput = string.Empty;
            string restartMessage = "Welcome to “Balloons Pops” game. Please try to pop the balloons. Use 'top' to view the top scoreboard, 'restart' to start a new game and 'exit' to quit the game.";
            BalloonsEngine game = new BalloonsEngine(5, 10);

            Console.WriteLine(restartMessage);
            Console.WriteLine(game.FieldOutput()); 

            while (userInput != "exit")
            {
                Console.WriteLine("Enter a row and column: ");
                userInput = GetInput();

                switch (userInput)
                {
                    case "restart":
                        Console.WriteLine();
                        Console.WriteLine(restartMessage);
                        game.RestartGame();
                        Console.WriteLine(game.FieldOutput()); 
                        break;

                    case "top":
                        Console.WriteLine(game.GenerateChart());
                        break;
                    case "exit": 
                        break;

                    default:
                        if (game.CheckMoveValidity(userInput))
                        {
                            ProcessMove(userInput, game);
                        }
                        else
                        {
                            Console.WriteLine("Invalid move or command! Try Again! ");
                        }
                        break;
                }
            }
            Console.WriteLine("Good Bye! ");

        }

        private static void ProcessMove(string userInput, BalloonsEngine game)
        {
            int userRow, userColumn;
            userRow = ConvertCharToInt(userInput[0]);
            userColumn = ConvertCharToInt(userInput[2]);

            if (userRow > game.FieldRows || userRow < 0 ||
                userColumn > game.FieldColumns || userColumn < 0)
            {
                Console.WriteLine("Wrong input! Try Again!");
            }


            if (!game.TryPopBalloons(userRow, userColumn))
            {
                Console.WriteLine("Cannot pop missing ballon!");
            }

            game.UserMoves++;
            game.CollapseRows();

            if (game.CheckIfWinning())
            {
                Console.WriteLine(game.FieldOutput());
                Console.WriteLine("Gratz! You completed it in {0} moves.", game.UserMoves);
                int place = game.ChartPlaceIndex();
                if (place != -1)
                {
                    Console.WriteLine("Type in your name.");
                    string username = Console.ReadLine();
                    game.RecordHighscore(username, place);
                    Console.WriteLine(game.GenerateChart());
                }
                else
                {
                    Console.WriteLine("I am sorry you are not skillful enough for TopFive chart!");
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
    }
}
