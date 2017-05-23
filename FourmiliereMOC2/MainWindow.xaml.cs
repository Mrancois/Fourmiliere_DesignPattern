using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace FourmiliereMOC2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();
            dt.Tick += new EventHandler(Redessine_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, App.FourmiliereVM.VitesseExecution);
            DataContext = App.FourmiliereVM;
        }

        private void Redessine_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                DrawBoard();
            }
        }

        private void DrawBoard()
        {
            Board.ColumnDefinitions.Clear();
            Board.RowDefinitions.Clear();
            Board.Children.Clear();

            for (int i = 0; i < App.FourmiliereVM.DimX; i++)
            {
                Board.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < App.FourmiliereVM.DimY; i++)
            {
                Board.RowDefinitions.Add(new RowDefinition());
            }


            for (int i = 0; i < App.FourmiliereVM.DimX; i++)
            {
                for (int j = 0; j < App.FourmiliereVM.DimY; i++)
                {
                    Button btn = new Button();
                    btn.Background = new SolidColorBrush(Colors.AliceBlue);
                    Grid.SetRow(btn, j);
                    Grid.SetColumn(btn, i);
                    Board.Children.Add(btn);
                    btn.Click += BtnOnClick;
                    btn.Tag = "col: " + i + " - row: " + j;
                }
            }


            Ellipse e = new Ellipse();
            e.Fill = new SolidColorBrush(Colors.BlueViolet);
            Grid.SetRow(e, 4);
            Grid.SetColumn(e, 4);
            Board.Children.Add(e);

            Ellipse pheromone = new Ellipse();
            pheromone.Width = 10;
            pheromone.Height = 10;
            pheromone.Stroke = new SolidColorBrush(Colors.Red);
            Grid.SetColumn(pheromone, 9);
            Grid.SetRow(pheromone, 9);
            Board.Children.Add(pheromone);

            foreach ( var fourmi in App.FourmiliereVM.ListFourmis)
            {
                Image img = new Image();
                Uri uri = new Uri("fourmis.jpg", UriKind.Relative);
                img.Source = new BitmapImage(uri);
                Grid.SetRow(img, fourmi.X);
                Grid.SetColumn(img, fourmi.Y);
                Board.Children.Add(img);
            }

        }

        private void BtnOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((Button)sender).Tag.ToString());
        }

        private void BtnAddFourmis_Click(object sender, RoutedEventArgs e)
        {
            App.FourmiliereVM.AddFourmi();
            DrawBoard();
        }

        private void BtnDeleteFourmis_Click(object sender, RoutedEventArgs e)
        {
            App.FourmiliereVM.DeleteFourmi();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.ShowDialog();
        }

        private void BtnTourSuivant_Click(object sender, RoutedEventArgs e)
        {
            App.FourmiliereVM.TourSuivant();
            // int x = App.FourmiliereVM.DimX;
            // int y = App.FourmiliereVM.DimY;
            DrawBoard();
        }

        private void BtnAvanceFourmis_Click(object sender, RoutedEventArgs e)
        {
            Thread tt = new Thread(App.FourmiliereVM.Avance);
            tt.Start();
            stopWatch.Start();
            dt.Start();
        }

        private void BtnStopFourmis_Click(object sender, RoutedEventArgs e)
        {
            App.FourmiliereVM.Stop();
            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
            }
            DrawBoard();
        }
    }
}
