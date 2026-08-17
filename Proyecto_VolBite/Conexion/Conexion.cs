using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Proyecto_VolBite.Conexion
{
    public class Conexion
    {

        private SqlConnection conectar = new SqlConnection(
            @"Server=localhost;
        Database=Proyecto_Integrador;
        User Id=sa;
        Password=15080715;
        TrustServerCertificate=True;"
        );

        public SqlConnection Abrir()
        {
            if (conectar.State == System.Data.ConnectionState.Closed)
                conectar.Open();

            return conectar;
        }

        public SqlConnection Cerrar()
        {
            if (conectar.State == System.Data.ConnectionState.Open)
                conectar.Close();

            return conectar;
        }
    }
}
