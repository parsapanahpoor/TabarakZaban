using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Framework.Persistence.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAvatarField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserAvatar",
                table: "IdentityUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAvatar",
                table: "IdentityUsers");
        }
    }
}
