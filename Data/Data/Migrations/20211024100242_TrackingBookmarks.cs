using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class TrackingBookmarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastTimeOpened",
                table: "Bookmark",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TimesAddedToFavorites",
                table: "Bookmark",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimesOpened",
                table: "Bookmark",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastTimeOpened",
                table: "Bookmark");

            migrationBuilder.DropColumn(
                name: "TimesAddedToFavorites",
                table: "Bookmark");

            migrationBuilder.DropColumn(
                name: "TimesOpened",
                table: "Bookmark");
        }
    }
}
