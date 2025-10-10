using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCSentinelUI_.Migrations
{
    public partial class AddVersionColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "OpenIddictApplications",
                type: "rowversion",
                rowVersion: true,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "OpenIddictApplications");
        }
    }
}
