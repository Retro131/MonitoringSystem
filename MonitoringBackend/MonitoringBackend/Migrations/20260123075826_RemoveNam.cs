using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfotecsBackend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Devices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Devices",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }
    }
}
