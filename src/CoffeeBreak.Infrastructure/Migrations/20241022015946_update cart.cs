using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeBreak.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatecart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductsCart");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductsCart",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
