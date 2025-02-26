using System;

namespace EndlichKeinWTFApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string kojnz_str = Console.ReadLine();
            string[] kojnz_feld = kojnz_str.Split(' ');
            int[] kojnz = new int[kojnz_feld.Length];
            for (int i = 0; i < kojnz_feld.Length; i++) kojnz[i] = Int32.Parse(kojnz_feld[i]);
            int suma = Int32.Parse(Console.ReadLine());
            Zahlen(suma, kojnz, "");
            Console.ReadKey();
        }
        static void Zahlen(int suma, int[] kojnz, string s)
        {
            for (int i = 0; i < kojnz.Length; i++)
            {
                int kojn = kojnz[i];
                if (suma < kojn) continue;
                if (suma == kojn)
                {
                    string ss = s + " " + kojn;
                    Console.WriteLine(ss.Substring(1));
                }
                if (suma > kojn)
                {
                    int[] kojnzz = new int[kojnz.Length - i];
                    for (int j = 0; j < kojnzz.Length; j++) kojnzz[j] = kojnz[i + j];
                    Zahlen(suma - kojn, kojnzz, s + " " + kojnz[i]);
                }
            }
        }
    }
}