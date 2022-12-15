using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Pedidos
{
    public class EmpleadoDto
    {
        public int Id_Empleado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Dni { get; set; }
        public string Direccion { get; set; }
        public string Distrito { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public int Id_Cargo { get; set; }
        public DateTime Fecha_Contrato { get; set; }
        public Nullable<bool> Activo { get; set; }
    }
}
