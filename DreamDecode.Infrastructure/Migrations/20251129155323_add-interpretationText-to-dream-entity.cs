using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamDecode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addinterpretationTexttodreamentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InterpretationText",
                table: "Dreams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterpretationText",
                table: "Dreams");
        }
    }
}
