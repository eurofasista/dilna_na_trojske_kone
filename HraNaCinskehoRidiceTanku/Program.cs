class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Zadejte počet kol.");
        int k = Int32.Parse(Console.ReadLine());
        int[,] pole = new int[100, k];
        int[,] yndexy = new int[100, k];
        yndexy[0, 0] = -1;
        yndexy[99, 0] = -1;
        for (int i = 0; i < 100; i++) pole[i, 0] = 100000;
        for(int a = 1; a < k; a++)
        {
            for(int n = 0; n < 100; n++)
            {
                int max = 0;
                for(int i = 0; i <= n; i++)
                {
                    int profit = pole[i, a - 1] + (100000 * (n - i)) / (n + 1);
                    if (profit > max)
                    {
                        max = profit;
                        yndexy[n, a] = i;
                    }
                }
                pole[n, a] = max;
            }
        }
        Console.WriteLine(pole[99, k - 1]);
        int m = 99;
        int[] finalni_yndexy = new int[k + 1];
        finalni_yndexy[k] = 99;
        for (int i = k - 1; i >= 0; i--) { finalni_yndexy[i] = yndexy[m, i]; m = yndexy[m, i]; }
        for(int i = k - 1; i >= 0; i--) Console.WriteLine(finalni_yndexy[i + 1] - finalni_yndexy[i]);
        Console.ReadKey();
    }
}
/*
Při psaní kódu jsem dbal na časovou složitost. Brute force je samozřejmě extrémně neefektivní.
Teoreticky se při řešení problému dá použít rekurze.
Ta by spočívala v postupném redukování počtu kol a vracení nejvyššího možného zisku, kterého je možné po a kolech dosáhnout
v případě, že by zbývalo ještě n hráčů.
Prohledávání stavů hry by ovšem bylo nejspíše podobně neefektivní, jako brute force.
Rekurzivní postup ovšem můžeme použít s malou změnou.
Mezivýsledky maximálního zisku pro a kol a n lidí si můžeme zapsat do pole a toto pole postupně vyplnit.
Takto se dostaneme až k výsledku pro 100 lidí a k kol.
Projekt jsem pojmenoval "Hra na čínského řidiče tanku", protože nápadně připomíná, co se nestalo na Náměstí nebeského míru.
Podle západního propagandistického mainstreamu zde řidiči tanků zabíjeli nebohé čínské studenty a získávali sociální kredit
podle počtu zabitých studentů.
 */