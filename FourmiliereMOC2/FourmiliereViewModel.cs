using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace FourmiliereMOC2
{
    public class FourmiliereViewModel : ViewModelBase
    {
        public int DimX { get; set; }
        public int DimY { get; set; }

        public string ApplicationTitle { get; set; }
        public ObservableCollection<Fourmi> ListFourmis { get; set; }


        private Fourmi fourmiSelect;
        public Fourmi FourmiSelect {
            get { return fourmiSelect; }
            set {
                fourmiSelect = value;
                OnPropertyChanged("FourmiSelect");
            }
        }



        public FourmiliereViewModel()
        {
            ApplicationTitle = "Une fourmiliere";
            DimX = 10;
            DimY = 20;
            VitesseExecution = 500;
            ListFourmis = new ObservableCollection<Fourmi>();
            ListFourmis.Add(new Fourmi(DimX, DimY, "Fourmi N.0"));
            ListFourmis.Add(new Fourmi(DimX, DimY, "Fourmi N.1"));
            ListFourmis.Add(new Fourmi(DimX, DimY, "Fourmi N.2"));
            // put  XML file in Debug/bin
            var fList = LireFichierXml("fourmis.xml").ToList();
            foreach (var uneFourmi in fList)
            {
                ListFourmis.Add(uneFourmi);
            }
        }

        public void AddFourmi()
        {
            ListFourmis.Add(new Fourmi(DimX, DimY, "Fourmi N." + ListFourmis.Count));
        }

        public void DeleteFourmi()
        {
            ListFourmis.Remove(FourmiSelect);
        }

        internal void TourSuivant()
        {
            foreach (var uneFourmi in ListFourmis)
            {
                uneFourmi.AvanceUnTour(DimX, DimY);
            }
        }

        public bool EnCours { get; set; }
        public int VitesseExecution { get; set; }

        public void Avance()
        {
            EnCours = true;
            while(EnCours)
            {
                Thread.Sleep(VitesseExecution);
                TourSuivant();
            }
        }

        internal void Stop()
        {
            EnCours = false;
        }

        public static IEnumerable<Fourmi> LireFichierXml(string strFichier)
        {
            XDocument xdoc = XDocument.Load(strFichier);
            XElement xdocattributs = xdoc.Root.Element("fourmis");
            if (xdocattributs != null)
            {
                IEnumerable<XElement> xFourmisList = xdocattributs.Elements("fourmi");
                foreach (XElement xFourmi in xFourmisList)
                {
                    string unNom = xFourmi.Attribute("nom").Value;
                    int X = int.Parse(xFourmi.Element("X").Value);
                    int Y = int.Parse(xFourmi.Element("Y").Value);

                    var uneFourmi = new Fourmi(X, Y, unNom);
                    yield return uneFourmi;
                }
            } 
        }
    }
}