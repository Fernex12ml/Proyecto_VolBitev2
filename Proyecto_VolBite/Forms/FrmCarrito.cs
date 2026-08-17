using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_VolBite.Clases;

namespace Proyecto_VolBite.Forms
{
    public partial class FrmCarrito : Form
    {
        // Controles dinámicos Guna2
        private Guna.UI2.WinForms.Guna2DataGridView dgvNewCarrito;
        private Label lblTotalDinamico;

        public FrmCarrito()
        {
            InitializeComponent();

            // 1. Ocultar los controles sencillos antiguos del diseñador
            foreach (Control c in this.Controls)
            {
                c.Visible = false;
            }

            // Configuración del Formulario
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.Size = new Size(750, 580);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 2. PANEL CONTENEDOR PRINCIPAL (CARD)
            Guna.UI2.WinForms.Guna2Panel card = new Guna.UI2.WinForms.Guna2Panel();
            card.Size = new Size(700, 500);
            card.Location = new Point((this.ClientSize.Width - card.Width) / 2, (this.ClientSize.Height - card.Height) / 2);
            card.FillColor = Color.FromArgb(28, 28, 28);
            card.BorderRadius = 16;
            this.Controls.Add(card);

            // Título Principal
            Label lblTitle = new Label();
            lblTitle.Text = "🛒 TU CARRITO DE COMPRAS";
            lblTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(25, 20);
            card.Controls.Add(lblTitle);

            // 3. TABLA MODERNA (Guna2DataGridView)
            dgvNewCarrito = new Guna.UI2.WinForms.Guna2DataGridView();
            dgvNewCarrito.Location = new Point(25, 60);
            dgvNewCarrito.Size = new Size(650, 280);
            dgvNewCarrito.BackgroundColor = Color.FromArgb(28, 28, 28);
            dgvNewCarrito.BorderStyle = BorderStyle.None;
            dgvNewCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNewCarrito.MultiSelect = false;
            dgvNewCarrito.ReadOnly = true;
            dgvNewCarrito.AllowUserToAddRows = false;

            // Estilos oscuros de la tabla
            dgvNewCarrito.GridColor = Color.FromArgb(40, 40, 40);
            dgvNewCarrito.ThemeStyle.RowsStyle.BackColor = Color.FromArgb(35, 35, 35);
            dgvNewCarrito.ThemeStyle.RowsStyle.ForeColor = Color.White;
            dgvNewCarrito.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgvNewCarrito.ThemeStyle.RowsStyle.SelectionForeColor = Color.White;

            // Filas alternas
            dgvNewCarrito.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(28, 28, 28);
            dgvNewCarrito.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;
            dgvNewCarrito.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgvNewCarrito.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;

            // Encabezados
            dgvNewCarrito.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(255, 128, 0);
            dgvNewCarrito.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvNewCarrito.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvNewCarrito.ThemeStyle.HeaderStyle.Height = 35;

            // Ajuste automático de columnas
            dgvNewCarrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            card.Controls.Add(dgvNewCarrito);

            // 4. SECCIÓN INFERIOR (TOTAL Y ACCIONES)

            // Panel contenedor del Total
            Guna.UI2.WinForms.Guna2Panel pnlTotal = new Guna.UI2.WinForms.Guna2Panel();
            pnlTotal.Size = new Size(650, 50);
            pnlTotal.Location = new Point(25, 355);
            pnlTotal.FillColor = Color.FromArgb(35, 35, 35);
            pnlTotal.BorderRadius = 10;
            card.Controls.Add(pnlTotal);

            Label lblTotalText = new Label();
            lblTotalText.Text = "TOTAL A PAGAR:";
            lblTotalText.ForeColor = Color.White;
            lblTotalText.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblTotalText.AutoSize = true;
            lblTotalText.Location = new Point(20, 15);
            lblTotalText.BackColor = Color.Transparent;
            pnlTotal.Controls.Add(lblTotalText);

            lblTotalDinamico = new Label();
            lblTotalDinamico.Text = "$ 0.00";
            lblTotalDinamico.ForeColor = Color.FromArgb(255, 128, 0);
            lblTotalDinamico.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTotalDinamico.AutoSize = true;
            lblTotalDinamico.Location = new Point(520, 12);
            lblTotalDinamico.BackColor = Color.Transparent;
            pnlTotal.Controls.Add(lblTotalDinamico);

            // 5. BOTONES DE ACCIÓN

            // Botón: Comprar
            Guna.UI2.WinForms.Guna2Button btnNewComprar = CrearBoton("💳 Procesar Compra", 25, 425, 200, Color.FromArgb(255, 128, 0), Color.White);
            btnNewComprar.Click += btnComprar_Click;
            card.Controls.Add(btnNewComprar);

            // Botón: Eliminar Producto
            Guna.UI2.WinForms.Guna2Button btnNewEliminar = CrearBoton("🗑️ Eliminar Producto", 240, 425, 200, Color.FromArgb(180, 40, 40), Color.White);
            btnNewEliminar.Click += btnEliminar_Click;
            card.Controls.Add(btnNewEliminar);

            // Botón: Volver / Cerrar
            Guna.UI2.WinForms.Guna2Button btnNewVolver = CrearBoton("← Volver", 515, 425, 160, Color.FromArgb(45, 45, 45), Color.White);
            btnNewVolver.Click += (s, e) => this.Close();
            card.Controls.Add(btnNewVolver);
        }

        // Método auxiliar para crear botones
        private Guna.UI2.WinForms.Guna2Button CrearBoton(string texto, int x, int y, int ancho, Color fillColor, Color textColor)
        {
            Guna.UI2.WinForms.Guna2Button btn = new Guna.UI2.WinForms.Guna2Button();
            btn.Text = texto;
            btn.Size = new Size(ancho, 45);
            btn.Location = new Point(x, y);
            btn.BorderRadius = 10;
            btn.FillColor = fillColor;
            btn.ForeColor = textColor;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            return btn;
        }

        private void FrmCarrito_Load(object sender, EventArgs e)
        {
            ActualizarCarrito();
        }

        private void ActualizarCarrito()
        {
            // Actualizar fuente de datos en la tabla moderna
            dgvNewCarrito.DataSource = null;
            dgvNewCarrito.DataSource = Carrito.Productos;

            // Actualizar etiqueta del total dinámica
            decimal total = Carrito.Total();
            string totalFormateado = "$ " + total.ToString("0.00");

            if (lblTotal != null)
            {
                lblTotal.Text = totalFormateado;
            }
            if (lblTotalDinamico != null)
            {
                lblTotalDinamico.Text = totalFormateado;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si el carrito ya está vacío
            if (Carrito.Productos.Count == 0)
            {
                Toast.Advertencia(this, "No hay productos en el carrito para eliminar.");
                return;
            }

            if (dgvNewCarrito.CurrentRow == null)
            {
                Toast.Advertencia(this, "Seleccione un producto de la tabla para eliminar.");
                return;
            }

            // Eliminación directa sin cuadro modal interrumpiendo
            DataGridViewRow fila = dgvNewCarrito.CurrentRow;
            string colId = dgvNewCarrito.Columns.Contains("IdProducto") ? "IdProducto" : (dgvNewCarrito.Columns.Contains("idProducto") ? "idProducto" : null);

            int id = colId != null ? Convert.ToInt32(fila.Cells[colId].Value) : Convert.ToInt32(fila.Cells[0].Value);

            CarritoItem item = Carrito.Productos.Find(x => x.IdProducto == id);

            if (item != null)
            {
                Carrito.Productos.Remove(item);
                Toast.Exito(this, "Producto removido del carrito.");
            }

            // Recalcular total y refrescar tabla
            ActualizarCarrito();
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            // 1. Si el carrito está vacío, se informa con Toast o se cierra directamente
            if (Carrito.Productos == null || Carrito.Productos.Count == 0)
            {
                Toast.Advertencia(this, "El carrito está vacío. Agrega productos antes de comprar.");
                return;
            }

            // 2. Bloqueo temporal del control para prevenir doble clic
            Control btn = sender as Control;
            if (btn != null)
            {
                btn.Enabled = false;
            }

            try
            {
                FrmCompra frm = new FrmCompra();
                frm.ShowDialog();

                // 3. Al regresar, si la compra fue exitosa (carrito en 0), cerramos sin avisos
                if (Carrito.Productos.Count == 0)
                {
                    this.Close();
                }
                else
                {
                    ActualizarCarrito();
                }
            }
            finally
            {
                if (btn != null)
                {
                    btn.Enabled = true;
                }
            }
        }
    }
}