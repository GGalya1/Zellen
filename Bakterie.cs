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

        public bool istTot { get; protected set; }

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
            istTot = false;
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

        public void Farbe(Brush color)
        {
            e.Fill = color;
        }

        public abstract List<Einzeller> Teilen();
        public abstract void Sterben();
        public abstract void Sterben(bool stirb);
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
            if (rnd.NextDouble() < 0.02) //das Entstehen mit einer Wahrscheinligkeit. Hier - 2 Prozent
            {
                toechter.Add(new Bakterie(this));
            }
            return toechter;
        }

        public override void Sterben()
        {
            if (rnd.NextDouble() < 0.01) //das Sterben mit einer Wahrscheinligkeit. Hier - 2 Prozent
            {
                this.istTot = true;
            }
        }
        public override void Sterben(bool stirb)
        {
            this.istTot = stirb;
        }
    }


    class Amoebe : Einzeller
    {
        //Eigenschaften

        //Konstrutor
        public Amoebe(Canvas Flache) : base(10, Brushes.Red, Flache) //braucht nur Variable, nicht erneut "Canvas Flache" deklarieren
        {

        }
        public Amoebe(Amoebe b) : base(b) { }//von oben abgeschrieben. Wiederholung: es kann zwei Konstruktoren sein


        //Methoden
        public override List<Einzeller> Teilen()
        {
            List<Einzeller> toechter = new List<Einzeller>();
            if (rnd.NextDouble() < 0.02) //das Entstehen mit einer Wahrscheinligkeit. Hier - 2 Prozent
            {
                toechter.Add(new Amoebe(this));
            }
            return toechter;
        }

        public override void Sterben()
        {
            if (rnd.NextDouble() < 0.01) //das Sterben mit einer Wahrscheinligkeit. Hier - 2 Prozent
            {
                this.istTot = true;
            }
        }
        public override void Sterben(bool stirb)
        {
            this.istTot = stirb;
        }

        public void Fressen(Bakterie futter)
        {
            double Abstand = Math.Sqrt(Math.Pow(PositionX - futter.PositionX,2) + Math.Pow(PositionY - futter.PositionY, 2));
            if (Abstand < 20)
            {
                futter.Sterben(true);
                this.Farbe(Brushes.AliceBlue); //von sich selbst aufraufen. Hier: Farbe 
            }
        }
    }
}
