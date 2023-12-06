
using System.Text.RegularExpressions;

namespace AOCDayTemplate
{
    internal class Part1
    {
        internal static void Solve(string input)
        {
            Regex cardRegex = new Regex(@"\b(: )(?<winningNumbers>.+)( \| )(?<cardNumbers>.+)\b");

            int pointSum = 0;

            foreach (Match card in cardRegex.Matches(input))
            {
                var winningNumbers = card.Groups["winningNumbers"]
                    .Value.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(int.Parse)
                    .ToArray();

                var numbersGot = card.Groups["cardNumbers"]
                    .Value.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(int.Parse)
                    .ToArray();

                var nMatches = winningNumbers.Where(a => numbersGot.Contains(a)).Count();
                int points;
                if (nMatches != 0)
                {
                    points = (int)Math.Pow(2, nMatches);
                }
                else
                {
                    points = 0;
                }
                

                pointSum += points;
            }

            Console.WriteLine(pointSum);
        }
    }
}