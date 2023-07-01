using System;
using System.Collections.Generic;

namespace MVCOBL.Models
{
    public partial class Factura
    {
        public Factura()
        {
            LineaFacturas = new HashSet<LineaFactura>();
        }

        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public string? TipoFactura { get; set; }
        public int? IdCliente { get; set; }
        public int? Cotizacion { get; set; }

        public virtual Cotizacione? CotizacionNavigation { get; set; }
        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual ICollection<LineaFactura> LineaFacturas { get; set; }
    }
}
