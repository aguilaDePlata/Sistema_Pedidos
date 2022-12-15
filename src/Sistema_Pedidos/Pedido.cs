//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sistema_Pedidos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pedido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pedido()
        {
            this.Comprobante = new HashSet<Comprobante>();
            this.Detalle_Pedido = new HashSet<Detalle_Pedido>();
        }
    
        public int ID_Pedido { get; set; }
        public Nullable<int> ID_Cliente { get; set; }
        public Nullable<int> ID_Empleado { get; set; }
        public Nullable<System.DateTime> Fecha_Pedido { get; set; }
        public Nullable<System.DateTime> Fecha_MaxEntrega { get; set; }
        public Nullable<decimal> Valor_Total { get; set; }
        public Nullable<bool> Activo { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comprobante> Comprobante { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalle_Pedido> Detalle_Pedido { get; set; }
        public virtual Empleado Empleado { get; set; }
    }
}
