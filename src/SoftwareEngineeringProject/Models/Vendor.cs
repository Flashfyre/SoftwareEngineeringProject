using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.Models
{
    public class Vendor : WebCrawlerManagedObject
    {
        [Key]
        [Column(Order = 0, TypeName = "varchar(64)")]
        public string VendorID { get; set; }
        [Column(Order = 1, TypeName = "datetime2")]
        public override DateTime? LastUpdatedDate { get; set; }
        public ICollection<VendorPhone> VendorPhones { get; set; }
        public ICollection<VendorCrawlPage> CrawlPages { get; set; }
        [NotMapped]
        protected override string[] ColumnNames
        {
            get
            {
                return new string[] { nameof(VendorID), nameof(LastUpdatedDate) };
            }
        }
        [NotMapped]
        protected override object[] Values
        {
            get
            {
                return new object[] { VendorID, LastUpdatedDate };
            }
        }
        [NotMapped]
        public string ImageURL
        {
            get
            {
                string URL = string.Format("/images/{0}.png", VendorID.Replace(" ", "_"));

                return File.Exists(string.Format("wwwroot{0}", URL)) ? URL : string.Empty;
            }
        }

        public static readonly string[] VendorIDs = new string[] { "Bell", "Best Buy", "TELUS" };
    }
}
