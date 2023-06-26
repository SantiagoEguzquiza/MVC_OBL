using System;
using System.Collections.Generic;

namespace MVCOBL.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
            DetalleVenta = new HashSet<DetalleVentum>();
            ProductoImagenes = new HashSet<ProductoImagene>();
            ProductoTienda = new HashSet<ProductoTiendum>();
        }

        public int IdProducto { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? IdCategoria { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual Categorium? IdCategoriaNavigation { get; set; }
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
        public virtual ICollection<ProductoImagene> ProductoImagenes { get; set; }
        public virtual ICollection<ProductoTiendum> ProductoTienda { get; set; }
    }
}
