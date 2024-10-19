using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class MessageAndChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dateTime",
                table: "Books",
                newName: "DateTime");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserOneId = table.Column<int>(type: "int", nullable: false),
                    UserTwoId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Chats_Users_UserOneId",
                        column: x => x.UserOneId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chats_Users_UserTwoId",
                        column: x => x.UserTwoId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
    name: "Messages",
    columns: table => new
    {
        Id = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
        Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
        SenderId = table.Column<int>(type: "int", nullable: false),
        ReceiverId = table.Column<int>(type: "int", nullable: false),
        IsRead = table.Column<bool>(type: "bit", nullable: false),
        ChatId = table.Column<int>(type: "int", nullable: false)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_Messages", x => x.Id);
        table.ForeignKey(
            name: "FK_Messages_Chats_ChatId",
            column: x => x.ChatId,
            principalTable: "Chats",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
        table.ForeignKey(
            name: "FK_Messages_Users_ReceiverId",
            column: x => x.ReceiverId,
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict); // Changed to Restrict
        table.ForeignKey(
            name: "FK_Messages_Users_SenderId",
            column: x => x.SenderId,
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict); // Changed to Restrict
    });

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId",
                table: "Chats",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserOneId",
                table: "Chats",
                column: "UserOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserTwoId",
                table: "Chats",
                column: "UserTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Books",
                newName: "dateTime");
        }
    }
}
