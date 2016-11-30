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
                    new VendorCrawlPage("Best Buy", "http://www.bestbuy.ca/en-CA/category/cell-phones-plans/696304.aspx?type=product&page=1&sortBy=relevance&sortDir=desc&pageSize=100"),
                    new VendorCrawlPage("TELUS", "http://www.telus.com/en/on/mobility/catalog/")
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

                if (context.PhoneModels.Any() && !context.OperatingSystemInclusions.Any())
                {
                    List<string> operatingSystems = context.PhoneModels.Select(p => p.OperatingSystem).Distinct().ToList();

                    List<string> inclusions;
                    string version;
                    bool checkVersions;

                    foreach (string osInclude in operatingSystems)
                    {
                        inclusions = new List<string>();
                        version = System.Text.RegularExpressions.Regex.Match(osInclude, " ([0-9x\\.]+)?").Groups[1].Value.Replace(".x", string.Empty);
                        checkVersions = version != string.Empty;
                        foreach (string os in operatingSystems)
                        {
                            if (os != osInclude)
                            {
                                if (osInclude.Contains(os))
                                    context.OperatingSystemInclusions.Add(new OperatingSystemInclusion()
                                    {
                                        OperatingSystemID = os,
                                        IncludedOperatingSystemID = osInclude
                                    });
                                else
                                {
                                    if (checkVersions)
                                    {
                                        string includeVersion = System.Text.RegularExpressions.Regex.Match(os, " ([0-9x\\.]+)?").Groups[1].Value.Replace(".x", string.Empty);
                                        if (includeVersion != string.Empty && version.Contains(includeVersion))
                                        {
                                            context.OperatingSystemInclusions.Add(new OperatingSystemInclusion()
                                            {
                                                OperatingSystemID = os,
                                                IncludedOperatingSystemID = osInclude
                                            });
                                        }
                                    }
                                }

                            }
                        }
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
