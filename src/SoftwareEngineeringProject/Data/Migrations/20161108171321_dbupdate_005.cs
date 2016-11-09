using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftwareEngineeringProject.Data.Migrations
{
    public partial class dbupdate_005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUnlocked",
                table: "Phones");

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "Phones",
                type: "varchar(32)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "Phones",
                type: "varchar(16)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Phones");

            migrationBuilder.AddColumn<bool>(
                name: "IsUnlocked",
                table: "Phones",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
