

using System.Linq.Expressions;

class Program
{
    //v Mainu se pouze spouští všechny vedlejší funkce, ke správnému fungování programu je nutné sem vepsat cesty ke vstupnímu a výstupnímu souboru
    static void Main(string[] args)
    {
        string fstup = "vstupní soubor (vepište zde)";
        string vistup = "výstupní soubor (vepište zde)";
        (Pole[,] pole_poli, Tuple<int, int>[] ch, int rozmer) = NactiVstup(fstup);
        if (pole_poli != null)
        {
            (string output1, string output2) = NajdiCestu(pole_poli, ch, rozmer);
            VypisVystup(output1, output2, vistup);
        }
        Console.ReadKey();
    }
    //tato funkce hledá cestu do pravého dolního rohu tak dlouho, dokud ji nenajde nebo dokud ji to nepřestane bavit
    //to se stane v momentě, kdy během jednoho průchodu naobjeví nic nového a přísun dopaminu z objevování nových věcí se zastaví
    //ze všech objevených polí program tvoří strom
    static (string, string) NajdiCestu(Pole[,] pole, Tuple<int, int>[] smery, int rozmer)
    {
        int cas = 0;
        pole[0, 0].Dostupnost = 0;
        while(true)
        {
            List<Pole> koncova_pole = pole[0, 0].KoncovaPole(cas);
            if (koncova_pole.Count == 0) break;
            foreach(Pole konec in koncova_pole)
            {
                foreach(Tuple<int, int> smer in smery) if(konec.X + smer.Item1 >= 0 && konec.X + smer.Item1 < rozmer && konec.Y + smer.Item2 >= 0 && konec.Y + smer.Item2 < rozmer)
                    {
                        if (pole[konec.X + smer.Item1, konec.Y + smer.Item2].Dostupnost == -1 && pole[konec.X + smer.Item1, konec.Y + smer.Item2].Pruchozi(cas + 1))
                        {
                            pole[konec.X + smer.Item1, konec.Y + smer.Item2].Dostupnost = cas + 1;
                            konec.Dalsi.Add(pole[konec.X + smer.Item1, konec.Y + smer.Item2]);
                        }
                    }
            }
            cas++;
            if (pole[rozmer - 1, rozmer - 1].Dostupnost != -1) break;
        }
        if (pole[rozmer - 1, rozmer - 1].Dostupnost == -1)
        {
            Console.WriteLine("Jestli se tam posel pokusí dojít, dopadne jako Petr Kellner.");
            return ("-1", null);
        }
        else Console.WriteLine("Posel na hrad dojde v " + pole[rozmer - 1, rozmer - 1].Dostupnost + " krocích.");
        return (pole[rozmer - 1, rozmer - 1].Dostupnost.ToString(), pole[0, 0].VypisCestu(rozmer));
    }
    //tato funkce pouze vypisuje požadovaný výstup
    static void VypisVystup(string output1, string output2, string vistup)
    {
        try
        {
            using(StreamWriter sw = new StreamWriter(vistup))
            {
                sw.WriteLine(output1);
                if(output2 != null) sw.WriteLine(output2);
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("Chyba výstupního souboru. Zkontrolujte prosím, zda Piráti nezdigitalizovali vaše úložiště.");
        }
    }
    //tato funkce načítá vstupní data, v případě nějaké chyby vstupního souboru skočí do catch bloku a chybu oznámí
    static (Pole[,], Tuple<int, int>[], int) NactiVstup(string fstup)
    {
        try
        {
            Pole[,] pole_poli;
            Tuple<int, int>[] smery;
            int v;
            using (StreamReader sr = new StreamReader(fstup))
            {
                v = Int32.Parse(sr.ReadLine());
                pole_poli = new Pole[v, v];
                for (int a = 0; a < v; a++) for (int b = 0; b < v; b++) pole_poli[a, b] = new Pole(a, b);
                int pocet = Int32.Parse(sr.ReadLine());
                for(int i = 0; i < pocet; i++)
                {
                    string[] s = sr.ReadLine().Split(' ');
                    pole_poli[Int32.Parse(s[0]), Int32.Parse(s[1])].Lavina(Int32.Parse(s[2]));
                }
                pocet = Int32.Parse(sr.ReadLine());
                smery = new Tuple<int, int>[pocet];
                for (int i = 0; i < pocet; i++)
                {
                    string[] s = sr.ReadLine().Split(' ');
                    smery[i] = new Tuple<int, int>(Int32.Parse(s[0]), Int32.Parse(s[1]));
                }
            }
            return (pole_poli, smery, v);
        }
        catch(Exception e)
        {
            Console.WriteLine("Chyba vstupního souboru. Zkontrolujte prosím, jestli náhodou čísla do tohoto souboru nezadával Stanjura.");
        }
        return (null, null, 0);
    }
}

class Pole
{
    private int Prostupnost;
    public int Dostupnost;
    public List<Pole> Dalsi;
    public int X;
    public int Y;
    public Pole(int x, int y)
    {
        X = x;
        Y = y;
        Prostupnost = -1;
        Dostupnost = -1;
        Dalsi = new List<Pole>();
    }
    //volání této funkce vytvoří lavinu nebo horu
    public void Lavina(int cas) { Prostupnost = cas; }
    //pomocí této funkce může program zjistit, zda je v daném čase pole průchozí
    public bool Pruchozi(int cas)
    {
        if (Prostupnost == -1) return true;
        else return (Prostupnost > cas);
    }
    //rekurentně vyhledává v podstromu, jehož vrcholem je toto pole, všechna pole, na která se posel může dostat v čase cas
    public List<Pole> KoncovaPole(int cas)
    {
        if (cas == Dostupnost) return new List<Pole> { this };
        if (cas == Dostupnost + 1) return Dalsi;
        else
        {
            List<Pole> pole = new List<Pole>();
            foreach (Pole policko in Dalsi) pole.AddRange(policko.KoncovaPole(cas));
            return pole;
        }
    }
    //rekurentně vypisuje cestu z tohoto pole na pravý dolní roh
    public string VypisCestu(int rozmer)
    {
        if (X == rozmer - 1 && Y == rozmer - 1) return "[" + X.ToString() + "," + Y.ToString() + "]";
        if (Dalsi.Count == 0) return "";
        string str = "";
        foreach (Pole pole in Dalsi) str += pole.VypisCestu(rozmer);
        if (str != "") return "[" + X + "," + Y + "] " + str;
        return "";
    }
}