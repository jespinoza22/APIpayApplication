using Microsoft.EntityFrameworkCore.Migrations;

namespace APIpayApplication.Migrations
{
    public partial class ColumnIncome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdMoneda",
                table: "Incomes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdMoneda",
                table: "Incomes");
        }
    }
}
