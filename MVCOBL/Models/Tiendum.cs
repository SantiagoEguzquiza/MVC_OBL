using System;
using System.Collections.Generic;

namespace MVCOBL.Models
{
    public partial class Tiendum
    {
        public Tiendum()
        {
            AspNetUsers = new HashSet<AspNetUser>();
            Compras = new HashSet<Compra>();
            ProductoTienda = new HashSet<ProductoTiendum>();
            Venta = new HashSet<Ventum>();
        }

        public int IdTienda { get; set; }
        public string? Nombre { get; set; }
        public string? Ruc { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<ProductoTiendum> ProductoTienda { get; set; }
        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
