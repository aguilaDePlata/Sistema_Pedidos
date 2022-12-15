using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Pedidos
{
    public class ComprobanteDto
    {
        public int Id_Comprobante { get; set; }
        public int Id_Pedido { get; set; }
        public string Tipo_Comp { get; set; }
        public DateTime Fecha_Emision { get; set; }
        public decimal Subtotal_Comp { get; set; }
        public decimal Descuento { get; set; }
        public decimal Igv { get; set; }
        public decimal Valor_Total { get; set; }
    }
}
