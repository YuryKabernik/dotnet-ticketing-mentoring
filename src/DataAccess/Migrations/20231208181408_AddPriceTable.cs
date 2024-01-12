using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "EventSeats");

            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "EventSeats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventSeats_PriceId",
                table: "EventSeats",
                column: "PriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSeats_Price_PriceId",
                table: "EventSeats",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSeats_Prices_PriceId",
                table: "EventSeats");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_EventSeats_PriceId",
                table: "EventSeats");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "EventSeats");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "EventSeats",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
