using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wtfapp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tb_ruda.TextChanged += TCH;
            tb_zelena.TextChanged += TCH;
            tb_modra.TextChanged += TCH;
            s_ruda.ValueChanged += SUP;
            s_zelena.ValueChanged += SUP;
            s_modra.ValueChanged += SUP;
        }

        public Slider NajdiKamaradaT(TextBox c)
        {
            if (c == tb_ruda) return s_ruda;
            if (c == tb_zelena) return s_zelena;
            return s_modra;
        }

        public TextBox NajdiKamaradaS(Slider c)
        {
            if (c == s_ruda) return tb_ruda;
            if (c == s_zelena) return tb_zelena;
            return tb_modra;
        }

        public void TCH(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            Slider s = NajdiKamaradaT(tb);

            if (int.TryParse(tb.Text, out int value))
            {
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("Zadej hodnotu v rozmezí 0-255 lyberále.");
                    tb.Text = s.Value.ToString();
                }
                else
                {
                    s.Value = value;
                    UpravBarvu();
                }
            }
            else
            {
                MessageBox.Show("Zadej platné číslo lyberále.");
                tb.Text = s.Value.ToString();
            }
        }

        public void SUP(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider s = sender as Slider;
            TextBox tb = NajdiKamaradaS(s);
            tb.Text = ((int)s.Value).ToString();
            UpravBarvu();
        }

        private void UpravBarvu()
        {
            byte komunisticka = (byte)s_ruda.Value;
            byte ekofasisticka = (byte)s_zelena.Value;
            byte lyberalska = (byte)s_modra.Value;
            R.Fill = new SolidColorBrush(Color.FromRgb(komunisticka, ekofasisticka, lyberalska));
            L.Content = $"#{komunisticka:X2}{ekofasisticka:X2}{lyberalska:X2}";
        }
    }
}