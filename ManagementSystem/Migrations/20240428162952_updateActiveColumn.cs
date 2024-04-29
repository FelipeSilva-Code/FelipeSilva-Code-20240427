using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class updateActiveColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TgInactive",
                table: "User",
                newName: "TgActive");

            migrationBuilder.RenameColumn(
                name: "TgInactive",
                table: "Unity",
                newName: "TgActive");

            migrationBuilder.RenameColumn(
                name: "TgInactive",
                table: "Employee",
                newName: "TgActive");

            migrationBuilder.CreateTable(
                name: "UserViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserViewModel", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserViewModel");

            migrationBuilder.RenameColumn(
                name: "TgActive",
                table: "User",
                newName: "TgInactive");

            migrationBuilder.RenameColumn(
                name: "TgActive",
                table: "Unity",
                newName: "TgInactive");

            migrationBuilder.RenameColumn(
                name: "TgActive",
                table: "Employee",
                newName: "TgInactive");
        }
    }
}
