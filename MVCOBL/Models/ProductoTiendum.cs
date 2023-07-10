using System;
using System.Collections.Generic;

namespace MVCOBL.Models
{
    public partial class ProductoTiendum
    {
        public int IdProductoTienda { get; set; }
        public int? IdProducto { get; set; }
        public int? IdTienda { get; set; }
        public decimal? PrecioUnidadCompra { get; set; }
        public decimal? PrecioUnidadVenta { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual Producto? IdProductoNavigation { get; set; }
        public virtual Tiendum? IdTiendaNavigation { get; set; }
    }
}
