using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace _01_RestauranteLasBrasas
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        DataClasses1DataContext data;
        public Login()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings["_01_RestauranteLasBrasas.Properties.Settings.BD_RestauranteLasBrasasConnectionString"].ConnectionString;

            data = new DataClasses1DataContext(connectionString);

        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            
            if (MessageBox.Show("Realmente desea salir?", "Consulta", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
            else
            {
                //No hace nada
            }

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuAdmin admon = new MenuAdmin();
            MenuEmpleado emple = new MenuEmpleado();

            if (txtUsuario.Text == "admin" && txtContrasena.Password == "admin")
            {
                MessageBox.Show("Usted a ingresado como adminstrador","Bienvenido!");
                admon.Show();
                this.Close();

            }
            else if (txtUsuario.Text == "emple" && txtContrasena.Password == "emple")
            {
                MessageBox.Show("Usted a ingresado como empleado", "Bienvenido!");
                emple.Show();
                this.Close();
            }
             else
            {
                MessageBox.Show("Usuario o contraseña invalida", "Ingreso fallido");
            }
        }
    }
}
