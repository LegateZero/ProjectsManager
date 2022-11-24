using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddGoalsToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Goals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ProjectId",
                table: "Goals",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Projects_ProjectId",
                table: "Goals",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Projects_ProjectId",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Goals_ProjectId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Goals");
        }
    }
}
