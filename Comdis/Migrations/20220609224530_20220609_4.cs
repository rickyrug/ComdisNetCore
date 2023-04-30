using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comdis.Migrations
{
    public partial class _20220609_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VendorId = table.Column<int>(type: "INTEGER", nullable: true),
                    RequestedDeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeliveryAdress = table.Column<string>(type: "TEXT", nullable: true),
                    Comments = table.Column<string>(type: "TEXT", nullable: true),
                    discount = table.Column<decimal>(type: "TEXT", nullable: false),
                    discount2 = table.Column<decimal>(type: "TEXT", nullable: false),
                    discount3 = table.Column<decimal>(type: "TEXT", nullable: false),
                    tax = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchase_Supplier_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Supplier",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    POHeaderId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: true),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseItems_Purchase_POHeaderId",
                        column: x => x.POHeaderId,
                        principalTable: "Purchase",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_VendorId",
                table: "Purchase",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_POHeaderId",
                table: "PurchaseItems",
                column: "POHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_ProductId",
                table: "PurchaseItems",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseItems");

            migrationBuilder.DropTable(
                name: "Purchase");
        }
    }
}
