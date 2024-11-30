using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeBreak.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderDeliveryMan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "User_Id",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Delivery_Man_Id",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Delivery_Man_Id",
                table: "Orders",
                column: "Delivery_Man_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User1_Id",
                table: "Orders",
                column: "Delivery_Man_Id",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_User1_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Delivery_Man_Id",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Delivery_Man_Id",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "User_Id",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
