using Microsoft.EntityFrameworkCore.Migrations;

namespace IDAGroupMVC.Migrations
{
    public partial class ViewCountCompanyNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_ViewCounts_ViewCountId",
                table: "Companies");

            migrationBuilder.AlterColumn<int>(
                name: "ViewCountId",
                table: "Companies",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 150);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_ViewCounts_ViewCountId",
                table: "Companies",
                column: "ViewCountId",
                principalTable: "ViewCounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_ViewCounts_ViewCountId",
                table: "Companies");

            migrationBuilder.AlterColumn<int>(
                name: "ViewCountId",
                table: "Companies",
                type: "int",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_ViewCounts_ViewCountId",
                table: "Companies",
                column: "ViewCountId",
                principalTable: "ViewCounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
