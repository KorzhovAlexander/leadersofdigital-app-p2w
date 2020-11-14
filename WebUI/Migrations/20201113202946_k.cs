using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUI.Migrations
{
    public partial class k : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Accuracy = table.Column<string>(nullable: true),
                    InputAddress = table.Column<string>(nullable: true),
                    OutputAddress = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ElementAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StName = table.Column<string>(nullable: true),
                    TName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    UsersAddressesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElementAddresses_UsersAddresses_UsersAddressesId",
                        column: x => x.UsersAddressesId,
                        principalTable: "UsersAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElementAddresses_UsersAddressesId",
                table: "ElementAddresses",
                column: "UsersAddressesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElementAddresses");

            migrationBuilder.DropTable(
                name: "UsersAddresses");
        }
    }
}
