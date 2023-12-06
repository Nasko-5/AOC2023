
namespace AOCDayTemplate
{
    internal class Part1
    {
        internal static void Solve(string[][][] input)
        {
            const int redCubeCount = 12;
            const int greenCubeCount = 13;
            const int blueCubeCount = 14;

            List<(uint gameID, uint red, uint green, uint blue)> ballz = new List<(uint gameID, uint red, uint green, uint blue)>();

            for (uint gameId = 0; gameId < input.Length; gameId++)
            {
                uint r, g, b;
                r = 0; g = 0; b = 0;
                for (uint turn = 0; turn < input[gameId].Length; turn++)
                {
                    for (uint picked = 0; picked < input[gameId][turn].Length; picked++)
                    {
                        string pick = input[gameId][turn][picked];
                        if (pick.Contains("red"))
                        {
                            r = (uint)Math.Max(r, int.Parse(pick.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]));
                        } else if (pick.Contains("green"))
                        {
                            g = (uint)Math.Max(g, int.Parse(pick.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]));
                        } else if (pick.Contains("blue"))
                        {
                            b = (uint)Math.Max(b, int.Parse(pick.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]));
                        }
                    }
                }
                ballz.Add((gameId+1, r, g, b));
            }

            var allValid = ballz.FindAll(a => a.red <= redCubeCount && a.blue <= blueCubeCount && a.green <= greenCubeCount);

            uint validsum = 0;

            foreach (var balidGame in allValid)
            {
                validsum += balidGame.gameID;
            }

            Console.WriteLine(validsum);
        }
    }
}