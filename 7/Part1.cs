
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
                //Console.WriteLine(game[game.Count-1]);
            }

            Console.WriteLine();

            game = game.OrderByDescending(a => a.Type).ToList();

            // ok so an array/list of strings is sorted by the ascii value of each character in every string
            // but what if i want to use different values for each of the characters defined in a dictionary??
            for (int i = 0; i < game.Count-1; i++)
            {
                Hand handOne = game[i];
                Hand handTwo = game[i + 1];

                if (handOne.Type != handTwo.Type) continue;

                foreach (var cards in handOne.handNum.Zip(handTwo.handNum))
                {
                    if (cards.First > cards.Second)
                    {
                        break;

                    } else if (cards.Second > cards.First)
                    {
                        game.Insert(i + 2, handOne);
                        game.RemoveAt(i);
                        break;
                    }
                }

            }

            int sum = 0;
            int rank = game.Count+1;
            foreach (var hand in game)
            {
                rank--;
                hand.Rank = rank;
                Console.WriteLine(hand);
            }

            Console.WriteLine();

            Console.WriteLine(string.Join(" + ", game.Select(a => $"{a.Bid} * {a.Rank}")));

            foreach(var hand in game)
            {
                sum += hand.Bid * hand.Rank;
            }

            Console.WriteLine($"Total winnings: {sum}");
        }




        class Hand
        {
            public int Bid { get; set; }
            public string HandText { get; set; }
            public int Rank { get; set; }
            public int Type { get; set; }

            public List<int> handNum = new();

            public Dictionary<char, int> cards = new Dictionary<char, int>()
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

                handNum = HandText.Select(a => cards[a]).ToList();
            }

            public override string ToString()
            {
                return $"{HandText}, the first card has a value of {cards[HandText[0]]}. The hand is a {types[Type]} hand, with a bid of {Bid}$ and rank {Rank}";
            }
        }
    }
}