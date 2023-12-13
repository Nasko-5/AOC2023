
using System.Collections;
using System.Linq.Expressions;
using System.Threading.Tasks.Dataflow;

namespace AOCDayTemplate
{
    internal class Part1
    {
        internal static void Solve(string input)
        {
            Grid<char> pipes = new Grid<char>(input.Split(input.Contains('\r') ? "\r\n" : "\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
            Console.CursorVisible = false;

            for (int i = 0; i < pipes.gridHeight; i++)
            {
                for (int j = 0; j < pipes.gridWidth; j++)
                {
                    var n = pipes.GetNeighbors2D(j, i, 5);
                    var a = pipes.GetNeighbors1D(j, i, 5);

                    Visualize2DGrid(n, 1, 5);

                    Thread.Sleep(10);
                }
            }


        }


        public static void Visualize2DGrid(char[,] grid, int x, int y)
        {

            Console.SetCursorPosition(x, y);

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int column = 0; column < grid.GetLength(0); column++)
                {
                    Console.Write(' ');
                
                }
                Console.SetCursorPosition(x, y + row);
            }

            Console.SetCursorPosition(x, y);

            for (int row = 0; row < grid.GetLength(0); row++)
            {   
                for (int column = 0; column < grid.GetLength(0); column++)
                {
                    char atChar = grid[row, column];

                    if (atChar == '\0')
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        if (grid.GetLength(0)/2 == row && grid.GetLength(1)/2 == column)
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write($"{atChar}");
                            Console.ResetColor();
                            Console.Write($" ");
                        } else
                        {
                            if (new char[6] {'|', '-', 'F', '7', 'L', 'J'}.Contains(atChar))
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write($"{atChar}");
                                Console.ResetColor();
                                Console.Write($" ");
                            } else
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.Write($"{atChar}");
                                Console.ResetColor();
                                Console.Write($" ");
                            }
                        }
                    }
                }
                Console.SetCursorPosition(x, y + row);
            }
        }
    }
}