using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoftwareEngineeringProject.Models;

namespace SoftwareEngineeringProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<string>, string>
    {
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<PhoneModel> PhoneModels { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<VendorPhone> VendorPhones { get; set; }
        public DbSet<VendorCrawlPage> VendorCrawlPages { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Vendor>(e =>
            {
                e.HasKey("VendorID");
                e.HasMany(x => x.CrawlPages).WithOne(x => x.Vendor);
                e.HasMany(x => x.VendorPhones).WithOne(x => x.Vendor);
            });
            builder.Entity<PhoneModel>(e =>
            {
                e.HasKey("PhoneModelID");
                e.HasOne(x => x.Manufacturer).WithMany(x => x.PhoneModels);
                e.HasMany(x => x.Phones).WithOne(x => x.Model);
            });
            builder.Entity<Phone>(e =>
            {
                e.HasKey("PhoneModelID", "PhoneModelVariantID", "CarrierID");
                e.HasOne(x => x.Model).WithMany(x => x.Phones);
                e.HasOne(x => x.Carrier).WithMany(x => x.Phones);
                e.HasMany(x => x.VendorPhones).WithOne(x => x.Phone);
            });
            builder.Entity<Carrier>(e =>
            {
                e.HasKey("CarrierID");
                e.HasMany(x => x.Phones).WithOne(x => x.Carrier);
            });
            builder.Entity<VendorPhone>(e =>
            {
                e.HasKey("VendorID", "PhoneModelID", "PhoneModelVariantID", "CarrierID");
                e.HasOne(x => x.Vendor).WithMany(x => x.VendorPhones);
                e.HasOne(x => x.Phone).WithMany(x => x.VendorPhones);
            });
            builder.Entity<VendorCrawlPage>(e =>
            {
                e.HasKey("VendorID", "VendorCrawlPageID");
                e.HasOne(x => x.Vendor).WithMany(x => x.CrawlPages);
            });
            builder.Entity<Manufacturer>(e =>
            {
                e.HasKey("ManufacturerID");
                e.HasMany(x => x.PhoneModels).WithOne(x => x.Manufacturer);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=tcp:softwareengineeringproject.database.windows.net,1433;Initial Catalog=SoftwareEngineeringProject;Persist Security Info=False;User ID=seprojadmin;Password=adm!nPassword;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;MultipleActiveResultSets=true;");
        }
    }
}
