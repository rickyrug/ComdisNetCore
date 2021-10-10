using Microsoft.EntityFrameworkCore.Migrations;

namespace Comdis.Migrations
{
    public partial class Update20211009_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
