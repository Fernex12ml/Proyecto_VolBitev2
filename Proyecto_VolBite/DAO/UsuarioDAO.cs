using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_VolBite.Clases;
using Proyecto_VolBite.Conexion;

namespace Proyecto_VolBite.DAO
{
    public class UsuarioDAO
    {
        private Conexion.Conexion conexion = new Conexion.Conexion();

        public Usuario Login(string correo, string contraseña)
        {
            Usuario usuario = null;

            try
            {
                SqlConnection con = conexion.Abrir();

                string query = "SELECT * FROM Usuario WHERE correo=@correo AND contraseña=@contraseña";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@contraseña", contraseña);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario = new Usuario();

                            usuario.Id = Convert.ToInt32(dr["id_usuario"]);
                            usuario.Nombre = dr["nombre"].ToString();
                            usuario.Correo = dr["correo"].ToString();
                            usuario.Contraseña = dr["contraseña"].ToString();
                            usuario.Rol = dr["rol"].ToString();

                            // Guardar datos en la sesión global para la bienvenida en los menús
                            Sesion.Nombre = usuario.Nombre;
                            Sesion.Rol = usuario.Rol;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.Cerrar();
            }

            return usuario;
        }

        public bool RegistrarUsuario(Usuario usuario)
        {
            try
            {
                SqlConnection con = conexion.Abrir();
                string query = "INSERT INTO Usuario (nombre, correo, contraseña, rol) VALUES (@Nombre, @Correo, @Contrasena, @Rol)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("@Contrasena", usuario.Contraseña);
                    cmd.Parameters.AddWithValue("@Rol", usuario.Rol);

                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}