using Microsoft.EntityFrameworkCore.Migrations;

namespace Delivery.Infrastructure.Migrations
{
    public partial class AddEstoque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantidadeEmEstoque",
                table: "Produto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantidadeEmEstoque",
                table: "Produto");
        }
    }
}
