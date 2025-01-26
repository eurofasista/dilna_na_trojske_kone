using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;

namespace DalsiWtfApp
{
    /// <summary>
    /// Interakční logika pro Wokno.xaml
    /// </summary>
    public partial class Wokno : Window
    {
        public bool Registrace;
        public string Losername;
        public string Heszlo;
        public BitmapImage ChotkaFodidel;
        public string Hlaaska;
        public List<Loser> SeznamLoseru;
        public Loser VybranyLoser;
        public Wokno(List<Loser> seznam)
        {
            InitializeComponent();
            Registrace = false;
            SeznamLoseru = seznam;
        }
        public void H1(object sender, EventArgs e)
        {
            Registrace = false;
            Hlaskovac.Content = "";
            Hlaska.IsEnabled = false;
            Hlaska.Visibility = Visibility.Hidden;
            Login.Content = "Přyhlásyt";
            Signup.IsEnabled = false;
            Signup.Visibility = Visibility.Hidden;
            Errorer.Content = "";
        }
        public void H2(object sender, EventArgs e)
        {
            Registrace = true;
            Hlaskovac.Content = "Oblíbená hláška";
            Hlaska.IsEnabled = true;
            Hlaska.Visibility = Visibility.Visible;
            Login.Content = "Nahrát fotku chodidel";
            Signup.IsEnabled = true;
            Signup.Visibility = Visibility.Visible;
            Errorer.Content = "";
        }
        public void B1(object sender, EventArgs e)
        {
            if (Registrace)
            {
                var dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.FileName = "Document"; // Default file name
                dialog.DefaultExt = ".png"; // Default file extension
                dialog.Filter = "Images (.png)|*.png"; // Filter files by extension

                // Show open file dialog box
                bool? result = dialog.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    // Open document
                    string filename = dialog.FileName;
                    ChotkaFodidel = new BitmapImage(new Uri(filename));
                }
            }
            else
            {
                if (SeznamLoseru.Where(x => x.LoserName == Username.Text).Count() > 0)
                {
                    if (SeznamLoseru.Where(x => x.LoserName == Username.Text && x.Heslo == Heslo.Password).Count() > 0)
                    {
                        VybranyLoser = SeznamLoseru.Where(x => x.LoserName == Username.Text).ElementAt(0);
                        this.Close();
                    }
                    else MessageBox.Show("Nesprávné heslo.");
                }
                else MessageBox.Show("Neexistující losername.");
            }
        }
        public void B2(object sender, EventArgs e)
        {
            if (Username.Text == "") Error("Zadejte losername.");
            else if (Heslo.Password.Count() < 2) Error("Zadejte heslo alespoň o 2 znacích.");
            else if (ChotkaFodidel == null) Error("Nahrajte chotku fodidel.");
            else if (Hlaska.Text.Count() < 30) Error("Vaše hláška je trapná. Pořádná hláška má aspoň 30 znaků.");
            else
            {
                VybranyLoser = new Loser(Username.Text, Heslo.Password, ChotkaFodidel, Hlaska.Text);
                SeznamLoseru.Add(VybranyLoser);
                this.Close();
            }
        }
        public void Error(string text)
        {
            Errorer.Content = text;
        }
    }
}