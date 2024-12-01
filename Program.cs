using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


class TextAnalyzer : StreamReader
{
    public Dictionary<string, int> _words { get; private set; }
    public int WordCount { get; private set; }
    public int CharactersNoSpacesCount { get; private set; }
    public int CharactersCount { get; private set; }
    public string Rewrite { get; private set; }
    public TextAnalyzer(string path) : base(path)
    {
        _words = new Dictionary<string, int>();
        WordCount = 0;
        CharactersCount = 0;
        CharactersNoSpacesCount = 0;
        Rewrite = "\n";
        Analyze();
    }
    private void Analyze()
    {
        try
        {
            string line;
            while((line = ReadLine()) != null)
            {
                CharactersCount += line.Length;
                string l = line;
                l = l.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");
                CharactersNoSpacesCount += l.Length;
                string word = "";
                int index = 0;
                foreach(char ch in l)
                {
                    if (ch == line[index])
                    {
                        word += ch;
                        index++;
                    }
                    else
                    {
                        if(word != "") Add(word);
                        while (ch != line[index]) index++;
                        word = "" + ch;
                        index++;
                    }
                }
                if (word != "") Add(word);
                Rewrite += '\n';
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("Input file error");
        }
    }
    private void Add(string word)
    {
        WordCount++;
        if (_words.ContainsKey(word)) _words[word]++;
        else _words[word] = 1;
        if (Rewrite.EndsWith('\n') == false) word = " " + word;
        Rewrite += word;
    }
}

class Program
{
    static void Main(string[] args)
    {
        TextAnalyzer ta = new TextAnalyzer("C:\\Users\\jandi\\Desktop\\soubory\\programování\\Analyzator\\vstupy\\2_vstup.txt");
        try
        {
            using(StreamWriter sw = new StreamWriter("C:\\Users\\jandi\\Desktop\\soubory\\programování\\Analyzator\\vstupy\\vystup.txt"))
            {
                sw.WriteLine("Počet slov: " + ta.WordCount);
                sw.WriteLine("Počet znaků (bez bílých znaků): " + ta.CharactersNoSpacesCount);
                sw.WriteLine("Počet znaků (s bílými znaky): " + ta.CharactersCount + "\n");
                foreach(var v in ta._words)
                {
                    sw.WriteLine(v.Key + ": " + v.Value);
                }
                sw.WriteLine(ta.Rewrite);
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("Output file error");
        }
        Console.WriteLine("Fertig");
        Console.ReadKey();
    }
}