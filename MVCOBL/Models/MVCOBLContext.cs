using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MVCOBL.Models
{
    public partial class MVCOBLContext : DbContext
    {
        public MVCOBLContext()
        {
        }

        public MVCOBLContext(DbContextOptions<MVCOBLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Categorium> Categoria { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<Cotizacione> Cotizaciones { get; set; } = null!;
        public virtual DbSet<DetalleCompra> DetalleCompras { get; set; } = null!;
        public virtual DbSet<DetalleVentum> DetalleVenta { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<ProductoTiendum> ProductoTienda { get; set; } = null!;
        public virtual DbSet<Tiendum> Tienda { get; set; } = null!;
        public virtual DbSet<Ventum> Venta { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-OK60JF3; Database= MVCOBL;Integrated Security=True; TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.IdTiendaNavigation)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.IdTienda)
                    .HasConstraintName("FK__AspNetUse__IdTie__68487DD7");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__CATEGORI__A3C02A105058EAE2");

                entity.ToTable("CATEGORIA");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__CLIENTE__D5946642AE8012B2");

                entity.ToTable("CLIENTE");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.IdCompra)
                    .HasName("PK__COMPRA__0A5CDB5CA0C3D992");

                entity.ToTable("COMPRA");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdUsuario).HasMaxLength(450);

                entity.Property(e => e.TipoComprobante)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Boleta')");

                entity.Property(e => e.TotalCosto)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdTiendaNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdTienda)
                    .HasConstraintName("FK__COMPRA__IdTienda__6A30C649");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__COMPRA__IdUsuari__6B24EA82");
            });

            modelBuilder.Entity<Cotizacione>(entity =>
            {
                entity.ToTable("COTIZACIONES");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.TipoMoneda)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValorMoneda).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<DetalleCompra>(entity =>
            {
                entity.HasKey(e => e.IdDetalleCompra)
                    .HasName("PK__DETALLE___E046CCBBA85EE6F5");

                entity.ToTable("DETALLE_COMPRA");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Moneda)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioUnitarioCompra).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalCosto).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdCompraNavigation)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.IdCompra)
                    .HasConstraintName("FK__DETALLE_C__IdCom__6C190EBB");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__DETALLE_C__IdPro__6E01572D");
            });

            modelBuilder.Entity<DetalleVentum>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVenta)
                    .HasName("PK__DETALLE___AAA5CEC2BD5EDF12");

                entity.ToTable("DETALLE_VENTA");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImporteTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Moneda)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioUnidad).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__DETALLE_V__IdPro__6FE99F9F");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("FK__DETALLE_V__IdVen__70DDC3D8");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__PRODUCTO__09889210124C81ED");

                entity.ToTable("PRODUCTO");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImagenUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Moneda)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__PRODUCTO__IdCate__71D1E811");

                entity.HasOne(d => d.IdCotizacionNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCotizacion)
                    .HasConstraintName("FK__PRODUCTO__IdCoti__0B91BA14");
            });

            modelBuilder.Entity<ProductoTiendum>(entity =>
            {
                entity.HasKey(e => e.IdProductoTienda)
                    .HasName("PK__PRODUCTO__CE9B4C833423FBB8");

                entity.ToTable("PRODUCTO_TIENDA");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PrecioUnidadCompra)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PrecioUnidadVenta)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.ProductoTienda)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__PRODUCTO___IdPro__72C60C4A");

                entity.HasOne(d => d.IdTiendaNavigation)
                    .WithMany(p => p.ProductoTienda)
                    .HasForeignKey(d => d.IdTienda)
                    .HasConstraintName("FK__PRODUCTO___IdTie__73BA3083");
            });

            modelBuilder.Entity<Tiendum>(entity =>
            {
                entity.HasKey(e => e.IdTienda)
                    .HasName("PK__TIENDA__5A1EB96B73C8EBE5");

                entity.ToTable("TIENDA");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Ruc)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("RUC");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ventum>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__VENTA__BC1240BD89BE04A5");

                entity.ToTable("VENTA");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdUsuario).HasMaxLength(450);

                entity.Property(e => e.TotalCosto).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__VENTA__IdCliente__74AE54BC");

                entity.HasOne(d => d.IdTiendaNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdTienda)
                    .HasConstraintName("FK__VENTA__IdTienda__75A278F5");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__VENTA__IdUsuario__76969D2E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
