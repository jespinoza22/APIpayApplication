using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIpayApplication.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    IdCard = table.Column<string>(nullable: false),
                    IdUser = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CardNumber = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateModify = table.Column<DateTime>(nullable: false),
                    IdMoneda = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.IdCard);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    IdExpense = table.Column<string>(nullable: false),
                    IdUser = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateCreation = table.Column<DateTime>(nullable: false),
                    DateApply = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    IdCard = table.Column<string>(nullable: true),
                    IdMoneda = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.IdExpense);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    IdIncome = table.Column<string>(nullable: false),
                    IdUser = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateCreation = table.Column<DateTime>(nullable: false),
                    DateApply = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    IdCard = table.Column<string>(nullable: true),
                    IdMoneda = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.IdIncome);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Incomes");
        }
    }
}
