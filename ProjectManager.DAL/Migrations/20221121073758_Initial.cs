using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<long>(type: "bigint", nullable: false),
                    TeamLeaderEmployeeId = table.Column<int>(type: "int", nullable: true),
                    CustomerCompanyId = table.Column<int>(type: "int", nullable: true),
                    ContractorCompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Companies_ContractorCompanyId",
                        column: x => x.ContractorCompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Companies_CustomerCompanyId",
                        column: x => x.CustomerCompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Employees_TeamLeaderEmployeeId",
                        column: x => x.TeamLeaderEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProject",
                columns: table => new
                {
                    InvolvedEmployeesEmployeeId = table.Column<int>(type: "int", nullable: false),
                    ParticipatedProjectsProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProject", x => new { x.InvolvedEmployeesEmployeeId, x.ParticipatedProjectsProjectId });
                    table.ForeignKey(
                        name: "FK_EmployeeProject_Employees_InvolvedEmployeesEmployeeId",
                        column: x => x.InvolvedEmployeesEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProject_Projects_ParticipatedProjectsProjectId",
                        column: x => x.ParticipatedProjectsProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProject_ParticipatedProjectsProjectId",
                table: "EmployeeProject",
                column: "ParticipatedProjectsProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ContractorCompanyId",
                table: "Projects",
                column: "ContractorCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerCompanyId",
                table: "Projects",
                column: "CustomerCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TeamLeaderEmployeeId",
                table: "Projects",
                column: "TeamLeaderEmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeProject");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
