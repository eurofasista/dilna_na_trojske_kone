using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DalsiWtfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Loser> SeznamLoseru;
        public Loser VybranyLoser;
        public MainWindow()
        {
            SeznamLoseru = NactiLosery();
            InitializeComponent();
            Wokno w = new Wokno(SeznamLoseru);
            w.ShowDialog();
            SeznamLoseru = w.SeznamLoseru;
            VybranyLoser = w.VybranyLoser;
            if (w.Registrace) ZapisLosery(SeznamLoseru);
            VlozLoserData(VybranyLoser);
            MessageBox.Show("Připravte si blicí sáček!");
        }
        public void KladaVen(object sender, EventArgs e)
        {
            this.Hide();
            MainWindow mw = new MainWindow();
            mw.ShowDialog();
        }
        public List<Loser> NactiLosery()
        {
            List<Loser> L = new List<Loser>();
            using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + @"\data.txt"))
            {
                string str;
                while ((str = sr.ReadLine()) != null)
                {
                    string[] s = str.Split(';');
                    BitmapImage chotka = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\" + s[0] + ".png"));
                    L.Add(new Loser(s[0], s[1], chotka, s[2]));
                }
            }
            return L;
        }
        public void ZapisLosery(List<Loser> loseri)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(VybranyLoser.ChotkaFodidel));

            // Save the encoded image to the specified file
            using (FileStream fileStream = new FileStream(Directory.GetCurrentDirectory() + @"\" + VybranyLoser.LoserName + ".png", FileMode.Create))
            {
                encoder.Save(fileStream);
            }
            using (StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + @"\data.txt"))
            {
                foreach (Loser l in loseri) sw.WriteLine(l.LoserName + ";" + l.Heslo + ";" + l.Hlaska);
            }
        }
        public void VlozLoserData(Loser loser)
        {
            LoserNameText.Text = loser.LoserName;
            HlaskaText.Text = loser.Hlaska;
            ChotkaFodidelOprasek.Source = loser.ChotkaFodidel;
        }
    }

    public class Loser
    {
        public string LoserName;
        public string Heslo;
        public BitmapImage ChotkaFodidel;
        public string Hlaska;
        public Loser(string losername, string heslo, BitmapImage chotkafodidel, string hlaska)
        {
            LoserName = losername;
            Heslo = heslo;
            ChotkaFodidel = chotkafodidel;
            Hlaska = hlaska;
        }
    }
}
