using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Proyecto_VolBite.Conexion;

namespace Proyecto_VolBite.DAO
{
    public class DetallePedidoDAO
    {
        private Conexion.Conexion conexion = new Conexion.Conexion();

        public void Agregar(
            int pedido,
            int producto,
            int cantidad,
            decimal subtotal)
        {
            try
            {
                SqlConnection con = conexion.Abrir();

                string query = @"INSERT INTO Detalle_Pedido (cantidad, subtotal, id_pedido, id_producto)
                                 VALUES (@cantidad, @subtotal, @pedido, @producto)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@subtotal", subtotal);
                    cmd.Parameters.AddWithValue("@pedido", pedido);
                    cmd.Parameters.AddWithValue("@producto", producto);

                    cmd.ExecuteNonQuery();
                }

                conexion.Cerrar();
            }
            catch (Exception ex)
            {
                conexion.Cerrar();
                throw new Exception("Error al insertar detalle del pedido: " + ex.Message);
            }
        }

        // MÉTODO NUEVO: Obtener los productos que pertenecen a un pedido
        public DataTable ObtenerDetallePorPedido(int idPedido)
        {
            DataTable tabla = new DataTable();
            try
            {
                SqlConnection con = conexion.Abrir();

                // Hacemos INNER JOIN con Producto (o Productos si tu tabla lleva S)
                string query = @"SELECT 
                                    p.nombre AS 'Producto', 
                                    dp.cantidad AS 'Cant.', 
                                    dp.subtotal AS 'Subtotal ($)'
                                 FROM Detalle_Pedido dp
                                 INNER JOIN Producto p ON dp.id_producto = p.id_producto
                                 WHERE dp.id_pedido = @id_pedido";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id_pedido", idPedido);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }

                conexion.Cerrar();
            }
            catch (Exception ex)
            {
                conexion.Cerrar();
                MessageBox.Show("Error al obtener el detalle del pedido: " + ex.Message, "Error DAO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tabla;
        }
    }
}