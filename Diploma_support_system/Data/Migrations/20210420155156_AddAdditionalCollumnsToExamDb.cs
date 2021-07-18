using Microsoft.EntityFrameworkCore.Migrations;

namespace Diploma_support_system.Data.Migrations
{
    public partial class AddAdditionalCollumnsToExamDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstAnswer",
                table: "Exam",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecoundAnswer",
                table: "Exam",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdAnswer",
                table: "Exam",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstAnswer",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "SecoundAnswer",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "ThirdAnswer",
                table: "Exam");
        }
    }
}
