using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftwareEngineeringProject.Data.Migrations
{
    public partial class dbupdate_002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "VendorPhones",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastCrawledDate",
                table: "VendorCrawlPages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Vendors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "PhoneModels",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Phones",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "VendorPhones");

            migrationBuilder.DropColumn(
                name: "LastCrawledDate",
                table: "VendorCrawlPages");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "PhoneModels");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Phones");
        }
    }
}
