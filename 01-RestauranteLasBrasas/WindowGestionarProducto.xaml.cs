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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace _01_RestauranteLasBrasas
{
    /// <summary>
    /// Lógica de interacción para WindowGestionarProducto.xaml
    /// </summary>
    public partial class WindowGestionarProducto : Window
    {
        Clase_Conectar conexion = new Clase_Conectar();
        public WindowGestionarProducto()
        {
            InitializeComponent();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conexion.AbrirConexion();
                if (conexion.Estado == 1)
                {
                    string query = string.Format("RegistarProducto");
                    SqlCommand command = new SqlCommand(query, conexion.Conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adaptador = new SqlDataAdapter(command);

                   /* using (adaptador)
                    {
                        command.Parameters.AddWithValue("@idCategoria", ComboCategoria.SelectedItem);
                        command.Parameters.AddWithValue("@nombre", txtNombreProducto.Text);
                        command.Parameters.AddWithValue("@marca", txtMarcaProducto.Text);
                        command.Parameters.AddWithValue("@stock");//falta agregar un campo
                        command.Parameters.AddWithValue("@precioVenta", txtPrecioProducto.Text);
                        command.Parameters.AddWithValue("@fechaVencimiento", dtVencimientoProducto);
                    }*/
                    MessageBox.Show("Registro agregado");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conexion.AbrirConexion();
                if (conexion.Estado == 1)
                {
                    string query = string.Format("RegistarProducto");
                    SqlCommand command = new SqlCommand(query, conexion.Conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adaptador = new SqlDataAdapter(command);

                  /*  using (adaptador)
                    {
                        command.Parameters.AddWithValue("@idCategoria", ComboCategoria.SelectedItem);
                        command.Parameters.AddWithValue("@nombre", txtNombreProducto.Text);
                        command.Parameters.AddWithValue("@marca", txtMarcaProducto.Text);
                        command.Parameters.AddWithValue("@stock", );//falta agregar un campo
                        command.Parameters.AddWithValue("@precioVenta", txtPrecioProducto.Text);
                        command.Parameters.AddWithValue("@fechaVencimiento", dtVencimientoProducto);
                    }*/
                    MessageBox.Show("Registro Actualizado");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtIdProducto.Clear();
            txtMarcaProducto.Clear();
            txtNombreProducto.Clear();
            txtPrecioProducto.Clear();
            txtStock.Clear();
            ComboCategoria.SelectedIndex = -1;
            dtVencimientoProducto.SelectedDate = DateTime.Now;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
