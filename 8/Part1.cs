
using System.Collections;

namespace AOCDayTemplate
{
    internal class Part1
    {
        internal static void Solve((string, string, string)[] input, char[] instructions)
        {
            (string, string, string) atNode = input.FirstOrDefault(a => a.Item1 == "AAA");
            int steps = 0;
            char direction;
            

            while (atNode.Item1 != "ZZZ")
            {
                Console.WriteLine($"{atNode.Item1} {atNode.Item2} {atNode.Item3}");
                direction = instructions[(steps % instructions.Length)];

                switch (direction)
                {
                    case 'L':
                        atNode = input.FirstOrDefault(a => a.Item1 == atNode.Item2);
                        break;
                    case 'R':
                        atNode = input.FirstOrDefault(a => a.Item1 == atNode.Item3);
                        break;
                }

                steps++;
            }

            Console.WriteLine(steps);
        }
    }
}