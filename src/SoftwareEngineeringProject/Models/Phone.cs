using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Column(Order = 2, TypeName = "bit")]
        public bool IsUnlocked { get; set; }
        [Column(Order = 3, TypeName = "varchar(16)")]
        public string Colour { get; set; }
        [Column(Order = 4, TypeName = "varchar(16)")]
        public string Memory { get; set; }
        [Column(Order = 5, TypeName = "datetime2")]
        public override DateTime? LastUpdatedDate { get; set; }
        public ICollection<VendorPhone> VendorPhones { get; set; }
        [NotMapped]
        protected override string[] columnNames
        {
            get
            {
                return new string[] { nameof(PhoneModelID), nameof(PhoneModelVariantID), nameof(IsUnlocked), nameof(Colour), nameof(Memory), nameof(LastUpdatedDate) };
            }
        }
        [NotMapped]
        protected override object[] values
        {
            get
            {
                return new object[] { PhoneModelID, PhoneModelVariantID, IsUnlocked, Colour, Memory, LastUpdatedDate };
            }
        }
        [NotMapped]
        protected override int keyCount
        {
            get
            {
                return 2;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}/{2}/{3}/{4}", GetType().Name, PhoneModelID, IsUnlocked ? "Unlocked" : "Contract", Memory, Colour);
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
