using System.Security;
using System.Security.Cryptography;

namespace AOCDayTemplate
{
    internal class Part1
    {
        internal static void Solve(int[][] input)
        {
            
            
            List<List<int>> diffs = new();
            List<int> diff = new();
            int sum = 0;

            // find the diffs for each value 
            foreach (int[] value in input) 
            {

                diffs.Add(value.ToList());

                do
                {
                    diff = new();

                    for (int i = 0; i < diffs[(diffs.Count-1)].Count-1; i++)
                    {
                        diff.Add(diffs[(diffs.Count - 1)][i+1] - diffs[(diffs.Count - 1)][i]);
                    }

                    diffs.Add(diff);
                } while (diff.Any(a => a != 0));

                foreach (var difff in diffs)
                {
                    difff.Add(0);
                }

                for (int i = diffs.Count-2; i >= 0; i--)
                {
                    int a = diffs[i + 1][diffs[i + 1].Count - 1];
                    int b = diffs[i][diffs[i].Count - 2];
                    int c = a + b;
                    diffs[i][diffs[i].Count - 1] = c;
                    Console.WriteLine($"{string.Join(" ", diffs[i])} | {a} + {b} = {c}");
                }

                sum += diffs[0][diffs[0].Count - 1];

                Console.WriteLine();
                diffs = new();
            }

            Console.WriteLine(sum);

        }
    }
}