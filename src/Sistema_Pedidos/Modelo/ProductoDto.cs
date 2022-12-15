using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Pedidos
{
    public class ProductoDto
    {
        public int Id_Producto { get; set; }
        public string Nombre_Producto { get; set; }
        public string Descripcion { get; set; }
        public string Precio_Venta { get; set; }
        public int Id_Marca { get; set; }
        public int Id_Modelo { get; set; }
        public int Id_Proveedor { get; set; }
        public int Stock { get; set; }
        public Nullable<bool> Activo { get; set; }
    }
}
