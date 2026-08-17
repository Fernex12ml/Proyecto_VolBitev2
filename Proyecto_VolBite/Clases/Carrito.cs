using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_VolBite.Clases
{
    public static class Carrito
    {
        public static List<CarritoItem> Productos =
            new List<CarritoItem>();

        public static decimal Total()
        {
            return Productos.Sum(x => x.Subtotal);
        }

        public static int TiempoEstimado()
        {
            if (Productos.Count == 0)
                return 0;

            return Productos.Max(x => x.TiempoPreparacion);
        }

        public static void Vaciar()
        {
            Productos.Clear();
        }
    }
}
