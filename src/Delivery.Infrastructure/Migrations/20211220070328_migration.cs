using Microsoft.EntityFrameworkCore.Migrations;

namespace Delivery.Infrastructure.Migrations
{
    public partial class migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "Pedidos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdentityUserId",
                table: "Pedidos",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_AspNetUsers_IdentityUserId",
                table: "Pedidos",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_AspNetUsers_IdentityUserId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_IdentityUserId",
                table: "Pedidos");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
