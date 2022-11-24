using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddGoalTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    GoalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorEmployeeId = table.Column<int>(type: "int", nullable: true),
                    ExecutorEmployeeId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.GoalId);
                    table.ForeignKey(
                        name: "FK_Goals_Employees_AuthorEmployeeId",
                        column: x => x.AuthorEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_Goals_Employees_ExecutorEmployeeId",
                        column: x => x.ExecutorEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goals_AuthorEmployeeId",
                table: "Goals",
                column: "AuthorEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ExecutorEmployeeId",
                table: "Goals",
                column: "ExecutorEmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goals");
        }
    }
}
