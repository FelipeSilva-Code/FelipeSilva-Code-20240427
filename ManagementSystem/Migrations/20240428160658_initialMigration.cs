using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Unity",
                columns: table => new
                {
                    PkId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DsName = table.Column<string>(type: "text", nullable: false),
                    DsCode = table.Column<string>(type: "text", nullable: false),
                    TgInactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unity", x => x.PkId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    PkId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DsLogin = table.Column<string>(type: "text", nullable: false),
                    DsPassword = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    TgInactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.PkId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    PkId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DsName = table.Column<string>(type: "text", nullable: false),
                    FkUser = table.Column<int>(type: "integer", nullable: false),
                    FkUnity = table.Column<int>(type: "integer", nullable: false),
                    TgInactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.PkId);
                    table.ForeignKey(
                        name: "FK_Employee_Unity_FkUnity",
                        column: x => x.FkUnity,
                        principalTable: "Unity",
                        principalColumn: "PkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_User_FkUser",
                        column: x => x.FkUser,
                        principalTable: "User",
                        principalColumn: "PkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_FkUnity",
                table: "Employee",
                column: "FkUnity");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_FkUser",
                table: "Employee",
                column: "FkUser",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Unity");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
