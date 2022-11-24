using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeysUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_ContractorId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_CustomerId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_TeamLeaderId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "TeamLeaderId",
                table: "Projects",
                newName: "TeamLeaderEmployeeId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Projects",
                newName: "CustomerCompanyId");

            migrationBuilder.RenameColumn(
                name: "ContractorId",
                table: "Projects",
                newName: "ContractorCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_TeamLeaderId",
                table: "Projects",
                newName: "IX_Projects_TeamLeaderEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CustomerId",
                table: "Projects",
                newName: "IX_Projects_CustomerCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ContractorId",
                table: "Projects",
                newName: "IX_Projects_ContractorCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companies_ContractorCompanyId",
                table: "Projects",
                column: "ContractorCompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companies_CustomerCompanyId",
                table: "Projects",
                column: "CustomerCompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employees_TeamLeaderEmployeeId",
                table: "Projects",
                column: "TeamLeaderEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_ContractorCompanyId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_CustomerCompanyId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_TeamLeaderEmployeeId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "TeamLeaderEmployeeId",
                table: "Projects",
                newName: "TeamLeaderId");

            migrationBuilder.RenameColumn(
                name: "CustomerCompanyId",
                table: "Projects",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "ContractorCompanyId",
                table: "Projects",
                newName: "ContractorId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_TeamLeaderEmployeeId",
                table: "Projects",
                newName: "IX_Projects_TeamLeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CustomerCompanyId",
                table: "Projects",
                newName: "IX_Projects_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ContractorCompanyId",
                table: "Projects",
                newName: "IX_Projects_ContractorId");

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
    }
}
