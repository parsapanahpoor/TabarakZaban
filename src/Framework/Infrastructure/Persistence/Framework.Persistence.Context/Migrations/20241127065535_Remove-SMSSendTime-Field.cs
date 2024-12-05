using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Framework.Persistence.Context.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSMSSendTimeField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireMobileSMSDateTime",
                table: "IdentityUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireMobileSMSDateTime",
                table: "IdentityUsers",
                type: "datetime2",
                nullable: true);
        }
    }
}
