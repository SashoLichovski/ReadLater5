using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class favouriteBookmark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavouriteBookmarks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BookmarkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteBookmarks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FavouriteBookmarks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavouriteBookmarks_Bookmark_BookmarkId",
                        column: x => x.BookmarkId,
                        principalTable: "Bookmark",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteBookmarks_BookmarkId",
                table: "FavouriteBookmarks",
                column: "BookmarkId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteBookmarks_UserId",
                table: "FavouriteBookmarks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouriteBookmarks");
        }
    }
}
