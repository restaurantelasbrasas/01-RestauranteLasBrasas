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
    /// Lógica de interacción para UserControlClientes.xaml
    /// </summary>
    public partial class UserControlClientes : UserControl
    {
        private DataClasses1DataContext data;

        public UserControlClientes()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings["_01_RestauranteLasBrasas.Properties.Settings.BD_RestauranteLasBrasasConnectionString"].ConnectionString;

            data = new DataClasses1DataContext(connectionString);
            var cliente = from u in data.GetTable<Cliente>()
                           select new { u.IdCliente, u.Identidad, u.Nombre, u.Apellido, u.Direccion, u.Sexo, u.Telefono, };
            dgCliente.ItemsSource = cliente.ToList();
        }
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }

        private void BtnGestionar_Click(object sender, RoutedEventArgs e)
        {
            WindowGestionarClientes win2 = new WindowGestionarClientes();
            win2.Show();
            
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdentidad.Text != "")
            {
                data = new DataClasses1DataContext();
                var cliente = from u in data.GetTable<Cliente>()
                               where u.Identidad.Equals(txtIdentidad.Text)
                               select new { u.IdCliente, u.Identidad, u.Nombre, u.Apellido, u.Direccion, u.Sexo, u.Telefono };
                if (cliente == null)
                { MessageBox.Show("no existe"); }//
                dgCliente.ItemsSource = cliente.ToList();
            }
            else
                MessageBox.Show("Ingrese un numero de identidad"); txtIdentidad.Focus();

        }

        private void BtnBuscarTodos_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
