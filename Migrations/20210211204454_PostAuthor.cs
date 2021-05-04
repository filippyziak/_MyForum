using Microsoft.EntityFrameworkCore.Migrations;

namespace MyForum.Migrations
{
    public partial class PostAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserUsername",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserUsername",
                table: "Posts");
        }
    }
}
