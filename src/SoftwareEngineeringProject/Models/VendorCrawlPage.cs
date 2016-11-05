using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.Models
{
    public class VendorCrawlPage : WebCrawlerManagedObject
    {
        public VendorCrawlPage() : base() { }

        public VendorCrawlPage(string VendorID, string URL) : base()
        {
            this.VendorID = VendorID;
            this.URL = URL;
        }

        [Key]
        [Column(Order = 0, TypeName = "varchar(64)")]
        public string VendorID { get; set; }
        [ForeignKey("VendorID")]
        public Vendor Vendor { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 1, TypeName = "tinyint")]
        public byte VendorCrawlPageID { get; set; }
        [Column(Order = 2, TypeName = "varchar(2083)")]
        public string URL { get; set; }
        [Column(Order = 3, TypeName = "datetime2")]
        public override DateTime? LastUpdatedDate { get; set; }

        protected override string[] ColumnNames
        {
            get
            {
                return new string[] { nameof(VendorID), nameof(VendorCrawlPageID), nameof(URL), nameof(LastUpdatedDate) };
            }
        }

        protected override object[] Values
        {
            get
            {
                return new object[] { VendorID, VendorCrawlPageID, URL, LastUpdatedDate };
            }
        }

        protected override int KeyCount
        {
            get
            {
                return 2;
            }
        }

        public override string ToString()
        {
            return string.Format("page {0} of {1} for {2} ({3})", VendorCrawlPageID, Vendor.CrawlPages.Count(), Vendor.ToString(), URL);
        }
    }
}
