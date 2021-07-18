using Microsoft.EntityFrameworkCore.Migrations;

namespace Diploma_support_system.Data.Migrations
{
    public partial class AddExamEntityToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstQuestion = table.Column<string>(nullable: true),
                    SecoundQuestion = table.Column<string>(nullable: true),
                    ThirdQuestion = table.Column<string>(nullable: true),
                    Reviewer = table.Column<string>(nullable: true),
                    PromoterId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exam_Promoter_PromoterId",
                        column: x => x.PromoterId,
                        principalTable: "Promoter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exam_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exam_PromoterId",
                table: "Exam",
                column: "PromoterId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_StudentId",
                table: "Exam",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exam");
        }
    }
}
