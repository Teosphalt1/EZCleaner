 using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace EZCleaner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DirectoryInfo winTemp;
        public DirectoryInfo appTemp;

        public MainWindow()
        {
            InitializeComponent();
            winTemp = new DirectoryInfo(@"C:\Windows\Temp");
            appTemp = new DirectoryInfo(System.IO.Path.GetTempPath());

        }

        /// <summary>
        /// Calcul la taille d'un dossier
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public long DirSize(DirectoryInfo dir)
        {
            return dir.GetFiles().Sum(f => f.Length) + dir.GetDirectories().Sum(d => DirSize(d));
        }

        /// <summary>
        /// Vider un dossier
        /// </summary>
        /// <param name="dir"></param>
        public void ClearTempData(DirectoryInfo dir)
        {
            foreach(FileInfo file in dir.GetFiles())
            {
                try
                {
                    file.Delete();
                    Console.WriteLine(file.FullName);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            foreach(DirectoryInfo di in dir.GetDirectories())
            {
                try
                {
                    di.Delete(true);
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        private void Button_MAJ_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Votre logiciel est à jour.", "Mise à jour", MessageBoxButton.YesNo, MessageBoxImage.Information);
        }

        private void Button_Historic_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TODO: créer page historique");
        }

        private void Button_Web_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = "https://www.theophile-mariotte-portfolio.fr/",
                    UseShellExecute = true
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erreur: {ex.Message}");
            }
        }

        private void Button_Analyze_Click(object sender, RoutedEventArgs e)
        {
            AnalyzeFolders();
        }

        public void AnalyzeFolders()
        {
            long totalSize = 0;

            try
            {
                totalSize += DirSize(winTemp) / 1000000;
                totalSize += DirSize(appTemp) / 1000000;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Impossible d'analyser les dossiers: {ex.Message}");
            }


            espace.Content = totalSize + " Mb";
            titre1.Content = "Analyse effectuée";
            btnClean.Content = "\n\n\nNETTOYER";
            date.Content = DateTime.Now;
        }

        private void Button_Clean_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Nettoyage en cours...");
            btnClean.Content = "\n\n\nNettoyage en cours";

            Clipboard.Clear();

            try
            {
                ClearTempData(winTemp);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
            }

            try
            {
                ClearTempData(appTemp);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
            }

            btnClean.Content = "\n\n\nNettoyage terminé";
            titre1.Content = "Nettoyage effectuée";
            espace.Content = "0 Mb";
        }
    }
}
