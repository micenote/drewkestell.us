using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrewKestellSite.Migrations
{
    public partial class AddDateTimeToSiteVisits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "SiteVisits",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "SiteVisits");
        }
    }
}
