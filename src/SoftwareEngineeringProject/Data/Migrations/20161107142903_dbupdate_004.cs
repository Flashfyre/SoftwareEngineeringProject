using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftwareEngineeringProject.Data.Migrations
{
    public partial class dbupdate_004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "VendorPhones",
                type: "nvarchar(4000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OperatingSystem",
                table: "PhoneModels",
                type: "varchar(64)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "VendorPhones");

            migrationBuilder.DropColumn(
                name: "OperatingSystem",
                table: "PhoneModels");
        }
    }
}
