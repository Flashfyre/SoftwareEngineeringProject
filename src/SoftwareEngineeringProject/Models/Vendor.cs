using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.Models
{
    public class Vendor : WebCrawlerManagedObject
    {
        [Key]
        [Column(Order = 0, TypeName = "varchar(64)")]
        public string VendorID { get; set; }
        public ICollection<VendorPhone> VendorPhones { get; set; }
        public ICollection<VendorCrawlPage> CrawlPages { get; set; }
        [Column(Order = 1, TypeName = "datetime2")]
        public override DateTime? LastUpdatedDate { get; set; }
        [NotMapped]
        protected override string[] columnNames
        {
            get
            {
                return new string[] { nameof(VendorID), nameof(LastUpdatedDate) };
            }
        }
        [NotMapped]
        protected override object[] values
        {
            get
            {
                return new object[] { VendorID, LastUpdatedDate };
            }
        }
    }
}
