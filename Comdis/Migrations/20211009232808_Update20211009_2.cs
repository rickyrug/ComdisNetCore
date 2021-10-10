using Microsoft.EntityFrameworkCore.Migrations;

namespace Comdis.Migrations
{
    public partial class Update20211009_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UOM_Product_ProductId",
                table: "UOM");

            migrationBuilder.DropIndex(
                name: "IX_UOM_ProductId",
                table: "UOM");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "UOM");

            migrationBuilder.AddColumn<int>(
                name: "UOMId",
                table: "Product",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_UOMId",
                table: "Product",
                column: "UOMId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_UOM_UOMId",
                table: "Product",
                column: "UOMId",
                principalTable: "UOM",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_UOM_UOMId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_UOMId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UOMId",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "UOM",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UOM_ProductId",
                table: "UOM",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_UOM_Product_ProductId",
                table: "UOM",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
