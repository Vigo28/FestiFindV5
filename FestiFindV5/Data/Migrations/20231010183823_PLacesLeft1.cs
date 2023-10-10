using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FestiFindV5.Data.Migrations
{
    /// <inheritdoc />
    public partial class PLacesLeft1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxParticipants",
                table: "Events",
                newName: "PlacesLeft");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlacesLeft",
                table: "Events",
                newName: "MaxParticipants");
        }
    }
}
