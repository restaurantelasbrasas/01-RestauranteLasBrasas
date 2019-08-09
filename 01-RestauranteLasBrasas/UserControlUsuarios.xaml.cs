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
    /// Lógica de interacción para UserControlUsuarios.xaml
    /// </summary>
    public partial class UserControlUsuarios : UserControl
    {
        DataClasses1DataContext data;
        public UserControlUsuarios()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings["_01_RestauranteLasBrasas.Properties.Settings.BD_RestauranteLasBrasasConnectionString"].ConnectionString;

            data = new DataClasses1DataContext(connectionString);
            var usuario = from u in data.GetTable<Usuario>()
                          select new { u.IdUsuario, u.IdEmpleado, u.Usuario1, u.TipoUsuario,u.Contraseña };
            dgUsuario.ItemsSource = data.Usuario;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }

        private void BtnAgregar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtContraseña.Text != "" && txtUsuario.Text != "" && txtNivel.Text != "")
                {
                    Usuario usu = new Usuario();
                    usu.Usuario1 = txtUsuario.Text;
                    usu.Contraseña = txtContraseña.Text;
                    usu.TipoUsuario = txtNivel.Text;
                    usu.IdEmpleado = Convert.ToInt32(txtdEmpleado.Text);

                    data.Usuario.InsertOnSubmit(usu);
                    data.SubmitChanges();

                    dgUsuario.ItemsSource = data.Usuario;
                    MessageBox.Show("Usuario Creado con exito");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsuario.Text != "")
            {
                var cliente = (from emp in data.Usuario
                               where emp.Usuario1 == txtUsuario.Text
                               select emp).First();
                //var empleado = data.Empleado.First(emp => emp.nombre.Equals(txtNombre.Text));
                if (cliente != null)
                {
                    var eliminar = from elim in data.Usuario
                                   where elim.Usuario1.Equals(txtUsuario.Text)
                                   select elim;
                    foreach (var cliente1 in eliminar)
                    {
                        data.Usuario.DeleteOnSubmit(cliente1);
                    }
                    try
                    {
                        data.SubmitChanges();
                        MessageBox.Show("Registro eliminado con exito");
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                    MessageBox.Show("Para eliminar escriba el nombre de usuario"); txtUsuario.Focus();
            }
            else
                MessageBox.Show("No existe registo con ese nombre");
        }
    }
}
