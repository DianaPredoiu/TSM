using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "WorkedHours",
                table: "TimesheetActivity",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "BreakTime",
                table: "Timesheet",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.CreateTable(
                name: "ProjectAssignments",
                columns: table => new
                {
                    IdProject = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdUser = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAssignments", x => x.IdProject);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectAssignments");

            migrationBuilder.AlterColumn<long>(
                name: "WorkedHours",
                table: "TimesheetActivity",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<long>(
                name: "BreakTime",
                table: "Timesheet",
                nullable: false,
                oldClrType: typeof(TimeSpan));
        }
    }
}
