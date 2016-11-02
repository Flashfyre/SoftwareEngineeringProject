using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftwareEngineeringProject.Data.Migrations
{
    public partial class dbupdate_003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastCrawledDate",
                table: "VendorCrawlPages");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "VendorCrawlPages",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "VendorCrawlPages");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastCrawledDate",
                table: "VendorCrawlPages",
                type: "datetime2",
                nullable: true);
        }
    }
}
