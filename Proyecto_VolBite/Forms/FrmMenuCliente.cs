using System;
using System.Drawing;
using System.Windows.Forms;
using Proyecto_VolBite.Clases;

namespace Proyecto_VolBite.Forms
{
    public partial class FrmMenuCliente : Form
    {
        public FrmMenuCliente()
        {
            InitializeComponent();
            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {
            // Configuración de la Ventana Principal
            this.Text = "Menu Cliente";
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(520, 560);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Card Central
            Panel card = new Panel();
            card.Size = new Size(420, 480);
            card.Location = new Point((this.ClientSize.Width - card.Width) / 2, (this.ClientSize.Height - card.Height) / 2);
            card.BackColor = Color.FromArgb(28, 28, 28);
            this.Controls.Add(card);

            // Título Principal
            Label lblTitulo = new Label();
            lblTitulo.Text = "MENÚ CLIENTE";
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(255, 128, 0);
            lblTitulo.Size = new Size(400, 35);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            lblTitulo.Location = new Point(10, 20);
            card.Controls.Add(lblTitulo);

            // Saludo personalizado con Sesion.Nombre
            Label lblBienvenida = new Label();
            string nombreCliente = string.IsNullOrEmpty(Sesion.Nombre) ? "Alumno" : Sesion.Nombre;
            lblBienvenida.Text = "¡Bienvenido " + nombreCliente + "!";
            lblBienvenida.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblBienvenida.ForeColor = Color.White;
            lblBienvenida.Size = new Size(400, 25);
            lblBienvenida.TextAlign = ContentAlignment.MiddleCenter;
            lblBienvenida.Location = new Point(10, 60);
            card.Controls.Add(lblBienvenida);

            // BOTÓN 1: Ver Productos
            Button btnProductos = new Button();
            btnProductos.Text = "🍔 Ver Productos";
            btnProductos.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnProductos.ForeColor = Color.White;
            btnProductos.BackColor = Color.FromArgb(255, 128, 0);
            btnProductos.FlatStyle = FlatStyle.Flat;
            btnProductos.FlatAppearance.BorderSize = 0;
            btnProductos.Size = new Size(320, 45);
            btnProductos.Location = new Point(50, 115);
            btnProductos.Cursor = Cursors.Hand;
            btnProductos.Click += (s, ev) =>
            {
                FrmCatalogoProductos frmProds = new FrmCatalogoProductos();
                frmProds.ShowDialog();
            };
            card.Controls.Add(btnProductos);

            // BOTÓN 2: Mi Carrito (Abre la ventana directamente)
            Button btnCarrito = new Button();
            btnCarrito.Text = "🛒 Mi Carrito";
            btnCarrito.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnCarrito.ForeColor = Color.White;
            btnCarrito.BackColor = Color.FromArgb(45, 45, 45);
            btnCarrito.FlatStyle = FlatStyle.Flat;
            btnCarrito.FlatAppearance.BorderSize = 0;
            btnCarrito.Size = new Size(320, 45);
            btnCarrito.Location = new Point(50, 180);
            btnCarrito.Cursor = Cursors.Hand;
            btnCarrito.Click += (s, ev) =>
            {
                FrmCarrito frmCarrito = new FrmCarrito();
                frmCarrito.ShowDialog();
            };
            card.Controls.Add(btnCarrito);

            // BOTÓN 3: Mis Pedidos
            Button btnMisPedidos = new Button();
            btnMisPedidos.Text = "📦 Mis Pedidos";
            btnMisPedidos.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnMisPedidos.ForeColor = Color.White;
            btnMisPedidos.BackColor = Color.FromArgb(45, 45, 45);
            btnMisPedidos.FlatStyle = FlatStyle.Flat;
            btnMisPedidos.FlatAppearance.BorderSize = 0;
            btnMisPedidos.Size = new Size(320, 45);
            btnMisPedidos.Location = new Point(50, 245);
            btnMisPedidos.Cursor = Cursors.Hand;
            btnMisPedidos.Click += (s, ev) =>
            {
                FrmMisPedidos frmMisPedidos = new FrmMisPedidos();
                frmMisPedidos.ShowDialog();
            };
            card.Controls.Add(btnMisPedidos);

            // BOTÓN 4: Cerrar Sesión
            Button btnCerrarSesion = new Button();
            btnCerrarSesion.Text = "⬅ Cerrar Sesión";
            btnCerrarSesion.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnCerrarSesion.ForeColor = Color.White;
            btnCerrarSesion.BackColor = Color.FromArgb(180, 40, 40);
            btnCerrarSesion.FlatStyle = FlatStyle.Flat;
            btnCerrarSesion.FlatAppearance.BorderSize = 0;
            btnCerrarSesion.Size = new Size(320, 45);
            btnCerrarSesion.Location = new Point(50, 380);
            btnCerrarSesion.Cursor = Cursors.Hand;
            btnCerrarSesion.Click += (s, ev) =>
            {
                // 1. Limpiamos las variables de la sesión
                Sesion.IdUsuario = 0;
                Sesion.Nombre = null;
                Sesion.Rol = null;

                // 2. Buscamos el formulario de Login que estaba oculto
                FrmLoginCliente login = (FrmLoginCliente)Application.OpenForms["FrmLoginCliente"];

                if (login != null)
                {
                    login.Show();
                }
                else
                {
                    FrmLoginCliente nuevoLogin = new FrmLoginCliente();
                    nuevoLogin.Show();
                }

                // 3. Cerramos este menú de cliente
                this.Close();
            };
            card.Controls.Add(btnCerrarSesion);
        }
    }
}