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
    public partial class FrmLoginAdmin : Form
    {
        public FrmLoginAdmin()
        {
            InitializeComponent();

            // 1. Ocultar elementos antiguos
            this.txtCorreo.Visible = false;
            this.txtPassword.Visible = false;

            string[] labelsToHide = { "lblCorreo", "lblPassword", "lblContrasena", "label1", "label2" };
            foreach (var name in labelsToHide)
            {
                if (this.Controls.ContainsKey(name)) this.Controls[name].Visible = false;
            }

            if (this.Controls.ContainsKey("btnEntrar")) this.Controls["btnEntrar"].Visible = false;
            if (this.Controls.ContainsKey("btnCliente")) this.Controls["btnCliente"].Visible = false;
            if (this.Controls.ContainsKey("btnVolver")) this.Controls["btnVolver"].Visible = false;

            foreach (Control c in this.Controls)
            {
                if (c is Label && (c.Text.Contains("Contraseña") || c.Text.Contains("Correo")))
                {
                    c.Visible = false;
                }
            }

            foreach (Control c in this.Controls)
            {
                if (c is Button && (c.Text == "Entrar" || c.Text == "Soy Cliente" || c.Text == "Regresar"))
                {
                    c.Visible = false;
                }
            }

            // 2. Fondo del Formulario
            this.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);

            // 3. Crear Tarjeta Central
            Guna.UI2.WinForms.Guna2Panel card = new Guna.UI2.WinForms.Guna2Panel();
            card.Size = new System.Drawing.Size(340, 410);
            card.FillColor = System.Drawing.Color.FromArgb(28, 28, 28);
            card.BorderRadius = 16;
            card.Location = new System.Drawing.Point((this.ClientSize.Width - card.Width) / 2, (this.ClientSize.Height - card.Height) / 2);
            this.Controls.Add(card);

            // 4. Título Principal
            Label lblTitle = new Label();
            lblTitle.Text = "ADMINISTRADOR";
            lblTitle.ForeColor = System.Drawing.Color.White;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new System.Drawing.Point(85, 30);
            card.Controls.Add(lblTitle);

            // 5. Input Correo Moderno
            Guna.UI2.WinForms.Guna2TextBox txtNewCorreo = new Guna.UI2.WinForms.Guna2TextBox();
            txtNewCorreo.PlaceholderText = "Correo electrónico";
            txtNewCorreo.Size = new System.Drawing.Size(260, 42);
            txtNewCorreo.Location = new System.Drawing.Point(40, 85);
            txtNewCorreo.BorderRadius = 8;
            txtNewCorreo.FillColor = System.Drawing.Color.FromArgb(40, 40, 40);
            txtNewCorreo.BorderColor = System.Drawing.Color.FromArgb(255, 128, 0);
            txtNewCorreo.ForeColor = System.Drawing.Color.White;
            txtNewCorreo.Font = new System.Drawing.Font("Segoe UI", 10);
            txtNewCorreo.TextChanged += (s, e) => { this.txtCorreo.Text = txtNewCorreo.Text; };
            card.Controls.Add(txtNewCorreo);

            // 6. Input Contraseña Moderno
            Guna.UI2.WinForms.Guna2TextBox txtNewPass = new Guna.UI2.WinForms.Guna2TextBox();
            txtNewPass.PlaceholderText = "Contraseña";
            txtNewPass.PasswordChar = '●';
            txtNewPass.Size = new System.Drawing.Size(260, 42);
            txtNewPass.Location = new System.Drawing.Point(40, 140);
            txtNewPass.BorderRadius = 8;
            txtNewPass.FillColor = System.Drawing.Color.FromArgb(40, 40, 40);
            txtNewPass.BorderColor = System.Drawing.Color.FromArgb(255, 128, 0);
            txtNewPass.ForeColor = System.Drawing.Color.White;
            txtNewPass.Font = new System.Drawing.Font("Segoe UI", 10);
            txtNewPass.TextChanged += (s, e) => { this.txtPassword.Text = txtNewPass.Text; };
            card.Controls.Add(txtNewPass);

            // 7. Botón Entrar (Naranja)
            Guna.UI2.WinForms.Guna2Button btnNewEntrar = new Guna.UI2.WinForms.Guna2Button();
            btnNewEntrar.Text = "Entrar";
            btnNewEntrar.Size = new System.Drawing.Size(260, 42);
            btnNewEntrar.Location = new System.Drawing.Point(40, 215);
            btnNewEntrar.BorderRadius = 8;
            btnNewEntrar.FillColor = System.Drawing.Color.FromArgb(255, 128, 0);
            btnNewEntrar.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            btnNewEntrar.Click += (s, e) => { btnEntrar_Click(s, e); };
            card.Controls.Add(btnNewEntrar);

            // 8. Botón "Cliente"
            Guna.UI2.WinForms.Guna2Button btnNewCliente = new Guna.UI2.WinForms.Guna2Button();
            btnNewCliente.Text = "Cliente";
            btnNewCliente.Size = new System.Drawing.Size(260, 36);
            btnNewCliente.Location = new System.Drawing.Point(40, 265);
            btnNewCliente.BorderRadius = 8;
            btnNewCliente.FillColor = System.Drawing.Color.Transparent;
            btnNewCliente.BorderColor = System.Drawing.Color.FromArgb(255, 128, 0);
            btnNewCliente.BorderThickness = 1;
            btnNewCliente.ForeColor = System.Drawing.Color.FromArgb(255, 128, 0);
            btnNewCliente.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            btnNewCliente.Click += (sender_cli, args_cli) =>
            {
                FrmLoginCliente cliente = new FrmLoginCliente();
                cliente.Show();
                this.Close();
            };
            card.Controls.Add(btnNewCliente);

            // 9. Botón "Inicio"
            Guna.UI2.WinForms.Guna2Button btnNewInicio = new Guna.UI2.WinForms.Guna2Button();
            btnNewInicio.Text = "Inicio";
            btnNewInicio.Size = new System.Drawing.Size(260, 36);
            btnNewInicio.Location = new System.Drawing.Point(40, 310);
            btnNewInicio.BorderRadius = 8;
            btnNewInicio.FillColor = System.Drawing.Color.Transparent;
            btnNewInicio.ForeColor = System.Drawing.Color.Gray;
            btnNewInicio.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            btnNewInicio.HoverState.FillColor = System.Drawing.Color.FromArgb(40, 40, 40);
            btnNewInicio.HoverState.ForeColor = System.Drawing.Color.White;

            btnNewInicio.Click += (sender_ini, args_ini) =>
            {
                FrmInicio inicio = new FrmInicio();
                inicio.Show();
                this.Close();
            };

            card.Controls.Add(btnNewInicio);
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            FrmLoginCliente frm = new FrmLoginCliente();
            frm.Show();
            this.Close();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            FrmInicio frm = new FrmInicio();
            frm.Show();
            this.Close();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (txtCorreo.Text == "")
            {
                MessageBox.Show("Ingrese un correo");
                return;
            }

            if (txtPassword.Text == "")
            {
                MessageBox.Show("Ingrese una contraseña");
                return;
            }

            UsuarioDAO dao = new UsuarioDAO();

            Usuario usuario = dao.Login(
                txtCorreo.Text,
                txtPassword.Text);

            if (usuario == null)
            {
                MessageBox.Show("Usuario incorrecto");
                return;
            }

            if (usuario.Rol != "Administrador")
            {
                MessageBox.Show("Este usuario no es administrador");
                return;
            }

            Sesion.IdUsuario = usuario.Id;
            Sesion.Nombre = usuario.Nombre;
            Sesion.Rol = usuario.Rol;

            // Abrimos el menú y cerramos este login para que no quede vivo de fondo
            FrmMenuAdmin frm = new FrmMenuAdmin();
            frm.Show();

            this.Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
        }
    }
}