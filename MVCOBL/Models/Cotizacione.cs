using System;
using System.Collections.Generic;

namespace MVCOBL.Models
{
    public partial class Cotizacione
    {
        public Cotizacione()
        {
            Productos = new HashSet<Producto>();
        }

        public int Id { get; set; }
        public string? TipoMoneda { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? ValorMoneda { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
