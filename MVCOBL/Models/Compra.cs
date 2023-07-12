using System;
using System.Collections.Generic;

namespace MVCOBL.Models
{
    public partial class Compra
    {
        public Compra()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
        }

        public int IdCompra { get; set; }
        public string? IdUsuario { get; set; }
        public int? IdTienda { get; set; }
        public decimal? TotalCosto { get; set; }
        public string? TipoComprobante { get; set; }
        public DateTime FechaRegistro { get; set; }

        public virtual Tiendum? IdTiendaNavigation { get; set; }
        public virtual AspNetUser? IdUsuarioNavigation { get; set; }
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
    }
}
