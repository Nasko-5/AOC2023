
namespace AOCDayTemplate
{
    internal class Part2
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

                    for (int i = 0; i < diffs[(diffs.Count - 1)].Count - 1; i++)
                    {
                        diff.Add(diffs[(diffs.Count - 1)][i + 1] - diffs[(diffs.Count - 1)][i]);
                    }

                    diffs.Add(diff);
                } while (diff.Any(a => a != 0));

                foreach (var difff in diffs)
                {
                    difff.Insert(0, 0);
                }

                for (int i = diffs.Count - 2; i >= 0; i--)
                {
                    int a = diffs[i + 1][0];
                    int b = diffs[i][1];         
                    int c = b - a;
                    diffs[i][0] = c;
                    Console.WriteLine($"{string.Join(" ", diffs[i])} | {b} - {a} = {c}");
                }

                sum += diffs[0][0];

                Console.WriteLine();
                diffs = new();
            }

            Console.WriteLine(sum);

        }
    }
}