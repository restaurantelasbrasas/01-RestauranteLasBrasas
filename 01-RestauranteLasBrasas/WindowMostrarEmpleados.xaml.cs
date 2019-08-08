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
using System.Configuration;

namespace _01_RestauranteLasBrasas
{
    /// <summary>
    /// Lógica de interacción para WindowMostrarEmpleados.xaml
    /// </summary>
    public partial class WindowMostrarEmpleados : Window
    {
        private DataClasses1DataContext data;        
        public WindowMostrarEmpleados()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["_01_RestauranteLasBrasas.Properties.Settings.BD_RestauranteLasBrasasConnectionString"].ConnectionString;

            data = new DataClasses1DataContext(connectionString);
            MostrarEmpleados();
        }        

        private void MostrarEmpleados()
        {
            try
            {
                data = new DataClasses1DataContext();
                var empleado = from u in data.Empleado
                               join c in data.Cargo on u.IdCargo equals c.IdCargo
                               select new { u.IdEmpleado, u.Identidad, u.Nombre, u.Apellido, u.Direccion, u.FechaNac, u.EstadoCivil, u.Sexo, c.Descripcion };
                dgEmpleado.ItemsSource = empleado.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdentidad.Text != "")
            {
                data = new DataClasses1DataContext();
                var empleado = from u in data.Empleado
                               join c in data.Cargo on u.IdCargo equals c.IdCargo
                               where u.Identidad.Contains(txtIdentidad.Text)
                              select new { u.IdEmpleado, u.Identidad, u.Nombre, u.Apellido, u.Direccion, u.FechaNac, u.EstadoCivil, u.Sexo, c.Descripcion };
                if (empleado == null)
                { MessageBox.Show("no existe"); }//
                dgEmpleado.ItemsSource = empleado.ToList();
            }
            else
                MessageBox.Show("Ingrese un numero de identidad"); txtIdentidad.Focus();
        }
        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtIdentidad.Clear();
            MostrarEmpleados();
        }
    }
}
