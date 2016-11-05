using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SoftwareEngineeringProject.Data;

namespace SoftwareEngineeringProject.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("SoftwareEngineeringProject.Models.Phone", b =>
                {
                    b.Property<string>("PhoneModelID")
                        .HasColumnType("varchar(64)");

                    b.Property<byte>("PhoneModelVariantID")
                        .HasColumnType("tinyint");

                    b.Property<string>("CarrierID")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Colour")
                        .HasColumnType("varchar(16)");

                    b.Property<bool>("IsUnlocked")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Memory")
                        .HasColumnType("varchar(16)");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PhoneModelID", "PhoneModelVariantID", "CarrierID");

                    b.HasIndex("CarrierID");

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

                    b.HasKey("PhoneModelID");

                    b.HasIndex("ManufacturerID");

                    b.ToTable("PhoneModels");
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

                    b.Property<string>("CarrierID")
                        .HasColumnType("varchar(64)");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("URL")
                        .HasColumnType("varchar(2083)");

                    b.HasKey("VendorID", "PhoneModelID", "PhoneModelVariantID", "CarrierID");

                    b.HasIndex("VendorID");

                    b.HasIndex("PhoneModelID", "PhoneModelVariantID", "CarrierID");

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

            modelBuilder.Entity("SoftwareEngineeringProject.Models.Phone", b =>
                {
                    b.HasOne("SoftwareEngineeringProject.Models.Carrier", "Carrier")
                        .WithMany("Phones")
                        .HasForeignKey("CarrierID")
                        .OnDelete(DeleteBehavior.Cascade);

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

            modelBuilder.Entity("SoftwareEngineeringProject.Models.VendorCrawlPage", b =>
                {
                    b.HasOne("SoftwareEngineeringProject.Models.Vendor", "Vendor")
                        .WithMany("CrawlPages")
                        .HasForeignKey("VendorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SoftwareEngineeringProject.Models.VendorPhone", b =>
                {
                    b.HasOne("SoftwareEngineeringProject.Models.Vendor", "Vendor")
                        .WithMany("VendorPhones")
                        .HasForeignKey("VendorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SoftwareEngineeringProject.Models.Phone", "Phone")
                        .WithMany("VendorPhones")
                        .HasForeignKey("PhoneModelID", "PhoneModelVariantID", "CarrierID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
