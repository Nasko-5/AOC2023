
using System.Drawing;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOCDayTemplate
{
    internal class Part2
    {
        internal static void Solve(string[] input)
        {
            List<(int x, int y)> pointList = new List<(int x, int y)>();
            char c;
            int log = 3;
            bool pretty = true;

            Thread.Sleep(5000);

            Console.CursorVisible = false;

            if (pretty)
            {
                //Thread.Sleep(10000);
                Console.SetCursorPosition(1, log);
                Console.Write("Getting input data");

                for (int y = 0; y <= input.Length - 1; y++)
                {
                    for (int x = 0; x <= input[0].Length - 1; x++)
                    {
                        c = FancyIndex(input, x, y);

                        Console.SetCursorPosition((x + ((Console.WindowWidth / 2) - input[0].Length + 5)),
                            y + ((Console.WindowHeight / 2) - input.Length + 7));

                        if (Char.IsNumber(c) == false && Char.IsLetter(c) == false && c != '\0' & c != '.')
                        {
                            Console.Write($"\u001b[38;2;180;102;180m{c}\u001b[0m");
                        }
                        else
                        {
                            Console.Write($"\u001b[38;2;65;65;65m{c}\u001b[0m");
                        }


                        Thread.Sleep(4);
                    }
                }
            }

            if (pretty)
            {
                Console.SetCursorPosition(1, log);
                Console.Write("\u001b[38;2;100;190;0mGetting input data\u001b[0m");
                log++;
                Thread.Sleep(500);
            }


            Regex numRegex = new Regex(@"\d{1,3}");

            List<int> validNumbers = new List<int>();
            Dictionary<(int x, int y), List<int>> gearList = new Dictionary<(int x, int y), List<int>>();


            bool found = false;
            string grLastNumberOn = default;
            bool grFound = false;
            for (int y = 0; y <= input.Length - 1; y++)
            {
                string line = input[y];
                foreach (Match numMatch in numRegex.Matches(line))
                {
                    int numIndex = numMatch.Index;
                    int numLength = numMatch.Value.Length;

                    found = false;
                    grFound = false;
                    for (int x = numIndex; x <= numIndex + (numLength - 1); x++)
                    {

                        (int x, int y)[] neighbors = lookAtNeieghbors1D(input, x, y);

                        if (pretty) VisualizeNeighbor(neighbors, input, (102, 170, 170));

                        char[] chars = Neighbor1DToChar1D(neighbors, input);

                        // check if any of the neighbors are a symbol
                        if (chars.Any(a => Char.IsNumber(a) == false && Char.IsLetter(a) == false && a != '\0' & a != '.'))
                        {


                            // highlight the number
                            if (pretty) HighlightNumber((numIndex, y), numLength, input, (255, 102, 255));
                            if (!found)
                            {
                                found = true;
                                validNumbers.Add(int.Parse(numMatch.Value));
                                Console.SetCursorPosition(1, log);
                                //if (pretty) Console.Write($"Found {numMatch.Value}!");
                                //if (pretty) log++;
                                //if (!pretty) break;
                            }


                            //Console.WriteLine(numMatch.Value);

                            // if any of the neighbors are a gear
                            if (chars.Any(a => a == '*'))
                            {
                                // loop throug every gear
                                foreach (Match gear in new Regex(@"\*").Matches(string.Join("", chars)))
                                {
                                    (int x, int y) gearpos = neighbors[gear.Index];

                                    if (pretty)
                                    {
                                        Console.SetCursorPosition((gearpos.x + ((Console.WindowWidth / 2) - input[0].Length + 5)), gearpos.y + ((Console.WindowHeight / 2) - input.Length + 7));
                                        Console.Write("\u001b[38;2;6;156;0m*\u001b[0m");
                                    }
                                    if (pretty) HighlightNumber((numIndex, y), numLength, input, (102, 255, 102));
                                    if (grFound != true)
                                    {
                                        if (!gearList.Keys.Contains(gearpos))
                                        {
                                            gearList.Add(gearpos, new List<int>());
                                            gearList[gearpos].Add(int.Parse(numMatch.Value));
                                        }
                                        else
                                        {
                                            gearList[gearpos].Add(int.Parse(numMatch.Value));
                                        }

                                        if (pretty)
                                        {
                                            Console.SetCursorPosition(1, log);
                                            Console.Write($"GR4G@ x{gearpos.x} y{gearpos.y} -> {numMatch.Value}!");
                                            log++;
                                        }

                                        grFound = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var validGears = gearList
                .Where(a => a.Value.Count == 2)
                .ToArray();

            var gearRatios = validGears
                .Select(gear => gear.Value)
                .Select(gearRation => gearRation[0] * gearRation[1])
                .ToArray();

            var gearRatioSum = gearRatios.Sum();

            if (pretty)
            {
                Thread.Sleep(500);
                Console.SetCursorPosition(1, log);
                Console.Write("Getting gear ratios...");


                Console.SetCursorPosition(1, log);
                Console.Write("\u001b[38;2;100;190;0mGetting gear ratios...\u001b[0m");
                log++;

                foreach (var gearRatio in gearRatios)
                {
                    Console.SetCursorPosition(1, log);
                    Console.Write($"Gear ratio: {gearRatio}");
                    log++;
                }

                log++;

                Console.SetCursorPosition(1, log);
                Console.Write($"\u001b[38;2;100;190;mAnswer: {gearRatioSum}\u001b[0m");

                Console.SetCursorPosition(1, Console.WindowHeight - 4);
            }

            //Console.WriteLine(gearRatioSum);
        }

        static void VisualizeNeighbor((int x, int y)[] neighbors, string[] array, (int r, int g, int b) color)
        {
            foreach (var point in neighbors)
            {
                char c = FancyIndex(array, point.x, point.y);

                if (point.x != int.MinValue || point.y != int.MinValue)
                {
                    if (c != '\0' && Char.IsNumber(c) != true && Char.IsSymbol(c) != true && c != '*')
                    {
                        Console.SetCursorPosition((point.x + ((Console.WindowWidth / 2) - array[0].Length + 5)), point.y + ((Console.WindowHeight / 2) - array.Length + 7));
                        Console.Write($"\u001b[38;2;{color.r};{color.g};{color.b}m{c}\u001b[0m");
                        Thread.Sleep(4);
                    }
                }

            }
        }

        static void HighlightNumber((int x, int y) position, int length, string[] array, (int r, int g, int b) color)
        {
            (int x, int y) point = position;

            for (int i = 0; i < length; i++)
            {
                char c = FancyIndex(array, point.x, point.y);
                Console.SetCursorPosition((point.x + ((Console.WindowWidth / 2) - array[0].Length + 5)), point.y + ((Console.WindowHeight / 2) - array.Length + 7));
                Console.Write($"\u001b[38;2;{color.r};{color.g};{color.b}m{c}\u001b[0m");
                point = (point.x + 1, point.y);
            }
        }

        static (int x, int y)[,] lookAtNeieghbors2D(string[] array, int x, int y)
        {
            (int x, int y)[,] a = new (int x, int y)[3, 3]
            {
                { (x - 1, y - 1), (x, y - 1), (x + 1, y - 1) },
                { (x - 1, y    ), (int.MinValue, int.MinValue    ), (x + 1, y    ) },
                { (x - 1, y + 1), (x, y + 1), (x + 1, y + 1) }
            };
            return a;
        }

        static (int x, int y)[] lookAtNeieghbors1D(string[] array, int x, int y)
        {
            (int x, int y)[] a = new (int x, int y)[9]
            {
                (x - 1, y - 1), (x, y - 1), (x + 1, y - 1),
                (x - 1, y    ), (int.MinValue, int.MinValue    ), (x + 1, y    ),
                (x - 1, y + 1), (x, y + 1), (x + 1, y + 1)
            };

            return a;
        }

        static char[] Neighbor1DToChar1D((int x, int y)[] neighbors, string[] array)
        {
            List<char> characters = new List<char>();
            foreach (var point in neighbors)
            {
                if (point.x == 1 && point.y == 1)
                {
                    characters.Add('\0');
                }
                else
                {
                    characters.Add(FancyIndex(array, point.x, point.y));
                }
            }
            return characters.ToArray();
        }

        static char FancyIndex(string[] array, int x, int y)
        {
            char res;

            try
            {
                res = array[y][x];
            }
            catch
            {
                res = '\0';
            }

            return res;
        }
    }
}