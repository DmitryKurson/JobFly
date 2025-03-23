using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFly.Migrations
{
    /// <inheritdoc />
    public partial class vacancy_status_replaced_to_active : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Vacancies");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Vacancies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Vacancies");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Vacancies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
