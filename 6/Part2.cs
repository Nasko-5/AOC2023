
using System;

namespace AOCDayTemplate
{
    internal class Part2
    {
        internal static void Solve(long[] input)
        {
            long time = input[0];
            long distance = input[1];

            long sum = 1;
            long possibleCount = 1;


                Console.WriteLine($"Figuring out how to beat a {time}ms button hold for a {distance}mm distance...");

                possibleCount = 0;

                for (long holdTime = 0; holdTime < time; holdTime++)
                {
                    long d = (time - holdTime) * holdTime;

                    if (d > distance)
                    {
                        //Console.WriteLine(
                        //    $"\t+ Found! hold the button for {holdTime}ms to achieve {d}mm of distance!");
                        possibleCount++;
                    }
                    else
                    {
                        //Console.WriteLine($"\t- Not found.. but checked {holdTime}ms that achieved {d}mm of distance ({d}mm < {distance}mm)");
                    }
                }

                //Console.WriteLine($"\t@ Result: {possibleCount} possible ways to beat the current record!");

                sum *= possibleCount == 0 ? 1 : possibleCount;

                //Console.WriteLine();


            Console.WriteLine($"\nSum of number of ways to beat each record: {sum}");
        }
    }
}