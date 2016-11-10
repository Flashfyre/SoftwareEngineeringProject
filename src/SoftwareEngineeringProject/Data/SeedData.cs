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

                foreach (string vendorID in Vendor.VendorIDs)
                {
                    if (!vendorIDs.Any(vid => vid == vendorID))
                    {
                        Vendor newVendor = new Vendor()
                        {
                            VendorID = vendorID
                        };

                        context.Vendors.Add(newVendor);
                        vendors.Add(newVendor);
                    }
                }

                List<VendorCrawlPage> newVendorCrawlPages = new List<VendorCrawlPage>() {
                    new VendorCrawlPage("Bell", "http://www.bell.ca/Mobility/Smartphones_and_mobile_internet_devices"),
                    new VendorCrawlPage("Best Buy", "http://www.bestbuy.ca/en-CA/category/cell-phones-plans/696304.aspx?type=product&page=1&sortBy=relevance&sortDir=desc&pageSize=100")
                };

                newVendorCrawlPages.RemoveAll(vcp => vendorCrawlPageURLs.Contains(vcp.URL));

                foreach (VendorCrawlPage vcp in newVendorCrawlPages)
                {
                    vcp.VendorCrawlPageID = (byte)((vendorCrawlPages.Any(cp => cp.VendorID == vcp.VendorID) ? vendorCrawlPages.Where(cp => cp.VendorID == vcp.VendorID).Max(cp => cp.VendorCrawlPageID) : 0) + 1);
                    context.VendorCrawlPages.Add(vcp);
                }

                List<Manufacturer> manufacturers = context.Manufacturers.ToList();

                foreach (string manufacturerName in Manufacturer.ManufacturerIDs.Except(manufacturers.Select(m => m.ManufacturerID)))
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
