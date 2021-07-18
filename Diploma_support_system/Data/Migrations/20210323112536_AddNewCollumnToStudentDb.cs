using Microsoft.EntityFrameworkCore.Migrations;

namespace Diploma_support_system.Data.Migrations
{
    public partial class AddNewCollumnToStudentDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiplomaName",
                table: "Student",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiplomaName",
                table: "Student");
        }
    }
}
