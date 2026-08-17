namespace Proyecto_VolBite.Forms
{
    partial class FrmEstadoPedido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEstadoPedido));
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblPedido = new System.Windows.Forms.Label();
            this.lblTiempo = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(236, 79);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(40, 13);
            this.lblEstado.TabIndex = 0;
            this.lblEstado.Text = "Estado";
            // 
            // lblPedido
            // 
            this.lblPedido.AutoSize = true;
            this.lblPedido.Location = new System.Drawing.Point(236, 35);
            this.lblPedido.Name = "lblPedido";
            this.lblPedido.Size = new System.Drawing.Size(40, 13);
            this.lblPedido.TabIndex = 1;
            this.lblPedido.Text = "Pedido";
            // 
            // lblTiempo
            // 
            this.lblTiempo.AutoSize = true;
            this.lblTiempo.Location = new System.Drawing.Point(236, 145);
            this.lblTiempo.Name = "lblTiempo";
            this.lblTiempo.Size = new System.Drawing.Size(42, 13);
            this.lblTiempo.TabIndex = 2;
            this.lblTiempo.Text = "Tiempo";
            // 
            // btnAceptar
            // 
            this.btnAceptar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAceptar.Location = new System.Drawing.Point(239, 218);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 3;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            // 
            // FrmEstadoPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.lblTiempo);
            this.Controls.Add(this.lblPedido);
            this.Controls.Add(this.lblEstado);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmEstadoPedido";
            this.Text = "Estado del Pedido";
            this.Load += new System.EventHandler(this.FrmEstadoPedido_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label lblPedido;
        private System.Windows.Forms.Label lblTiempo;
        private System.Windows.Forms.Button btnAceptar;
    }
}