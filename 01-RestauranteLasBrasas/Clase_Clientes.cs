using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace _01_RestauranteLasBrasas
{
    class Clase_Clientes
    {
        Clase_Conectar conectar = new Clase_Conectar();
        public DataTable MostrarClientes()
        {
            SqlDataAdapter da = new SqlDataAdapter("ListaCliente",conectar.conexion);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
