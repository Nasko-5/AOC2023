
namespace AOCDayTemplate
{
    internal class Part1
    {
        internal static void Solve(int[][] input)
        {
            int[] times = input[0];
            int[] distances = input[1];

            int sum = 1;
            int possibleCount = 1;

            foreach (var a in times.Zip(distances, Tuple.Create))
            {

                Console.WriteLine($"Figuring out how to beat a {a.Item1}ms button hold for a {a.Item2}mm distance...");

                possibleCount = 0;

                for (int holdTime = 0; holdTime < a.Item1; holdTime++)
                {
                    int d = (a.Item1 - holdTime) * holdTime;

                    if (d > a.Item2)
                    {
                        Console.WriteLine(
                            $"\t+ Found! hold the button for {holdTime}ms to achieve {d}mm of distance!");
                        possibleCount++;
                    }
                    else
                    {
                        Console.WriteLine($"\t- Not found.. but checked {holdTime}ms that achieved {d}mm of distance ({d}mm < {a.Item2}mm)");
                    }
                }

                Console.WriteLine($"\t@ Result: {possibleCount} possible ways to beat the current record!");

                sum *= possibleCount == 0 ? 1 : possibleCount;

                Console.WriteLine();
            }


            Console.WriteLine($"\nSum of number of ways to beat each record: {sum}");
        }
    }
}