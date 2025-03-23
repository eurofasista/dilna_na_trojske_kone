
/*
Řešení lze optimalizovat vyhledáváním nejdelší podposloupnosti do i-tého prvku pomocí vyváženého vyhledávacího stromu a ne
pomocí standardního vyhledávání (podposloupnosti ve stromu seřadíme podle délky), ale implementaci tohoto stromu se mi nechce dělat.
 */
class Program
{
    static void Main(string[] args)
    {
        int[] fstup = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        List<int>[] pole = new List<int>[fstup.Length];
        if (fstup.Length == 0) Console.WriteLine("Prázdná posloupnost");
        else if (fstup.Length == 1) Console.WriteLine(fstup[0]);
        else
        {
            pole[0] = new List<int>{fstup[0]};
            for (int i = 1; i < fstup.Length; i++)
            {
                pole[i] = new List<int>{fstup[i]};
                for (int y = 0; y < i; y++)
                {
                    if (pole[y].Count >= pole[i].Count && pole[y].ElementAt(pole[y].Count - 1) < fstup[i])
                    {
                        pole[i] = pole[y];
                        pole[i].Add(fstup[i]);
                    }
                }
            }
            Console.WriteLine(string.Join(", ", pole[pole.Length - 1]));
        }
        Console.ReadKey();
    }
}