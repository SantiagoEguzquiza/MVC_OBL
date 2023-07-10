using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCOBL.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_producto",
                table: "producto");

            migrationBuilder.RenameTable(
                name: "producto",
                newName: "PRODUCTO");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "PRODUCTO",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PRODUCTO",
                newName: "IdProducto");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "PRODUCTO",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "PRODUCTO",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "PRODUCTO",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRegistro",
                table: "PRODUCTO",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<int>(
                name: "IdCategoria",
                table: "PRODUCTO",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagenUrl",
                table: "PRODUCTO",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Precio",
                table: "PRODUCTO",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "stock",
                table: "PRODUCTO",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdTienda",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__PRODUCTO__09889210FBEFDAD7",
                table: "PRODUCTO",
                column: "IdProducto");

            migrationBuilder.CreateTable(
                name: "CATEGORIA",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CATEGORI__A3C02A104647F967", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "CLIENTE",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroDocumento = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Direccion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Telefono = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CLIENTE__D5946642D954E764", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "COTIZACIONES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoMoneda = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ValorMoneda = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COTIZACIONES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TIENDA",
                columns: table => new
                {
                    IdTienda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true),
                    RUC = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true),
                    Direccion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TIENDA__5A1EB96B4A89AC14", x => x.IdTienda);
                });

            migrationBuilder.CreateTable(
                name: "COMPRA",
                columns: table => new
                {
                    IdCompra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    IdTienda = table.Column<int>(type: "int", nullable: true),
                    TotalCosto = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValueSql: "((0))"),
                    TipoComprobante = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValueSql: "('Boleta')"),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__COMPRA__0A5CDB5CF84FCFBD", x => x.IdCompra);
                    table.ForeignKey(
                        name: "FK__COMPRA__IdTienda__797309D9",
                        column: x => x.IdTienda,
                        principalTable: "TIENDA",
                        principalColumn: "IdTienda");
                    table.ForeignKey(
                        name: "FK__COMPRA__IdUsuari__787EE5A0",
                        column: x => x.IdUsuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTO_TIENDA",
                columns: table => new
                {
                    IdProductoTienda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<int>(type: "int", nullable: true),
                    IdTienda = table.Column<int>(type: "int", nullable: true),
                    PrecioUnidadCompra = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValueSql: "((0))"),
                    PrecioUnidadVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValueSql: "((0))"),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PRODUCTO__CE9B4C831C88AD03", x => x.IdProductoTienda);
                    table.ForeignKey(
                        name: "FK__PRODUCTO___IdPro__68487DD7",
                        column: x => x.IdProducto,
                        principalTable: "PRODUCTO",
                        principalColumn: "IdProducto");
                    table.ForeignKey(
                        name: "FK__PRODUCTO___IdTie__693CA210",
                        column: x => x.IdTienda,
                        principalTable: "TIENDA",
                        principalColumn: "IdTienda");
                });

            migrationBuilder.CreateTable(
                name: "VENTA",
                columns: table => new
                {
                    IdVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTienda = table.Column<int>(type: "int", nullable: true),
                    IdUsuario = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    IdCliente = table.Column<int>(type: "int", nullable: true),
                    TotalCosto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VENTA__BC1240BD398B5B72", x => x.IdVenta);
                    table.ForeignKey(
                        name: "FK__VENTA__IdCliente__0F624AF8",
                        column: x => x.IdCliente,
                        principalTable: "CLIENTE",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK__VENTA__IdTienda__0D7A0286",
                        column: x => x.IdTienda,
                        principalTable: "TIENDA",
                        principalColumn: "IdTienda");
                    table.ForeignKey(
                        name: "FK__VENTA__IdUsuario__0E6E26BF",
                        column: x => x.IdUsuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DETALLE_COMPRA",
                columns: table => new
                {
                    IdDetalleCompra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCompra = table.Column<int>(type: "int", nullable: true),
                    IdProducto = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    PrecioUnitarioCompra = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalCosto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IdCotizacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DETALLE___E046CCBB37E015AA", x => x.IdDetalleCompra);
                    table.ForeignKey(
                        name: "FK__DETALLE_C__IdCom__01142BA1",
                        column: x => x.IdCompra,
                        principalTable: "COMPRA",
                        principalColumn: "IdCompra");
                    table.ForeignKey(
                        name: "FK__DETALLE_C__IdCot__03F0984C",
                        column: x => x.IdCotizacion,
                        principalTable: "COTIZACIONES",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__DETALLE_C__IdPro__02084FDA",
                        column: x => x.IdProducto,
                        principalTable: "PRODUCTO",
                        principalColumn: "IdProducto");
                });

            migrationBuilder.CreateTable(
                name: "DETALLE_VENTA",
                columns: table => new
                {
                    IdDetalleVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVenta = table.Column<int>(type: "int", nullable: true),
                    IdProducto = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    PrecioUnidad = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ImporteTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IdCotizacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DETALLE___AAA5CEC2687ABDDA", x => x.IdDetalleVenta);
                    table.ForeignKey(
                        name: "FK__DETALLE_V__IdCot__160F4887",
                        column: x => x.IdCotizacion,
                        principalTable: "COTIZACIONES",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__DETALLE_V__IdPro__14270015",
                        column: x => x.IdProducto,
                        principalTable: "PRODUCTO",
                        principalColumn: "IdProducto");
                    table.ForeignKey(
                        name: "FK__DETALLE_V__IdVen__1332DBDC",
                        column: x => x.IdVenta,
                        principalTable: "VENTA",
                        principalColumn: "IdVenta");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTO_IdCategoria",
                table: "PRODUCTO",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdTienda",
                table: "AspNetUsers",
                column: "IdTienda");

            migrationBuilder.CreateIndex(
                name: "IX_COMPRA_IdTienda",
                table: "COMPRA",
                column: "IdTienda");

            migrationBuilder.CreateIndex(
                name: "IX_COMPRA_IdUsuario",
                table: "COMPRA",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLE_COMPRA_IdCompra",
                table: "DETALLE_COMPRA",
                column: "IdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLE_COMPRA_IdCotizacion",
                table: "DETALLE_COMPRA",
                column: "IdCotizacion");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLE_COMPRA_IdProducto",
                table: "DETALLE_COMPRA",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLE_VENTA_IdCotizacion",
                table: "DETALLE_VENTA",
                column: "IdCotizacion");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLE_VENTA_IdProducto",
                table: "DETALLE_VENTA",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLE_VENTA_IdVenta",
                table: "DETALLE_VENTA",
                column: "IdVenta");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTO_TIENDA_IdProducto",
                table: "PRODUCTO_TIENDA",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTO_TIENDA_IdTienda",
                table: "PRODUCTO_TIENDA",
                column: "IdTienda");

            migrationBuilder.CreateIndex(
                name: "IX_VENTA_IdCliente",
                table: "VENTA",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_VENTA_IdTienda",
                table: "VENTA",
                column: "IdTienda");

            migrationBuilder.CreateIndex(
                name: "IX_VENTA_IdUsuario",
                table: "VENTA",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK__AspNetUse__IdTie__1AD3FDA4",
                table: "AspNetUsers",
                column: "IdTienda",
                principalTable: "TIENDA",
                principalColumn: "IdTienda");

            migrationBuilder.AddForeignKey(
                name: "FK__PRODUCTO__IdCate__6477ECF3",
                table: "PRODUCTO",
                column: "IdCategoria",
                principalTable: "CATEGORIA",
                principalColumn: "IdCategoria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__AspNetUse__IdTie__1AD3FDA4",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK__PRODUCTO__IdCate__6477ECF3",
                table: "PRODUCTO");

            migrationBuilder.DropTable(
                name: "CATEGORIA");

            migrationBuilder.DropTable(
                name: "DETALLE_COMPRA");

            migrationBuilder.DropTable(
                name: "DETALLE_VENTA");

            migrationBuilder.DropTable(
                name: "PRODUCTO_TIENDA");

            migrationBuilder.DropTable(
                name: "COMPRA");

            migrationBuilder.DropTable(
                name: "COTIZACIONES");

            migrationBuilder.DropTable(
                name: "VENTA");

            migrationBuilder.DropTable(
                name: "CLIENTE");

            migrationBuilder.DropTable(
                name: "TIENDA");

            migrationBuilder.DropPrimaryKey(
                name: "PK__PRODUCTO__09889210FBEFDAD7",
                table: "PRODUCTO");

            migrationBuilder.DropIndex(
                name: "IX_PRODUCTO_IdCategoria",
                table: "PRODUCTO");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdTienda",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "PRODUCTO");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "PRODUCTO");

            migrationBuilder.DropColumn(
                name: "FechaRegistro",
                table: "PRODUCTO");

            migrationBuilder.DropColumn(
                name: "IdCategoria",
                table: "PRODUCTO");

            migrationBuilder.DropColumn(
                name: "ImagenUrl",
                table: "PRODUCTO");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "PRODUCTO");

            migrationBuilder.DropColumn(
                name: "stock",
                table: "PRODUCTO");

            migrationBuilder.DropColumn(
                name: "IdTienda",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "PRODUCTO",
                newName: "producto");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "producto",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "IdProducto",
                table: "producto",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "nombre",
                table: "producto",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_producto",
                table: "producto",
                column: "id");
        }
    }
}
