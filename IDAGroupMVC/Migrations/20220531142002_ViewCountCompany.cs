using Microsoft.EntityFrameworkCore.Migrations;

namespace IDAGroupMVC.Migrations
{
    public partial class ViewCountCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClickName",
                table: "ViewCounts",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "Companies",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewCountId",
                table: "Companies",
                maxLength: 150,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ViewCountId",
                table: "Companies",
                column: "ViewCountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_ViewCounts_ViewCountId",
                table: "Companies",
                column: "ViewCountId",
                principalTable: "ViewCounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_ViewCounts_ViewCountId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ViewCountId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ClickName",
                table: "ViewCounts");

            migrationBuilder.DropColumn(
                name: "ViewCountId",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "Companies",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
