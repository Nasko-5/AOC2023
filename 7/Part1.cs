
using System.ComponentModel.Design;
using System.Linq;

namespace AOCDayTemplate
{
    internal class Part1
    {
        internal static void Solve((string, int)[] input)
        {
            List<Hand> game = new List<Hand>();

            foreach (var hand in input)
            {
                game.Add(new Hand(hand.Item2, hand.Item1));
                Console.WriteLine(game[game.Count-1]);
            }

            Console.WriteLine();

            game = game.OrderByDescending(a => a.Type).ToList();
            

        }




        class Hand
        {
            public int Bid { get; set; }
            public string HandText { get; set; }
            public int Rank { get; set; }
            public int Type { get; set; }

            Dictionary<char, int> cards = new Dictionary<char, int>()
            {
                {'A', 13}, {'K', 12}, {'Q', 11}, {'J', 10}, {'T', 9}, {'9', 8}, {'8', 7}, {'7', 6}, {'6', 5}, {'5', 4}, {'4', 3}, {'3', 2}, {'2', 1}
            };

            public Dictionary<int, string> types = new Dictionary<int, string>()
            {
                { 7, "Five of a kind" },
                { 6, "Four of a kind" },
                { 5, "Full house" },
                { 4, "Three of a kind" },
                { 3, "Two pair" },
                { 2, "One pair" },
                { 1, "High card" }
            };
            /*
             *
               Five of a kind, where all five cards have the same label: AAAAA
               Four of a kind, where four cards have the same label and one card has a different label: AA8AA
               Full house, where three cards have the same label, and the remaining two cards share a different label: 23332
               Three of a kind, where three cards have the same label, and the remaining two cards are each different from any other card in the hand: TTT98
               Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432
               One pair, where two cards share one label, and the other three cards have a different label from the pair and each other: A23A4
               High card, where all cards' labels are distinct: 23456
               
             */
            public Hand(int bid, string handText)
            {
                Bid = bid;
                HandText = handText;
                Rank = 0;
                Type = -1;

                Dictionary<char, int> presentCards = new Dictionary<char, int>();

                foreach (KeyValuePair<char, int> card in cards)
                {
                    int cardCount = HandText.Count(a => a == card.Key);

                    if (cardCount >= 1)
                    {
                        presentCards.Add(card.Key, cardCount);
                        if (presentCards.Values.Sum() == 5) break;
                    }
                }

                if ( presentCards.Any(a => a.Value == 5) ) // Five of a kind
                {
                    Type = 7;
                } else if ( presentCards.Any(a => a.Value == 4) ) // Four of a kind

                {
                    Type = 6;
                } else if (   presentCards.Any(a => a.Value == 3) // Full house?
                              && presentCards.Any(a => a.Value == 2))
                {
                    Type = 5;
                } else if (presentCards.Any(a => a.Value == 3)) // Three of a kind
                {
                    Type = 4;
                } else if (presentCards.Where(a => a.Value == 2).Count() == 2) // Two pair
                {
                    Type = 3;
                } else if (presentCards.Any(a => a.Value == 2))
                {
                    Type = 2;
                } else if (presentCards.All(a => a.Value == 1)) // High card
                {
                    Type = 1;
                }


            }

            public override string ToString()
            {
                return $"{HandText} is a {types[Type]} card, with a bid of {Bid}$ and rank {Rank}";
            }
        }
    }
}