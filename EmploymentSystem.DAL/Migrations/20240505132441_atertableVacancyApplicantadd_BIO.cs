using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmploymentSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class atertableVacancyApplicantadd_BIO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BIO",
                table: "VacancyApplicants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BIO",
                table: "VacancyApplicants");
        }
    }
}
