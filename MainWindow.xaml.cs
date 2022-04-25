using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace Zellen
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        List<Einzeller> zoo = new List<Einzeller>(); //die Objekten des Types "Einzeller"

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(17);
            timer.Tick += animieren;
        }

        public void animieren(object sender, EventArgs e)
        {
            Flache.Children.Clear();
            zoo.ForEach(x => { x.Bewegen(); x.Zeichen(Flache); }); //viele Zeilen in einer kombiniert. Man kann auch eine foreach-Schleife machen
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            for (int i = 0; i < 5; i++)
            {
                zoo.Add(new Bakterie(Flache));
                zoo.Add(new Amoebe(Flache));
            }
        }
    }
}
