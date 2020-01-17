using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Infrastructure.Migrations
{
    public partial class token_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "User");
        }
    }
}
