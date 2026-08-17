using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Proyecto_VolBite.Clases;
using Proyecto_VolBite.Conexion;

namespace Proyecto_VolBite.DAO
{
    public class PedidoDAO
    {
        private Conexion.Conexion conexionObj = new Conexion.Conexion();

        // 1. REGISTRAR UN NUEVO PEDIDO
        public int RegistrarPedido(Pedido pedido)
        {
            int idGenerado = 0;
            try
            {
                SqlConnection con = conexionObj.Abrir();
                // Incluimos tipo_pago en la inserción
                string query = @"INSERT INTO Pedido (id_usuario, fecha, total, tiempo_estimado, estado, tipo_pago) 
                                 VALUES (@id_usuario, @fecha, @total, @tiempo_estimado, 'En Preparación', @tipo_pago);
                                 SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id_usuario", pedido.IdUsuario);
                    cmd.Parameters.AddWithValue("@fecha", pedido.Fecha);
                    cmd.Parameters.AddWithValue("@total", pedido.Total);
                    cmd.Parameters.AddWithValue("@tiempo_estimado", pedido.TiempoEstimado);
                    cmd.Parameters.AddWithValue("@tipo_pago", string.IsNullOrEmpty(pedido.TipoPago) ? "Presencial (Pago en Caja)" : pedido.TipoPago);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        idGenerado = Convert.ToInt32(result);
                    }
                }
                conexionObj.Cerrar();
            }
            catch (Exception ex)
            {
                conexionObj.Cerrar();
                MessageBox.Show("Error al registrar pedido: " + ex.Message, "Error DAO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return idGenerado;
        }

        // 2. OBTENER TODOS LOS PEDIDOS 
        public DataTable ObtenerTodosLosPedidos()
        {
            DataTable tabla = new DataTable();
            try
            {
                SqlConnection con = conexionObj.Abrir();

                string query = @"SELECT 
                                    p.id_pedido AS 'ID', 
                                    p.fecha AS 'Fecha', 
                                    u.nombre AS 'Cliente', 
                                    p.total AS 'Total ($)', 
                                    ISNULL(p.tipo_pago, 'Presencial') AS 'Tipo de Pago', 
                                    p.tiempo_estimado AS 'Tiempo (Min)', 
                                    ISNULL(p.estado, 'En Preparación') AS 'Estado'
                                 FROM Pedido p
                                 INNER JOIN Usuario u ON p.id_usuario = u.id_usuario
                                 ORDER BY p.id_pedido DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
                conexionObj.Cerrar();
            }
            catch (Exception ex)
            {
                conexionObj.Cerrar();
                MessageBox.Show("Error al consultar pedidos: " + ex.Message, "Error DAO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tabla;
        }

        // 3. OBTENER PEDIDOS POR USUARIO
        public DataTable ObtenerPedidosPorUsuario(int idUsuario)
        {
            DataTable tabla = new DataTable();
            try
            {
                SqlConnection con = conexionObj.Abrir();
                string query = @"SELECT 
                                    p.id_pedido AS 'N° Pedido', 
                                    p.fecha AS 'Fecha', 
                                    p.total AS 'Total ($)', 
                                    ISNULL(p.tipo_pago, 'Presencial') AS 'Pago', 
                                    p.tiempo_estimado AS 'Tiempo (min)', 
                                    ISNULL(p.estado, 'En Preparación') AS 'Estado'
                                 FROM Pedido p
                                 WHERE p.id_usuario = @id_usuario
                                 ORDER BY p.id_pedido DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
                conexionObj.Cerrar();
            }
            catch (Exception ex)
            {
                conexionObj.Cerrar();
                MessageBox.Show("Error al consultar tus pedidos: " + ex.Message, "Error DAO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tabla;
        }

        // 4. ACTUALIZAR ESTADO DE UN PEDIDO
        public bool ActualizarEstado(int idPedido, string nuevoEstado)
        {
            try
            {
                SqlConnection con = conexionObj.Abrir();
                string query = "UPDATE Pedido SET estado = @estado WHERE id_pedido = @id_pedido";

                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@id_pedido", idPedido);
                    filasAfectadas = cmd.ExecuteNonQuery();
                }

                conexionObj.Cerrar();
                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                conexionObj.Cerrar();
                MessageBox.Show("Error al actualizar estado: " + ex.Message, "Error DAO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // 5. ELIMINAR PEDIDO Y SUS DETALLES
        public bool EliminarPedido(int idPedido)
        {
            try
            {
                SqlConnection con = conexionObj.Abrir();

                string queryDetalle = "DELETE FROM Detalle_Pedido WHERE id_pedido = @id_pedido";
                using (SqlCommand cmdDet = new SqlCommand(queryDetalle, con))
                {
                    cmdDet.Parameters.AddWithValue("@id_pedido", idPedido);
                    cmdDet.ExecuteNonQuery();
                }

                string queryPedido = "DELETE FROM Pedido WHERE id_pedido = @id_pedido";
                int filasAfectadas = 0;
                using (SqlCommand cmdPed = new SqlCommand(queryPedido, con))
                {
                    cmdPed.Parameters.AddWithValue("@id_pedido", idPedido);
                    filasAfectadas = cmdPed.ExecuteNonQuery();
                }

                conexionObj.Cerrar();
                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                conexionObj.Cerrar();
                MessageBox.Show("Error al eliminar pedido: " + ex.Message, "Error DAO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}