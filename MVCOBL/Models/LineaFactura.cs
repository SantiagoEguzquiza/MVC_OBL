using System;
using System.Collections.Generic;

namespace MVCOBL.Models
{
    public partial class LineaFactura
    {
        public int Id { get; set; }
        public int? Cantidad { get; set; }
        public int? Precio { get; set; }
        public int? IdFactura { get; set; }
        public int? IdProducto { get; set; }

        public virtual Factura? IdFacturaNavigation { get; set; }
        public virtual Producto? IdProductoNavigation { get; set; }
    }
}
