
using System.Collections;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks.Dataflow;

namespace AOCDayTemplate
{
    internal class Part1
    {
        internal static void Solve(string input)
        {

            Grid<char> pipes = new Grid<char>(input.Split(input.Contains('\r') ? "\r\n" : "\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));

            Dictionary<char, Dictionary<(int x, int y), char[]>> pipeDict =
                new Dictionary<char, Dictionary<(int x, int y), char[]>>()
                {
                    {'-',  new Dictionary<(int x, int y), char[]>
                    {
                        {(0, 1), new []{'L', '-', 'F'}},
                        {(2, 1), new []{'J', '-', '7'}}
                    }},
                    {'|',  new Dictionary<(int x , int y), char[]>()
                    {
                        {(1, 0), new []{'7', '|', 'F'}},
                        {(1, 2), new []{'J', '|', 'L'}}
                    }},
                    {'F',  new Dictionary<(int x , int y), char[]>()
                    {
                        {(2, 1), new []{'J', '-', '7'}},
                        {(1, 2), new []{'J', '|', 'L'}}
                    }},
                    {'7',  new Dictionary<(int x , int y), char[]>()
                    {
                        {(0, 1), new []{'L', '-', 'F'}},
                        {(1, 2), new []{'J', '|', 'L'}}
                    }},
                    {'J',  new Dictionary<(int x , int y), char[]>()
                    {
                        {(1, 0), new []{'7', '|', 'F'}},
                        {(0, 1), new []{'L', '-', 'F'}}
                    }},
                    {'L',  new Dictionary<(int x , int y), char[]>()
                    {
                        {(1, 0), new []{'7', '|', 'F'}},
                        {(2, 1), new []{'7', '-', 'J'}}
                    }}

                };
            Console.CursorVisible = false;


            var SLocation = FindS(pipes, 1);
        }

        public static (int x, int y) FindS(Grid<char> pipes, int n)
        {
            for (int i = 0; i < pipes.gridHeight; i += n)
            {
                for (int j = 0; j < pipes.gridWidth; j += n)
                {
                    var neighborChars2D = pipes.GetNeighbors2D(j, i, n);
                    var neighborChars1dD = pipes.GetNeighbors1D(j, i, n);

                    Visualize2DGrid(neighborChars2D, 1, 5);

                    if (neighborChars1dD.Contains('S'))
                    {
                        var neighborsAsPoints1D = pipes.GetPointNeighbors1D(j, i, n);
                        var startingPipeIndex = Array.IndexOf(neighborChars1dD, 'S');
                        var startingPipeLocation = neighborsAsPoints1D[startingPipeIndex];

                        var a = (startingPipeLocation.x - j, startingPipeLocation.y - i);

                        // Issue is here!

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        int visA = pipes.gridWidth - ((2 * n) + 1);
                        int visB = pipes.gridHeight - ((2 * n) + 1);
                        Console.SetCursorPosition(1 + Math.Abs(visA - startingPipeLocation.x),
                                                   5 + Math.Abs(visB - startingPipeLocation.y));
                        Console.Write('S');
                        Console.ResetColor();

                        Console.SetCursorPosition(0, 20);
                        return startingPipeLocation;


                    }

                    Thread.Sleep(10);
                }
            }

            return (int.MinValue, int.MinValue);
        }
        public static void Visualize2DGrid(char[,] grid, int x, int y)
        {

            Console.SetCursorPosition(x, y);

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int column = 0; column < grid.GetLength(1); column++)
                { 
                    Console.Write("  ");
                
                }
                Console.SetCursorPosition(x, y + (row+1));
            }

            Console.SetCursorPosition(x, y);

            for (int row = 0; row < grid.GetLength(0); row++)
            {   
                for (int column = 0; column < grid.GetLength(1); column++)
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
                Console.SetCursorPosition(x, y + (row+1));
            }
        }

    }
}