using System;
using System.Collections.Generic;

namespace MVCOBL.Models
{
    public partial class Ventum
    {
        public Ventum()
        {
            DetalleVenta = new HashSet<DetalleVentum>();
        }

        public int IdVenta { get; set; }
        public int? IdTienda { get; set; }
        public string? IdUsuario { get; set; }
        public int? IdCliente { get; set; }
        public decimal? TotalCosto { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual Tiendum? IdTiendaNavigation { get; set; }
        public virtual AspNetUser? IdUsuarioNavigation { get; set; }
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
