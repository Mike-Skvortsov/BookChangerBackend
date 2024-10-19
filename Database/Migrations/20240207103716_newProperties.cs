using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class newProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OnlineTime",
                table: "Users",
                nullable: false,
                defaultValueSql: "GETDATE()"); // Або використовуйте інше значення за замовчуванням

            migrationBuilder.AddColumn<DateTime>(
                name: "BDay",
                table: "Authors",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DayOfDeath",
                table: "Authors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
