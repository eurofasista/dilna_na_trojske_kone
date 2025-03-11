using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections;

namespace BenchmarkKnapsack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var results = BenchmarkRunner.Run<MyBenchmark>(); // spustíme benchmarky (vše, co je označeno [Benchmark])
        }
    }

    [MemoryDiagnoser]
    public class MyBenchmark
    {
        int m;
        List<int> b;
        int[] w;
        int[] c;
        int k;
        public MyBenchmark()
        {
            // TODO: Deklarace weights, costs, capacity
            w = "5 6 8 9 4 4 5 2 8 8 7 9 9 9 6".Split(' ').Select(int.Parse).ToArray();
            c = "4 8 9 9 6 7 5 5 8 9 9 5 7 6 4".Split(' ').Select(int.Parse).ToArray();

            k = Int32.Parse("24");
            m = 0;
            b = new List<int>();
        }

        [Benchmark]
        public void Knapsack_Backtracking()
        {
            // TODO: doimplementovat
            //zkopírováno z prezentace
            int i = 0;
            int s = 0;
            int z = 0;
            List<int> p = new List<int>();
            if (z > k) return;

            if (s > m)

            {

                m = s;

                b = new List<int>(p);

            }

            if (i < w.Length)

            {

                p.Add(i + 1);

                q(i + 1, s + c[i], z + w[i], p);

                p.RemoveAt(p.Count - 1);

                q(i + 1, s, z, p);
            }
        }
        public void q(int i, int s, int z, List<int> p)
        {
            //zkopírováno z prezentace

            if (z > k) return;

            if (s > m)

            {

                m = s;

                b = new List<int>(p);

            }

            if (i < w.Length)

            {

                p.Add(i + 1);

                q(i + 1, s + c[i], z + w[i], p);

                p.RemoveAt(p.Count - 1);

                q(i + 1, s, z, p);
            }
        }
        [Benchmark]
        public void Knapsack_DynamicProgramming()
        {
            // TODO: Doimplementovat
            int[,] i = new int[w.Length + 1, k];
            for (int z = 0; z <= w.Length; z++) for (int o = 0; o < k; o++) i[z, o] = 0;
            for (int f = 0; f < w.Length; f++)
            {
                for (int r = 0; r < k; r++)
                {
                    if (r < w[f]) i[f + 1, r] = i[f, r];
                    else i[f + 1, r] = Math.Max(i[f, r], i[f, r - w[f]] + c[f]);
                }
            }
            string s = "";
            int y = k - 1;
            for (int g = 0; g < w.Length - 1; g++)
            {
                if (i[w.Length - g, y] - i[w.Length - g - 1, y] == 0 == false)
                {
                    y -= w[w.Length - g - 1];
                    s += w.Length - g + " ";
                }
            }
        }
    }

}