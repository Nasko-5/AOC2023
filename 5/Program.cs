using System.Collections;
using System.Text.RegularExpressions;

namespace AOCDayTemplate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string basePath = File.ReadAllText(@"D:\My stuff\.programming\advent of code\basepath.txt");

            byte day = 5;
            short year = 2023;
            byte puzzlePart = 1;
            bool testing = true;

            string dayPath = $@"{basePath}\c#\{year}\{day}";

            if (!Directory.Exists(dayPath))
            {
                Directory.CreateDirectory(dayPath);
            }

            {
                Console.Write(" Welcome to ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("A");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("C");
                Console.ResetColor();
                Console.WriteLine($" {year}, day {day}!");
                Console.WriteLine($" Running part {puzzlePart} with {(testing ? "testing" : "real")} input...");
                Console.WriteLine(new string('~', Console.WindowWidth));
            }

            string input = default;

            if (testing)
            {
                if (puzzlePart == 1)
                {
                    try
                    {
                        input = File.ReadAllText($@"{dayPath}\test1.txt");
                    }
                    catch
                    {
                        File.Create($@"{dayPath}\test1.txt");
                        throw new Exception(
                            $"Test input file for AOC {year}, day {day}, part 1 does not exist!\nCreated file test1.txt at {dayPath}\\");
                    }
                }
                else if (puzzlePart == 2)
                {
                    try
                    {
                        input = File.ReadAllText($@"{dayPath}\test2.txt");
                    }
                    catch
                    {
                        File.Create($@"{dayPath}\test2.txt");
                        throw new Exception(
                            $"Test input file for AOC {year}, day {day}, part 2 does not exist!\nCreated file test2.txt at {dayPath}\\");
                    }
                }
            }
            else
            {
                if (File.Exists($@"{dayPath}\real.txt"))
                {
                    input = File.ReadAllText($@"{dayPath}\real.txt");
                }
                else
                {
                    File.Create($@"{dayPath}\real.txt");
                    throw new Exception(
                        $"Real input file for AOC {year}, day {day}, part 1 does not exist!\nCreated file real.txt at {dayPath}\\");
                }
            }


            var maps = input
                .Split("\r\n\r\n")
                .Select(x => x.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1])
                .Select(map => map.Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .Select(mapmap => mapmap
                            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                            .Select(long.Parse)
                            .ToArray())
                        .ToArray())
                .ToArray();
                
            

            if (puzzlePart == 1)
            {
                Part1.Solve(maps);
            } else if (puzzlePart == 2)
            {
                Part2.Solve(maps);
            }
        }
    }
}
