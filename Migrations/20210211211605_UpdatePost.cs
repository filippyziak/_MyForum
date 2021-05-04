using Microsoft.EntityFrameworkCore.Migrations;

namespace MyForum.Migrations
{
    public partial class UpdatePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserUsername",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "UserUsername",
                table: "Posts",
                type: "text",
                nullable: true);
        }
    }
}
