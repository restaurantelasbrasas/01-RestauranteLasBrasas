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
        //Intancia de la Clase DataClasses de LINQ
        private DataClasses1DataContext data;

        
        public UserControlEmpleados()
        {
            InitializeComponent();

            //Creamos la conexion con connectionString  para poder acceder a la base de datos.
            string connectionString = ConfigurationManager.ConnectionStrings["_01_RestauranteLasBrasas.Properties.Settings.BD_RestauranteLasBrasasConnectionString"].ConnectionString;

            //a la intancia le asignamos la conexion con la base de datos 
            data = new DataClasses1DataContext(connectionString);

            //codificamo la variable por la cual accederemos a la rabla dentro de la base de datos
            //Junto con todos los campos de la tabla de la base de datos
            var empleado = from u in data.GetTable<Empleado>()
                          select new { u.IdEmpleado, u.Identidad, u.Nombre, u.Apellido, u.Direccion, u.Sexo, u.Usuario , u.FechaNac, u.EstadoCivil};

        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Cerramos el Usercontrol
            (this.Parent as Grid).Children.Remove(this);
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                // Tomando el objeto generado por el datacontext
                // Le ingresamos los valores para que puedan ser ingresado en la base de datos
                Empleado emp = new Empleado();
                emp.Identidad = txtIdentidad.Text;
                emp.Nombre = txtNombre.Text;
                emp.Apellido = txtApellido.Text;
                emp.Direccion = txtDireccion.Text;
                emp.FechaNac = Convert.ToDateTime(dtFecha.Text);
                emp.Sexo = cbSexo.Text;
                emp.IdCargo = Convert.ToInt32(txtCargo.Text);
                emp.EstadoCivil = cbEstadoCivil.Text;

                // Inserta los datos directamente en la base de datos
                data.Empleado.InsertOnSubmit(emp);
                data.SubmitChanges();

                // Muestra un mensaje de que los resgistros han sido guardados
                MessageBox.Show("REGISTRO SE A GUARDADO CORRECTAMENTE");

            }
            catch (Exception ex)
            {
                // Si ocurrre algun error el catch nos lo notifica
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnVer_Click(object sender, RoutedEventArgs e)
        {
            // Empleamos la condicion para poder borrar los registros
            if (txtIdentidad.Text != "")
            {
                //le asignamos los paramerametros a LINQ para que pueda realizar la tarea
                var empleado = (from emp in data.Empleado
                                where emp.Identidad == txtIdentidad.Text
                                select emp).First();
                //var empleado = data.Empleado.First(emp => emp.nombre.Equals(txtNombre.Text));

                // Si el nombre del empleado es diferente de NULL entonces nos permitira eliminar
                if (empleado != null)
                {
                    var eliminar = from elim in data.Empleado
                                   //donde la identidad que se le asigna corrsponda a un registro dentro de la base de datos 
                                   where elim.Identidad.Equals(txtIdentidad.Text)
                                   select elim;
                    foreach (var detalles in eliminar)
                    {
                        // elimina el registro
                        data.Empleado.DeleteOnSubmit(detalles);
                    }
                    try
                    {
                        // realiza la eliminacion
                        data.SubmitChanges();
                        //notifica que ha sido eliminado el registro
                        MessageBox.Show("Registro eliminado con exito");
                    }
                    catch (Exception ex)
                    {
                        // Si ocurrre algun error el catch nos lo notifica
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                    // si el usuario da clic en el boton si que este escrito nada en el textbox nos notifica el error 
                    MessageBox.Show("Para eliminar escriba un numero de identidad"); txtIdentidad.Focus();
            }
            else
                // de no existir la identidad ingresada en la base de datos nos notifica 
                MessageBox.Show("No existe registo con ese nombre");

        }
    }
}
