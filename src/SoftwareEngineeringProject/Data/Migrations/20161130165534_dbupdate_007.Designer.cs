﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SoftwareEngineeringProject.Data;

namespace SoftwareEngineeringProject.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161130165534_dbupdate_007")]
    partial class dbupdate_007
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<string>", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.Carrier", b =>
                {
                    b.Property<string>("CarrierID")
                        .HasColumnType("varchar(64)");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CarrierID");

                    b.ToTable("Carriers");
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.Manufacturer", b =>
                {
                    b.Property<string>("ManufacturerID")
                        .HasColumnType("varchar(64)");

                    b.HasKey("ManufacturerID");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.MergedPhoneModel", b =>
                {
                    b.Property<string>("FromPhoneModelID")
                        .HasColumnType("varchar(64)");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ToPhoneModelID")
                        .HasColumnType("varchar(64)");

                    b.HasKey("FromPhoneModelID");

                    b.HasIndex("ToPhoneModelID");

                    b.ToTable("MergedPhoneModels");
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.OperatingSystemInclusion", b =>
                {
                    b.Property<string>("OperatingSystemID")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("IncludedOperatingSystemID")
                        .HasColumnType("varchar(64)");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OperatingSystemID", "IncludedOperatingSystemID");

                    b.ToTable("OperatingSystemInclusions");
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.Phone", b =>
                {
                    b.Property<string>("PhoneModelID")
                        .HasColumnType("varchar(64)");

                    b.Property<byte>("PhoneModelVariantID")
                        .HasColumnType("tinyint");

                    b.Property<string>("Colour")
                        .HasColumnType("varchar(64)");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Memory")
                        .HasColumnType("varchar(16)");

                    b.HasKey("PhoneModelID", "PhoneModelVariantID");

                    b.HasIndex("PhoneModelID");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.PhoneModel", b =>
                {
                    b.Property<string>("PhoneModelID")
                        .HasColumnType("varchar(64)");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ManufacturerID")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("OperatingSystem")
                        .HasColumnType("varchar(64)");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PhoneModelID");

                    b.HasIndex("ManufacturerID");

                    b.ToTable("PhoneModels");
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.SavedPhoneModel", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhoneModelID")
                        .HasColumnType("varchar(64)");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserID", "PhoneModelID");

                    b.HasIndex("PhoneModelID");

                    b.HasIndex("UserID");

                    b.ToTable("SavedPhoneModels");
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.Vendor", b =>
                {
                    b.Property<string>("VendorID")
                        .HasColumnType("varchar(64)");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("VendorID");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.VendorCrawlPage", b =>
                {
                    b.Property<string>("VendorID")
                        .HasColumnType("varchar(64)");

                    b.Property<byte>("VendorCrawlPageID")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("URL")
                        .HasColumnType("varchar(2083)");

                    b.HasKey("VendorID", "VendorCrawlPageID");

                    b.HasIndex("VendorID");

                    b.ToTable("VendorCrawlPages");
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.VendorPhone", b =>
                {
                    b.Property<string>("VendorID")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("PhoneModelID")
                        .HasColumnType("varchar(64)");

                    b.Property<byte>("PhoneModelVariantID")
                        .HasColumnType("tinyint");

                    b.Property<byte>("PhoneVendorPhoneID")
                        .HasColumnType("tinyint");

                    b.Property<string>("CarrierID")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentType")
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Price")
                        .HasColumnType("varchar(16)");

                    b.Property<string>("Restrictions")
                        .HasColumnType("varchar(128)");

                    b.Property<string>("URL")
                        .HasColumnType("varchar(2083)");

                    b.HasKey("VendorID", "PhoneModelID", "PhoneModelVariantID", "PhoneVendorPhoneID");

                    b.HasIndex("CarrierID");

                    b.HasIndex("VendorID");

                    b.HasIndex("PhoneModelID", "PhoneModelVariantID");

                    b.ToTable("VendorPhones");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<string>")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SoftwareEngineeringProject.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SoftwareEngineeringProject.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<string>")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SoftwareEngineeringProject.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.MergedPhoneModel", b =>
                {
                    b.HasOne("SoftwareEngineeringProject.Models.PhoneModel", "ToPhoneModel")
                        .WithMany()
                        .HasForeignKey("ToPhoneModelID");
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.Phone", b =>
                {
                    b.HasOne("SoftwareEngineeringProject.Models.PhoneModel", "Model")
                        .WithMany("Phones")
                        .HasForeignKey("PhoneModelID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.PhoneModel", b =>
                {
                    b.HasOne("SoftwareEngineeringProject.Models.Manufacturer", "Manufacturer")
                        .WithMany("PhoneModels")
                        .HasForeignKey("ManufacturerID");
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.SavedPhoneModel", b =>
                {
                    b.HasOne("SoftwareEngineeringProject.Models.PhoneModel", "PhoneModel")
                        .WithMany()
                        .HasForeignKey("PhoneModelID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SoftwareEngineeringProject.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.VendorCrawlPage", b =>
                {
                    b.HasOne("SoftwareEngineeringProject.Models.Vendor", "Vendor")
                        .WithMany("CrawlPages")
                        .HasForeignKey("VendorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.VendorPhone", b =>
                {
                    b.HasOne("SoftwareEngineeringProject.Models.Carrier", "Carrier")
                        .WithMany("VendorPhones")
                        .HasForeignKey("CarrierID");

                    b.HasOne("SoftwareEngineeringProject.Models.Vendor", "Vendor")
                        .WithMany("VendorPhones")
                        .HasForeignKey("VendorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SoftwareEngineeringProject.Models.Phone", "Phone")
                        .WithMany("VendorPhones")
                        .HasForeignKey("PhoneModelID", "PhoneModelVariantID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
