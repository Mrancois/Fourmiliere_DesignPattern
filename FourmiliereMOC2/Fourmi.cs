using System;
using System.Collections.ObjectModel;

namespace FourmiliereMOC2
{
    public class Fourmi
    {
        static Random Hazard = new Random();
        public string Name { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public ObservableCollection<Etape> EtapesList { get; set; }

        public Fourmi(int maxDimX, int maxDimY, string aName = "Anonyme")
        {
            Name = aName;
            X = Hazard.Next(maxDimX);
            Y = Hazard.Next(maxDimY);

            EtapesList = new ObservableCollection<Etape>();
            int nbEtapes = Hazard.Next(10);
            for (int i = 0; i < nbEtapes; i++)
            {
                EtapesList.Add(new Etape());
            }
        }

        public override string ToString()
        {
            return "(Brouillon) " + Name;
        }

        internal void AvanceUnTour(int dimX, int dimY)
        {
            AvanceAuHasard(dimX, dimY);
            EtapesList.Add(new Etape() { Lieu = X + "" + Y });
        }

        internal void AvanceAuHasard(int dimensionX, int dimensionY)
        {
            int newX = X + Hazard.Next(3) - 1;
            int newY = Y + Hazard.Next(3) - 1;
            if ((newX >= 0) && (newX < dimensionX))
            {
                X = newX;
            }
            if ((newY >= 0) && (newY < dimensionY))
            {
                Y = newY;
            }
        }
    }
}