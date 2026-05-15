// Game.cs

using System;

namespace ConsoleTicTacToe
{
    class Game
    {
        private Board board;

        private Player player1;
        private Player player2;

        private bool player1Turn = true;

        private int selectedCell = 4;

        private bool vsComputer;

        public Game(Player p1, Player p2, bool computerMode)
        {
            player1 = p1;
            player2 = p2;

            vsComputer = computerMode;

            board = new Board();
        }

        public void Start()
        {
            while (true)
            {
                board.Draw(selectedCell);

                ShowTurn();

                if (vsComputer && !player1Turn)
                {
                    ComputerMove();

                    if (CheckGameEnd())
                    {
                        break;
                    }

                    continue;
                }

                ConsoleKeyInfo key = Console.ReadKey(true);

                HandleInput(key);

                if (CheckGameEnd())
                {
                    break;
                }
            }
        }

        private void ShowTurn()
        {
            Console.WriteLine();

            if (player1Turn)
            {
                Console.WriteLine("Current Turn: " + player1.Name);
            }
            else
            {
                Console.WriteLine("Current Turn: " + player2.Name);
            }

            Console.WriteLine();
            Console.WriteLine("Arrow Keys = Move");
            Console.WriteLine("Enter = Place Symbol");
        }

        private void HandleInput(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:

                    if (selectedCell >= 3)
                    {
                        selectedCell -= 3;
                    }

                    break;

                case ConsoleKey.DownArrow:

                    if (selectedCell <= 5)
                    {
                        selectedCell += 3;
                    }

                    break;

                case ConsoleKey.LeftArrow:

                    if (selectedCell % 3 != 0)
                    {
                        selectedCell--;
                    }

                    break;

                case ConsoleKey.RightArrow:

                    if (selectedCell % 3 != 2)
                    {
                        selectedCell++;
                    }

                    break;

                case ConsoleKey.Enter:

                    PlaceMove();

                    break;
            }
        }

        private void PlaceMove()
        {
            char symbol;

            if (player1Turn)
            {
                symbol = player1.Symbol;
            }
            else
            {
                symbol = player2.Symbol;
            }

            bool success = board.PlaceSymbol(selectedCell, symbol);

            if (success)
            {
                player1Turn = !player1Turn;
            }
        }

        private void ComputerMove()
        {
            int move = board.GetFreeCell();

            if (move != -1)
            {
                board.PlaceSymbol(move, player2.Symbol);

                player1Turn = true;
            }
        }

        private bool CheckGameEnd()
        {
            if (board.CheckWinner())
            {
                board.Draw(selectedCell);

                Console.WriteLine();

                if (!player1Turn)
                {
                    Console.WriteLine(player1.Name + " Wins!");
                }
                else
                {
                    Console.WriteLine(player2.Name + " Wins!");
                }

                Console.ReadKey();

                return true;
            }

            if (board.IsFull())
            {
                board.Draw(selectedCell);

                Console.WriteLine();
                Console.WriteLine("Draw!");

                Console.ReadKey();

                return true;
            }

            return false;
        }
    }
}