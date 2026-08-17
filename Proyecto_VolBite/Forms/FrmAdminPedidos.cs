using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Proyecto_VolBite.DAO;

namespace Proyecto_VolBite.Forms
{
    public partial class FrmAdminPedidos : Form
    {
        private DataGridView dgvPedidos;
        private DataGridView dgvDetalles;
        private ComboBox cmbEstados;
        private Button btnActualizar;
        private PedidoDAO pedidoDAO = new PedidoDAO();
        private DetallePedidoDAO detalleDAO = new DetallePedidoDAO();

        // Controles para el Panel Derecho
        private Panel panelDetalle;
        private Label lblDetalleTitulo;
        private Label lblClienteDetalle;
        private Label lblEstadoDetalle;
        private Label lblTotalPedido;
        private Label lblMensajeRecibido;
        private Label lblCambiar;

        private int idPedidoSeleccionado = -1;
        private Timer timerActualizacion;

        public FrmAdminPedidos()
        {
            InitializeComponent();
            ConstruirInterfaz();
            InicializarTimer();
        }

        private void ConstruirInterfaz()
        {
            // Configuración de la Ventana Principal
            this.Text = "Administrar Pedidos - VolBite";
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(920, 560);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Panel / Card Central
            Panel card = new Panel();
            card.Size = new Size(880, 510);
            card.Location = new Point((this.ClientSize.Width - card.Width) / 2, (this.ClientSize.Height - card.Height) / 2);
            card.BackColor = Color.FromArgb(28, 28, 28);
            this.Controls.Add(card);

            // Título Principal
            Label lblTitulo = new Label();
            lblTitulo.Text = "PANEL DE CONTROL - PEDIDOS ENTRANTES";
            lblTitulo.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(255, 128, 0);
            lblTitulo.Size = new Size(500, 30);
            lblTitulo.Location = new Point(25, 15);
            card.Controls.Add(lblTitulo);

            // Botón Volver al Menú
            Button btnVolver = new Button();
            btnVolver.Text = "⬅ Volver al Menú";
            btnVolver.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnVolver.ForeColor = Color.FromArgb(255, 128, 0);
            btnVolver.BackColor = Color.Transparent;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            btnVolver.FlatAppearance.BorderSize = 1;
            btnVolver.Size = new Size(130, 32);
            btnVolver.Location = new Point(725, 15);
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.Click += (s, ev) =>
            {
                if (timerActualizacion != null) timerActualizacion.Stop();
                this.Close();
            };
            card.Controls.Add(btnVolver);

            // --- TABLA DE PEDIDOS (IZQUIERDA) ---
            dgvPedidos = CrearTablaStylized();
            dgvPedidos.Size = new Size(490, 425);
            dgvPedidos.Location = new Point(25, 60);
            dgvPedidos.CellClick += DgvPedidos_CellClick;
            dgvPedidos.SelectionChanged += DgvPedidos_SelectionChanged;
            card.Controls.Add(dgvPedidos);

            // --- PANEL DERECHO: DETALLE DEL PEDIDO Y ACCIONES ---
            panelDetalle = new Panel();
            panelDetalle.Size = new Size(325, 425);
            panelDetalle.Location = new Point(530, 60);
            panelDetalle.BackColor = Color.FromArgb(20, 20, 20);
            panelDetalle.Visible = false; // Se muestra al seleccionar un pedido
            card.Controls.Add(panelDetalle);

            lblDetalleTitulo = new Label();
            lblDetalleTitulo.Text = "Detalle Pedido #0";
            lblDetalleTitulo.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblDetalleTitulo.ForeColor = Color.FromArgb(255, 128, 0);
            lblDetalleTitulo.Location = new Point(15, 10);
            lblDetalleTitulo.AutoSize = true;
            panelDetalle.Controls.Add(lblDetalleTitulo);

            lblClienteDetalle = new Label();
            lblClienteDetalle.Text = "Cliente: -";
            lblClienteDetalle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblClienteDetalle.ForeColor = Color.LightGray;
            lblClienteDetalle.Location = new Point(15, 33);
            lblClienteDetalle.AutoSize = true;
            panelDetalle.Controls.Add(lblClienteDetalle);

            lblEstadoDetalle = new Label();
            lblEstadoDetalle.Text = "Estado: -";
            lblEstadoDetalle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblEstadoDetalle.ForeColor = Color.Yellow;
            lblEstadoDetalle.Location = new Point(15, 52);
            lblEstadoDetalle.AutoSize = true;
            panelDetalle.Controls.Add(lblEstadoDetalle);

            // Tabla secundaria para los productos comprados
            dgvDetalles = CrearTablaStylized();
            dgvDetalles.Size = new Size(295, 180);
            dgvDetalles.Location = new Point(15, 78);
            panelDetalle.Controls.Add(dgvDetalles);

            lblTotalPedido = new Label();
            lblTotalPedido.Text = "Total: $ 0.00";
            lblTotalPedido.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTotalPedido.ForeColor = Color.White;
            lblTotalPedido.Location = new Point(15, 270);
            lblTotalPedido.AutoSize = true;
            panelDetalle.Controls.Add(lblTotalPedido);

            // Sección de Cambio de Estado
            lblCambiar = new Label();
            lblCambiar.Text = "Cambiar Estado:";
            lblCambiar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCambiar.ForeColor = Color.White;
            lblCambiar.Location = new Point(15, 305);
            lblCambiar.AutoSize = true;
            panelDetalle.Controls.Add(lblCambiar);

            cmbEstados = new ComboBox();
            cmbEstados.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstados.Font = new Font("Segoe UI", 9.5F);
            cmbEstados.BackColor = Color.FromArgb(45, 45, 45);
            cmbEstados.ForeColor = Color.White;
            cmbEstados.FlatStyle = FlatStyle.Flat;
            cmbEstados.Items.AddRange(new string[] { "Pendiente", "En Preparación", "Listo para Entregar", "Entregado", "Cancelado" });
            cmbEstados.Size = new Size(295, 25);
            cmbEstados.Location = new Point(15, 328);
            panelDetalle.Controls.Add(cmbEstados);

            btnActualizar = new Button();
            btnActualizar.Text = "ACTUALIZAR ESTADO";
            btnActualizar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.BackColor = Color.FromArgb(255, 128, 0);
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.Size = new Size(295, 35);
            btnActualizar.Location = new Point(15, 363);
            btnActualizar.Cursor = Cursors.Hand;
            btnActualizar.Click += BtnActualizar_Click;
            panelDetalle.Controls.Add(btnActualizar);

            // Mensaje verde para indicar que ya fue recibido por el usuario
            lblMensajeRecibido = new Label();
            lblMensajeRecibido.Text = "✔ Pedido Recibido por el Cliente";
            lblMensajeRecibido.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMensajeRecibido.ForeColor = Color.FromArgb(40, 167, 69);
            lblMensajeRecibido.Size = new Size(295, 35);
            lblMensajeRecibido.TextAlign = ContentAlignment.MiddleCenter;
            lblMensajeRecibido.Location = new Point(15, 340);
            lblMensajeRecibido.Visible = false;
            panelDetalle.Controls.Add(lblMensajeRecibido);

            // Cargar datos al arrancar
            CargarPedidos();
        }

        private DataGridView CrearTablaStylized()
        {
            DataGridView dgv = new DataGridView();
            dgv.BackgroundColor = Color.FromArgb(20, 20, 20);
            dgv.ForeColor = Color.White;
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(35, 35, 35);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 45);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.BorderStyle = BorderStyle.None;
            return dgv;
        }

        private void InicializarTimer()
        {
            timerActualizacion = new Timer();
            timerActualizacion.Interval = 3000; // Refresca cada 3 segundos
            timerActualizacion.Tick += (s, ev) => CargarPedidosSilencioso();
            timerActualizacion.Start();

            this.FormClosing += (s, ev) => {
                if (timerActualizacion != null)
                {
                    timerActualizacion.Stop();
                    timerActualizacion.Dispose();
                }
            };
        }

        private void CargarPedidos()
        {
            try
            {
                dgvPedidos.SelectionChanged -= DgvPedidos_SelectionChanged;
                dgvPedidos.DataSource = pedidoDAO.ObtenerTodosLosPedidos();
                dgvPedidos.ClearSelection();
                panelDetalle.Visible = false;
                idPedidoSeleccionado = -1;
                dgvPedidos.SelectionChanged += DgvPedidos_SelectionChanged;
            }
            catch (Exception ex)
            {
                MostrarNotificacion("Error al cargar pedidos: " + ex.Message, isError: true);
            }
        }

        private void CargarPedidosSilencioso()
        {
            try
            {
                int idPrevio = idPedidoSeleccionado;

                DataTable dt = pedidoDAO.ObtenerTodosLosPedidos();

                dgvPedidos.SelectionChanged -= DgvPedidos_SelectionChanged;
                dgvPedidos.DataSource = dt;

                if (idPrevio != -1)
                {
                    foreach (DataGridViewRow row in dgvPedidos.Rows)
                    {
                        if (Convert.ToInt32(row.Cells["ID"].Value) == idPrevio)
                        {
                            row.Selected = true;
                            string estadoActual = row.Cells["Estado"].Value.ToString();
                            lblEstadoDetalle.Text = "Estado: " + estadoActual;
                            EvaluarVistaEstado(estadoActual);
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
            catch { }
        }

        private void DgvPedidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) MostrarDetalleFila();
        }

        private void DgvPedidos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPedidos.Focused && dgvPedidos.SelectedRows.Count > 0) MostrarDetalleFila();
        }

        private void MostrarDetalleFila()
        {
            if (dgvPedidos.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvPedidos.SelectedRows[0];
                idPedidoSeleccionado = Convert.ToInt32(fila.Cells["ID"].Value);
                string cliente = fila.Cells["Cliente"].Value.ToString();
                string total = fila.Cells["Total ($)"].Value.ToString();
                string estado = fila.Cells["Estado"].Value.ToString();

                lblDetalleTitulo.Text = "Detalle Pedido #" + idPedidoSeleccionado;
                lblClienteDetalle.Text = "Cliente: " + cliente;
                lblEstadoDetalle.Text = "Estado: " + estado;
                lblTotalPedido.Text = "Total: $ " + total;

                cmbEstados.SelectedItem = estado;

                CargarProductosDelPedido(idPedidoSeleccionado);
                EvaluarVistaEstado(estado);

                panelDetalle.Visible = true;
            }
        }

        private void EvaluarVistaEstado(string estado)
        {
            if (estado.Equals("Entregado", StringComparison.OrdinalIgnoreCase))
            {
                lblCambiar.Visible = false;
                cmbEstados.Visible = false;
                btnActualizar.Visible = false;
                lblMensajeRecibido.Visible = true;
            }
            else
            {
                lblCambiar.Visible = true;
                cmbEstados.Visible = true;
                btnActualizar.Visible = true;
                lblMensajeRecibido.Visible = false;
            }
        }

        private void CargarProductosDelPedido(int idPedido)
        {
            try
            {
                DataTable dt = detalleDAO.ObtenerDetallePorPedido(idPedido);
                dgvDetalles.DataSource = dt;
            }
            catch (Exception ex)
            {
                MostrarNotificacion("Error al cargar detalles: " + ex.Message, isError: true);
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.SelectedRows.Count == 0 || idPedidoSeleccionado <= 0)
            {
                MostrarNotificacion("Por favor selecciona un pedido.", isError: true);
                return;
            }

            string nuevoEstado = cmbEstados.SelectedItem.ToString();

            if (pedidoDAO.ActualizarEstado(idPedidoSeleccionado, nuevoEstado))
            {
                // NOTIFICACIÓN MODERNA Y FLOTANTE
                MostrarNotificacion($"¡Pedido #{idPedidoSeleccionado} actualizado a '{nuevoEstado}'!");
                CargarPedidosSilencioso();
            }
            else
            {
                MostrarNotificacion("No se pudo actualizar el estado.", isError: true);
            }
        }

        // --- NOTIFICACIÓN PERSONALIZADA VOLBITE ---
        private void MostrarNotificacion(string mensaje, bool isError = false)
        {
            Panel toast = new Panel();
            toast.Size = new Size(380, 42);
            toast.BackColor = isError ? Color.FromArgb(200, 35, 51) : Color.FromArgb(46, 125, 50);
            toast.Location = new Point((this.ClientSize.Width - toast.Width) / 2, this.ClientSize.Height - 60);

            Label lbl = new Label();
            lbl.Text = (isError ? "✖  " : "✔  ") + mensaje;
            lbl.ForeColor = Color.White;
            lbl.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
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