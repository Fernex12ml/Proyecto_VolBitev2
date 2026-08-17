using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Proyecto_VolBite.Clases;

namespace Proyecto_VolBite.Forms
{
    public partial class FrmEstadoPedido : Form
    {
        // Campos públicos para recibir la información desde FrmCompra
        public int IdPedido { get; set; } = 1;

        private int _tiempo = 30;
        public int Tiempo
        {
            get => _tiempo;
            set => _tiempo = (value <= 0) ? 30 : value; // Si llega 0 o negativo, asigna 30 min por defecto
        }

        // Importación de API de Windows para permitir mover la ventana sin bordes
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void MoverVentana()
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public FrmEstadoPedido()
        {
            InitializeComponent();
        }

        private void FrmEstadoPedido_Load(object sender, EventArgs e)
        {
            // Permitir arrastrar la ventana desde el fondo
            this.MouseDown += (s, ev) => { if (ev.Button == MouseButtons.Left) MoverVentana(); };

            // Configuración de la Ventana Principal
            this.BackColor = Color.FromArgb(18, 18, 18); // Negro ultra oscuro
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(500, 520);

            // Card / Tarjeta Central Flotante
            Panel card = new Panel();
            card.Size = new Size(420, 440);
            card.Location = new Point((this.ClientSize.Width - card.Width) / 2, (this.ClientSize.Height - card.Height) / 2);
            card.BackColor = Color.FromArgb(28, 28, 28);
            this.Controls.Add(card);
            card.BringToFront();
            card.MouseDown += (s, ev) => { if (ev.Button == MouseButtons.Left) MoverVentana(); };

            // Ícono de Éxito
            Label lblCheck = new Label();
            lblCheck.Text = "🎉";
            lblCheck.Font = new Font("Segoe UI Emoji", 32, FontStyle.Regular);
            lblCheck.Size = new Size(400, 55);
            lblCheck.TextAlign = ContentAlignment.MiddleCenter;
            lblCheck.Location = new Point(10, 20);
            lblCheck.MouseDown += (s, ev) => { if (ev.Button == MouseButtons.Left) MoverVentana(); };
            card.Controls.Add(lblCheck);

            // Reutilización de tus etiquetas del diseñador con mejor diseño:

            // 1. Título con Número de Pedido
            lblPedido.Parent = card;
            lblPedido.Text = "¡PEDIDO REGISTRADO!\n# " + IdPedido;
            lblPedido.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblPedido.ForeColor = Color.FromArgb(255, 128, 0); // Naranja VolBite
            lblPedido.Size = new Size(400, 55);
            lblPedido.TextAlign = ContentAlignment.MiddleCenter;
            lblPedido.Location = new Point(10, 80);
            lblPedido.MouseDown += (s, ev) => { if (ev.Button == MouseButtons.Left) MoverVentana(); };

            // Subtítulo
            Label lblEstatusHeader = new Label();
            lblEstatusHeader.Text = "Estado actual:";
            lblEstatusHeader.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblEstatusHeader.ForeColor = Color.Gray;
            lblEstatusHeader.Size = new Size(400, 20);
            lblEstatusHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblEstatusHeader.Location = new Point(10, 145);
            lblEstatusHeader.MouseDown += (s, ev) => { if (ev.Button == MouseButtons.Left) MoverVentana(); };
            card.Controls.Add(lblEstatusHeader);

            // 2. Estado del Pedido
            lblEstado.Parent = card;
            lblEstado.Text = "⏳ EN PREPARACIÓN";
            lblEstado.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblEstado.ForeColor = Color.White;
            lblEstado.Size = new Size(400, 30);
            lblEstado.TextAlign = ContentAlignment.MiddleCenter;
            lblEstado.Location = new Point(10, 168);
            lblEstado.MouseDown += (s, ev) => { if (ev.Button == MouseButtons.Left) MoverVentana(); };

            // Panel contenedor del tiempo
            Panel pnlTiempo = new Panel();
            pnlTiempo.Size = new Size(320, 80);
            pnlTiempo.Location = new Point(50, 220);
            pnlTiempo.BackColor = Color.FromArgb(20, 20, 20);
            pnlTiempo.BorderStyle = BorderStyle.FixedSingle;
            card.Controls.Add(pnlTiempo);

            Label lblTiempoTitulo = new Label();
            lblTiempoTitulo.Text = "Tiempo Estimado de Entrega";
            lblTiempoTitulo.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblTiempoTitulo.ForeColor = Color.DarkGray;
            lblTiempoTitulo.Size = new Size(310, 20);
            lblTiempoTitulo.TextAlign = ContentAlignment.MiddleCenter;
            lblTiempoTitulo.Location = new Point(5, 12);
            pnlTiempo.Controls.Add(lblTiempoTitulo);

            // 3. Tiempo
            lblTiempo.Parent = pnlTiempo;
            lblTiempo.Text = "⏱️ " + Tiempo + " minutos"; // Usa la variable asignada, no Carrito.TiempoEstimado()
            lblTiempo.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTiempo.ForeColor = Color.FromArgb(255, 128, 0);
            lblTiempo.Size = new Size(310, 32);
            lblTiempo.TextAlign = ContentAlignment.MiddleCenter;
            lblTiempo.Location = new Point(5, 36);

            // Botón Aceptar
            Button btnAceptar = new Button();
            btnAceptar.Text = "ENTENDIDO";
            btnAceptar.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnAceptar.ForeColor = Color.White;
            btnAceptar.BackColor = Color.FromArgb(255, 128, 0);
            btnAceptar.FlatStyle = FlatStyle.Flat;
            btnAceptar.FlatAppearance.BorderSize = 0;
            btnAceptar.Size = new Size(320, 45);
            btnAceptar.Location = new Point(50, 335);
            btnAceptar.Cursor = Cursors.Hand;
            btnAceptar.Click += (s, ev) => { this.Close(); };
            card.Controls.Add(btnAceptar);
        }
    }
}
