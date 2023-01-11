using Microsoft.EntityFrameworkCore.Migrations;

namespace DailyReportWeb_Api.Migrations
{
    public partial class statuspropertyaddedinusermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserStatus",
                table: "UserTask",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserStatus",
                table: "UserTask");
        }
    }
}
