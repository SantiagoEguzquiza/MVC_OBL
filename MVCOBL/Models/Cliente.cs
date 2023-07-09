using System;
using System.Collections.Generic;

namespace MVCOBL.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Venta = new HashSet<Ventum>();
        }

        public int IdCliente { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
