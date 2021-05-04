using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyForum.Migrations
{
    public partial class RemovedTokenExpiration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateExpired",
                table: "Tokens");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateExpired",
                table: "Tokens",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
