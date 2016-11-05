using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftwareEngineeringProject.Data.Migrations
{
    public partial class dbupdate_001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carriers",
                columns: table => new
                {
                    CarrierID = table.Column<string>(type: "varchar(64)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carriers", x => x.CarrierID);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    ManufacturerID = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.ManufacturerID);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorID = table.Column<string>(type: "varchar(64)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorID);
                });

            migrationBuilder.CreateTable(
                name: "PhoneModels",
                columns: table => new
                {
                    PhoneModelID = table.Column<string>(type: "varchar(64)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ManufacturerID = table.Column<string>(type: "varchar(64)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneModels", x => x.PhoneModelID);
                    table.ForeignKey(
                        name: "FK_PhoneModels_Manufacturers_ManufacturerID",
                        column: x => x.ManufacturerID,
                        principalTable: "Manufacturers",
                        principalColumn: "ManufacturerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendorCrawlPages",
                columns: table => new
                {
                    VendorID = table.Column<string>(type: "varchar(64)", nullable: false),
                    VendorCrawlPageID = table.Column<byte>(type: "tinyint", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    URL = table.Column<string>(type: "varchar(2083)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorCrawlPages", x => new { x.VendorID, x.VendorCrawlPageID });
                    table.ForeignKey(
                        name: "FK_VendorCrawlPages_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "VendorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    PhoneModelID = table.Column<string>(type: "varchar(64)", nullable: false),
                    PhoneModelVariantID = table.Column<byte>(type: "tinyint", nullable: false),
                    CarrierID = table.Column<string>(type: "varchar(64)", nullable: false),
                    Colour = table.Column<string>(type: "varchar(16)", nullable: true),
                    IsUnlocked = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Memory = table.Column<string>(type: "varchar(16)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => new { x.PhoneModelID, x.PhoneModelVariantID, x.CarrierID });
                    table.ForeignKey(
                        name: "FK_Phones_Carriers_CarrierID",
                        column: x => x.CarrierID,
                        principalTable: "Carriers",
                        principalColumn: "CarrierID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Phones_PhoneModels_PhoneModelID",
                        column: x => x.PhoneModelID,
                        principalTable: "PhoneModels",
                        principalColumn: "PhoneModelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorPhones",
                columns: table => new
                {
                    VendorID = table.Column<string>(type: "varchar(64)", nullable: false),
                    PhoneModelID = table.Column<string>(type: "varchar(64)", nullable: false),
                    PhoneModelVariantID = table.Column<byte>(type: "tinyint", nullable: false),
                    CarrierID = table.Column<string>(type: "varchar(64)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    URL = table.Column<string>(type: "varchar(2083)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorPhones", x => new { x.VendorID, x.PhoneModelID, x.PhoneModelVariantID, x.CarrierID });
                    table.ForeignKey(
                        name: "FK_VendorPhones_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "VendorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorPhones_Phones_PhoneModelID_PhoneModelVariantID_CarrierID",
                        columns: x => new { x.PhoneModelID, x.PhoneModelVariantID, x.CarrierID },
                        principalTable: "Phones",
                        principalColumns: new[] { "PhoneModelID", "PhoneModelVariantID", "CarrierID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phones_CarrierID",
                table: "Phones",
                column: "CarrierID");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_PhoneModelID",
                table: "Phones",
                column: "PhoneModelID");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneModels_ManufacturerID",
                table: "PhoneModels",
                column: "ManufacturerID");

            migrationBuilder.CreateIndex(
                name: "IX_VendorCrawlPages_VendorID",
                table: "VendorCrawlPages",
                column: "VendorID");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPhones_VendorID",
                table: "VendorPhones",
                column: "VendorID");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPhones_PhoneModelID_PhoneModelVariantID_CarrierID",
                table: "VendorPhones",
                columns: new[] { "PhoneModelID", "PhoneModelVariantID", "CarrierID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorCrawlPages");

            migrationBuilder.DropTable(
                name: "VendorPhones");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "Carriers");

            migrationBuilder.DropTable(
                name: "PhoneModels");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
