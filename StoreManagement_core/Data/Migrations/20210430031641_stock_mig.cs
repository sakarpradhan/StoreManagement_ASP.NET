using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreManagement_core.Data.Migrations
{
    public partial class stock_mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StockedDate",
                table: "Stock",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockedDate",
                table: "Stock");
        }
    }
}
