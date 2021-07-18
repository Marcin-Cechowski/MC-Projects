using Microsoft.EntityFrameworkCore.Migrations;

namespace Diploma_support_system.Data.Migrations
{
    public partial class AddNewRelationsToExamEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Exam",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Exam",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Exam_AppUserId",
                table: "Exam",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_GroupId",
                table: "Exam",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_AspNetUsers_AppUserId",
                table: "Exam",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Group_GroupId",
                table: "Exam",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_AspNetUsers_AppUserId",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Group_GroupId",
                table: "Exam");

            migrationBuilder.DropIndex(
                name: "IX_Exam_AppUserId",
                table: "Exam");

            migrationBuilder.DropIndex(
                name: "IX_Exam_GroupId",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Exam");
        }
    }
}
