using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EventDesigns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "EventSeats",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Events",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_EventSections_EventId",
                table: "EventSections",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSeats_RowId",
                table: "EventSeats",
                column: "RowId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueId",
                table: "Events",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRows_SectionId",
                table: "EventRows",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRows_EventSections_SectionId",
                table: "EventRows",
                column: "SectionId",
                principalTable: "EventSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Venues_VenueId",
                table: "Events",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSeats_EventRows_RowId",
                table: "EventSeats",
                column: "RowId",
                principalTable: "EventRows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSections_Events_EventId",
                table: "EventSections",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRows_EventSections_SectionId",
                table: "EventRows");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Venues_VenueId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSeats_EventRows_RowId",
                table: "EventSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSections_Events_EventId",
                table: "EventSections");

            migrationBuilder.DropIndex(
                name: "IX_EventSections_EventId",
                table: "EventSections");

            migrationBuilder.DropIndex(
                name: "IX_EventSeats_RowId",
                table: "EventSeats");

            migrationBuilder.DropIndex(
                name: "IX_Events_VenueId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_EventRows_SectionId",
                table: "EventRows");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "EventSeats",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Events",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }
    }
}
