using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmploymentSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ater_tableVacancyadd_non_clustered_indexOnTitleandDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Vacancies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Vacancies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_Description",
                table: "Vacancies",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_Title",
                table: "Vacancies",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vacancies_Description",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_Title",
                table: "Vacancies");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Vacancies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Vacancies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
