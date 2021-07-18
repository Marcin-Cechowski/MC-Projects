using Microsoft.EntityFrameworkCore.Migrations;

namespace Diploma_support_system.Data.Migrations
{
    public partial class addMessagesToAppDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_AppUserId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Student_StudentId",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Messages");

            migrationBuilder.RenameIndex(
                name: "IX_Message_StudentId",
                table: "Messages",
                newName: "IX_Messages_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_AppUserId",
                table: "Messages",
                newName: "IX_Messages_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_AppUserId",
                table: "Messages",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Student_StudentId",
                table: "Messages",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_AppUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Student_StudentId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_StudentId",
                table: "Message",
                newName: "IX_Message_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_AppUserId",
                table: "Message",
                newName: "IX_Message_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_AppUserId",
                table: "Message",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Student_StudentId",
                table: "Message",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
