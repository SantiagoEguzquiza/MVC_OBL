using System;
using System.Collections.Generic;

namespace MVCOBL.Models
{
    public partial class Cotizacione
    {
        public Cotizacione()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
            DetalleVenta = new HashSet<DetalleVentum>();
            Facturas = new HashSet<Factura>();
        }

        public int Id { get; set; }
        public string? TipoMoneda { get; set; }
        public int? ValorMoneda { get; set; }

        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
