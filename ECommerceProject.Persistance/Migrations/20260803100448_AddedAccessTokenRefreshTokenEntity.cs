using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceProject.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddedAccessTokenRefreshTokenEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "RefreshTokens");
        }
    }
}
