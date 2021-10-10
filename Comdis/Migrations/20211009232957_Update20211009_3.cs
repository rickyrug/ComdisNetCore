using Microsoft.EntityFrameworkCore.Migrations;

namespace Comdis.Migrations
{
    public partial class Update20211009_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_UOM_UOMId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "UOMId",
                table: "Product",
                newName: "UomId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_UOMId",
                table: "Product",
                newName: "IX_Product_UomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_UOM_UomId",
                table: "Product",
                column: "UomId",
                principalTable: "UOM",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_UOM_UomId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "UomId",
                table: "Product",
                newName: "UOMId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_UomId",
                table: "Product",
                newName: "IX_Product_UOMId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_UOM_UOMId",
                table: "Product",
                column: "UOMId",
                principalTable: "UOM",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
