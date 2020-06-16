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
            var articulos = ArticulosBLL.Buscar(Utilidades.ToInt(IdArticuloTextbox.Text));
            if (articulos != null)
                this.Articulos = articulos;
            else
                this.Articulos = new Articulos();

            this.DataContext = this.Articulos;
        }
        //=====================================================[ NUEVO ]=====================================================
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        //=====================================================[ GUARDAR ]=====================================================
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (!Validar())
                    return;

                var paso = ArticulosBLL.Guardar(Articulos);
                if (paso)
                {
                    Limpiar();
                    MessageBox.Show("Transaccion Exitosa", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Transaccion Errada", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        //=====================================================[ ELIMINAR ]=====================================================
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (ArticulosBLL.Eliminar(Utilidades.ToInt(IdArticuloTextbox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //=====================================================[ FORMULA (Existencia * Costo) ]=====================================================
        public double add(double a, double b)
        {
            double c = a * b;
            return c;
        }
        //=====================================================[ TEXT CHANGED ]=====================================================
        private void ExistenciaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int a = int.Parse(ExistenciaTextBox.Text);
                int b = int.Parse(CostoTextBox.Text);
                ValorInventarioTextBox.Text = add(a, b).ToString();
            }
            catch
            {
                
            }
        }
        private void CostoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double a = Convert.ToDouble(ExistenciaTextBox.Text);
                double b = Convert.ToDouble(CostoTextBox.Text);
                ValorInventarioTextBox.Text = "$ " + add(a, b).ToString();
            }
            catch
            {
                
            }
        }
    }
}
