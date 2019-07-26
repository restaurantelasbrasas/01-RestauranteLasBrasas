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

namespace _01_RestauranteLasBrasas
{
    /// <summary>
    /// Lógica de interacción para UserControlEmpleados.xaml
    /// </summary>
    public partial class UserControlEmpleados : UserControl 

    {
        Clase_Conectar conexion = new Clase_Conectar();
        public UserControlEmpleados()
        {
            InitializeComponent();
        }

        
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conexion.AbrirConexion();
                if (conexion.Estado == 1)
                {
                    string query = string.Format("MantenimientoEmpleados");
                    SqlCommand command = new SqlCommand(query, conexion.Conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adaptador = new SqlDataAdapter(command);

                    using (adaptador)
                    {
                        command.Parameters.AddWithValue("@IDN", Identidad.Text);
                        command.Parameters.AddWithValue("@Apellidos", Apellido.Text);
                        command.Parameters.AddWithValue("@Nombres", Nombre.Text);
                        command.Parameters.AddWithValue("@Sexo", cbSexo.SelectedValue);
                        command.Parameters.AddWithValue("@FechaNac", Fecha);
                        command.Parameters.AddWithValue("@Direccion", Direccion);
                        command.Parameters.AddWithValue("@EstadoCivil", EstadoCivil.SelectedValue);
                        command.Parameters.AddWithValue("@IdCargo", Cargo.SelectedValue);
                    }
                }
                MessageBox.Show("Registro Guardado Correctamente");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
