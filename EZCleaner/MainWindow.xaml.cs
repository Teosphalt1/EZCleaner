 using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
