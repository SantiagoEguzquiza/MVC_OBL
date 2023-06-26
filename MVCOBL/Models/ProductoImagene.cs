using System;
using System.Collections.Generic;

namespace MVCOBL.Models
{
    public partial class ProductoImagene
    {
        public int Id { get; set; }
        public int? IdProducto { get; set; }
        public string? LinkUrl { get; set; }

        public virtual Producto? IdProductoNavigation { get; set; }
    }
}
