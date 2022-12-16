using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Pedidos
{
    public class Detalle_PedidoDto
    {
        public int ID_Detalle { get; set; }
        public int Id_Pedido { get; set; }
        public int Id_Producto { get; set; }
        public string Producto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio_Venta { get; set; }
        public decimal? Subtotal_Prod { get; set; }
    }
}
