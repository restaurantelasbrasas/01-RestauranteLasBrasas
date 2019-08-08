using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace _01_RestauranteLasBrasas
{
    class Clase_Conectar
    {
        public SqlConnection conexion;
        private int estado;

        public SqlConnection Conexion
        {
            get { return conexion; }
            set { conexion = value; }
        }
        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public Clase_Conectar()
        {
            conexion = new SqlConnection();
            estado = 0;

        }

        public SqlConnection AbrirConexion()
        {
            try
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                    Estado = 0;
                }
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.ConnectionString = string.Format(@"server(local)\SQLEXPRESS; database =BD_RestauranteLasBrasas;integrated security = true;");
                    conexion.Open();
                    Estado = 1;
                }
                return conexion;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Estado = 0;
                return conexion;
            }
        }
    }
}
