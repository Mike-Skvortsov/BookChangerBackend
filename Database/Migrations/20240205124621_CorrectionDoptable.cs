using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class CorrectionDoptable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookGenres_BookGenreBookId_BookGenreGenreId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookGenreBookId_BookGenreGenreId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookGenreBookId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookGenreGenreId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BookAuthors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookGenreBookId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookGenreGenreId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BookGenres",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BookAuthors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookGenreBookId_BookGenreGenreId",
                table: "Books",
                columns: new[] { "BookGenreBookId", "BookGenreGenreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookGenres_BookGenreBookId_BookGenreGenreId",
                table: "Books",
                columns: new[] { "BookGenreBookId", "BookGenreGenreId" },
                principalTable: "BookGenres",
                principalColumns: new[] { "BookId", "GenreId" });
        }
    }
}
