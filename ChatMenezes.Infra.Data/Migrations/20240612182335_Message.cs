using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatMenezes.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Message : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MessageText",
                table: "Message",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageText",
                table: "Message");
        }
    }
}
