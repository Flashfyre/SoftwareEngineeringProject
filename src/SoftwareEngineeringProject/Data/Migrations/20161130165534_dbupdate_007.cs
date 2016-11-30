using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftwareEngineeringProject.Data.Migrations
{
    public partial class dbupdate_007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperatingSystemInclusions",
                columns: table => new
                {
                    OperatingSystemID = table.Column<string>(type: "varchar(64)", nullable: false),
                    IncludedOperatingSystemID = table.Column<string>(type: "varchar(64)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingSystemInclusions", x => new { x.OperatingSystemID, x.IncludedOperatingSystemID });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperatingSystemInclusions");
        }
    }
}
