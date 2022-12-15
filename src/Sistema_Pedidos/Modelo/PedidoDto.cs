using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Pedidos
{
    public class PedidoDto
    {
        public int Id_Pedido { get; set; }
        public int Id_Cliente { get; set; }
        public int Id_Empleado { get; set; }
        public DateTime Fecha_Pedido { get; set; }
        public DateTime Fecha_MaxEntrega { get; set; }
        public decimal Valor_Total { get; set; }
        public Nullable<bool> Activo { get; set; }
    }
}
