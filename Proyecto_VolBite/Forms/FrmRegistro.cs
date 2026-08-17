using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_VolBite.DAO;
using Proyecto_VolBite.Clases;

namespace Proyecto_VolBite.Forms
{
    public partial class FrmRegistro : Form
    {
        private Guna.UI2.WinForms.Guna2TextBox txtRegNombre;
        private Guna.UI2.WinForms.Guna2TextBox txtRegCorreo;
        private Guna.UI2.WinForms.Guna2TextBox txtRegPass;
        private Guna.UI2.WinForms.Guna2ComboBox cboRegRol;

        public FrmRegistro()
        {
            InitializeComponent();
            ConstruirInterfazOscura();
        }

        private void ConstruirInterfazOscura()
        {
            this.Controls.Clear();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(800, 580);
            this.BackColor = Color.FromArgb(18, 18, 18);

            Guna.UI2.WinForms.Guna2Elipse elipseForm = new Guna.UI2.WinForms.Guna2Elipse();
            elipseForm.BorderRadius = 16;
            elipseForm.TargetControl = this;

            // Panel Central
            Guna.UI2.WinForms.Guna2Panel pnlCentro = new Guna.UI2.WinForms.Guna2Panel();
            pnlCentro.Size = new Size(400, 510);
            pnlCentro.Location = new Point(200, 35);
            pnlCentro.FillColor = Color.FromArgb(28, 28, 28);
            pnlCentro.BorderRadius = 14;
            this.Controls.Add(pnlCentro);

            // Botón X
            Guna.UI2.WinForms.Guna2ControlBox btnCerrar = new Guna.UI2.WinForms.Guna2ControlBox();
            btnCerrar.Location = new Point(750, 10);
            btnCerrar.Size = new Size(35, 35);
            btnCerrar.FillColor = Color.Transparent;
            btnCerrar.IconColor = Color.Gray;
            btnCerrar.HoverState.IconColor = Color.White;
            btnCerrar.Click += (s, e) => Application.Exit();
            this.Controls.Add(btnCerrar);

            // TÍTULO
            Label lblTitle = new Label();
            lblTitle.Text = "REGISTRO";
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(135, 20);
            lblTitle.BackColor = Color.Transparent;
            pnlCentro.Controls.Add(lblTitle);

            // 1. NOMBRE
            txtRegNombre = new Guna.UI2.WinForms.Guna2TextBox();
            txtRegNombre.PlaceholderText = "Nombre completo";
            txtRegNombre.Size = new Size(310, 36);
            txtRegNombre.Location = new Point(45, 65);
            txtRegNombre.BorderRadius = 8;
            txtRegNombre.FillColor = Color.FromArgb(38, 38, 38);
            txtRegNombre.BorderColor = Color.FromArgb(255, 128, 0);
            txtRegNombre.FocusedState.BorderColor = Color.FromArgb(255, 160, 50);
            txtRegNombre.ForeColor = Color.White;
            txtRegNombre.Font = new Font("Segoe UI", 10);
            pnlCentro.Controls.Add(txtRegNombre);

            // 2. CORREO
            txtRegCorreo = new Guna.UI2.WinForms.Guna2TextBox();
            txtRegCorreo.PlaceholderText = "Correo electrónico";
            txtRegCorreo.Size = new Size(310, 36);
            txtRegCorreo.Location = new Point(45, 115);
            txtRegCorreo.BorderRadius = 8;
            txtRegCorreo.FillColor = Color.FromArgb(38, 38, 38);
            txtRegCorreo.BorderColor = Color.FromArgb(255, 128, 0);
            txtRegCorreo.FocusedState.BorderColor = Color.FromArgb(255, 160, 50);
            txtRegCorreo.ForeColor = Color.White;
            txtRegCorreo.Font = new Font("Segoe UI", 10);
            pnlCentro.Controls.Add(txtRegCorreo);

            // 3. CONTRASEÑA
            txtRegPass = new Guna.UI2.WinForms.Guna2TextBox();
            txtRegPass.PlaceholderText = "Contraseña";
            txtRegPass.PasswordChar = '●';
            txtRegPass.Size = new Size(310, 36);
            txtRegPass.Location = new Point(45, 165);
            txtRegPass.BorderRadius = 8;
            txtRegPass.FillColor = Color.FromArgb(38, 38, 38);
            txtRegPass.BorderColor = Color.FromArgb(255, 128, 0);
            txtRegPass.FocusedState.BorderColor = Color.FromArgb(255, 160, 50);
            txtRegPass.ForeColor = Color.White;
            txtRegPass.Font = new Font("Segoe UI", 10);
            pnlCentro.Controls.Add(txtRegPass);

            // 4. ROL -> Solo 3 opciones requeridas: Empleado, Alumno y Maestro
            cboRegRol = new Guna.UI2.WinForms.Guna2ComboBox();
            cboRegRol.Size = new Size(310, 36);
            cboRegRol.Location = new Point(45, 245);
            cboRegRol.BorderRadius = 8;
            cboRegRol.FillColor = Color.FromArgb(38, 38, 38);
            cboRegRol.BorderColor = Color.FromArgb(255, 128, 0);
            cboRegRol.ForeColor = Color.White;
            cboRegRol.Font = new Font("Segoe UI", 10);
            cboRegRol.Items.Clear();
            cboRegRol.Items.AddRange(new object[] { "Empleado", "Alumno", "Maestro" });
            cboRegRol.SelectedIndex = 1; // Selecciona "Alumno" por defecto
            pnlCentro.Controls.Add(cboRegRol);

            // 5. BOTÓN REGISTRARSE
            Guna.UI2.WinForms.Guna2Button btnRegistrar = new Guna.UI2.WinForms.Guna2Button();
            btnRegistrar.Text = "Registrarse";
            btnRegistrar.Size = new Size(310, 42);
            btnRegistrar.Location = new Point(45, 300);
            btnRegistrar.BorderRadius = 8;
            btnRegistrar.FillColor = Color.FromArgb(255, 128, 0);
            btnRegistrar.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.White;
            btnRegistrar.Click += (s, e) => EjecutarRegistro();
            pnlCentro.Controls.Add(btnRegistrar);

            // 6. BOTÓN VOLVER
            Guna.UI2.WinForms.Guna2Button btnVolverUI = new Guna.UI2.WinForms.Guna2Button();
            btnVolverUI.Text = "Volver";
            btnVolverUI.Size = new Size(310, 38);
            btnVolverUI.Location = new Point(45, 360);
            btnVolverUI.BorderRadius = 8;
            btnVolverUI.FillColor = Color.Transparent;
            btnVolverUI.BorderColor = Color.FromArgb(255, 128, 0);
            btnVolverUI.BorderThickness = 1;
            btnVolverUI.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnVolverUI.ForeColor = Color.FromArgb(255, 128, 0);
            btnVolverUI.Click += (s, e) => {
                FrmLoginCliente login = new FrmLoginCliente();
                login.Show();
                this.Hide();
            };
            pnlCentro.Controls.Add(btnVolverUI);
        }

        private void EjecutarRegistro()
        {
            string nombre = txtRegNombre != null ? txtRegNombre.Text.Trim() : "";
            string correo = txtRegCorreo != null ? txtRegCorreo.Text.Trim() : "";
            string pass = txtRegPass != null ? txtRegPass.Text.Trim() : "";
            string rol = (cboRegRol != null && cboRegRol.SelectedItem != null)
                         ? cboRegRol.SelectedItem.ToString()
                         : "Alumno";

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Por favor complete todos los campos (incluyendo la contraseña).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Usuario nuevoUsuario = new Usuario
            {
                Nombre = nombre,
                Correo = correo,
                Rol = rol
            };

            var tipo = typeof(Usuario);
            var propPass = tipo.GetProperty("Contrasena")
                        ?? tipo.GetProperty("Password")
                        ?? tipo.GetProperty("Pass")
                        ?? tipo.GetProperty("Contraseña");

            if (propPass != null)
            {
                propPass.SetValue(nuevoUsuario, pass);
            }
            else
            {
                try { ((dynamic)nuevoUsuario).Contrasena = pass; } catch { }
                try { ((dynamic)nuevoUsuario).Password = pass; } catch { }
            }

            UsuarioDAO dao = new UsuarioDAO();
            bool exito = false;

            var metodoRegistrar = typeof(UsuarioDAO).GetMethod("Registrar")
                               ?? typeof(UsuarioDAO).GetMethod("RegistrarUsuario")
                               ?? typeof(UsuarioDAO).GetMethod("Insertar");

            if (metodoRegistrar != null)
            {
                try
                {
                    exito = (bool)metodoRegistrar.Invoke(dao, new object[] { nuevoUsuario });
                }
                catch (System.Reflection.TargetInvocationException ex)
                {
                    MessageBox.Show("Error en la base de datos: " + (ex.InnerException != null ? ex.InnerException.Message : ex.Message), "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (exito)
            {
                Usuario usuarioRegistrado = dao.Login(correo, pass);

                if (usuarioRegistrado != null)
                {
                    Sesion.IdUsuario = usuarioRegistrado.Id;
                    Sesion.Nombre = usuarioRegistrado.Nombre;
                    Sesion.Rol = usuarioRegistrado.Rol;

                    string rolNorm = usuarioRegistrado.Rol != null ? usuarioRegistrado.Rol.Trim().ToLower() : "";

                    // Ahora evalúa "empleado", "admin" o "administrador" para ir a la vista de administración
                    if (rolNorm == "empleado" || rolNorm == "admin" || rolNorm == "administrador")
                    {
                        FrmMenuAdmin adminMenu = new FrmMenuAdmin();
                        adminMenu.Show();
                    }
                    else
                    {
                        FrmMenuCliente clienteMenu = new FrmMenuCliente();
                        clienteMenu.Show();
                    }

                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("No se pudo registrar el usuario. Verifique sus datos o intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void btnGuardar_Click(object sender, EventArgs e) { EjecutarRegistro(); }
        public void btnRegistrarse_Click(object sender, EventArgs e) { EjecutarRegistro(); }
        public void btnVolver_Click(object sender, EventArgs e)
        {
            FrmLoginCliente login = new FrmLoginCliente();
            login.Show();
            this.Hide();
        }
        private void FrmRegistro_Load(object sender, EventArgs e) { }
    }
}