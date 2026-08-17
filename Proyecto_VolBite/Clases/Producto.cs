using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_VolBite.Clases
{
    public class Producto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int TiempoPreparacion { get; set; }

        public bool Disponible { get; set; }

        public int Stock { get; set; }
    }
}
