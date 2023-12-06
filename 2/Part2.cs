
namespace AOCDayTemplate
{
    internal class Part2
    {
        internal static void Solve(string[][][] input)
        {


            List<(uint power, uint red, uint green, uint blue)> ballz = new List<(uint power, uint red, uint green, uint blue)>();

            for (uint gameId = 0; gameId < input.Length; gameId++)
            {
                uint r, g, b;
                r = 0; g = 0; ; b = 0;
                for (uint turn = 0; turn < input[gameId].Length; turn++)
                {
                    for (uint picked = 0; picked < input[gameId][turn].Length; picked++)
                    {
                        string pick = input[gameId][turn][picked];
                        if (pick.Contains("red"))
                        {
                            r = (uint)Math.Max(r, int.Parse(pick.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]));
                        }
                        else if (pick.Contains("green"))
                        {
                            g = (uint)Math.Max(g, int.Parse(pick.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]));
                        }
                        else if (pick.Contains("blue"))
                        {
                            b = (uint)Math.Max(b, int.Parse(pick.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]));
                        }
                    }
                }
                ballz.Add((r * g * b, r, g, b));
            }

            uint sum = 0;

            foreach (var ball in ballz)
            {
                sum += ball.power;
            }

            Console.WriteLine(sum);
        }
    }
    }
