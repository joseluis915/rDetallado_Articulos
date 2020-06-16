using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//Añadí estos using
using PrimerParcial_JoseLuis.BLL;
using PrimerParcial_JoseLuis.Entidades;


namespace PrimerParcial_JoseLuis.UI.Registro
{
    public partial class rArticulos : Window
    {
        private Articulos Articulos = new Articulos();
        public rArticulos()
        {
            InitializeComponent();
            this.DataContext = Articulos;
        }
        //=====================================================[ LIMPIAR ]=====================================================
        private void Limpiar()
        {
            this.Articulos = new Articulos();
            this.DataContext = Articulos;
        }
        //=====================================================[ Validar ]=====================================================
        private bool Validar()
        {
            bool Validado = true;
            if (IdArticuloTextbox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transaccion Errada", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return Validado;
        }
        //=====================================================[ BUSCAR ]=====================================================
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {

        }
        //=====================================================[ NUEVO ]=====================================================
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {

        }
        //=====================================================[ GUARDAR ]=====================================================
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {

        }
        //=====================================================[ ELIMINAR ]=====================================================
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
