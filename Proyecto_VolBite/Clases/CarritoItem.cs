using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_VolBite.Clases
{
    public class CarritoItem
    {
        public int IdProducto { get; set; }

        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

        public int TiempoPreparacion { get; set; }

        public decimal Subtotal
        {
            get
            {
                return Precio * Cantidad;
            }
        }
    }
}
