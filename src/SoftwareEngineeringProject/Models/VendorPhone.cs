using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.Models
{
    public class VendorPhone : WebCrawlerManagedObject
    {
        [Key]
        [Column(Order = 0, TypeName = "varchar(64)")]
        public string VendorID { get; set; }
        [ForeignKey("VendorID")]
        public Vendor Vendor { get; set; }
        [Key]
        [Column(Order = 1, TypeName = "varchar(64)")]
        public string PhoneModelID { get; set; }
        [Key]
        [Column(Order = 2, TypeName = "tinyint")]
        public byte PhoneModelVariantID { get; set; }
        [ForeignKey("PhoneModelID,PhoneModelVariantID")]
        public Phone Phone { get; set; }
        [Column(Order = 3, TypeName = "varchar(2083)")]
        public string URL { get; set; }
        [Column(Order = 4, TypeName = "datetime2")]
        public override DateTime? LastUpdatedDate { get; set; }
        [NotMapped]
        protected override string[] columnNames { get
            {
                return new string[] { nameof(VendorID), nameof(PhoneModelID), nameof(PhoneModelVariantID), nameof(URL), nameof(LastUpdatedDate) };
            }
        }
        [NotMapped]
        protected override object[] values { get
            {
                return new object[] { VendorID, PhoneModelID, PhoneModelVariantID, URL, LastUpdatedDate };
            }
        }
        [NotMapped]
        protected override int keyCount
        {
            get
            {
                return 3;
            }
        }
    }
}
