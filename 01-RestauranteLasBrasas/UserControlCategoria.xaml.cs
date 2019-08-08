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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _01_RestauranteLasBrasas
{
    /// <summary>
    /// Lógica de interacción para UserControlCategoria.xaml
    /// </summary>
    public partial class UserControlCategoria : UserControl
    {
        DataClasses1DataContext data;
        public UserControlCategoria()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["_01_RestauranteLasBrasas.Properties.Settings.BD_RestauranteLasBrasasConnectionString"].ConnectionString;

            data = new DataClasses1DataContext(connectionString);
            var empleado = from u in data.GetTable<Empleado>()
                           select new { u.IdEmpleado, u.Identidad, u.Nombre, u.Apellido, u.Direccion, u.Sexo, u.Usuario, u.FechaNac, u.EstadoCivil };
            dgEmpleado.ItemsSource = empleado.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtIdEmpleado.Text != " ")
            {
                data = new DataClasses1DataContext();
                var empleado = from u in data.GetTable<Empleado>()
                               where u.Identidad.Equals(txtIdEmpleado.Text)
                               select new { u.IdEmpleado, u.Identidad, u.Nombre, u.Apellido, u.Direccion, u.Sexo, u.Usuario, u.FechaNac, u.EstadoCivil };
                if (empleado == null)
                {
                    MessageBox.Show("no existe");
                }
                dgEmpleado.ItemsSource = empleado.ToList();
            }
        }
    }
}
