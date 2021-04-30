using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreManagement_core.Data.Migrations
{
    public partial class sales_salesDetailsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductStock");

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SalesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustId = table.Column<int>(nullable: false),
                    BillNo = table.Column<string>(nullable: false),
                    SalesDate = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SalesId);
                    table.ForeignKey(
                        name: "FK_Sales_Customer_CustId",
                        column: x => x.CustId,
                        principalTable: "Customer",
                        principalColumn: "CustId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesDetail",
                columns: table => new
                {
                    SalesDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesId = table.Column<int>(nullable: false),
                    ProdId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDetail", x => x.SalesDetailId);
                    table.ForeignKey(
                        name: "FK_SalesDetail_Product_ProdId",
                        column: x => x.ProdId,
                        principalTable: "Product",
                        principalColumn: "ProdId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesDetail_Sales_SalesId",
                        column: x => x.SalesId,
                        principalTable: "Sales",
                        principalColumn: "SalesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CustId",
                table: "Sales",
                column: "CustId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetail_ProdId",
                table: "SalesDetail",
                column: "ProdId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetail_SalesId",
                table: "SalesDetail",
                column: "SalesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesDetail");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.CreateTable(
                name: "ProductStock",
                columns: table => new
                {
                    ProdStockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStock", x => x.ProdStockId);
                });
        }
    }
}
