using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class g : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddrType",
                table: "UsersAddresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Delivery",
                table: "UsersAddresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryArea",
                table: "UsersAddresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Direct",
                table: "UsersAddresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Origin",
                table: "UsersAddresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Unparsed",
                table: "UsersAddresses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Historical",
                table: "ElementAddresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Origin",
                table: "ElementAddresses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddrType",
                table: "UsersAddresses");

            migrationBuilder.DropColumn(
                name: "Delivery",
                table: "UsersAddresses");

            migrationBuilder.DropColumn(
                name: "DeliveryArea",
                table: "UsersAddresses");

            migrationBuilder.DropColumn(
                name: "Direct",
                table: "UsersAddresses");

            migrationBuilder.DropColumn(
                name: "Origin",
                table: "UsersAddresses");

            migrationBuilder.DropColumn(
                name: "Unparsed",
                table: "UsersAddresses");

            migrationBuilder.DropColumn(
                name: "Historical",
                table: "ElementAddresses");

            migrationBuilder.DropColumn(
                name: "Origin",
                table: "ElementAddresses");
        }
    }
}
