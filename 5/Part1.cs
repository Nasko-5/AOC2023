
namespace AOCDayTemplate
{
    internal class Part1
    {
        internal static void Solve(int[][][] input)
        {
            int[]     seeds = input[0][0];
            int[][][] maps  = input[1..7];

            List<mapRange> mapRanges = new List<mapRange>();

            foreach (var map in maps)
            {
                mapRanges.Add(new mapRange());
                foreach (var range in map)
                {
                    mapRanges[mapRanges.Count-1].AddRange(range[1], range[0], range[2]); 
                }
            }

            foreach (var map in mapRanges)
            {
                Console.WriteLine(string.Join(" ", seeds));

                for (int i = 0; i < seeds.Length-1; i++)
                {
                    seeds[i] = map.ConvertFrom(seeds[i]);
                }
            }
        }

        class mapRange
        {
            private List<Range> sourceRanges = new List<Range>();
            private List<Range> resultRanges = new List<Range>();

            public void AddRange(int sourceRangeStart, int resultRangeStart, int rangeLength)
            {
                sourceRanges.Add(new Range(sourceRangeStart, sourceRangeStart+rangeLength));
                resultRanges.Add(new Range(resultRangeStart, resultRangeStart+rangeLength));
            }


            public static int ConvertRange(
                Range originalRange,
                Range resultRange, // desired range
                int value) // value to convert
            {
                double scale = (double)(resultRange.End.Value - resultRange.Start.Value) / (originalRange.End.Value - originalRange.Start.Value);
                return (int)(resultRange.Start.Value + ((value - originalRange.Start.Value) * scale));
            }




            public int ConvertFrom(int number)
            {
                foreach (Range range in sourceRanges)
                {
                    if ( range.Start.Value <= number && number <= range.End.Value)
                    {
                        var usingSourceRange = sourceRanges
                            .Where(range => range.Start.Value <= number && number <= range.End.Value)
                            .ToArray()[0];

                        var usingResultRange = resultRanges[sourceRanges.IndexOf(usingSourceRange)];

                        int converted = ConvertRange(usingSourceRange, usingResultRange, number);

                        Console.WriteLine(
                            $"Mapping value {number} from sr[{usingSourceRange.Start.Value}, {usingSourceRange.End.Value}] to rr[{usingResultRange.Start.Value}, {usingResultRange.End.Value}] -> {converted}");

                        return converted;
                    }
                    else
                    {
                        Console.WriteLine($"{number} is not in any of the ranges");
                        return number;
                    }
                }

                return -11111111;
            }
        }
    }
}