using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Proyecto_VolBite.Clases;
using Proyecto_VolBite.DAO;

namespace Proyecto_VolBite.Forms
{
    public partial class FrmCompra : Form
    {
        private string metodoPagoSeleccionado = "Presencial (Pago en Caja)"; // Opción por defecto
        private Panel panelEfectivo;
        private Panel panelTarjeta;
        private Panel panelCamposTarjeta;
        private TextBox txtNumeroTarjeta;
        private TextBox txtExpiracion;
        private TextBox txtCVV;
        private Button btnVolver;
        private Panel card;
        private bool procesandoCompra = false; // Prevención contra doble ejecución

        // Api de Windows para permitir mover la ventana sin bordes
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void MoverVentana()
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public FrmCompra()
        {
            InitializeComponent();
        }

        private void FrmCompra_Load(object sender, EventArgs e)
        {
            // Evento mover ventana desde la base
            this.MouseDown += (s, ev) => { if (ev.Button == MouseButtons.Left) MoverVentana(); };

            // Ventana Principal
            this.BackColor = Color.FromArgb(20, 20, 20);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(500, 520);

            // Card / Panel Principal
            card = new Panel();
            card.Size = new Size(420, 460);
            card.Location = new Point((this.ClientSize.Width - card.Width) / 2, (this.ClientSize.Height - card.Height) / 2);
            card.BackColor = Color.FromArgb(32, 32, 32);
            this.Controls.Add(card);
            card.BringToFront();
            card.MouseDown += (s, ev) => { if (ev.Button == MouseButtons.Left) MoverVentana(); };

            // Título
            Label lblTitulo = new Label();
            lblTitulo.Text = "RESUMEN DE COMPRA";
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(255, 128, 0);
            lblTitulo.Size = new Size(400, 35);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            lblTitulo.Location = new Point(10, 15);
            lblTitulo.MouseDown += (s, ev) => { if (ev.Button == MouseButtons.Left) MoverVentana(); };
            card.Controls.Add(lblTitulo);

            // Total
            lblTotal.Parent = card;
            lblTotal.Text = "Total a Pagar:\n$ " + Carrito.Total().ToString("0.00");
            lblTotal.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblTotal.ForeColor = Color.White;
            lblTotal.Size = new Size(400, 55);
            lblTotal.TextAlign = ContentAlignment.MiddleCenter;
            lblTotal.Location = new Point(10, 50);
            lblTotal.MouseDown += (s, ev) => { if (ev.Button == MouseButtons.Left) MoverVentana(); };

            // Tiempo Estimado
            lblTiempo.Parent = card;
            lblTiempo.Text = "⏱️ Tiempo estimado: " + Carrito.TiempoEstimado() + " min";
            lblTiempo.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblTiempo.ForeColor = Color.DarkGray;
            lblTiempo.Size = new Size(400, 25);
            lblTiempo.TextAlign = ContentAlignment.MiddleCenter;
            lblTiempo.Location = new Point(10, 110);
            lblTiempo.MouseDown += (s, ev) => { if (ev.Button == MouseButtons.Left) MoverVentana(); };

            // Etiqueta Método de Pago
            Label lblMetodo = new Label();
            lblMetodo.Text = "Selecciona Método de Pago:";
            lblMetodo.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblMetodo.ForeColor = Color.Orange;
            lblMetodo.Location = new Point(40, 145);
            lblMetodo.Size = new Size(340, 20);
            card.Controls.Add(lblMetodo);

            // --- BOTÓN / TARJETA EFECTIVO ---
            panelEfectivo = CrearOpcionPago("💵 Pago en Caja (Efectivo)", 170, true, card, (s, ev) => SeleccionarPago(true));

            // --- BOTÓN / TARJETA TARJETA ---
            panelTarjeta = CrearOpcionPago("💳 Tarjeta (Débito / Crédito)", 225, false, card, (s, ev) => SeleccionarPago(false));

            // --- PANEL DE CAMPOS DE TARJETA ---
            CrearCamposTarjeta();

            // Botón Pagar
            btnPagar.Parent = card;
            btnPagar.Text = "CONFIRMAR Y ORDENAR";
            btnPagar.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnPagar.ForeColor = Color.White;
            btnPagar.BackColor = Color.FromArgb(255, 128, 0);
            btnPagar.FlatStyle = FlatStyle.Flat;
            btnPagar.FlatAppearance.BorderSize = 0;
            btnPagar.Size = new Size(340, 45);
            btnPagar.Location = new Point(40, 295);
            btnPagar.Cursor = Cursors.Hand;
            btnPagar.Click -= new EventHandler(this.btnPagar_Click);
            btnPagar.Click += new EventHandler(this.btnPagar_Click);

            // Botón Volver
            btnVolver = new Button();
            btnVolver.Text = "← Volver al Carrito";
            btnVolver.Font = new Font("Segoe UI", 10, FontStyle.Underline);
            btnVolver.ForeColor = Color.FromArgb(255, 128, 0);
            btnVolver.BackColor = Color.Transparent;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.Size = new Size(340, 30);
            btnVolver.Location = new Point(40, 350);
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.Click += (s, ev) => { this.Close(); };
            card.Controls.Add(btnVolver);
        }

        private void CrearCamposTarjeta()
        {
            panelCamposTarjeta = new Panel();
            panelCamposTarjeta.Size = new Size(340, 115);
            panelCamposTarjeta.Location = new Point(40, 280);
            panelCamposTarjeta.BackColor = Color.FromArgb(25, 25, 25);
            panelCamposTarjeta.Visible = false;
            card.Controls.Add(panelCamposTarjeta);

            // Número de Tarjeta
            Label lblNum = new Label();
            lblNum.Text = "Número de Tarjeta (16 dígitos):";
            lblNum.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblNum.ForeColor = Color.White;
            lblNum.Location = new Point(10, 8);
            lblNum.AutoSize = true;
            panelCamposTarjeta.Controls.Add(lblNum);

            txtNumeroTarjeta = new TextBox();
            txtNumeroTarjeta.Location = new Point(10, 28);
            txtNumeroTarjeta.Size = new Size(320, 23);
            txtNumeroTarjeta.MaxLength = 16;
            txtNumeroTarjeta.BackColor = Color.FromArgb(45, 45, 45);
            txtNumeroTarjeta.ForeColor = Color.White;
            txtNumeroTarjeta.BorderStyle = BorderStyle.FixedSingle;
            panelCamposTarjeta.Controls.Add(txtNumeroTarjeta);

            // Expiración
            Label lblExp = new Label();
            lblExp.Text = "Vencimiento (MM/AA):";
            lblExp.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblExp.ForeColor = Color.White;
            lblExp.Location = new Point(10, 58);
            lblExp.AutoSize = true;
            panelCamposTarjeta.Controls.Add(lblExp);

            txtExpiracion = new TextBox();
            txtExpiracion.Location = new Point(10, 78);
            txtExpiracion.Size = new Size(140, 23);
            txtExpiracion.MaxLength = 5;
            txtExpiracion.BackColor = Color.FromArgb(45, 45, 45);
            txtExpiracion.ForeColor = Color.White;
            txtExpiracion.BorderStyle = BorderStyle.FixedSingle;

            // Auto-formato para colocar "/" al escribir 2 dígitos
            txtExpiracion.TextChanged += (s, ev) =>
            {
                if (txtExpiracion.Text.Length == 2 && !txtExpiracion.Text.Contains("/"))
                {
                    txtExpiracion.Text += "/";
                    txtExpiracion.SelectionStart = txtExpiracion.Text.Length;
                }
            };

            panelCamposTarjeta.Controls.Add(txtExpiracion);

            // CVV
            Label lblCvv = new Label();
            lblCvv.Text = "CVV (3 dígitos):";
            lblCvv.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblCvv.ForeColor = Color.White;
            lblCvv.Location = new Point(180, 58);
            lblCvv.AutoSize = true;
            panelCamposTarjeta.Controls.Add(lblCvv);

            txtCVV = new TextBox();
            txtCVV.Location = new Point(180, 78);
            txtCVV.Size = new Size(150, 23);
            txtCVV.MaxLength = 4;
            txtCVV.UseSystemPasswordChar = true;
            txtCVV.BackColor = Color.FromArgb(45, 45, 45);
            txtCVV.ForeColor = Color.White;
            txtCVV.BorderStyle = BorderStyle.FixedSingle;
            panelCamposTarjeta.Controls.Add(txtCVV);
        }

        private Panel CrearOpcionPago(string texto, int top, bool seleccionado, Control padre, EventHandler alHacerClic)
        {
            Panel pnl = new Panel();
            pnl.Size = new Size(340, 45);
            pnl.Location = new Point(40, top);
            pnl.Cursor = Cursors.Hand;
            pnl.Click += alHacerClic;

            Label lbl = new Label();
            lbl.Text = texto;
            lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lbl.Size = new Size(320, 25);
            lbl.Location = new Point(10, 10);
            lbl.Cursor = Cursors.Hand;
            lbl.Click += alHacerClic;
            pnl.Controls.Add(lbl);

            padre.Controls.Add(pnl);
            ActualizarEstiloOpcion(pnl, lbl, seleccionado);

            return pnl;
        }

        private void SeleccionarPago(bool esEfectivo)
        {
            if (esEfectivo)
            {
                metodoPagoSeleccionado = "Presencial (Pago en Caja)";
                ActualizarEstiloOpcion(panelEfectivo, (Label)panelEfectivo.Controls[0], true);
                ActualizarEstiloOpcion(panelTarjeta, (Label)panelTarjeta.Controls[0], false);

                panelCamposTarjeta.Visible = false;
                btnPagar.Location = new Point(40, 295);
                btnVolver.Location = new Point(40, 350);
                card.Size = new Size(420, 410);
                this.Size = new Size(500, 470);
            }
            else
            {
                metodoPagoSeleccionado = "Tarjeta (Crédito / Débito)";
                ActualizarEstiloOpcion(panelEfectivo, (Label)panelEfectivo.Controls[0], false);
                ActualizarEstiloOpcion(panelTarjeta, (Label)panelTarjeta.Controls[0], true);

                panelCamposTarjeta.Visible = true;
                btnPagar.Location = new Point(40, 410);
                btnVolver.Location = new Point(40, 465);
                card.Size = new Size(420, 515);
                this.Size = new Size(500, 575);
            }
        }

        private void ActualizarEstiloOpcion(Panel pnl, Label lbl, bool seleccionado)
        {
            if (seleccionado)
            {
                pnl.BackColor = Color.FromArgb(45, 45, 45);
                pnl.BorderStyle = BorderStyle.FixedSingle;
                lbl.ForeColor = Color.FromArgb(255, 128, 0);
            }
            else
            {
                pnl.BackColor = Color.FromArgb(25, 25, 25);
                pnl.BorderStyle = BorderStyle.None;
                lbl.ForeColor = Color.Gray;
            }
        }

        // LÓGICA DE REGISTRO EN LA BD Y APERTURA DE LA VENTANA DE ESTADO
        private void btnPagar_Click(object sender, EventArgs e)
        {
            // Si ya se procesó o está procesándose, se cierra directo sin alertas
            if (procesandoCompra)
            {
                this.Close();
                return;
            }

            if (Carrito.Productos == null || Carrito.Productos.Count == 0)
            {
                // Si el carrito está vacío pero venimos de completar la orden, salimos silenciosamente
                this.Close();
                return;
            }

            // Validar campos de tarjeta solo si la opción seleccionada es Tarjeta
            if (metodoPagoSeleccionado.Contains("Tarjeta"))
            {
                // 1. Validar Número de Tarjeta (debe ser exactamente de 16 dígitos numéricos)
                string tarjeta = txtNumeroTarjeta.Text.Trim();
                if (string.IsNullOrWhiteSpace(tarjeta) || tarjeta.Length != 16 || !long.TryParse(tarjeta, out _))
                {
                    MostrarNotificacion("Ingresa un número de tarjeta válido (16 dígitos numéricos).", isError: true);
                    txtNumeroTarjeta.Focus();
                    return;
                }

                // 2. Validar Fecha de Vencimiento (estricto MM/AA de 5 caracteres en total: 4 dígitos + 1 diagonal)
                string exp = txtExpiracion.Text.Trim();
                if (string.IsNullOrWhiteSpace(exp) || exp.Length != 5 || !exp.Contains("/"))
                {
                    MostrarNotificacion("Ingresa la fecha completa en formato MM/AA (4 dígitos, ej. 12/28).", isError: true);
                    txtExpiracion.Focus();
                    return;
                }

                string[] partesExp = exp.Split('/');
                if (partesExp.Length != 2 || partesExp[0].Length != 2 || partesExp[1].Length != 2 ||
                    !int.TryParse(partesExp[0], out int mes) || !int.TryParse(partesExp[1], out int anio))
                {
                    MostrarNotificacion("Formato de fecha inválido. Usa 4 dígitos MM/AA.", isError: true);
                    txtExpiracion.Focus();
                    return;
                }

                if (mes < 1 || mes > 12)
                {
                    MostrarNotificacion("El mes de vencimiento debe ser entre 01 y 12.", isError: true);
                    txtExpiracion.Focus();
                    return;
                }

                // 3. Validar CVV (mínimo 3 dígitos numéricos)
                string cvv = txtCVV.Text.Trim();
                if (string.IsNullOrWhiteSpace(cvv) || cvv.Length < 3 || !int.TryParse(cvv, out _))
                {
                    MostrarNotificacion("Ingresa un código CVV válido (3 o 4 dígitos).", isError: true);
                    txtCVV.Focus();
                    return;
                }
            }

            // Marcamos como iniciado e inhabilitamos el botón
            procesandoCompra = true;
            btnPagar.Enabled = false;

            try
            {
                // 1. Crear el objeto Pedido y asignarle la Fecha Actual
                Pedido pedido = new Pedido();
                pedido.Fecha = DateTime.Now;
                pedido.Total = Carrito.Total();
                pedido.TiempoEstimado = Carrito.TiempoEstimado();
                pedido.Estado = "Pendiente";
                pedido.TipoPago = metodoPagoSeleccionado;
                pedido.IdUsuario = Sesion.IdUsuario;

                PedidoDAO pedidoDAO = new PedidoDAO();
                int idPedidoGenerado = pedidoDAO.RegistrarPedido(pedido);

                if (idPedidoGenerado <= 0)
                {
                    MostrarNotificacion("No se pudo generar la orden en el sistema.", isError: true);
                    procesandoCompra = false;
                    btnPagar.Enabled = true;
                    return;
                }

                // 2. Insertar detalles del pedido y restar stock
                DetallePedidoDAO detalle = new DetallePedidoDAO();
                ProductoDAO producto = new ProductoDAO();

                foreach (CarritoItem item in Carrito.Productos)
                {
                    detalle.Agregar(idPedidoGenerado, item.IdProducto, item.Cantidad, item.Subtotal);
                    producto.DescontarStock(item.IdProducto, item.Cantidad);
                }

                int tiempoCalculado = Carrito.TiempoEstimado();

                // 3. Limpiar carrito
                Carrito.Vaciar();

                // 4. Abrir la pantalla de estado
                FrmEstadoPedido frm = new FrmEstadoPedido();
                frm.IdPedido = idPedidoGenerado;
                frm.Tiempo = tiempoCalculado;

                this.Hide();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MostrarNotificacion("Ocurrió un error: " + ex.Message, isError: true);
                procesandoCompra = false;
                btnPagar.Enabled = true;
            }
            finally
            {
                // Siempre aseguramos cerrar la ventana al finalizar el ciclo de compra
                this.Close();
            }
        }

        // --- NOTIFICACIÓN PERSONALIZADA VOLBITE ---
        private void MostrarNotificacion(string mensaje, bool isError = false)
        {
            Panel toast = new Panel();
            toast.Size = new Size(380, 42);
            toast.BackColor = isError ? Color.FromArgb(200, 35, 51) : Color.FromArgb(46, 125, 50);
            toast.Location = new Point((this.ClientSize.Width - toast.Width) / 2, this.ClientSize.Height - 50);

            Label lbl = new Label();
            lbl.Text = (isError ? "✖  " : "✔  ") + mensaje;
            lbl.ForeColor = Color.White;
            lbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;

            toast.Controls.Add(lbl);
            this.Controls.Add(toast);
            toast.BringToFront();

            Timer timerToast = new Timer();
            timerToast.Interval = 2500;
            timerToast.Tick += (s, e) =>
            {
                this.Controls.Remove(toast);
                toast.Dispose();
                timerToast.Stop();
                timerToast.Dispose();
            };
            timerToast.Start();
        }
    }
}