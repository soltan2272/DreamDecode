using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamDecode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InterpretationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interpretations",
                columns: table => new
                {
                    InterpretationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DreamId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InterpretationText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterpretedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interpretations", x => x.InterpretationId);
                    table.ForeignKey(
                        name: "FK_Interpretations_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Interpretations_Dreams_DreamId",
                        column: x => x.DreamId,
                        principalTable: "Dreams",
                        principalColumn: "DreamId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interpretations_AdminId",
                table: "Interpretations",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Interpretations_DreamId",
                table: "Interpretations",
                column: "DreamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interpretations");
        }
    }
}
