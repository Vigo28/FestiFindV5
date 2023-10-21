using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FestiFindV5.Data.Migrations
{
    /// <inheritdoc />
    public partial class Db10Build : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey("pk_aspnetusertokens", "aspnetusertokens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
