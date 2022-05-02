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
            List<Einzeller> Kindergarten = new List<Einzeller>();
            List<Einzeller> Futter = new List<Einzeller>();

            foreach (Einzeller item in zoo) //nur Bakterien in Zoo durchlaufen
            {
                if (item is Bakterie)
                    Futter.Add(item);
            }

            zoo.ForEach(x => 
            { 
                x.Bewegen();
                x.Zeichen(Flache);
                Kindergarten.AddRange(x.Teilen());
                x.Sterben();
            }); //viele Zeilen in einer kombiniert. Man kann auch eine foreach-Schleife machen

            foreach (Einzeller itemAmoebe in zoo) //jetzt jede Amöbe guckt, ob sie eine Bakterie fressen kann
            {
                if(itemAmoebe is Amoebe)
                {
                    foreach (Bakterie itemBakterie in Futter)
                    {
                        itemAmoebe.Fressen(itemBakterie);
                    }
                }
            }

            //foreach (Einzeller item in zoo) //erstellen zwei Liste mit Zellen die geteilt und die gestorben wurden
            //{
            //   Kindergarten.AddRange(item.Teilen());
            //    item.Sterben();
            //}

            zoo.AddRange(Kindergarten); //und dann fuegen die ganze Liste in unserer Hauptliste
            zoo.RemoveAll(x => x.istTot);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            button.IsEnabled = false;
            for (int i = 0; i < 5; i++)
            {
                zoo.Add(new Bakterie(Flache));
                zoo.Add(new Amoebe(Flache));
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point position = Mouse.GetPosition(Flache);
            Amoebe a = new Amoebe(Flache);
            //double PosX = position.X;
            //double PosY = position.Y;

            a.Click(position.X, position.Y);
            zoo.Add(a);
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point position = Mouse.GetPosition(Flache);
            Bakterie a = new Bakterie(Flache);

            a.Click(position.X, position.Y);
            zoo.Add(a);
        }
    }
}
