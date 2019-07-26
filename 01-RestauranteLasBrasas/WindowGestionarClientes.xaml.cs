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
    /// Lógica de interacción para WindowGestionarClientes.xaml
    /// </summary>
    public partial class WindowGestionarClientes : Window
    {
        Clase_Conectar conexion = new Clase_Conectar();
        public WindowGestionarClientes()
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
                    string query = string.Format("MantenimientoDelCliente");
                    SqlCommand command = new SqlCommand(query, conexion.Conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adaptador = new SqlDataAdapter(command);

                    using (adaptador)
                    {
                        command.Parameters.AddWithValue("@identidad", txtIdentidadCliente.Text);
                        command.Parameters.AddWithValue("@apellido", txtApellidoCliente.Text);
                        command.Parameters.AddWithValue("@nombre", txtNombreCliente.Text);
                        command.Parameters.AddWithValue("@sexo", txtSexoCliente);//esto necesita revision
                        command.Parameters.AddWithValue("@direccion", txtDireccionCliente.Text);
                        command.Parameters.AddWithValue("@telefono", txtTelefonoCliente);
                    }
                    MessageBox.Show("Registro agregado");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
        }
    }
}
