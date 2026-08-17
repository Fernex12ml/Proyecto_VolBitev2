namespace Proyecto_VolBite.Forms
{
    partial class FrmLoginCliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLoginCliente));
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnAdministrador = new System.Windows.Forms.Button();
            this.btnEntrar = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.lblContraseña = new System.Windows.Forms.Label();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(331, 326);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(127, 23);
            this.btnVolver.TabIndex = 13;
            this.btnVolver.Text = "Regresar";
            this.btnVolver.UseVisualStyleBackColor = true;
            
            // 
            // btnAdministrador
            // 
            this.btnAdministrador.Location = new System.Drawing.Point(331, 287);
            this.btnAdministrador.Name = "btnAdministrador";
            this.btnAdministrador.Size = new System.Drawing.Size(127, 23);
            this.btnAdministrador.TabIndex = 12;
            this.btnAdministrador.Text = "Soy Administrador\r\n";
            this.btnAdministrador.UseVisualStyleBackColor = true;
            this.btnAdministrador.Click += new System.EventHandler(this.btnAdministrador_Click);
            // 
            // btnEntrar
            // 
            this.btnEntrar.Location = new System.Drawing.Point(331, 247);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.Size = new System.Drawing.Size(127, 23);
            this.btnEntrar.TabIndex = 11;
            this.btnEntrar.Text = "Entrar";
            this.btnEntrar.UseVisualStyleBackColor = true;
            this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(395, 158);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 10;
            // 
            // txtCorreo
            // 
            this.txtCorreo.Location = new System.Drawing.Point(395, 91);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(100, 20);
            this.txtCorreo.TabIndex = 9;
            // 
            // lblContraseña
            // 
            this.lblContraseña.AutoSize = true;
            this.lblContraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContraseña.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblContraseña.Location = new System.Drawing.Point(305, 165);
            this.lblContraseña.Name = "lblContraseña";
            this.lblContraseña.Size = new System.Drawing.Size(75, 13);
            this.lblContraseña.TabIndex = 8;
            this.lblContraseña.Text = "Contraseña;";
            // 
            // lblCorreo
            // 
            this.lblCorreo.AutoSize = true;
            this.lblCorreo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorreo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblCorreo.Location = new System.Drawing.Point(328, 94);
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.Size = new System.Drawing.Size(48, 13);
            this.lblCorreo.TabIndex = 7;
            this.lblCorreo.Text = "Correo:";
            // 
            // FrmLoginCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnAdministrador);
            this.Controls.Add(this.btnEntrar);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.lblContraseña);
            this.Controls.Add(this.lblCorreo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLoginCliente";
            this.Text = "Iniciar Sesión Cliente";
            this.Load += new System.EventHandler(this.FrmLoginCliente_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnAdministrador;
        private System.Windows.Forms.Button btnEntrar;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblContraseña;
        private System.Windows.Forms.Label lblCorreo;
    }
}