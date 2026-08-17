namespace Proyecto_VolBite.Forms
{
    partial class FrmCatalogoProductos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCatalogoProductos));
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.btnAgregarCarrito = new System.Windows.Forms.Button();
            this.btnCarrito = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProductos
            // 
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(45, 63);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.Size = new System.Drawing.Size(654, 155);
            this.dgvProductos.TabIndex = 0;
            // 
            // nudCantidad
            // 
            this.nudCantidad.Location = new System.Drawing.Point(313, 244);
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(99, 20);
            this.nudCantidad.TabIndex = 1;
            // 
            // btnAgregarCarrito
            // 
            this.btnAgregarCarrito.Location = new System.Drawing.Point(290, 302);
            this.btnAgregarCarrito.Name = "btnAgregarCarrito";
            this.btnAgregarCarrito.Size = new System.Drawing.Size(141, 23);
            this.btnAgregarCarrito.TabIndex = 2;
            this.btnAgregarCarrito.Text = "Agregar al carrito";
            this.btnAgregarCarrito.UseVisualStyleBackColor = true;
            this.btnAgregarCarrito.Click += new System.EventHandler(this.btnAgregarCarrito_Click);
            // 
            // btnCarrito
            // 
            this.btnCarrito.Location = new System.Drawing.Point(290, 346);
            this.btnCarrito.Name = "btnCarrito";
            this.btnCarrito.Size = new System.Drawing.Size(141, 23);
            this.btnCarrito.TabIndex = 3;
            this.btnCarrito.Text = "Ir al carrito\n";
            this.btnCarrito.UseVisualStyleBackColor = true;
            this.btnCarrito.Click += new System.EventHandler(this.btnCarrito_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(290, 386);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(141, 23);
            this.btnCerrar.TabIndex = 4;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            // 
            // FrmCatalogoProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(735, 450);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnCarrito);
            this.Controls.Add(this.btnAgregarCarrito);
            this.Controls.Add(this.nudCantidad);
            this.Controls.Add(this.dgvProductos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCatalogoProductos";
            this.Text = "Catalogo Productos";
            this.Load += new System.EventHandler(this.FrmCatalogoProductos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Button btnAgregarCarrito;
        private System.Windows.Forms.Button btnCarrito;
        private System.Windows.Forms.Button btnCerrar;
    }
}