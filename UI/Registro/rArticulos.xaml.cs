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
using rDetallado_Articulos.BLL;
using rDetallado_Articulos.Entidades;


namespace rDetallado_Articulos.UI.Registro
{
    public partial class rArticulos : Window
    {
        private Articulos Articulos = new Articulos();
        public rArticulos()
        {
            InitializeComponent();
            //Constructor
            this.DataContext = Articulos;
        }
        //----------------------------------[ CARGAR - Registro Detallado ]----------------------------------
        private void Cargar()
        {
            this.DataContext = null;
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
            if (DescripcionTextBox.Text.Trim() == String.Empty)
            {
                MessageBox.Show($"El Campo ({DescripcionLabel.Content}) esta vacio.\n\nDescriba el articulo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                DescripcionTextBox.Focus();
            }

            /*try
            {
                if (IdArticuloTextbox.Text.Trim() != "")
                {
                    MessageBox.Show($"El Id no fue encontrado.\n\nVerifique que existe o cree uno nuevo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                    IdArticuloTextbox.Focus();
                }
                
            }
            catch
            {
                MessageBox.Show($"El Id no fue encontrado.\n\nVerifique que existe o cree uno nuevo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }*/

            /*if (IdArticuloTextbox.Text.Trim() != IdArticulo)
            {
                MessageBox.Show($"El Id no fue encontrado.\n\nVerifique que existe o cree uno nuevo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                DescripcionTextBox.Focus();
            }*/

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
            //----------------------------------[ BUSCAR - Registro Detallado ]----------------------------------
            Articulos encontrado = ArticulosBLL.Buscar(Articulos.IdArticulo);

            if (encontrado != null)
            {
                Articulos = encontrado;
                Cargar();
                MessageBox.Show("Articulo Encontrado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                
                /*var articulos = ArticulosBLL.Buscar(Utilidades.ToInt(IdArticuloTextbox.Text));
                if (articulos != null)
                    this.Articulos = articulos;*/
            }
            else
            {
                Limpiar();
            }
        }
        //----------------------------------[ AGREGAR FILA - Registro Detallado ]----------------------------------
        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            var filaDetalle = new ArticulosDetalle(Articulos.IdArticulo, RequerimientoTextBox.Text, Convert.ToSingle(ValorTextBox.Text));

            Articulos.Detalle.Add(filaDetalle);
            Cargar();

            RequerimientoTextBox.Clear();
            ValorTextBox.Clear();
        }
        //----------------------------------[ REMOVER FILA - Registro Detallado ]----------------------------------
        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
            {
                Articulos.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                Cargar();
            }
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
                    /*MessageBox.Show($"El Campo ({ExistenciaLabel.Content}) esta vacio.\n\nEscriba la existencia actual del articulo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ExistenciaTextBox.Focus();*/
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
                    /*MessageBox.Show($"El Campo ({CostoLabel.Content}) esta vacio.\n\nEscriba el costo del articulo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                    CostoTextBox.Focus();*/
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
