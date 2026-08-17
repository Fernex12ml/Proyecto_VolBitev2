using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_VolBite.Clases
{
    public class Pedido
    {
        public int Id { get; set; }
        public int IdPedido { get => Id; set => Id = value; } // Para compatibilidad
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int TiempoEstimado { get; set; }
        public string Estado { get; set; }
        public int IdUsuario { get; set; }
        public string TipoPago { get; set; } // 👈 Agregamos esta propiedad
    }
}