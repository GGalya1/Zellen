using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Zellen
{
    abstract class Einzeller
    {
        //Eigenschaften
       static protected Random rnd = new Random();

        public double PositionX { get; private set; }/* = rnd.Next(10, 752);*/
        public double PositionY { get; private set; }/* = rnd.Next(10, 350);*/

        Ellipse e = new Ellipse();

        //Konstruktor
        public Einzeller(double radius, Brush color,  Canvas Flache)
        {
            PositionX = rnd.Next(0, Convert.ToInt32(Flache.ActualWidth));
            PositionY = rnd.Next(0, Convert.ToInt32(Flache.ActualHeight));

            e.Width = radius;
            e.Height = radius;
            e.Fill = color; //alle diese Variablen geben wir dann in abgeleiteten Klassen wieder
        }

        public Einzeller(Einzeller ez)//es kann mehr als ein Konstruktor sein
        {
            PositionX = ez.PositionX;
            PositionY = ez.PositionY;
            e.Width = ez.e.Width;
            e.Height = ez.e.Height;
            e.Fill = ez.e.Fill;
        }

        //Methoden
        public void Zeichen(Canvas Flache)
        {
            Flache.Children.Add(e);
            Canvas.SetTop(e, PositionY);
            Canvas.SetLeft(e, PositionX);
        }

        public void Bewegen()
        {
            PositionX += rnd.Next(-1, 2) * 2; //mit der zufälligen Richtung für 4 Pixels
            PositionY += rnd.Next(-1, 2) * 2;
        }

        public abstract List<Einzeller> Teilen();
    }


    class Bakterie : Einzeller
    {
        //Eigenschaften

        //Konstrutor
        public Bakterie(Canvas Flache): base(5, Brushes.Green , Flache) //braucht nur Variable, nicht erneut "Canvas Flache" deklarieren
        {

        }
        public Bakterie(Bakterie b) : base(b) { }//von oben abgeschrieben. Wiederholung: es kann zwei Konstruktoren sein

        //Methoden
        public override List<Einzeller> Teilen()
        {
            List<Einzeller> toechter = new List<Einzeller>();
            if (rnd.NextDouble() < 0.2) //das Entstehen mit einer Wahrscheinligkeit. Hier - 20 Prozent
            {
                toechter.Add(new Bakterie(this));
            }
            return toechter;
        }
    }


    class Amoebe : Einzeller
    {
        //Eigenschaften

        //Konstrutor
        public Amoebe(Canvas Flache) : base(10, Brushes.Red, Flache) //braucht nur Variable, nicht erneut "Canvas Flache" deklarieren
        {

        }


        //Methoden
    }
}
