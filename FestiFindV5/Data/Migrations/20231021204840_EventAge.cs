using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FestiFindV5.Data.Migrations
{
    /// <inheritdoc />
    public partial class EventAge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "age",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "age",
                table: "Events");
        }
    }
}
