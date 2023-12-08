
namespace AOCDayTemplate
{
    class Part1
    {
        public static void Solve(long[][][] input)
        {
            long[]     seeds = input[0][0];
            long[][][] maps  = input[1..8];

            List<mapRange> mapRanges = new List<mapRange>();

            foreach (var map in maps)
            {
                mapRanges.Add(new mapRange());
                foreach (var range in map)
                {
                    mapRanges[mapRanges.Count-1].AddRange(range[1], range[0], range[2]); 
                }
            }


            string[] mapNames = new[] { "seed", "soil", "fertilizer", "water", "light", "temperature", "humidity", "location" };
            int onMap = 0;

            Console.WriteLine($"{mapNames[onMap]}{new string(' ', 12-mapNames[onMap].Length)}: {string.Join(" ", seeds)}");

            foreach (var map in mapRanges)
            {
                for (long i = 0; i < seeds.Length; i++)
                {
                    seeds[i] = map.ConvertFrom(seeds[i]);
                }

                onMap++;
                Console.WriteLine($"{mapNames[onMap]}{new string(' ', 12 - mapNames[onMap].Length)}: {string.Join(" ", seeds)}");
            }

            Console.WriteLine(new string('-', 13));

            Array.Sort(seeds);
            Console.WriteLine($"sorted{new string(' ', 6)}: {string.Join(" ", seeds)}");
            Console.WriteLine($"lowest{new string(' ', 6)}: {seeds[0]}");


        }

        class myRange
        {
            public long Start { get; set; }
            public long End { get; set; }

            public myRange(long start, long end)
            {
                Start = start; End = end; 
            }
        }

        class mapRange
        {
            private Dictionary<myRange, myRange> rangeMap = new();
            public void AddRange(long sourceRangeStart, long resultRangeStart, long rangeLength)
            {
                rangeMap.Add(new myRange(sourceRangeStart, sourceRangeStart + rangeLength), new myRange(resultRangeStart, resultRangeStart + rangeLength));
            }


            public static long ConvertRange(
                myRange originalRange,
                myRange resultRange, // desired range
                long value) // value to convert
            {
                double scale = (double)(resultRange.End - resultRange.Start) / (originalRange.End - originalRange.Start);
                return (long)(resultRange.Start + ((value - originalRange.Start) * scale));
            }




            public long ConvertFrom(long number)
            {
                foreach (myRange range in rangeMap.Keys)
                {
                    bool inRange = number >= range.Start && range.End >= number;

                    if (inRange)
                    {
                        myRange sourceRange = range;
                        myRange resultRange = rangeMap[range];

                        long resultValue = ConvertRange(sourceRange, resultRange, number);

                        /*Console.WriteLine($"Converting {number} -> {resultValue} (from {sourceRange.Start}-{sourceRange.End} " +
                                          $"-> {resultRange.Start}-{resultRange.End})");*/

                        return resultValue;
                    }
                }



                //Console.WriteLine($"None match! returning {number}");
                return number;
            }
        }
    }
}