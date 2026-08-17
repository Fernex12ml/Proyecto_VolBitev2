using System;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto_VolBite.Clases
{
    public static class Toast
    {
        public static void Exito(Form owner, string mensaje)
        {
            Mostrar(owner, mensaje, Color.FromArgb(40, 167, 69), "✔ Success");
        }

        public static void Advertencia(Form owner, string mensaje)
        {
            Mostrar(owner, mensaje, Color.FromArgb(255, 193, 7), "⚠ Warning", Color.Black);
        }

        public static void Error(Form owner, string mensaje)
        {
            Mostrar(owner, mensaje, Color.FromArgb(220, 53, 69), "✖ Error");
        }

        public static DialogResult Confirmar(Form owner, string mensaje, string titulo = "Confirmar")
        {
            return MessageBox.Show(owner, mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private static void Mostrar(Form owner, string mensaje, Color colorFondo, string titulo, Color? colorTexto = null)
        {
            Form toast = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.CenterParent,
                BackColor = colorFondo,
                Size = new Size(320, 80),
                ShowInTaskbar = false,
                TopMost = true
            };

            Label lbl = new Label
            {
                Text = $"{titulo}\n{mensaje}",
                ForeColor = colorTexto ?? Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            toast.Controls.Add(lbl);

            Timer timer = new Timer { Interval = 2500 };
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                toast.Close();
            };

            timer.Start();
            toast.ShowDialog(owner);
        }
    }
}