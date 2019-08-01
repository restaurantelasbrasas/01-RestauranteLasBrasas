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

namespace _01_RestauranteLasBrasas
{
    /// <summary>
    /// Lógica de interacción para UserControlClientes.xaml
    /// </summary>
    public partial class UserControlClientes : UserControl
    {
        Clase_Clientes clientes = new Clase_Clientes();

        public UserControlClientes()
        {
            InitializeComponent();
        }
        
        public void UserControlCliente_Load(object sender, EventArgs e)
        {
            ListarEmpleado.DataContext = clientes.MostrarClientes();
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

        private void ListarEmpleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
