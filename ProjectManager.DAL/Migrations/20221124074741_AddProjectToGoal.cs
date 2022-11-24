using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectToGoal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Projects_ProjectId",
                table: "Goals");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Goals",
                newName: "ProjectRelatedId");

            migrationBuilder.RenameIndex(
                name: "IX_Goals_ProjectId",
                table: "Goals",
                newName: "IX_Goals_ProjectRelatedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Projects_ProjectRelatedId",
                table: "Goals",
                column: "ProjectRelatedId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Projects_ProjectRelatedId",
                table: "Goals");

            migrationBuilder.RenameColumn(
                name: "ProjectRelatedId",
                table: "Goals",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Goals_ProjectRelatedId",
                table: "Goals",
                newName: "IX_Goals_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Projects_ProjectId",
                table: "Goals",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
