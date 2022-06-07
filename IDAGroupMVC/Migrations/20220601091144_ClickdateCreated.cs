using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IDAGroupMVC.Migrations
{
    public partial class ClickdateCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClickDate",
                table: "ViewCounts");

            migrationBuilder.CreateTable(
                name: "ClickDates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ViewCountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClickDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClickDates_ViewCounts_ViewCountId",
                        column: x => x.ViewCountId,
                        principalTable: "ViewCounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClickDates_ViewCountId",
                table: "ClickDates",
                column: "ViewCountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClickDates");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClickDate",
                table: "ViewCounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
