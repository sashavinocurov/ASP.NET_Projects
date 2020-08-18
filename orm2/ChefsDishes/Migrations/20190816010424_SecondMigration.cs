using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chefs_Dishes.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "chefs");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "chefs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "chefs");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "chefs",
                nullable: false,
                defaultValue: 0);
        }
    }
}
