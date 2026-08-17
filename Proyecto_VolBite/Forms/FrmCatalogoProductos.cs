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
using Proyecto_VolBite.DAO;

namespace Proyecto_VolBite.Forms
{
    public partial class FrmCatalogoProductos : Form
    {
        // Controles dinámicos Guna2
        private Guna.UI2.WinForms.Guna2DataGridView dgvNewProductos;
        private Guna.UI2.WinForms.Guna2NumericUpDown nudNewCantidad;
        private Label lblBadgeCarrito;

        public FrmCatalogoProductos()
        {
            InitializeComponent();

            foreach (Control c in this.Controls)
            {
                c.Visible = false;
            }

            this.BackColor = Color.FromArgb(18, 18, 18);
            this.Size = new Size(850, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Guna.UI2.WinForms.Guna2Panel card = new Guna.UI2.WinForms.Guna2Panel();
            card.Size = new Size(800, 520);
            card.Location = new Point(25, 20);
            card.FillColor = Color.FromArgb(28, 28, 28);
            card.BorderRadius = 16;
            this.Controls.Add(card);

            Label lblTitle = new Label();
            lblTitle.Text = "🍔 CATÁLOGO DE PRODUCTOS";
            lblTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(25, 20);
            card.Controls.Add(lblTitle);

            dgvNewProductos = new Guna.UI2.WinForms.Guna2DataGridView();
            dgvNewProductos.Location = new Point(25, 60);
            dgvNewProductos.Size = new Size(750, 340);
            dgvNewProductos.BackgroundColor = Color.FromArgb(28, 28, 28);
            dgvNewProductos.BorderStyle = BorderStyle.None;
            dgvNewProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNewProductos.MultiSelect = false;
            dgvNewProductos.ReadOnly = true;
            dgvNewProductos.AllowUserToAddRows = false;

            dgvNewProductos.GridColor = Color.FromArgb(40, 40, 40);
            dgvNewProductos.ThemeStyle.RowsStyle.BackColor = Color.FromArgb(35, 35, 35);
            dgvNewProductos.ThemeStyle.RowsStyle.ForeColor = Color.White;
            dgvNewProductos.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgvNewProductos.ThemeStyle.RowsStyle.SelectionForeColor = Color.White;

            dgvNewProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(28, 28, 28);
            dgvNewProductos.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;
            dgvNewProductos.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgvNewProductos.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;

            dgvNewProductos.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(255, 128, 0);
            dgvNewProductos.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvNewProductos.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvNewProductos.ThemeStyle.HeaderStyle.Height = 35;

            dgvNewProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            card.Controls.Add(dgvNewProductos);

            Label lblCantidad = new Label();
            lblCantidad.Text = "Cantidad:";
            lblCantidad.ForeColor = Color.White;
            lblCantidad.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(25, 430);
            card.Controls.Add(lblCantidad);

            nudNewCantidad = new Guna.UI2.WinForms.Guna2NumericUpDown();
            nudNewCantidad.Location = new Point(100, 423);
            nudNewCantidad.Size = new Size(100, 38);
            nudNewCantidad.Value = 1;
            nudNewCantidad.Minimum = 1;
            nudNewCantidad.Maximum = 100;
            nudNewCantidad.BorderRadius = 8;
            nudNewCantidad.FillColor = Color.FromArgb(40, 40, 40);
            nudNewCantidad.ForeColor = Color.White;
            nudNewCantidad.BorderColor = Color.FromArgb(255, 128, 0);
            nudNewCantidad.UpDownButtonFillColor = Color.FromArgb(255, 128, 0);
            nudNewCantidad.UpDownButtonForeColor = Color.White;
            card.Controls.Add(nudNewCantidad);

            Guna.UI2.WinForms.Guna2Button btnNewAgregar = CrearBoton("🛒 Agregar al Carrito", 220, 422, 190, Color.FromArgb(255, 128, 0), Color.White);
            btnNewAgregar.Click += btnAgregarCarrito_Click;
            card.Controls.Add(btnNewAgregar);

            Guna.UI2.WinForms.Guna2Button btnNewCarrito = CrearBoton("🛍️ Ir al Carrito", 420, 422, 160, Color.FromArgb(45, 45, 45), Color.White);
            btnNewCarrito.Click += btnCarrito_Click;
            card.Controls.Add(btnNewCarrito);

            lblBadgeCarrito = new Label();
            lblBadgeCarrito.Size = new Size(24, 24);
            lblBadgeCarrito.Location = new Point(btnNewCarrito.Location.X + btnNewCarrito.Width - 12, btnNewCarrito.Location.Y - 8);
            lblBadgeCarrito.BackColor = Color.FromArgb(230, 50, 50);
            lblBadgeCarrito.ForeColor = Color.White;
            lblBadgeCarrito.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBadgeCarrito.TextAlign = ContentAlignment.MiddleCenter;
            lblBadgeCarrito.Visible = false;

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, lblBadgeCarrito.Width, lblBadgeCarrito.Height);
            lblBadgeCarrito.Region = new Region(path);

            lblBadgeCarrito.Cursor = Cursors.Hand;
            lblBadgeCarrito.Click += (s, e) => btnCarrito_Click(s, e);

            card.Controls.Add(lblBadgeCarrito);
            lblBadgeCarrito.BringToFront();

            Guna.UI2.WinForms.Guna2Button btnNewVolver = CrearBoton("← Volver", 590, 422, 180, Color.FromArgb(45, 45, 45), Color.White);
            btnNewVolver.Click += (s, e) => this.Close();
            card.Controls.Add(btnNewVolver);
        }

        private Guna.UI2.WinForms.Guna2Button CrearBoton(string texto, int x, int y, int ancho, Color fillColor, Color textColor)
        {
            Guna.UI2.WinForms.Guna2Button btn = new Guna.UI2.WinForms.Guna2Button();
            btn.Text = texto;
            btn.Size = new Size(ancho, 40);
            btn.Location = new Point(x, y);
            btn.BorderRadius = 8;
            btn.FillColor = fillColor;
            btn.ForeColor = textColor;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            return btn;
        }

        private void FrmCatalogoProductos_Load(object sender, EventArgs e)
        {
            CargarProductos();
            ActualizarBadgeCarrito();
        }

        private void ActualizarBadgeCarrito()
        {
            int totalItems = 0;

            if (Carrito.Productos != null)
            {
                foreach (var item in Carrito.Productos)
                {
                    totalItems += item.Cantidad;
                }
            }

            if (totalItems > 0)
            {
                lblBadgeCarrito.Text = totalItems > 99 ? "99+" : totalItems.ToString();
                lblBadgeCarrito.Visible = true;
                lblBadgeCarrito.BringToFront();
            }
            else
            {
                lblBadgeCarrito.Visible = false;
            }
        }

        private void CargarProductos()
        {
            ProductoDAO dao = new ProductoDAO();
            dgvNewProductos.DataSource = dao.ListarDisponibles();

            if (dgvNewProductos.Columns.Contains("Id"))
            {
                dgvNewProductos.Columns["Id"].HeaderText = "ID";
                dgvNewProductos.Columns["Id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNewProductos.Columns["Id"].FillWeight = 40;
            }

            if (dgvNewProductos.Columns.Contains("Nombre"))
            {
                dgvNewProductos.Columns["Nombre"].HeaderText = "Nombre";
                dgvNewProductos.Columns["Nombre"].FillWeight = 120;
            }

            if (dgvNewProductos.Columns.Contains("Descripcion"))
            {
                dgvNewProductos.Columns["Descripcion"].HeaderText = "Descripción";
                dgvNewProductos.Columns["Descripcion"].FillWeight = 150;
            }

            if (dgvNewProductos.Columns.Contains("Precio"))
            {
                dgvNewProductos.Columns["Precio"].HeaderText = "Precio";
                dgvNewProductos.Columns["Precio"].DefaultCellStyle.Format = "$#,##0.00";
                dgvNewProductos.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNewProductos.Columns["Precio"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNewProductos.Columns["Precio"].FillWeight = 80;
            }

            if (dgvNewProductos.Columns.Contains("TiempoPreparacion"))
            {
                dgvNewProductos.Columns["TiempoPreparacion"].HeaderText = "Prep. (Min)";
                dgvNewProductos.Columns["TiempoPreparacion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNewProductos.Columns["TiempoPreparacion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNewProductos.Columns["TiempoPreparacion"].FillWeight = 90;
            }

            if (dgvNewProductos.Columns.Contains("Disponible"))
            {
                dgvNewProductos.Columns["Disponible"].Visible = false;
            }

            if (dgvNewProductos.Columns.Contains("Stock"))
            {
                dgvNewProductos.Columns["Stock"].HeaderText = "Stock";
                dgvNewProductos.Columns["Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNewProductos.Columns["Stock"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNewProductos.Columns["Stock"].FillWeight = 60;
            }
        }

        private void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            if (dgvNewProductos.CurrentRow == null)
            {
                Toast.Advertencia(this, "Seleccione un producto de la lista.");
                return;
            }

            DataGridViewRow fila = dgvNewProductos.CurrentRow;
            string colId = dgvNewProductos.Columns.Contains("Id") ? "Id" : (dgvNewProductos.Columns.Contains("id") ? "id" : null);

            int id = colId != null ? Convert.ToInt32(fila.Cells[colId].Value) : Convert.ToInt32(fila.Cells[0].Value);
            string nombre = fila.Cells["Nombre"].Value?.ToString();
            decimal precio = Convert.ToDecimal(fila.Cells["Precio"].Value);
            int tiempo = Convert.ToInt32(fila.Cells["TiempoPreparacion"].Value);
            int cantidad = Convert.ToInt32(nudNewCantidad.Value);

            CarritoItem existente = Carrito.Productos.Find(x => x.IdProducto == id);

            if (existente != null)
            {
                existente.Cantidad += cantidad;
            }
            else
            {
                Carrito.Productos.Add(new CarritoItem
                {
                    IdProducto = id,
                    Nombre = nombre,
                    Precio = precio,
                    Cantidad = cantidad,
                    TiempoPreparacion = tiempo
                });
            }

            ActualizarBadgeCarrito();
            Toast.Exito(this, "¡Producto agregado al carrito con éxito!");
        }

        private void btnCarrito_Click(object sender, EventArgs e)
        {
            FrmCarrito frm = new FrmCarrito();
            frm.ShowDialog();

            CargarProductos();
            ActualizarBadgeCarrito();
        }
    }
}