using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningPortal.Migrations
{
    public partial class EditRoleRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "AspNetUsers",
                newName: "Position");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position",
                table: "AspNetUsers",
                newName: "Role");
        }
    }
}
