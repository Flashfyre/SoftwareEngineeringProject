using SoftwareEngineeringProject.Data;
using SoftwareEngineeringProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftwareEngineeringProject.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext())
            {
                List<Vendor> vendors = context.Vendors.ToList();
                IEnumerable<string> vendorIDs = vendors.Select(v => v.VendorID);
                List<VendorCrawlPage> vendorCrawlPages = context.VendorCrawlPages.ToList();
                IEnumerable<string> vendorCrawlPageURLs = vendorCrawlPages.Select(vcp => vcp.URL);

                List<Vendor> newVendors = new List<Vendor>()
                {
                    new Vendor()
                    {
                        VendorID = "Bell"
                    }
                };

                newVendors.RemoveAll(v => vendorIDs.Contains(v.VendorID));

                foreach (Vendor v in newVendors)
                {
                    context.Vendors.Add(v);
                    vendors.Add(v);
                }

                List<VendorCrawlPage> newVendorCrawlPages = new List<VendorCrawlPage>() {
                    new VendorCrawlPage("Bell", "http://www.bell.ca/Mobility/Smartphones_and_mobile_internet_devices")
                };

                newVendorCrawlPages.RemoveAll(vcp => vendorCrawlPageURLs.Contains(vcp.URL));

                foreach (VendorCrawlPage vcp in newVendorCrawlPages)
                {
                    vcp.VendorCrawlPageID = (byte)((vendorCrawlPages.Any() ? vendorCrawlPages.Max(cp => cp.VendorCrawlPageID) : 0) + 1);
                    context.VendorCrawlPages.Add(vcp);
                }

                List<Manufacturer> manufacturers = context.Manufacturers.ToList();
                List<string> manufacturerNames = new List<string>() {
                    "Alcatel",
                    "Apple",
                    "Blackberry",
                    "Doro",
                    "Google",
                    "Huawei",
                    "HTC",
                    "Kyocera",
                    "LG",
                    "Motorola",
                    "Nokia",
                    "Samsung",
                    "Sony",
                    "ZTE"
                };

                foreach (string manufacturerName in manufacturerNames.Except(manufacturers.Select(m => m.ManufacturerID)))
                {
                    context.Manufacturers.Add(new Manufacturer()
                    {
                        ManufacturerID = manufacturerName
                    });
                }

                context.SaveChanges();
            }
        }
    }
}
