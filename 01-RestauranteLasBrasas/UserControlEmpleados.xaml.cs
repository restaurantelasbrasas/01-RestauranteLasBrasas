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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace _01_RestauranteLasBrasas
{
    /// <summary>
    /// Lógica de interacción para UserControlEmpleados.xaml
    /// </summary>
    public partial class UserControlEmpleados : UserControl 

    {
        private DataClasses1DataContext data;
        public UserControlEmpleados()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["_01_RestauranteLasBrasas.Properties.Settings.BD_RestauranteLasBrasasConnectionString"].ConnectionString;

            data = new DataClasses1DataContext(connectionString);
            var empleado = from u in data.GetTable<Empleado>()
                          select new { u.IdEmpleado, u.Identidad, u.Nombre, u.Apellido, u.Direccion, u.Sexo, u.Usuario , u.FechaNac, u.EstadoCivil};
        }

        
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Empleado emp = new Empleado();
                emp.Identidad = txtIdentidad.Text;
                emp.Nombre = txtNombre.Text;
                emp.Apellido = txtApellido.Text;
                emp.Direccion = txtDireccion.Text;
                emp.FechaNac = Convert.ToDateTime(dtFecha.Text);
                emp.Sexo = Convert.ToChar(cbSexo);
                emp.IdCargo = Convert.ToInt32(txtCargo.Text);
                emp.EstadoCivil = Convert.ToChar(cbEstadoCivil);

                data.Empleado.InsertOnSubmit(emp);
                data.SubmitChanges();

                MessageBox.Show("REGISTRO GUARDADO CORRECTAMENTE");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnVer_Click(object sender, RoutedEventArgs e)
        {
            WindowMostrarEmpleados mostrarEmpleados = new WindowMostrarEmpleados();
            mostrarEmpleados.Show();
        }
    }
}
