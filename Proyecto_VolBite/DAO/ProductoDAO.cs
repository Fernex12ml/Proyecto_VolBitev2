using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_VolBite.Clases;
using Proyecto_VolBite.Conexion;

namespace Proyecto_VolBite.DAO
{
    public class ProductoDAO
    {
        Conexion.Conexion conexion = new Conexion.Conexion();

        public void Agregar(Producto p)
        {
            SqlCommand cmd = new SqlCommand(
            @"INSERT INTO Producto
            (nombre, descripcion, precio, tiempo_preparacion, disponible, articulos_disponibles)
            VALUES
            (@nombre, @descripcion, @precio, @tiempo, @disponible, @stock)",
            conexion.Abrir());

            cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            cmd.Parameters.AddWithValue("@descripcion", p.Descripcion);
            cmd.Parameters.AddWithValue("@precio", p.Precio);
            cmd.Parameters.AddWithValue("@tiempo", p.TiempoPreparacion);
            cmd.Parameters.AddWithValue("@disponible", p.Disponible);
            cmd.Parameters.AddWithValue("@stock", p.Stock);

            cmd.ExecuteNonQuery();
            conexion.Cerrar();
        }

        public void Editar(Producto p)
        {
            SqlCommand cmd = new SqlCommand(
            @"UPDATE Producto SET
             nombre=@nombre,
             descripcion=@descripcion,
             precio=@precio,
             tiempo_preparacion=@tiempo,
             disponible=@disponible,
             articulos_disponibles=@stock
             WHERE id_producto=@id",
            conexion.Abrir());

            cmd.Parameters.AddWithValue("@id", p.Id);
            cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            cmd.Parameters.AddWithValue("@descripcion", p.Descripcion);
            cmd.Parameters.AddWithValue("@precio", p.Precio);
            cmd.Parameters.AddWithValue("@tiempo", p.TiempoPreparacion);
            cmd.Parameters.AddWithValue("@disponible", p.Disponible);
            cmd.Parameters.AddWithValue("@stock", p.Stock);

            cmd.ExecuteNonQuery();
            conexion.Cerrar();
        }

        public void Eliminar(int id)
        {
            // Borrado lógico: Marcamos disponible = 0 para que ya no aparezca en las listas
            SqlCommand cmd = new SqlCommand(
            "UPDATE Producto SET disponible = 0 WHERE id_producto=@id",
            conexion.Abrir());

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conexion.Cerrar();
        }

        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            // FILTRADO: Ahora solo trae los productos que NO han sido eliminados (disponible = 1)
            SqlCommand cmd = new SqlCommand(
            "SELECT * FROM Producto WHERE disponible = 1",
            conexion.Abrir());

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Producto p = new Producto();

                p.Id = Convert.ToInt32(dr["id_producto"]);
                p.Nombre = dr["nombre"] != DBNull.Value ? dr["nombre"].ToString() : "";
                p.Descripcion = dr["descripcion"] != DBNull.Value ? dr["descripcion"].ToString() : "";
                p.Precio = dr["precio"] != DBNull.Value ? Convert.ToDecimal(dr["precio"]) : 0;
                p.TiempoPreparacion = dr["tiempo_preparacion"] != DBNull.Value ? Convert.ToInt32(dr["tiempo_preparacion"]) : 0;
                p.Disponible = dr["disponible"] != DBNull.Value ? Convert.ToBoolean(dr["disponible"]) : false;
                p.Stock = dr["articulos_disponibles"] != DBNull.Value ? Convert.ToInt32(dr["articulos_disponibles"]) : 0;

                lista.Add(p);
            }

            dr.Close();
            conexion.Cerrar();

            return lista;
        }

        // --- MÉTODO PARA MOSTRAR SOLO PRODUCTOS ACTIVOS AL CLIENTE ---
        public List<Producto> ListarDisponibles()
        {
            List<Producto> lista = new List<Producto>();

            SqlCommand cmd = new SqlCommand(
            "SELECT * FROM Producto WHERE disponible = 1",
            conexion.Abrir());

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Producto p = new Producto();

                p.Id = Convert.ToInt32(dr["id_producto"]);
                p.Nombre = dr["nombre"] != DBNull.Value ? dr["nombre"].ToString() : "";
                p.Descripcion = dr["descripcion"] != DBNull.Value ? dr["descripcion"].ToString() : "";
                p.Precio = dr["precio"] != DBNull.Value ? Convert.ToDecimal(dr["precio"]) : 0;
                p.TiempoPreparacion = dr["tiempo_preparacion"] != DBNull.Value ? Convert.ToInt32(dr["tiempo_preparacion"]) : 0;
                p.Disponible = dr["disponible"] != DBNull.Value ? Convert.ToBoolean(dr["disponible"]) : false;
                p.Stock = dr["articulos_disponibles"] != DBNull.Value ? Convert.ToInt32(dr["articulos_disponibles"]) : 0;

                lista.Add(p);
            }

            dr.Close();
            conexion.Cerrar();

            return lista;
        }

        public List<Producto> Buscar(string nombre)
        {
            List<Producto> lista = new List<Producto>();

            // FILTRADO: Al buscar, también aseguramos que no traiga productos eliminados
            SqlCommand cmd = new SqlCommand(
            "SELECT * FROM Producto WHERE disponible = 1 AND nombre LIKE @nombre",
            conexion.Abrir());

            cmd.Parameters.AddWithValue("@nombre", "%" + nombre + "%");

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Producto p = new Producto();

                p.Id = Convert.ToInt32(dr["id_producto"]);
                p.Nombre = dr["nombre"] != DBNull.Value ? dr["nombre"].ToString() : "";
                p.Descripcion = dr["descripcion"] != DBNull.Value ? dr["descripcion"].ToString() : "";
                p.Precio = dr["precio"] != DBNull.Value ? Convert.ToDecimal(dr["precio"]) : 0;
                p.TiempoPreparacion = dr["tiempo_preparacion"] != DBNull.Value ? Convert.ToInt32(dr["tiempo_preparacion"]) : 0;
                p.Disponible = dr["disponible"] != DBNull.Value ? Convert.ToBoolean(dr["disponible"]) : false;
                p.Stock = dr["articulos_disponibles"] != DBNull.Value ? Convert.ToInt32(dr["articulos_disponibles"]) : 0;

                lista.Add(p);
            }

            dr.Close();
            conexion.Cerrar();

            return lista;
        }

        public void DescontarStock(int idProducto, int cantidad)
        {
            SqlCommand cmd = new SqlCommand(
            @"UPDATE Producto
            SET articulos_disponibles = articulos_disponibles - @cantidad
            WHERE id_producto=@id",
            conexion.Abrir());

            cmd.Parameters.AddWithValue("@cantidad", cantidad);
            cmd.Parameters.AddWithValue("@id", idProducto);

            cmd.ExecuteNonQuery();
            conexion.Cerrar();
        }

        public int ObtenerStock(int idProducto, SqlConnection cn, SqlTransaction tr)
        {
            SqlCommand cmd = new SqlCommand(
            @"SELECT articulos_disponibles
             FROM Producto
             WHERE id_producto=@id",
            cn, tr);

            cmd.Parameters.AddWithValue("@id", idProducto);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}