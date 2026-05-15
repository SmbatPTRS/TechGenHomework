// Board.cs

using System;

namespace ConsoleTicTacToe
{
    class Board
    {
        private char[] cells = new char[9];

        public Board()
        {
            Reset();
        }

        public void Reset()
        {
            for (int i = 0; i < 9; i++)
            {
                cells[i] = (char)('1' + i);
            }
        }

        public void Draw(int selectedCell)
        {
            Console.Clear();

            for (int row = 0; row < 3; row++)
            {
                Console.WriteLine("   |   |");

                for (int col = 0; col < 3; col++)
                {
                    int index = row * 3 + col;

                    Console.Write(" ");

                    if (index == selectedCell)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.Write(cells[index]);

                    Console.ResetColor();

                    if (col < 2)
                    {
                        Console.Write(" |");
                    }
                }

                Console.WriteLine();

                Console.WriteLine("   |   |");

                if (row < 2)
                {
                    Console.WriteLine("---+---+---");
                }
            }
        }

        public bool PlaceSymbol(int index, char symbol)
        {
            if (cells[index] == 'X' || cells[index] == 'O')
            {
                return false;
            }

            cells[index] = symbol;

            return true;
        }

        public bool CheckWinner()
        {
            int[,] patterns =
            {
                {0,1,2},
                {3,4,5},
                {6,7,8},

                {0,3,6},
                {1,4,7},
                {2,5,8},

                {0,4,8},
                {2,4,6}
            };

            for (int i = 0; i < 8; i++)
            {
                int a = patterns[i, 0];
                int b = patterns[i, 1];
                int c = patterns[i, 2];

                if (cells[a] == cells[b] &&
                    cells[b] == cells[c])
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsFull()
        {
            for (int i = 0; i < 9; i++)
            {
                if (cells[i] != 'X' &&
                    cells[i] != 'O')
                {
                    return false;
                }
            }

            return true;
        }

        public int GetFreeCell()
        {
            for (int i = 0; i < 9; i++)
            {
                if (cells[i] != 'X' &&
                    cells[i] != 'O')
                {
                    return i;
                }
            }

            return -1;
        }
    }
}