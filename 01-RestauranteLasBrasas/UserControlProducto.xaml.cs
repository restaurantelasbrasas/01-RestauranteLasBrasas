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
using System.Configuration;


namespace _01_RestauranteLasBrasas
{
    /// <summary>
    /// Lógica de interacción para UserControlProducto.xaml
    /// </summary>
    public partial class UserControlProducto : UserControl
    {
        private DataClasses1DataContext data;
        public UserControlProducto()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["_01_RestauranteLasBrasas.Properties.Settings.BD_RestauranteLasBrasasConnectionString"].ConnectionString;
            data = new DataClasses1DataContext(connectionString);
            var producto = from u in data.GetTable<Producto>()
                           select new { u.IdProducto, u.IdCategoria, u.Nombre, u.Marca, u.PrecioVenta, u.FechaVencimiento, u.Stock };
            dgProducto.ItemsSource = producto.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text != "")
            {
                data = new DataClasses1DataContext();
                var producto = from u in data.GetTable<Producto>()
                               where u.Nombre.Equals(txtNombre.Text)
                               select new { u.IdProducto, u.IdCategoria, u.Nombre, u.Marca, u.PrecioVenta, u.FechaVencimiento, u.Stock };
               
                if (producto == null)
                {
                    MessageBox.Show("no existe");
                }
                dgProducto.ItemsSource = producto.ToList();
            }
            else
                MessageBox.Show("Ingrese un Nombre"); txtNombre.Focus();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowGestionarProducto win = new WindowGestionarProducto();
            win.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }
    }
}
