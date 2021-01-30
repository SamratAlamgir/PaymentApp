using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AddPaymentTableColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CardNumberMasked",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CardNumberMasked",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "Payments");
        }
    }
}
