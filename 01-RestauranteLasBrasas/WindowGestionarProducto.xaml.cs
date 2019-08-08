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
using System.Configuration;

namespace _01_RestauranteLasBrasas
{
    /// <summary>
    /// Lógica de interacción para WindowGestionarProducto.xaml
    /// </summary>
    public partial class WindowGestionarProducto : Window
    {
        private DataClasses1DataContext data;
        public WindowGestionarProducto()
        {
            InitializeComponent();
            
            string connectionString = ConfigurationManager.ConnectionStrings["_01_RestauranteLasBrasas.Properties.Settings.BD_RestauranteLasBrasasConnectionString"].ConnectionString;

            data = new DataClasses1DataContext(connectionString);
            var producto = from u in data.GetTable<Producto>()
                           select new { u.IdProducto, u.IdCategoria, u.Nombre, u.Marca, u.PrecioVenta, u.FechaVencimiento, u.Stock };
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Producto pro = new Producto();

                pro.Nombre = txtNombreProducto.Text;
                pro.Marca = txtMarcaProducto.Text;
                pro.PrecioVenta = Convert.ToDecimal(txtPrecioProducto.Text);
                pro.FechaVencimiento = Convert.ToDateTime(dtVencimientoProducto.Text);
                pro.IdCategoria = Convert.ToInt32(txtCategoria.Text);
                pro.Stock = Convert.ToInt32(txtStock.Text);

                data.Producto.InsertOnSubmit(pro);
                data.SubmitChanges();
                MessageBox.Show("REGISTRO AGREGADO");
            
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
                Producto pro = new Producto();
                if (txtNombreProducto.Text == pro.Nombre)
                {
                    pro.Nombre = txtNombreProducto.Text;
                    pro.Marca = txtMarcaProducto.Text;
                    pro.PrecioVenta = Convert.ToDecimal(txtPrecioProducto.Text);
                    pro.FechaVencimiento = Convert.ToDateTime(dtVencimientoProducto.Text);
                    pro.IdCategoria = Convert.ToInt32(txtCategoria.Text);
                    pro.Stock = Convert.ToInt32(txtStock.Text);

                    data.Producto.InsertOnSubmit(pro);
                    data.SubmitChanges();

                    MessageBox.Show("Registro Actualizado");
                }
                else
                    MessageBox.Show("El producto no existe");
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            var producto = (from pro in data.Producto
                            where pro.Nombre == txtNombreProducto.Text
                            select pro).First();
             if(producto != null)
            {
                var eliminar = from elim in data.Producto
                               where elim.Nombre.Equals(txtNombreProducto.Text)
                               select elim;
                foreach( var detalles in eliminar)
                {
                    data.Producto.DeleteOnSubmit(detalles);
                }
                try
                {
                    data.SubmitChanges();
                    MessageBox.Show("Registro Eliminado con exito");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }    
            else
                MessageBox.Show("Para eliminar escriba un nombre"); txtNombreProducto.Focus();

        }
    }
}
