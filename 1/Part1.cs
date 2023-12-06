
namespace AOCDayTemplate
{
    internal class Part1
    {
        internal static void Solve(string[] input)
        {
            int callibrationSum = 0;

            foreach (var line in input)
            {
                callibrationSum += int.Parse($"{line.First(c => Char.IsDigit(c))}{line.Last(c => Char.IsDigit(c))}");
            }

            Console.WriteLine(callibrationSum);
        }
    }
}