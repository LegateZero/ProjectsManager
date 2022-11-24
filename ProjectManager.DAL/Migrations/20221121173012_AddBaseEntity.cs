using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Employees_InvolvedEmployeesEmployeeId",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Projects_ParticipatedProjectsProjectId",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_ContractorCompanyId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_CustomerCompanyId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_TeamLeaderEmployeeId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_TeamLeaderEmployeeId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "CustomerCompanyId",
                table: "Projects",
                newName: "TeamLeaderId");

            migrationBuilder.RenameColumn(
                name: "ContractorCompanyId",
                table: "Projects",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Projects",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CustomerCompanyId",
                table: "Projects",
                newName: "IX_Projects_TeamLeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ContractorCompanyId",
                table: "Projects",
                newName: "IX_Projects_CustomerId");

            migrationBuilder.RenameColumn(
                name: "GoalId",
                table: "Goals",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Employees",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ParticipatedProjectsProjectId",
                table: "EmployeeProject",
                newName: "ParticipatedProjectsId");

            migrationBuilder.RenameColumn(
                name: "InvolvedEmployeesEmployeeId",
                table: "EmployeeProject",
                newName: "InvolvedEmployeesId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProject_ParticipatedProjectsProjectId",
                table: "EmployeeProject",
                newName: "IX_EmployeeProject_ParticipatedProjectsId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Companies",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ContractorId",
                table: "Projects",
                column: "ContractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Employees_InvolvedEmployeesId",
                table: "EmployeeProject",
                column: "InvolvedEmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Projects_ParticipatedProjectsId",
                table: "EmployeeProject",
                column: "ParticipatedProjectsId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companies_ContractorId",
                table: "Projects",
                column: "ContractorId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companies_CustomerId",
                table: "Projects",
                column: "CustomerId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employees_TeamLeaderId",
                table: "Projects",
                column: "TeamLeaderId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Employees_InvolvedEmployeesId",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Projects_ParticipatedProjectsId",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_ContractorId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_CustomerId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_TeamLeaderId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ContractorId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "TeamLeaderId",
                table: "Projects",
                newName: "CustomerCompanyId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Projects",
                newName: "ContractorCompanyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Projects",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_TeamLeaderId",
                table: "Projects",
                newName: "IX_Projects_CustomerCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CustomerId",
                table: "Projects",
                newName: "IX_Projects_ContractorCompanyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Goals",
                newName: "GoalId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Employees",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "ParticipatedProjectsId",
                table: "EmployeeProject",
                newName: "ParticipatedProjectsProjectId");

            migrationBuilder.RenameColumn(
                name: "InvolvedEmployeesId",
                table: "EmployeeProject",
                newName: "InvolvedEmployeesEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProject_ParticipatedProjectsId",
                table: "EmployeeProject",
                newName: "IX_EmployeeProject_ParticipatedProjectsProjectId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Companies",
                newName: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TeamLeaderEmployeeId",
                table: "Projects",
                column: "TeamLeaderEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Employees_InvolvedEmployeesEmployeeId",
                table: "EmployeeProject",
                column: "InvolvedEmployeesEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Projects_ParticipatedProjectsProjectId",
                table: "EmployeeProject",
                column: "ParticipatedProjectsProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companies_ContractorCompanyId",
                table: "Projects",
                column: "ContractorCompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companies_CustomerCompanyId",
                table: "Projects",
                column: "CustomerCompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employees_TeamLeaderEmployeeId",
                table: "Projects",
                column: "TeamLeaderEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
