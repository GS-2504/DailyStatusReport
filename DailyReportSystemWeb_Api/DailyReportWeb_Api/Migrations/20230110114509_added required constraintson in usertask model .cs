using Microsoft.EntityFrameworkCore.Migrations;

namespace DailyReportWeb_Api.Migrations
{
    public partial class addingrequiredconstraintsonforeignkeyinusertask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTask_AspNetUsers_UserId",
                table: "UserTask");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserTask",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTask_AspNetUsers_UserId",
                table: "UserTask",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTask_AspNetUsers_UserId",
                table: "UserTask");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserTask",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTask_AspNetUsers_UserId",
                table: "UserTask",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
