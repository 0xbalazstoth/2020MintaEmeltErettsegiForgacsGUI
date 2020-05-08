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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int ablakokSzamaAkt = 0;
        public CheckBox[,] matrix = new CheckBox[8, 8];
        public MainWindow()
        {
            InitializeComponent();
            Kirajzol();
        }

        private void Kirajzol()
        {
            StreamReader olvas = new StreamReader(@"kodlemez.txt");
            for (int sor = 0; sor <= 7; sor++)
            {
                string kodSor = olvas.ReadLine();

                for (int oszlop = 0; oszlop <= 7; oszlop++)
                {
                    CheckBox ch = new CheckBox();
                    Canvas.SetLeft(ch, oszlop * 38 + 30);
                    Canvas.SetTop(ch, sor * 25 + 50);

                    if (kodSor[oszlop] == '#')
                        ch.IsChecked = true;
                    else
                        ablakokSzamaAkt++;

                    canvas.Children.Add(ch);
                    ch.Click += Ch_Click;
                    matrix[sor, oszlop] = ch;
                }
            }
        }

        private void Ch_Click(object sender, RoutedEventArgs e)
        {
            CheckBox ch = (CheckBox)sender;
            if (ch.IsChecked == true)
                ablakokSzamaAkt--;
            else
                ablakokSzamaAkt++;

            Title = $"GUI - {ablakokSzamaAkt}";

            btnMentes.IsEnabled = ablakokSzamaAkt == 16;
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamWriter ki = new StreamWriter(@"kodlemezNEW.txt");

                for (int sor = 0; sor <= 7; sor++)
                {
                    for (int oszlop = 0; oszlop <= 7; oszlop++)
                    {
                        ki.Write(matrix[sor, oszlop].IsChecked == true ? "#" : "A");
                    }
                    ki.WriteLine();
                }

                ki.Close();

                MessageBox.Show("Sikeres mentés!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
