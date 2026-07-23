using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceProject.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddedMessageInExceptionLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "ExceptionLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "ExceptionLogs");
        }
    }
}
