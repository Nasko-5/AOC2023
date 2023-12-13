
using System.Collections;
using System.Linq.Expressions;

namespace AOCDayTemplate
{
    internal class Part1
    {
        internal static void Solve(string input)
        {
            Grid<char> pipes = new Grid<char>(input.Split(input.Contains('\r') ? "\r\n" : "\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));

            var n = pipes.GetNeighbors1D(0, 0, 2);


            //for (int row = 0; row < n.GetLength(0); row++)
            //{
            //    for (int column = 0; column < n.GetLength(0); column++)
            //    {
            //        char atChar = n[row, column];

            //        if (atChar == '\0')
            //        {
            //            Console.Write(' ');
            //        }
            //        else
            //        {
            //            Console.Write(atChar);
            //        }
            //    }

            //    Console.WriteLine();
            //}
        }
    }
}