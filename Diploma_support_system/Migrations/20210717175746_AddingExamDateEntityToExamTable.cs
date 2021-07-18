using Microsoft.EntityFrameworkCore.Migrations;

namespace Diploma_support_system.Migrations
{
    public partial class AddingExamDateEntityToExamTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExamDate",
                table: "Exam",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamDate",
                table: "Exam");
        }
    }
}
