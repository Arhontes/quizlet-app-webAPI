using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quizlet_app_webAPI.Migrations
{
    public partial class RestartDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TokenExpires = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordsModules",
                columns: table => new
                {
                    WordsModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WordsCount = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordsModules", x => x.WordsModuleId);
                    table.ForeignKey(
                        name: "FK_WordsModules_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    WordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Meaning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Definition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transcription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WordImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WordsModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.WordId);
                    table.ForeignKey(
                        name: "FK_Words_WordsModules_WordsModuleId",
                        column: x => x.WordsModuleId,
                        principalTable: "WordsModules",
                        principalColumn: "WordsModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Words_WordsModuleId",
                table: "Words",
                column: "WordsModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_WordsModules_UserId",
                table: "WordsModules",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "WordsModules");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
