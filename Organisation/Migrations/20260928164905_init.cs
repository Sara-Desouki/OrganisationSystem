using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganisationSystem.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence(
                name: "OrganizationReferenceSequence",
                schema: "dbo",
                startValue: 700000L);

            migrationBuilder.CreateSequence(
                name: "VolunteerReferenceSequence",
                schema: "dbo",
                startValue: 800000L);

            migrationBuilder.CreateTable(
                name: "organisations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefranceId = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "CAST(NEXT VALUE FOR dbo.OrganizationReferenceSequence AS varchar(20))"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organisations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Volunteers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceId = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "CAST(NEXT VALUE FOR dbo.VolunteerReferenceSequence AS varchar(20))"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    OrganisationId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    MaritalStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Volunteers_organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_organisations_Name",
                table: "organisations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_Name",
                table: "Volunteers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_OrganisationId",
                table: "Volunteers",
                column: "OrganisationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "Volunteers");

            migrationBuilder.DropTable(
                name: "organisations");

            migrationBuilder.DropSequence(
                name: "OrganizationReferenceSequence",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "VolunteerReferenceSequence",
                schema: "dbo");
        }
    }
}
