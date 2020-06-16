using PrimerParcial_JoseLuis.UI.Registro;
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

namespace PrimerParcial_JoseLuis
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void rArticulosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rArticulos rVehiculos = new rArticulos();
            rVehiculos.Show();
        }
    }
}
