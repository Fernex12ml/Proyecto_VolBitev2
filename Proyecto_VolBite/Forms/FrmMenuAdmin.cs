using System;
using System.Drawing;
using System.Windows.Forms;
using Proyecto_VolBite.Clases;

namespace Proyecto_VolBite.Forms
{
    public partial class FrmMenuAdmin : Form
    {
        public FrmMenuAdmin()
        {
            InitializeComponent();
            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {
            // Configuración de la Ventana Principal
            this.Text = "Menu Administrador";
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(700, 520);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Tarjeta / Card Central
            Panel card = new Panel();
            card.Size = new Size(380, 460);
            card.Location = new Point((this.ClientSize.Width - card.Width) / 2, (this.ClientSize.Height - card.Height) / 2);
            card.BackColor = Color.FromArgb(28, 28, 28);
            this.Controls.Add(card);

            // Título Principal
            Label lblTitulo = new Label();
            lblTitulo.Text = "PANEL ADMINISTRADOR";
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Size = new Size(360, 35);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            lblTitulo.Location = new Point(10, 30);
            card.Controls.Add(lblTitulo);

            // Saludo / Bienvenida
            Label lblBienvenida = new Label();
            lblBienvenida.Text = "Bienvenido, Administrador";
            lblBienvenida.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblBienvenida.ForeColor = Color.FromArgb(255, 140, 0);
            lblBienvenida.Size = new Size(360, 25);
            lblBienvenida.TextAlign = ContentAlignment.MiddleCenter;
            lblBienvenida.Location = new Point(10, 70);
            card.Controls.Add(lblBienvenida);

            // BOTÓN 1: Administrar Pedidos
            Button btnPedidos = new Button();
            btnPedidos.Text = "Administrar Pedidos";
            btnPedidos.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnPedidos.ForeColor = Color.White;
            btnPedidos.BackColor = Color.FromArgb(255, 120, 0);
            btnPedidos.FlatStyle = FlatStyle.Flat;
            btnPedidos.FlatAppearance.BorderSize = 0;
            btnPedidos.Size = new Size(300, 45);
            btnPedidos.Location = new Point(40, 150);
            btnPedidos.Cursor = Cursors.Hand;
            btnPedidos.Click += (s, ev) =>
            {
                this.Hide();
                FrmAdminPedidos frmPedidos = new FrmAdminPedidos();
                frmPedidos.ShowDialog();
                this.Show();
            };
            card.Controls.Add(btnPedidos);

            // BOTÓN 2: Administrar Productos
            Button btnProductos = new Button();
            btnProductos.Text = "Administrar Productos";
            btnProductos.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnProductos.ForeColor = Color.White;
            btnProductos.BackColor = Color.FromArgb(255, 120, 0);
            btnProductos.FlatStyle = FlatStyle.Flat;
            btnProductos.FlatAppearance.BorderSize = 0;
            btnProductos.Size = new Size(300, 45);
            btnProductos.Location = new Point(40, 215);
            btnProductos.Cursor = Cursors.Hand;
            btnProductos.Click += (s, ev) =>
            {
                this.Hide();
                FrmProductos frmProds = new FrmProductos();
                frmProds.ShowDialog();
                this.Show();
            };
            card.Controls.Add(btnProductos);

            // BOTÓN 3: Cerrar Sesión (EXTERMINA VENTANAS TRASERAS OCULTAS)
            Button btnCerrarSesion = new Button();
            btnCerrarSesion.Text = "Cerrar Sesión";
            btnCerrarSesion.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnCerrarSesion.ForeColor = Color.FromArgb(255, 140, 0);
            btnCerrarSesion.BackColor = Color.Transparent;
            btnCerrarSesion.FlatStyle = FlatStyle.Flat;
            btnCerrarSesion.FlatAppearance.BorderColor = Color.FromArgb(255, 140, 0);
            btnCerrarSesion.FlatAppearance.BorderSize = 1;
            btnCerrarSesion.Size = new Size(300, 45);
            btnCerrarSesion.Location = new Point(40, 340);
            btnCerrarSesion.Cursor = Cursors.Hand;
            btnCerrarSesion.Click += (s, ev) =>
            {
                // 1. Limpiar variables de sesión
                Sesion.IdUsuario = 0;
                Sesion.Nombre = null;
                Sesion.Rol = null;

                // 2. Ocultar el menú de inmediato
                this.Hide();

                // 3. Buscar y ocultar TODAS las ventanas que se hayan quedado abiertas por detrás
                for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    Form formOculto = Application.OpenForms[i];
                    if (formOculto != this)
                    {
                        formOculto.Hide();
                    }
                }

                // 4. Abrir una sola ventana limpia del Login UTS
                FrmLoginCliente loginUTS = new FrmLoginCliente();
                loginUTS.Show();

                // 5. Destruir este menú de admin
                this.Close();
            };
            card.Controls.Add(btnCerrarSesion);
        }
    }
}