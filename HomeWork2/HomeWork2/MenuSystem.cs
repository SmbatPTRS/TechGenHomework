// MenuSystem.cs

using System;

namespace ConsoleTicTacToe
{
    class MenuSystem
    {
        private string username = "";

        public void Start()
        {
            AskUsername();

            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("=== TIC TAC TOE ===");
                Console.WriteLine();
                Console.WriteLine("Current User: " + username);
                Console.WriteLine();
                Console.WriteLine("1. Play");
                Console.WriteLine("2. Settings");
                Console.WriteLine("3. About");
                Console.WriteLine("4. Quit");
                Console.WriteLine();

                Console.Write("Choose option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PlayMenu();
                        break;

                    case "2":
                        ChangeUsername();
                        break;

                    case "3":
                        ShowAbout();
                        break;

                    case "4":
                        running = false;
                        break;
                }
            }
        }

        private void AskUsername()
        {
            Console.Clear();

            Console.Write("Enter username: ");

            username = Console.ReadLine();

            while (username == "")
            {
                Console.Write("Username cannot be empty. Enter again: ");
                username = Console.ReadLine();
            }
        }

        private void PlayMenu()
        {
            Console.Clear();

            Console.WriteLine("1. Player vs Player");
            Console.WriteLine("2. Player vs Computer");
            Console.WriteLine();

            Console.Write("Choose mode: ");

            string mode = Console.ReadLine();

            Console.WriteLine();

            Console.Write("Choose symbol (X/O): ");

            char symbol = Char.ToUpper(Console.ReadKey().KeyChar);

            while (symbol != 'X' && symbol != 'O')
            {
                Console.WriteLine();
                Console.Write("Choose X or O: ");

                symbol = Char.ToUpper(Console.ReadKey().KeyChar);
            }

            Player player1 = new Player(username, symbol);

            char secondSymbol = symbol == 'X' ? 'O' : 'X';

            Player player2;

            if (mode == "1")
            {
                player2 = new Player("Player2", secondSymbol);
            }
            else
            {
                player2 = new Player("Computer", secondSymbol);
            }

            Game game = new Game(player1, player2, mode == "2");

            game.Start();
        }

        private void ChangeUsername()
        {
            Console.Clear();

            Console.Write("New username: ");

            username = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Username Updated");

            Console.ReadKey();
        }

        private void ShowAbout()
        {
            Console.Clear();

            Console.WriteLine("Developer: Your Name");
            Console.WriteLine("Course: C#");
            Console.WriteLine("Year: 2026");

            Console.ReadKey();
        }
    }
}