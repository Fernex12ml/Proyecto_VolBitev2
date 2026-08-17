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
    public partial class FrmLoginCliente : Form
    {
        private Guna.UI2.WinForms.Guna2TextBox txtNewCorreo;
        private Guna.UI2.WinForms.Guna2TextBox txtNewPass;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblErrorInline;

        public FrmLoginCliente()
        {
            InitializeComponent();

            // 1. Ocultar controles del diseñador anterior
            foreach (Control c in this.Controls)
            {
                c.Visible = false;
            }

            // 2. Configurar Formulario Principal
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(780, 470);
            this.BackColor = Color.FromArgb(24, 24, 24);

            Guna.UI2.WinForms.Guna2Elipse elipseForm = new Guna.UI2.WinForms.Guna2Elipse();
            elipseForm.BorderRadius = 16;
            elipseForm.TargetControl = this;

            // =========================================================================
            // LADO IZQUIERDO: SECCIÓN NARANJA DE MARCA
            // =========================================================================
            Guna.UI2.WinForms.Guna2Panel pnlIzquierdo = new Guna.UI2.WinForms.Guna2Panel();
            pnlIzquierdo.Size = new Size(300, 470);
            pnlIzquierdo.Location = new Point(0, 0);
            pnlIzquierdo.FillColor = Color.FromArgb(255, 128, 0);
            this.Controls.Add(pnlIzquierdo);

            // LOGO PRINCIPAL VOLTBITE
            PictureBox picLogoVoltBite = new PictureBox();
            picLogoVoltBite.Size = new Size(240, 240);
            picLogoVoltBite.Location = new Point(30, 25);
            picLogoVoltBite.SizeMode = PictureBoxSizeMode.Zoom;
            picLogoVoltBite.BackColor = Color.Transparent;
            if (Properties.Resources.logo_voltbite != null)
            {
                picLogoVoltBite.Image = Properties.Resources.logo_voltbite;
            }
            pnlIzquierdo.Controls.Add(picLogoVoltBite);

            // LOGO UTS INSTITUCIONAL
            PictureBox picUtsFondo = new PictureBox();
            picUtsFondo.Size = new Size(240, 100);
            picUtsFondo.Location = new Point(30, 320);
            picUtsFondo.SizeMode = PictureBoxSizeMode.Zoom;
            picUtsFondo.BackColor = Color.Transparent;
            if (Properties.Resources.logo_uts != null)
            {
                picUtsFondo.Image = Properties.Resources.logo_uts;
            }
            pnlIzquierdo.Controls.Add(picUtsFondo);

            // =========================================================================
            // LADO DERECHO: FORMULARIO UNIFICADO
            // =========================================================================
            Guna.UI2.WinForms.Guna2Panel pnlDerecho = new Guna.UI2.WinForms.Guna2Panel();
            pnlDerecho.Size = new Size(480, 470);
            pnlDerecho.Location = new Point(300, 0);
            pnlDerecho.FillColor = Color.FromArgb(24, 24, 24);
            this.Controls.Add(pnlDerecho);

            // CERRAR (X)
            Guna.UI2.WinForms.Guna2ControlBox btnCerrar = new Guna.UI2.WinForms.Guna2ControlBox();
            btnCerrar.Location = new Point(430, 10);
            btnCerrar.Size = new Size(35, 35);
            btnCerrar.FillColor = Color.Transparent;
            btnCerrar.IconColor = Color.Gray;
            btnCerrar.HoverState.IconColor = Color.White;
            btnCerrar.Click += (s, e) => Application.Exit();
            pnlDerecho.Controls.Add(btnCerrar);

            // TÍTULOS
            Guna.UI2.WinForms.Guna2HtmlLabel lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTitle.Text = "INICIAR SESIÓN";
            lblTitle.ForeColor = Color.White;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.Location = new Point(50, 35);
            pnlDerecho.Controls.Add(lblTitle);

            Guna.UI2.WinForms.Guna2HtmlLabel lblSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSubtitle.Text = "Acceso a Cafetería VoltBite";
            lblSubtitle.ForeColor = Color.Gray;
            lblSubtitle.BackColor = Color.Transparent;
            lblSubtitle.Font = new Font("Segoe UI", 9.5f);
            lblSubtitle.Location = new Point(52, 70);
            pnlDerecho.Controls.Add(lblSubtitle);

            // CORREO
            txtNewCorreo = new Guna.UI2.WinForms.Guna2TextBox();
            txtNewCorreo.PlaceholderText = "Correo electrónico";
            txtNewCorreo.Size = new Size(370, 38);
            txtNewCorreo.Location = new Point(50, 120);
            txtNewCorreo.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            txtNewCorreo.FillColor = Color.FromArgb(24, 24, 24);
            txtNewCorreo.BorderColor = Color.FromArgb(70, 70, 70);
            txtNewCorreo.FocusedState.BorderColor = Color.FromArgb(255, 128, 0);
            txtNewCorreo.ForeColor = Color.White;
            txtNewCorreo.Font = new Font("Segoe UI", 10);
            txtNewCorreo.TextChanged += (s, e) => LimpiarEstadoError();
            pnlDerecho.Controls.Add(txtNewCorreo);

            // CONTRASEÑA
            txtNewPass = new Guna.UI2.WinForms.Guna2TextBox();
            txtNewPass.PlaceholderText = "Contraseña";
            txtNewPass.PasswordChar = '●';
            txtNewPass.Size = new Size(370, 38);
            txtNewPass.Location = new Point(50, 175);
            txtNewPass.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            txtNewPass.FillColor = Color.FromArgb(24, 24, 24);
            txtNewPass.BorderColor = Color.FromArgb(70, 70, 70);
            txtNewPass.FocusedState.BorderColor = Color.FromArgb(255, 128, 0);
            txtNewPass.ForeColor = Color.White;
            txtNewPass.Font = new Font("Segoe UI", 10);
            txtNewPass.TextChanged += (s, e) => LimpiarEstadoError();

            if (Properties.Resources.ojo_cerrado != null)
            {
                txtNewPass.IconRight = Properties.Resources.ojo_cerrado;
            }

            txtNewPass.IconRightCursor = Cursors.Hand;
            txtNewPass.IconRightClick += (s, e) =>
            {
                if (txtNewPass.PasswordChar == '●')
                {
                    txtNewPass.PasswordChar = '\0';
                    if (Properties.Resources.ojo_abierto != null)
                        txtNewPass.IconRight = Properties.Resources.ojo_abierto;
                }
                else
                {
                    txtNewPass.PasswordChar = '●';
                    if (Properties.Resources.ojo_cerrado != null)
                        txtNewPass.IconRight = Properties.Resources.ojo_cerrado;
                }
            };
            pnlDerecho.Controls.Add(txtNewPass);

            // ETIQUETA DE ERROR INLINE (Ubicada exactamente debajo de la línea y arriba del botón)
            lblErrorInline = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblErrorInline.Text = "";
            lblErrorInline.ForeColor = Color.FromArgb(255, 80, 80);
            lblErrorInline.BackColor = Color.Transparent;
            lblErrorInline.Font = new Font("Segoe UI", 8.5f, FontStyle.Regular);
            lblErrorInline.Location = new Point(52, 245);
            lblErrorInline.Visible = false;
            pnlDerecho.Controls.Add(lblErrorInline);

            // BOTÓN ENTRAR (Ajustado a Y = 265 para dar un espacio holgado)
            Guna.UI2.WinForms.Guna2Button btnNewEntrar = new Guna.UI2.WinForms.Guna2Button();
            btnNewEntrar.Text = "ENTRAR";
            btnNewEntrar.Size = new Size(370, 42);
            btnNewEntrar.Location = new Point(50, 265);
            btnNewEntrar.BorderRadius = 8;
            btnNewEntrar.FillColor = Color.FromArgb(255, 128, 0);
            btnNewEntrar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnNewEntrar.Click += new EventHandler(this.btnEntrar_Click);
            pnlDerecho.Controls.Add(btnNewEntrar);

            // BOTÓN REGISTRO
            Guna.UI2.WinForms.Guna2Button btnIrRegistro = new Guna.UI2.WinForms.Guna2Button();
            btnIrRegistro.Text = "¿No tienes cuenta? Regístrate aquí";
            btnIrRegistro.Size = new Size(370, 38);
            btnIrRegistro.Location = new Point(50, 325);
            btnIrRegistro.BorderRadius = 8;
            btnIrRegistro.FillColor = Color.Transparent;
            btnIrRegistro.BorderColor = Color.FromArgb(255, 128, 0);
            btnIrRegistro.BorderThickness = 1;
            btnIrRegistro.ForeColor = Color.FromArgb(255, 128, 0);
            btnIrRegistro.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnIrRegistro.Click += (s, e) => {
                FrmRegistro reg = new FrmRegistro();
                reg.Show();
                this.Hide();
            };
            pnlDerecho.Controls.Add(btnIrRegistro);

            // MOVER VENTANA CON EL MOUSE
            Guna.UI2.WinForms.Guna2DragControl dragForm = new Guna.UI2.WinForms.Guna2DragControl();
            dragForm.TargetControl = pnlDerecho;

            // ENTER PARA INICIAR SESIÓN
            this.AcceptButton = btnNewEntrar;
        }

        private void MostrarErrorInline(string mensaje)
        {
            lblErrorInline.Text = "⚠️ " + mensaje;
            lblErrorInline.Visible = true;
            lblErrorInline.BringToFront();
        }

        private void LimpiarEstadoError()
        {
            // Restablecer colores estándar de Guna2TextBox
            txtNewCorreo.BorderColor = Color.FromArgb(70, 70, 70);
            txtNewCorreo.FocusedState.BorderColor = Color.FromArgb(255, 128, 0);

            txtNewPass.BorderColor = Color.FromArgb(70, 70, 70);
            txtNewPass.FocusedState.BorderColor = Color.FromArgb(255, 128, 0);

            lblErrorInline.Visible = false;
        }

        private void MarcaCampoError(Guna.UI2.WinForms.Guna2TextBox txt)
        {
            txt.BorderColor = Color.Red;
            txt.FocusedState.BorderColor = Color.Red;
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            LimpiarEstadoError();

            string correo = txtNewCorreo != null ? txtNewCorreo.Text.Trim() : "";
            string pass = txtNewPass != null ? txtNewPass.Text.Trim() : "";

            // 1. Ambas cajas vacías
            if (string.IsNullOrWhiteSpace(correo) && string.IsNullOrWhiteSpace(pass))
            {
                MarcaCampoError(txtNewCorreo);
                MarcaCampoError(txtNewPass);
                MostrarErrorInline("Ingresa tu correo y tu contraseña.");
                txtNewCorreo.Focus();
                return;
            }

            // 2. Correo vacío
            if (string.IsNullOrWhiteSpace(correo))
            {
                MarcaCampoError(txtNewCorreo);
                MostrarErrorInline("Por favor, ingresa tu correo electrónico.");
                txtNewCorreo.Focus();
                return;
            }

            // 3. Contraseña vacía
            if (string.IsNullOrWhiteSpace(pass))
            {
                MarcaCampoError(txtNewPass);
                MostrarErrorInline("Por favor, ingresa tu contraseña.");
                txtNewPass.Focus();
                return;
            }

            // Validar en la Base de Datos
            UsuarioDAO dao = new UsuarioDAO();
            Usuario usuario = dao.Login(correo, pass);

            if (usuario == null)
            {
                MarcaCampoError(txtNewCorreo);
                MarcaCampoError(txtNewPass);
                MostrarErrorInline("Correo o contraseña incorrectos.");
                return;
            }

            // Iniciar Sesión exitoso
            Sesion.IdUsuario = usuario.Id;
            Sesion.Nombre = usuario.Nombre;
            Sesion.Rol = usuario.Rol;

            string rol = usuario.Rol != null ? usuario.Rol.Trim().ToLower() : "";

            if (rol == "empleado" || rol == "admin" || rol == "administrador")
            {
                FrmMenuAdmin adminMenu = new FrmMenuAdmin();
                adminMenu.Show();
                this.Hide();
            }
            else if (rol == "cliente" || rol == "alumno" || rol == "alumna" || rol == "maestro" || rol == "maestra")
            {
                FrmMenuCliente clienteMenu = new FrmMenuCliente();
                clienteMenu.Show();
                this.Hide();
            }
            else
            {
                MostrarErrorInline("El rol asignado a este usuario no es válido.");
            }
        }

        private void FrmLoginCliente_Load(object sender, EventArgs e) { }
        private void btnAdministrador_Click(object sender, EventArgs e) { }
        private void btnVolver_Click(object sender, EventArgs e) { }
    }
}