using System;
using System.Collections.Generic;

class Program
{
    static int m = 0;
    static List<int> b = new List<int>();
    public static void Main()
    {
        string[] d = Console.ReadLine().Split(' ');
        string[] h = Console.ReadLine().Split(' ');
        int[] w = new int[d.Length];
        int[] v = new int[h.Length];
        for(int i = 0; i < d.Length; i++)
        {
            w[i] = Int32.Parse(d[i]);
            v[i] = Int32.Parse(h[i]);
        }
        int c = Int32.Parse(Console.ReadLine());

        q(w, v, c, 0, 0, 0, new List<int>());

        Console.WriteLine("Celková cena: " + m);
        Console.WriteLine("Ponožky: " + string.Join(" ", b));
        Console.ReadKey();
    }
    /*  popisky:
     * w - pole vah
     * v - pole cen
     * c - celková hmotnost
     * i - index zkoumané ponožky
     * s - celková cena
     * z - celková hmotnost
     * p - seznam položek
     */
    public static void q(int[] w, int[] v, int c, int i, int s, int z, List<int> p)
    {
        if (z > c) return;
        if (s > m)
        {
            m = s;
            b = new List<int>(p);
        }
        if (i < w.Length)
        {
            p.Add(i + 1);
            q(w, v, c, i + 1, s + v[i], z + w[i], p);
            p.RemoveAt(p.Count - 1);
            q(w, v, c, i + 1, s, z, p);
        }
    }
}