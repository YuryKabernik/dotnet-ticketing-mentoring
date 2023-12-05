using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class VenueDesigns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Geography",
                table: "Venues");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Venues",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Venues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Sections",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Seats",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Rows",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Building = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Venues_AddressId",
                table: "Venues",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_VenueId",
                table: "Sections",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_RowId",
                table: "Seats",
                column: "RowId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_SectionId",
                table: "Rows",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_Sections_SectionId",
                table: "Rows",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Rows_RowId",
                table: "Seats",
                column: "RowId",
                principalTable: "Rows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Venues_VenueId",
                table: "Sections",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_Addresses_AddressId",
                table: "Venues",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rows_Sections_SectionId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Rows_RowId",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Venues_VenueId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Venues_Addresses_AddressId",
                table: "Venues");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Venues_AddressId",
                table: "Venues");

            migrationBuilder.DropIndex(
                name: "IX_Sections_VenueId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Seats_RowId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Rows_SectionId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Venues");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Geography",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Sections",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Seats",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Rows",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
