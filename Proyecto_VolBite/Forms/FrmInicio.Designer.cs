namespace Proyecto_VolBite
{
    partial class FrmInicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInicio));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnAdministrador = new System.Windows.Forms.Button();
            this.btnCliente = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnEmpleado = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(270, 25);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(107, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "VOLBITE";
            // 
            // btnAdministrador
            // 
            this.btnAdministrador.Location = new System.Drawing.Point(248, 285);
            this.btnAdministrador.Name = "btnAdministrador";
            this.btnAdministrador.Size = new System.Drawing.Size(75, 23);
            this.btnAdministrador.TabIndex = 1;
            this.btnAdministrador.Text = "Empleado";
            this.btnAdministrador.UseVisualStyleBackColor = true;
            this.btnAdministrador.Click += new System.EventHandler(this.btnAdministrador_Click);
            // 
            // btnCliente
            // 
            this.btnCliente.Location = new System.Drawing.Point(248, 342);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(75, 23);
            this.btnCliente.TabIndex = 2;
            this.btnCliente.Text = "Cliente";
            this.btnCliente.UseVisualStyleBackColor = true;
            this.btnCliente.Click += new System.EventHandler(this.btnCliente_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::Proyecto_VolBite.Properties.Resources.Adobe_Express___file;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(165, 53);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(315, 198);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // btnEmpleado
            // 
            this.btnEmpleado.BorderRadius = 15;
            this.btnEmpleado.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEmpleado.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEmpleado.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEmpleado.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEmpleado.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(107)))), ((int)(((byte)(26)))));
            this.btnEmpleado.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEmpleado.ForeColor = System.Drawing.Color.White;
            this.btnEmpleado.Location = new System.Drawing.Point(351, 285);
            this.btnEmpleado.Name = "btnEmpleado";
            this.btnEmpleado.Size = new System.Drawing.Size(180, 45);
            this.btnEmpleado.TabIndex = 4;
            this.btnEmpleado.Text = "Empleado";
            // 
            // FrmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(653, 436);
            this.Controls.Add(this.btnEmpleado);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnCliente);
            this.Controls.Add(this.btnAdministrador);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmInicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VolBite";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnAdministrador;
        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2Button btnEmpleado;
    }
}

