
using System.Text.RegularExpressions;

namespace AOCDayTemplate
{
    internal class Part2
    {
        internal static void Solve(string[] input)
        {
            Regex firstNumberRegex = new Regex("(?<firstNumber>one|two|three|four|five|six|seven|eight|nine|\\d).+$");
            Regex lastNumberRegex = new Regex("^.+(?<lastNumber>one|two|three|four|five|six|seven|eight|nine|\\d)");

            int calibrationOne, calibrationTwo, calibrationValue;
            int calibrationSum = 0;
            int hold;

            foreach (var line in input)
            {
                calibrationOne = Convert(firstNumberRegex.Match(line).Groups["firstNumber"].ToString());
                calibrationTwo = Convert(lastNumberRegex.Match(line).Groups["lastNumber"].ToString());


                if (calibrationOne == 0 || calibrationTwo == 0)
                {
                    hold = Math.Max(calibrationOne, calibrationTwo);
                    calibrationValue = int.Parse($"{hold}{hold}");
                    Console.WriteLine($"{line.Trim()} -> {hold}{hold}");
                }
                else
                {
                    calibrationValue = int.Parse($"{calibrationOne}{calibrationTwo}");
                    Console.WriteLine($"{line.Trim()} -> {calibrationOne}{calibrationTwo}");
                }

                calibrationSum += calibrationValue;
            }

            Console.WriteLine(calibrationSum);
        }

        private static string[] numbers = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

        static int Convert(string number)
        {
            if (number.Length == 1)
            {
                return int.Parse(number);
            }
            else
            {
                return Array.IndexOf(numbers, number) + 1;
            }
        }
    }
}