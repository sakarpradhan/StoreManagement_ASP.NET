using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreManagement_core.Data.Migrations
{
    public partial class stockManagementMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "Stock",
                newName: "Quantity");

            migrationBuilder.AddColumn<string>(
                name: "ProdCode",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProdDesc",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    PurId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorName = table.Column<string>(nullable: true),
                    PurDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.PurId);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDetail",
                columns: table => new
                {
                    PurDetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurId = table.Column<int>(nullable: false),
                    ProdId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDetail", x => x.PurDetId);
                    table.ForeignKey(
                        name: "FK_PurchaseDetail_Product_ProdId",
                        column: x => x.ProdId,
                        principalTable: "Product",
                        principalColumn: "ProdId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseDetail_Purchase_PurId",
                        column: x => x.PurId,
                        principalTable: "Purchase",
                        principalColumn: "PurId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetail_ProdId",
                table: "PurchaseDetail",
                column: "ProdId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetail_PurId",
                table: "PurchaseDetail",
                column: "PurId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseDetail");

            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropColumn(
                name: "ProdCode",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProdDesc",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Stock",
                newName: "quantity");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
