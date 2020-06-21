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
                MessageBox.Show("El registro no fue encontrado.\n\nIntente buscar un registro existente o Cree uno nuevo.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
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

                if (DescripcionTextBox.Text.Trim() == String.Empty)
                {
                    MessageBox.Show($"El Campo ({DescripcionLabel.Content}) esta vacio.\n\nDescriba el articulo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                    DescripcionTextBox.Focus();
                    return;
                }

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
        public double resultado(int existencia, double costo)
        {
            double formula = existencia * costo;
            return formula;
        }
        //=====================================================[ TEXT CHANGED ]=====================================================
        private void ExistenciaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (ExistenciaTextBox.Text.Trim() != "")
                {
                    int existencia = int.Parse(ExistenciaTextBox.Text);

                    if (CostoTextBox.Text != "")
                    {
                        double costo = Convert.ToDouble(CostoTextBox.Text.Replace('.', ','));
                        ValorInventarioTextBox.Text = "$ " + resultado(existencia, costo);
                    }
                    else
                    {
                        double costo = 0;
                        ValorInventarioTextBox.Text = "$ " + resultado(existencia, costo);
                    }
                }
                else
                {
                    MessageBox.Show($"El Campo ({ExistenciaLabel.Content}) esta vacio.\n\nEscriba la existencia actual del articulo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ExistenciaTextBox.Focus();
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo ({ExistenciaLabel.Content}) no es un numero.\n\nPorfavor, digite un numero.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                ExistenciaTextBox.Text = "";
                ExistenciaTextBox.Focus();
            }
        }
        private void CostoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (CostoTextBox.Text.Trim() != "")
                {
                    int existencia = int.Parse(ExistenciaTextBox.Text);
                    double costo = Convert.ToDouble(CostoTextBox.Text.Replace('.', ','));
                    ValorInventarioTextBox.Text = "$ " + resultado(existencia, costo);
                }
                else
                {
                    MessageBox.Show($"El Campo ({CostoLabel.Content}) esta vacio.\n\nEscriba el costo del articulo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                    CostoTextBox.Focus();
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo ({CostoLabel.Content}) no es un numero.\n\nPorfavor, digite un numero.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                CostoTextBox.Text = "";
                CostoTextBox.Focus();
            }
        }
    }
}
