using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.Models
{
    public class Phone : WebCrawlerManagedObject
    {
        [Key]
        [Column(Order = 0, TypeName = "varchar(64)")]
        public string PhoneModelID { get; set; }
        [ForeignKey("PhoneModelID")]
        public PhoneModel Model { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 1, TypeName = "tinyint")]
        public byte PhoneModelVariantID { get; set; }
        [Column(Order = 2, TypeName = "varchar(64)")]
        public string Colour { get; set; }
        [Column(Order = 3, TypeName = "varchar(16)")]
        public string Memory { get; set; }
        [Column(Order = 4, TypeName = "datetime2")]
        public override DateTime? LastUpdatedDate { get; set; }
        public ICollection<VendorPhone> VendorPhones { get; set; }
        [NotMapped]
        protected override string[] ColumnNames
        {
            get
            {
                return new string[] { nameof(PhoneModelID), nameof(PhoneModelVariantID), nameof(Colour), nameof(Memory), nameof(LastUpdatedDate) };
            }
        }
        [NotMapped]
        protected override object[] Values
        {
            get
            {
                return new object[] { PhoneModelID, PhoneModelVariantID, Colour, Memory, LastUpdatedDate };
            }
        }
        [NotMapped]
        protected override int KeyCount
        {
            get
            {
                return 2;
            }
        }

        public string GetImageURL(ICollection<VendorPhone> vendorPhones = null)
        {
            if (vendorPhones == null)
                vendorPhones = VendorPhones;
            string vendorID = vendorPhones.Any() ? GetImageSourceVendorID(vendorPhones) : null;
            string url = vendorID != null ? string.Format("/images/{0}/{1}_{2}1.png", vendorID, PhoneModelID.Replace(" ", "_").Replace("+", "_Plus"),
                Colour == "N/A" ? string.Empty : Colour.Replace(" ", "_") + "_") : null;
            if (url != null && File.Exists("wwwroot" + url))
                return url;
            else
                return "/images/Default.png";
        }

        public string GetImageSourceVendorID(ICollection<VendorPhone> vendorPhones)
        {
            foreach (string vendorID in Vendor.VendorIDs)
            {
                if (vendorPhones.Any(vp => vp.VendorID == vendorID))
                    return vendorID;
            }

            return null;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}/{2}/{3}", GetType().Name, PhoneModelID, Memory, Colour);
        }
    }
}
