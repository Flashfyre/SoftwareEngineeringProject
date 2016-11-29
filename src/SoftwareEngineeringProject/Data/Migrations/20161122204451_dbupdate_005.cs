using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftwareEngineeringProject.Data.Migrations
{
    public partial class dbupdate_005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MergedPhoneModels",
                columns: table => new
                {
                    FromPhoneModelID = table.Column<string>(type: "varchar(64)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToPhoneModelID = table.Column<string>(type: "varchar(64)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MergedPhoneModels", x => x.FromPhoneModelID);
                    table.ForeignKey(
                        name: "FK_MergedPhoneModels_PhoneModels_ToPhoneModelID",
                        column: x => x.ToPhoneModelID,
                        principalTable: "PhoneModels",
                        principalColumn: "PhoneModelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MergedPhoneModels_ToPhoneModelID",
                table: "MergedPhoneModels",
                column: "ToPhoneModelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MergedPhoneModels");
        }
    }
}
