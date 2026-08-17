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
    public partial class FrmProductos : Form
    {
        // Controles dinámicos modernos
        private Guna.UI2.WinForms.Guna2TextBox txtNewId;
        private Guna.UI2.WinForms.Guna2TextBox txtNewNombre;
        private Guna.UI2.WinForms.Guna2TextBox txtNewDescripcion;
        private Guna.UI2.WinForms.Guna2TextBox txtNewPrecio;
        private Guna.UI2.WinForms.Guna2TextBox txtNewTiempo;
        private Guna.UI2.WinForms.Guna2TextBox txtNewStock;
        private Guna.UI2.WinForms.Guna2CheckBox chkNewDisponible;
        private Guna.UI2.WinForms.Guna2DataGridView dgvNewProductos;

        public FrmProductos()
        {
            InitializeComponent();

            // 1. Ocultar los controles anticuados del diseñador
            foreach (Control c in this.Controls)
            {
                c.Visible = false;
            }

            // Fondo del Formulario
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.Size = new Size(950, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 2. PANEL IZQUIERDO (TARJETA DE FORMULARIO)
            Guna.UI2.WinForms.Guna2Panel card = new Guna.UI2.WinForms.Guna2Panel();
            card.Size = new Size(340, 520);
            card.Location = new Point(20, 20);
            card.FillColor = Color.FromArgb(28, 28, 28);
            card.BorderRadius = 16;
            this.Controls.Add(card);

            // Título
            Label lblTitle = new Label();
            lblTitle.Text = "GESTIÓN DE PRODUCTOS";
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 20);
            card.Controls.Add(lblTitle);

            // Campo Oculto ID (Para edición y eliminación)
            txtNewId = new Guna.UI2.WinForms.Guna2TextBox();
            txtNewId.Visible = false;
            card.Controls.Add(txtNewId);

            // Inputs con estilo VoltBite
            txtNewNombre = CrearInput("Nombre del Producto", 30, 60);
            card.Controls.Add(txtNewNombre);

            txtNewDescripcion = CrearInput("Descripción", 30, 110);
            card.Controls.Add(txtNewDescripcion);

            txtNewPrecio = CrearInput("Precio ($)", 30, 160);
            card.Controls.Add(txtNewPrecio);

            txtNewTiempo = CrearInput("Tiempo Prep. (min)", 30, 210);
            card.Controls.Add(txtNewTiempo);

            txtNewStock = CrearInput("Stock", 30, 260);
            card.Controls.Add(txtNewStock);

            // CheckBox Disponible
            chkNewDisponible = new Guna.UI2.WinForms.Guna2CheckBox();
            chkNewDisponible.Text = "Disponible para venta";
            chkNewDisponible.ForeColor = Color.White;
            chkNewDisponible.Font = new Font("Segoe UI", 9.5F);
            chkNewDisponible.CheckedState.FillColor = Color.FromArgb(255, 128, 0);
            chkNewDisponible.CheckedState.BorderColor = Color.FromArgb(255, 128, 0);
            chkNewDisponible.Location = new Point(30, 310);
            chkNewDisponible.AutoSize = true;
            card.Controls.Add(chkNewDisponible);

            // Botones de Acción
            Guna.UI2.WinForms.Guna2Button btnNewAgregar = CrearBoton("Agregar", 30, 355, 130, Color.FromArgb(255, 128, 0), Color.White);
            btnNewAgregar.Click += btnAgregar_Click;
            card.Controls.Add(btnNewAgregar);

            Guna.UI2.WinForms.Guna2Button btnNewEditar = CrearBoton("Editar", 175, 355, 135, Color.FromArgb(45, 45, 45), Color.White);
            btnNewEditar.Click += btnEditar_Click;
            card.Controls.Add(btnNewEditar);

            Guna.UI2.WinForms.Guna2Button btnNewEliminar = CrearBoton("Eliminar", 30, 410, 130, Color.FromArgb(180, 40, 40), Color.White);
            btnNewEliminar.Click += btnEliminar_Click;
            card.Controls.Add(btnNewEliminar);

            Guna.UI2.WinForms.Guna2Button btnNewBuscar = CrearBoton("Buscar", 175, 410, 135, Color.FromArgb(45, 45, 45), Color.White);
            btnNewBuscar.Click += btnBuscar_Click;
            card.Controls.Add(btnNewBuscar);

            // --- FILA INFERIOR DE BOTONES (LIMPIAR + REGRESAR) ---

            // Botón Limpiar Campos
            Guna.UI2.WinForms.Guna2Button btnNewLimpiar = CrearBoton("Limpiar", 30, 460, 130, Color.Transparent, Color.FromArgb(255, 128, 0));
            btnNewLimpiar.BorderColor = Color.FromArgb(255, 128, 0);
            btnNewLimpiar.BorderThickness = 1;
            btnNewLimpiar.Click += (s, e) => Limpiar();
            card.Controls.Add(btnNewLimpiar);

            // Botón Regresar / Volver
            Guna.UI2.WinForms.Guna2Button btnNewRegresar = CrearBoton("← Volver", 175, 460, 135, Color.FromArgb(45, 45, 45), Color.White);
            btnNewRegresar.Click += (s, e) =>
            {
                FrmMenuAdmin menu = new FrmMenuAdmin();
                menu.Show();
                this.Close();
            };
            card.Controls.Add(btnNewRegresar);

            // 3. TABLA MODERNA (Guna2DataGridView)
            dgvNewProductos = new Guna.UI2.WinForms.Guna2DataGridView();
            dgvNewProductos.Location = new Point(380, 20);
            dgvNewProductos.Size = new Size(530, 520);
            dgvNewProductos.BackgroundColor = Color.FromArgb(28, 28, 28);
            dgvNewProductos.BorderStyle = BorderStyle.None;
            dgvNewProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNewProductos.MultiSelect = false;
            dgvNewProductos.ReadOnly = true;
            dgvNewProductos.AllowUserToAddRows = false;

            // Estilos de filas
            dgvNewProductos.GridColor = Color.FromArgb(40, 40, 40);
            dgvNewProductos.ThemeStyle.RowsStyle.BackColor = Color.FromArgb(35, 35, 35);
            dgvNewProductos.ThemeStyle.RowsStyle.ForeColor = Color.White;
            dgvNewProductos.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgvNewProductos.ThemeStyle.RowsStyle.SelectionForeColor = Color.White;

            dgvNewProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(28, 28, 28);
            dgvNewProductos.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;
            dgvNewProductos.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgvNewProductos.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;

            // Encabezados
            dgvNewProductos.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(255, 128, 0);
            dgvNewProductos.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvNewProductos.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvNewProductos.ThemeStyle.HeaderStyle.Height = 35;

            dgvNewProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNewProductos.SelectionChanged += dgvProductos_SelectionChanged;
            this.Controls.Add(dgvNewProductos);
        }

        private Guna.UI2.WinForms.Guna2TextBox CrearInput(string placeholder, int x, int y)
        {
            Guna.UI2.WinForms.Guna2TextBox txt = new Guna.UI2.WinForms.Guna2TextBox();
            txt.PlaceholderText = placeholder;
            txt.Size = new Size(280, 36);
            txt.Location = new Point(x, y);
            txt.BorderRadius = 8;
            txt.FillColor = Color.FromArgb(40, 40, 40);
            txt.BorderColor = Color.FromArgb(255, 128, 0);
            txt.ForeColor = Color.White;
            return txt;
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
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            return btn;
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void CargarProductos()
        {
            ProductoDAO dao = new ProductoDAO();
            dgvNewProductos.DataSource = dao.Listar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string tiempoTexto = System.Text.RegularExpressions.Regex.Replace(txtNewTiempo.Text, @"[^\d]", "");
            string precioTexto = txtNewPrecio.Text.Replace("$", "").Trim();
            string stockTexto = System.Text.RegularExpressions.Regex.Replace(txtNewStock.Text, @"[^\d]", "");

            if (!decimal.TryParse(precioTexto, out decimal precio) ||
                !int.TryParse(tiempoTexto, out int tiempo) ||
                !int.TryParse(stockTexto, out int stock))
            {
                Toast.Advertencia(this, "Asegúrese de ingresar montos válidos en los campos de precio, tiempo y stock.");
                return;
            }

            Producto p = new Producto
            {
                Nombre = txtNewNombre.Text,
                Descripcion = txtNewDescripcion.Text,
                Precio = precio,
                TiempoPreparacion = tiempo,
                Stock = stock,
                Disponible = chkNewDisponible.Checked
            };

            ProductoDAO dao = new ProductoDAO();
            dao.Agregar(p);

            Toast.Exito(this, "Producto agregado correctamente.");

            CargarProductos();
            Limpiar();
        }

        private void Limpiar()
        {
            txtNewId.Clear();
            txtNewNombre.Clear();
            txtNewDescripcion.Clear();
            txtNewPrecio.Clear();
            txtNewTiempo.Clear();
            txtNewStock.Clear();
            chkNewDisponible.Checked = false;
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNewProductos.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvNewProductos.SelectedRows[0];
                string colId = dgvNewProductos.Columns.Contains("Id") ? "Id" : (dgvNewProductos.Columns.Contains("id") ? "id" : null);

                if (colId != null && fila.Cells[colId].Value != null)
                {
                    txtNewId.Text = fila.Cells[colId].Value.ToString();
                    txtNewNombre.Text = fila.Cells["Nombre"].Value?.ToString();
                    txtNewDescripcion.Text = fila.Cells["Descripcion"].Value?.ToString();
                    txtNewPrecio.Text = fila.Cells["Precio"].Value?.ToString();
                    txtNewTiempo.Text = fila.Cells["TiempoPreparacion"].Value?.ToString();
                    txtNewStock.Text = fila.Cells["Stock"].Value?.ToString();
                    chkNewDisponible.Checked = Convert.ToBoolean(fila.Cells["Disponible"].Value);
                }
                else if (fila.Cells.Count > 0 && fila.Cells[0].Value != null)
                {
                    txtNewId.Text = fila.Cells[0].Value.ToString();
                    txtNewNombre.Text = fila.Cells[1].Value?.ToString();
                    txtNewDescripcion.Text = fila.Cells[2].Value?.ToString();
                    txtNewPrecio.Text = fila.Cells[3].Value?.ToString();
                    txtNewTiempo.Text = fila.Cells[4].Value?.ToString();
                    txtNewStock.Text = fila.Cells[5].Value?.ToString();
                    chkNewDisponible.Checked = Convert.ToBoolean(fila.Cells[6].Value);
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewId.Text))
            {
                Toast.Advertencia(this, "Seleccione un producto de la tabla antes de editar.");
                return;
            }

            if (!decimal.TryParse(txtNewPrecio.Text, out decimal precio) ||
                !int.TryParse(txtNewTiempo.Text, out int tiempo) ||
                !int.TryParse(txtNewStock.Text, out int stock))
            {
                Toast.Advertencia(this, "Verifique que los campos numéricos sean correctos.");
                return;
            }

            Producto p = new Producto
            {
                Id = Convert.ToInt32(txtNewId.Text),
                Nombre = txtNewNombre.Text,
                Descripcion = txtNewDescripcion.Text,
                Precio = precio,
                TiempoPreparacion = tiempo,
                Stock = stock,
                Disponible = chkNewDisponible.Checked
            };

            ProductoDAO dao = new ProductoDAO();
            dao.Editar(p);

            Toast.Exito(this, "Producto actualizado correctamente.");

            CargarProductos();
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewId.Text) && dgvNewProductos.SelectedRows.Count > 0)
            {
                var fila = dgvNewProductos.SelectedRows[0];
                txtNewId.Text = fila.Cells[0].Value?.ToString();
            }

            if (string.IsNullOrWhiteSpace(txtNewId.Text))
            {
                Toast.Advertencia(this, "Seleccione un producto de la tabla antes de eliminar.");
                return;
            }

            try
            {
                ProductoDAO dao = new ProductoDAO();

                if (int.TryParse(txtNewId.Text, out int idEliminar))
                {
                    dao.Eliminar(idEliminar);
                    Toast.Exito(this, "Producto deshabilitado correctamente.");

                    CargarProductos();
                    Limpiar();
                }
            }
            catch (Exception ex)
            {
                Toast.Error(this, "Ocurrió un error al intentar deshabilitar el producto: " + ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ProductoDAO dao = new ProductoDAO();
            dgvNewProductos.DataSource = dao.Buscar(txtNewNombre.Text);
        }
    }
}