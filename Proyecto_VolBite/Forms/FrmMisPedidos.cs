using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Proyecto_VolBite.Clases;
using Proyecto_VolBite.DAO;

namespace Proyecto_VolBite.Forms
{
    public partial class FrmMisPedidos : Form
    {
        private DataGridView dgvPedidos;
        private DataGridView dgvDetalles;
        private Label lblDetalleTitulo;
        private Label lblEstadoDetalle; // Muestra el estado del pedido en el detalle
        private Label lblTotalPedido;
        private Panel panelDetalle;

        // Nuevos controles para la confirmación de recepción del pedido
        private Button btnConfirmarRecibido;
        private Label lblMensajeEntregado;
        private int idPedidoSeleccionado = -1;

        // Timer para la recarga en tiempo real
        private Timer timerActualizacion;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void MoverVentana()
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public FrmMisPedidos()
        {
            InitializeComponent();
            ConstruirInterfaz();
            InicializarTimer();
        }

        private void ConstruirInterfaz()
        {
            this.Text = "Mis Pedidos";
            this.BackColor = Color.FromArgb(20, 20, 20);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(880, 520);
            this.MouseDown += (s, ev) => { if (ev.Button == MouseButtons.Left) MoverVentana(); };

            // Panel Superior / Header
            Panel panelHeader = new Panel();
            panelHeader.Size = new Size(880, 50);
            panelHeader.Location = new Point(0, 0);
            panelHeader.BackColor = Color.FromArgb(32, 32, 32);
            panelHeader.MouseDown += (s, ev) => { if (ev.Button == MouseButtons.Left) MoverVentana(); };
            this.Controls.Add(panelHeader);

            Label lblTitulo = new Label();
            lblTitulo.Text = "📜 HISTORIAL DE MIS PEDIDOS";
            lblTitulo.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(255, 128, 0);
            lblTitulo.Location = new Point(20, 12);
            lblTitulo.AutoSize = true;
            panelHeader.Controls.Add(lblTitulo);

            Button btnCerrar = new Button();
            btnCerrar.Text = "✕";
            btnCerrar.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Size = new Size(40, 40);
            btnCerrar.Location = new Point(830, 5);
            btnCerrar.Cursor = Cursors.Hand;
            btnCerrar.Click += (s, ev) => RegresarAlMenu();
            panelHeader.Controls.Add(btnCerrar);

            // --- TABLA IZQUIERDA: LISTA DE PEDIDOS ---
            Label lblSub1 = new Label();
            lblSub1.Text = "Haz clic en un pedido para desplegar su detalle:";
            lblSub1.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            lblSub1.ForeColor = Color.DarkGray;
            lblSub1.Location = new Point(20, 65);
            lblSub1.AutoSize = true;
            this.Controls.Add(lblSub1);

            dgvPedidos = CrearTabla();
            dgvPedidos.Size = new Size(480, 335);
            dgvPedidos.Location = new Point(20, 95);
            dgvPedidos.CellClick += DgvPedidos_CellClick;
            this.Controls.Add(dgvPedidos);

            // --- BOTÓN REGRESAR AL MENÚ ---
            Button btnRegresar = new Button();
            btnRegresar.Text = "← Regresar al Menú";
            btnRegresar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnRegresar.ForeColor = Color.White;
            btnRegresar.BackColor = Color.FromArgb(255, 128, 0);
            btnRegresar.FlatStyle = FlatStyle.Flat;
            btnRegresar.FlatAppearance.BorderSize = 0;
            btnRegresar.Size = new Size(170, 38);
            btnRegresar.Location = new Point(20, 442);
            btnRegresar.Cursor = Cursors.Hand;
            btnRegresar.Click += (s, ev) => RegresarAlMenu();
            this.Controls.Add(btnRegresar);

            // --- PANEL DERECHO: DETALLE DEL PEDIDO ---
            panelDetalle = new Panel();
            panelDetalle.Size = new Size(340, 385);
            panelDetalle.Location = new Point(520, 95);
            panelDetalle.BackColor = Color.FromArgb(32, 32, 32);
            panelDetalle.Visible = false; // INICIA OCULTO
            this.Controls.Add(panelDetalle);

            lblDetalleTitulo = new Label();
            lblDetalleTitulo.Text = "Productos del Pedido";
            lblDetalleTitulo.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblDetalleTitulo.ForeColor = Color.FromArgb(255, 128, 0);
            lblDetalleTitulo.Location = new Point(15, 12);
            lblDetalleTitulo.AutoSize = true;
            panelDetalle.Controls.Add(lblDetalleTitulo);

            // Label para visualizar el estado dentro del panel derecho
            lblEstadoDetalle = new Label();
            lblEstadoDetalle.Text = "Estado: -";
            lblEstadoDetalle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblEstadoDetalle.ForeColor = Color.Yellow;
            lblEstadoDetalle.Location = new Point(15, 33);
            lblEstadoDetalle.AutoSize = true;
            panelDetalle.Controls.Add(lblEstadoDetalle);

            dgvDetalles = CrearTabla();
            dgvDetalles.Size = new Size(310, 240);
            dgvDetalles.Location = new Point(15, 60);
            panelDetalle.Controls.Add(dgvDetalles);

            lblTotalPedido = new Label();
            lblTotalPedido.Text = "Total: $ 0.00";
            lblTotalPedido.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblTotalPedido.ForeColor = Color.White;
            lblTotalPedido.Location = new Point(15, 308);
            lblTotalPedido.AutoSize = true;
            panelDetalle.Controls.Add(lblTotalPedido);

            // --- BOTÓN VERDE "MARCAR COMO RECIBIDO" ---
            btnConfirmarRecibido = new Button();
            btnConfirmarRecibido.Text = "✅ Confirmar Recibido";
            btnConfirmarRecibido.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnConfirmarRecibido.ForeColor = Color.White;
            btnConfirmarRecibido.BackColor = Color.FromArgb(40, 167, 69); // Verde de éxito
            btnConfirmarRecibido.FlatStyle = FlatStyle.Flat;
            btnConfirmarRecibido.FlatAppearance.BorderSize = 0;
            btnConfirmarRecibido.Size = new Size(310, 36);
            btnConfirmarRecibido.Location = new Point(15, 338);
            btnConfirmarRecibido.Cursor = Cursors.Hand;
            btnConfirmarRecibido.Visible = false;
            btnConfirmarRecibido.Click += BtnConfirmarRecibido_Click;
            panelDetalle.Controls.Add(btnConfirmarRecibido);

            // Label estado "Entregado"
            lblMensajeEntregado = new Label();
            lblMensajeEntregado.Text = "✔ Pedido Entregado con éxito";
            lblMensajeEntregado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMensajeEntregado.ForeColor = Color.FromArgb(40, 167, 69);
            lblMensajeEntregado.Size = new Size(310, 25);
            lblMensajeEntregado.TextAlign = ContentAlignment.MiddleCenter;
            lblMensajeEntregado.Location = new Point(15, 343);
            lblMensajeEntregado.Visible = false;
            panelDetalle.Controls.Add(lblMensajeEntregado);

            // Cargar Historial al Iniciar
            CargarHistorial();
        }

        // --- TIMER AUTOMÁTICO DE REFRESCO ---
        private void InicializarTimer()
        {
            timerActualizacion = new Timer();
            timerActualizacion.Interval = 3000; // Consulta cambios cada 3 segundos
            timerActualizacion.Tick += (s, ev) => ActualizarDatosSilencioso();
            timerActualizacion.Start();

            this.FormClosing += (s, ev) => {
                if (timerActualizacion != null)
                {
                    timerActualizacion.Stop();
                    timerActualizacion.Dispose();
                }
            };
        }

        private void RegresarAlMenu()
        {
            if (timerActualizacion != null)
            {
                timerActualizacion.Stop();
            }
            this.Close();
        }

        private DataGridView CrearTabla()
        {
            DataGridView dgv = new DataGridView();
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = Color.FromArgb(25, 25, 25);
            dgv.BorderStyle = BorderStyle.None;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 128, 0);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;

            dgv.DefaultCellStyle.BackColor = Color.FromArgb(35, 35, 35);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Orange;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            return dgv;
        }

        private void CargarHistorial()
        {
            try
            {
                dgvPedidos.SelectionChanged -= DgvPedidos_SelectionChanged;

                PedidoDAO pedidoDAO = new PedidoDAO();
                DataTable dt = pedidoDAO.ObtenerPedidosPorUsuario(Sesion.IdUsuario);
                dgvPedidos.DataSource = dt;
                dgvPedidos.ClearSelection(); // Desselecciona al abrir para dejar el panel derecho oculto

                if (dt == null || dt.Rows.Count == 0)
                {
                    MostrarNotificacion("Aún no tienes pedidos registrados en tu historial.", isError: false);
                }

                dgvPedidos.SelectionChanged += DgvPedidos_SelectionChanged;
            }
            catch (Exception ex)
            {
                MostrarNotificacion("Error al cargar historial: " + ex.Message, isError: true);
            }
        }

        // --- ACTUALIZACIÓN SILENCIOSA DE ESTADO EN TIEMPO REAL ---
        private void ActualizarDatosSilencioso()
        {
            try
            {
                int idSeleccionado = -1;
                if (dgvPedidos.SelectedRows.Count > 0)
                {
                    idSeleccionado = Convert.ToInt32(dgvPedidos.SelectedRows[0].Cells["N° Pedido"].Value);
                }

                PedidoDAO pedidoDAO = new PedidoDAO();
                DataTable dt = pedidoDAO.ObtenerPedidosPorUsuario(Sesion.IdUsuario);

                dgvPedidos.SelectionChanged -= DgvPedidos_SelectionChanged;
                dgvPedidos.DataSource = dt;

                // Re-seleccionar el pedido activo para refrescar el estado actualizado
                if (idSeleccionado != -1)
                {
                    foreach (DataGridViewRow row in dgvPedidos.Rows)
                    {
                        if (Convert.ToInt32(row.Cells["N° Pedido"].Value) == idSeleccionado)
                        {
                            row.Selected = true;
                            string nuevoEstado = row.Cells["Estado"].Value.ToString();
                            lblEstadoDetalle.Text = "Estado: " + nuevoEstado;
                            EvaluarBotonConfirmar(nuevoEstado);
                            break;
                        }
                    }
                }
                else
                {
                    dgvPedidos.ClearSelection();
                }

                dgvPedidos.SelectionChanged += DgvPedidos_SelectionChanged;
            }
            catch
            {
                // Manejo silencioso en segundo plano
            }
        }

        private void DgvPedidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                MostrarDetalleFila();
            }
        }

        private void DgvPedidos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPedidos.Focused && dgvPedidos.SelectedRows.Count > 0)
            {
                MostrarDetalleFila();
            }
        }

        private void MostrarDetalleFila()
        {
            if (dgvPedidos.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvPedidos.SelectedRows[0];
                idPedidoSeleccionado = Convert.ToInt32(fila.Cells["N° Pedido"].Value);
                string total = fila.Cells["Total ($)"].Value.ToString();
                string estado = fila.Cells["Estado"].Value.ToString();

                lblDetalleTitulo.Text = "Detalle Pedido #" + idPedidoSeleccionado;
                lblEstadoDetalle.Text = "Estado: " + estado;
                lblTotalPedido.Text = "Total: $ " + total;

                CargarProductosDelPedido(idPedidoSeleccionado);
                EvaluarBotonConfirmar(estado);

                // Muestra el panel derecho solo cuando el usuario selecciona un pedido
                panelDetalle.Visible = true;
            }
        }

        private void EvaluarBotonConfirmar(string estado)
        {
            if (estado.Equals("Listo para Entregar", StringComparison.OrdinalIgnoreCase))
            {
                btnConfirmarRecibido.Visible = true;
                lblMensajeEntregado.Visible = false;
            }
            else if (estado.Equals("Entregado", StringComparison.OrdinalIgnoreCase))
            {
                btnConfirmarRecibido.Visible = false;
                lblMensajeEntregado.Visible = true;
            }
            else
            {
                btnConfirmarRecibido.Visible = false;
                lblMensajeEntregado.Visible = false;
            }
        }

        // --- BOTÓN DIRECTO SIN POP-UPS / MESSAGEBOX ---
        private void BtnConfirmarRecibido_Click(object sender, EventArgs e)
        {
            if (idPedidoSeleccionado <= 0) return;

            PedidoDAO pedidoDAO = new PedidoDAO();
            bool ok = pedidoDAO.ActualizarEstado(idPedidoSeleccionado, "Entregado");

            if (ok)
            {
                MostrarNotificacion("¡Excelente! Pedido #" + idPedidoSeleccionado + " marcado como entregado.", isError: false);
                ActualizarDatosSilencioso(); // Recarga y actualiza de inmediato la vista
            }
            else
            {
                MostrarNotificacion("No se pudo actualizar el estado del pedido.", isError: true);
            }
        }

        private void CargarProductosDelPedido(int idPedido)
        {
            try
            {
                DetallePedidoDAO detalleDAO = new DetallePedidoDAO();
                DataTable dt = detalleDAO.ObtenerDetallePorPedido(idPedido);
                dgvDetalles.DataSource = dt;
            }
            catch (Exception ex)
            {
                MostrarNotificacion("Error al obtener productos: " + ex.Message, isError: true);
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