﻿namespace AOCDayTemplate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string basePath = File.ReadAllText(@"D:\My stuff\.programming\advent of code\basepath.txt");

            byte day = 9;
            short year = 2023;
            byte puzzlePart = 2;
            bool testing = false;

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


            var parsed = input.Split(input.Contains('\r') ? "\r\n" : "\n").Select(val =>
                val.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.TrimEntries).Select(int.Parse)
                    .ToArray()).ToArray();

            if (puzzlePart == 1)
            {
                Part1.Solve(parsed);
            } else if (puzzlePart == 2)
            {
                Part2.Solve(parsed);
            }
        }
    }
}
