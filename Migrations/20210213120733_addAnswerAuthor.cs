using Microsoft.EntityFrameworkCore.Migrations;

namespace MyForum.Migrations
{
    public partial class addAnswerAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Answers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Answers");
        }
    }
}
