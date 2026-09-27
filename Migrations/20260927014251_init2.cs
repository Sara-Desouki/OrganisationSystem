using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganisationSystem.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefranceId",
                table: "Volunteers",
                newName: "ReferenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReferenceId",
                table: "Volunteers",
                newName: "RefranceId");
        }
    }
}
