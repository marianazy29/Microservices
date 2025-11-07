using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ventas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Descuento = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    MontoTotal = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    Comentarios = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetalleDeVenta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VentaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Descuento = table.Column<double>(type: "float", nullable: false),
                    TotalLinea = table.Column<double>(type: "float", nullable: false),
                    Estado = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleDeVenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleDeVenta_Venta_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Venta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleDeVenta_VentaId",
                table: "DetalleDeVenta",
                column: "VentaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleDeVenta");

            migrationBuilder.DropTable(
                name: "Venta");
        }
    }
}
