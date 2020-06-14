using Microsoft.EntityFrameworkCore.Migrations;

namespace APIpayApplication.Migrations
{
    public partial class addingMoneda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdMoneda",
                table: "Expenses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdMoneda",
                table: "Cards",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdMoneda",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "IdMoneda",
                table: "Cards");
        }
    }
}
