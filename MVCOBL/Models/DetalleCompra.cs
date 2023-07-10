using System;
using System.Collections.Generic;

namespace MVCOBL.Models
{
    public partial class DetalleCompra
    {
        public int IdDetalleCompra { get; set; }
        public int? IdCompra { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitarioCompra { get; set; }
        public decimal? TotalCosto { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int? IdCotizacion { get; set; }

        public virtual Compra? IdCompraNavigation { get; set; }
        public virtual Cotizacione? IdCotizacionNavigation { get; set; }
        public virtual Producto? IdProductoNavigation { get; set; }
    }
}
