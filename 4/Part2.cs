
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.RegularExpressions;

namespace AOCDayTemplate
{
    internal class Part2
    {
        internal static void Solve(string input)
        {
            Regex cardRegex = new Regex(@"\b(: )(?<winningNumbers>.+)( \| )(?<cardNumbers>.+)\b");

            List<(int matchingNumbers, int copies)> cardList =
                new List<(int matchingNumbers, int copies)>();



            foreach (Match card in cardRegex.Matches(input))
            {
                int[] winningNumbers = card.Groups["winningNumbers"]
                    .Value.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(int.Parse)
                    .ToArray();

                int[] numbersGot = card.Groups["cardNumbers"]
                    .Value.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(int.Parse)
                    .ToArray();

                cardList.Add((winningNumbers.Where(a => numbersGot.Contains(a)).Count(), 1));
            }

            int log = 3;

            for (int cardIndex = 0; cardIndex < cardList.Count; cardIndex++)
            {
                //PrintCards(cardList, log);

                var card = cardList[cardIndex];

                for (int cardCopy = cardIndex; cardCopy < (card.matchingNumbers+cardIndex)+1; cardCopy++)
                {
                    cardList[cardCopy] = (cardList[cardCopy].matchingNumbers, cardList[cardCopy].copies + card.copies);
                }
            }

            Console.WriteLine(cardList.Select(a => a.copies).Sum() / 2);
        }

        static void PrintCards(List<(int matchingNumbers, int copies)> cards, int log)
        {
            
            foreach (var card in cards)
            {

                Console.SetCursorPosition(1, log);

                Console.WriteLine($"{log - 2} - matching {card.matchingNumbers} | copies {card.copies}");

                log++;


            }

            Console.WriteLine();
        }
    }
}