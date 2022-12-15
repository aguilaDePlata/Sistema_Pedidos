using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Pedidos
{
    public class ProveedorDto
    {
        public int Id_Proveedor { get; set; }
        public string Nombres_Prov { get; set; }
        public string Apellidos_Prov { get; set; }
        public string Direccion { get; set; }
        public string Distrito { get; set; }
        public string Telefono { get; set; }
        public Nullable<bool> Activo { get; set; }
    }
}
