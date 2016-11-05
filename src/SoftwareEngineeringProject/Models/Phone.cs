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
        [Key]
        [Column(Order = 2, TypeName = "varchar(64)")]
        public string CarrierID { get; set; }
        [ForeignKey("CarrierID")]
        public Carrier Carrier { get; set; }
        [Column(Order = 3, TypeName = "bit")]
        public bool IsUnlocked { get; set; }
        [Column(Order = 4, TypeName = "varchar(16)")]
        public string Colour { get; set; }
        [Column(Order = 5, TypeName = "varchar(16)")]
        public string Memory { get; set; }
        [Column(Order = 6, TypeName = "datetime2")]
        public DateTime? ReleaseDate { get; set; }
        [Column(Order = 7, TypeName = "datetime2")]
        public override DateTime? LastUpdatedDate { get; set; }
        public ICollection<VendorPhone> VendorPhones { get; set; }
        [NotMapped]
        protected override string[] ColumnNames
        {
            get
            {
                return new string[] { nameof(PhoneModelID), nameof(PhoneModelVariantID), nameof(CarrierID), nameof(IsUnlocked), nameof(Colour), nameof(Memory), nameof(LastUpdatedDate) };
            }
        }
        [NotMapped]
        protected override object[] Values
        {
            get
            {
                return new object[] { PhoneModelID, PhoneModelVariantID, CarrierID, IsUnlocked, Colour, Memory, LastUpdatedDate };
            }
        }
        [NotMapped]
        protected override int KeyCount
        {
            get
            {
                return 3;
            }
        }
        [NotMapped]
        public string ImageURL
        {
            get
            {
                string vendorID = VendorPhones.Any() ? VendorPhones.First().VendorID : null;
                string url = vendorID != null ? string.Format("/images/{0}/{1}_{2}1.png", vendorID, PhoneModelID, Colour == "N/A" ? string.Empty : Colour + "_") : null;
                if (url != null && File.Exists("wwwroot" + url))
                    return string.Format("/images/{0}/{1}_{2}1.png", vendorID, PhoneModelID, Colour == "N/A" ? string.Empty : Colour + "_");
                else
                    return string.Empty;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}/{2}/{3}/{4}/{5}", GetType().Name, PhoneModelID, CarrierID, IsUnlocked ? "Unlocked" : "Contract", Memory, Colour);
        }
    }

    /*public enum EnumNetworkType : byte
    {
        _None,
        _3G,
        _4G,
        _LTE,
        _LTE_Advanced
    }*/
}
