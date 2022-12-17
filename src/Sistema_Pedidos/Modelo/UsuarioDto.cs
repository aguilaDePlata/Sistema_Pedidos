using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Pedidos
{
    public class UsuarioDto
    {
        public int ID_Usuario { get; set; }
        public int? ID_Empleado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Usuario1 { get; set; }
        public string Password { get; set; }
    }
}
