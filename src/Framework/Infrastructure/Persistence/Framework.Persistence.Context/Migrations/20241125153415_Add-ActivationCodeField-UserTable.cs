using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Framework.Persistence.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddActivationCodeFieldUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivationCode",
                table: "IdentityUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireMobileSMSDateTime",
                table: "IdentityUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "IdentityUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBan",
                table: "IdentityUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationCode",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "ExpireMobileSMSDateTime",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "IsBan",
                table: "IdentityUsers");
        }
    }
}
