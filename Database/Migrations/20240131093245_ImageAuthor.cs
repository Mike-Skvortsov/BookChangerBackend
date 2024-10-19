using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class ImageAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookAuthors_BookAuthorBookId_BookAuthorAuthorId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookAuthorBookId_BookAuthorAuthorId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookAuthorAuthorId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookAuthorBookId",
                table: "Books");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Authors",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Authors");

            migrationBuilder.AddColumn<int>(
                name: "BookAuthorAuthorId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookAuthorBookId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookAuthorBookId_BookAuthorAuthorId",
                table: "Books",
                columns: new[] { "BookAuthorBookId", "BookAuthorAuthorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookAuthors_BookAuthorBookId_BookAuthorAuthorId",
                table: "Books",
                columns: new[] { "BookAuthorBookId", "BookAuthorAuthorId" },
                principalTable: "BookAuthors",
                principalColumns: new[] { "BookId", "AuthorId" });
        }
    }
}
