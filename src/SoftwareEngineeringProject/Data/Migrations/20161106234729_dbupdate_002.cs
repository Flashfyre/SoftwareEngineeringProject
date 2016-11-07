using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftwareEngineeringProject.Data.Migrations
{
    public partial class dbupdate_002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedPhoneModels",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneModelID = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedPhoneModels", x => new { x.UserID, x.PhoneModelID });
                    table.ForeignKey(
                        name: "FK_SavedPhoneModels_PhoneModels_PhoneModelID",
                        column: x => x.PhoneModelID,
                        principalTable: "PhoneModels",
                        principalColumn: "PhoneModelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavedPhoneModels_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedPhoneModels_PhoneModelID",
                table: "SavedPhoneModels",
                column: "PhoneModelID");

            migrationBuilder.CreateIndex(
                name: "IX_SavedPhoneModels_UserID",
                table: "SavedPhoneModels",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedPhoneModels");
        }
    }
}
