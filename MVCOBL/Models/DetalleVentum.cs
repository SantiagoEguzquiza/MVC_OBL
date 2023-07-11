using System;
using System.Collections.Generic;

namespace MVCOBL.Models
{
    public partial class DetalleVentum
    {
        public int IdDetalleVenta { get; set; }
        public int? IdVenta { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnidad { get; set; }
        public decimal? ImporteTotal { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int? IdCotizacion { get; set; }

        public virtual Producto? IdProductoNavigation { get; set; }
        public virtual Ventum? IdVentaNavigation { get; set; }
    }
}
