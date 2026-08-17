using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_VolBite.Forms;

namespace Proyecto_VolBite
{
    public partial class FrmInicio : Form
    {
        public FrmInicio()
        {
            InitializeComponent();
            this.btnEmpleado.Click += new System.EventHandler(this.btnAdministrador_Click);
            this.btnAdministrador.Visible = false;
            btnEmpleado.Text = "Administrador";


            Guna.UI2.WinForms.Guna2Button btnClienteNaranja = new Guna.UI2.WinForms.Guna2Button();
            btnClienteNaranja.Text = "Cliente";
            btnClienteNaranja.BorderRadius = this.btnEmpleado.BorderRadius; // Misma redondez
            btnClienteNaranja.FillColor = this.btnEmpleado.FillColor; // Mismo color naranja
            btnClienteNaranja.ForeColor = System.Drawing.Color.White;
            btnClienteNaranja.Font = this.btnEmpleado.Font;

            // ===================================================
            // BOTÓN "REGISTRARSE" (Guna2Button)
            // ===================================================
            Guna.UI2.WinForms.Guna2Button btnRegistrarse = new Guna.UI2.WinForms.Guna2Button();
            btnRegistrarse.Text = "Registrarse";
            btnRegistrarse.Size = new System.Drawing.Size(260, 42);
            btnRegistrarse.Location = new System.Drawing.Point(40, 330); // Ajusta la posición si lo necesitas
            btnRegistrarse.BorderRadius = 8;
            btnRegistrarse.FillColor = System.Drawing.Color.FromArgb(255, 128, 0); // Color Naranja
            btnRegistrarse.ForeColor = System.Drawing.Color.White;
            btnRegistrarse.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);

            btnRegistrarse.Size = this.btnEmpleado.Size; // Mismo tamaño que Administrador y Cliente
            btnRegistrarse.BorderRadius = this.btnEmpleado.BorderRadius;
            btnRegistrarse.FillColor = this.btnEmpleado.FillColor;
            btnRegistrarse.ForeColor = System.Drawing.Color.White;
            btnRegistrarse.Font = this.btnEmpleado.Font;

            // Evento al hacer clic en Registrarse
            btnRegistrarse.Click += (s, e) =>
            {
                Proyecto_VolBite.Forms.FrmRegistro frm = new Proyecto_VolBite.Forms.FrmRegistro();
                frm.ShowDialog();
            };

            // Agregar el botón a la pantalla
            this.Controls.Add(btnRegistrarse);


            // Conectar el clic y ocultar el botón viejo
            btnClienteNaranja.Click += new System.EventHandler(this.btnCliente_Click);
            this.btnCliente.Visible = false;

            // Agregar el nuevo botón a la pantalla
            this.Controls.Add(btnClienteNaranja);

            

           

            int centroX = (this.ClientSize.Width - this.btnEmpleado.Width) / 2;

            // Aplica el centro a ambos botones
            this.btnEmpleado.Location = new System.Drawing.Point(centroX, 290);
            btnClienteNaranja.Location = new System.Drawing.Point(centroX, 345);
            btnRegistrarse.Location = new System.Drawing.Point(centroX, 400);

            this.lblTitulo.Text = "VOLTBITE";

            // Cambia el título de la ventana (la barra superior de Windows)
            this.Text = "VOLTBITE";
        }

        private void btnAdministrador_Click(object sender, EventArgs e)
        {
            FrmLoginAdmin frm = new FrmLoginAdmin();

            frm.Show();

            this.Hide();
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            FrmLoginCliente frm = new FrmLoginCliente();

            frm.Show();

            this.Hide();
        }
    }
}
