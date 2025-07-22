using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleDateAndUpdateCancelledFieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Canceled",
                table: "SaleItem",
                newName: "Cancelled");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Sale",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Sale");

            migrationBuilder.RenameColumn(
                name: "Cancelled",
                table: "SaleItem",
                newName: "Canceled");
        }
    }
}
