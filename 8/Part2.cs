
using System.Diagnostics;

namespace AOCDayTemplate
{
    internal class Part2
    {
        internal static void Solve((string, string, string)[] input, char[] instructions)
        {
            (string, string, string)[] atNodes = input.Where(a => a.Item1[2] == 'A').ToArray();
            (string, string, string) innerAtNode;
            List<long> pathLengths = new List<long>();
            long steps = 0;
            char direction;

            Stopwatch SW = new Stopwatch();
            SW.Start();

            int oldtime = 0;
            int newtime = 0;

            for (int i = 0; i < atNodes.Length; i++)
            {
                //Console.WriteLine($"{atNodes[i].Item1} {atNodes[i].Item2} {atNodes[i].Item3}");
                innerAtNode = atNodes[i];
                steps = 0;

                while (innerAtNode.Item1[2] != 'Z')
                {
                    direction = instructions[(steps % instructions.Length)];

                    switch (direction)
                    {
                        case 'L':
                            innerAtNode = input.First(a => a.Item1 == innerAtNode.Item2);
                            break;
                        case 'R':
                            innerAtNode = input.First(a => a.Item1 == innerAtNode.Item3);
                            break;
                    }

                    steps++;
                }
                pathLengths.Add(steps);

                Console.WriteLine(string.Join(", ", pathLengths));
            }

            for (int j = 0; j < 100; j++)
            {
                for (int i = 0; i < pathLengths.Count - 1; i++)
                {
                    long a = pathLengths[i];
                    long b = pathLengths[i + 1];

                    pathLengths[i] = findLCM(a, b);
                }

                Console.WriteLine(string.Join(", ", pathLengths));
            }
            // Console.WriteLine(steps);
        }

        public static long findLCM(long a, long b)
        {
            long num1, num2;

            if (a > b)
            {
                num1 = a;
                num2 = b;
            }
            else
            {
                num1 = b;
                num2 = a;
            }

            for (long i = 1; i <= num2; i++)
            {
                if ((num1 * i) % num2 == 0)
                {
                    return i * num1;
                }
            }
            return num2;
        }
    }
}