using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.Models
{
    public class PhoneModel : WebCrawlerManagedObject
    {
        [Key]
        [Column(Order = 0, TypeName = "varchar(64)")]
        public string PhoneModelID { get; set; }
        [Column(Order = 1, TypeName = "varchar(64)")]
        public string ManufacturerID { get; set; }
        [ForeignKey("ManufacturerID")]
        public Manufacturer Manufacturer { get; set; }
        [Column(Order = 2, TypeName = "varchar(64)")]
        public string OperatingSystem { get; set; }
        [Column(Order = 3, TypeName = "datetime2")]
        public DateTime? ReleaseDate { get; set; }
        [Column(Order = 4, TypeName = "datetime2")]
        public override DateTime? LastUpdatedDate { get; set; }
        public ICollection<Phone> Phones { get; set; }
        [NotMapped]
        protected override string[] ColumnNames
        {
            get
            {
                return new string[] { nameof(PhoneModelID), nameof(ManufacturerID), nameof(OperatingSystem), nameof(ReleaseDate), nameof(LastUpdatedDate) };
            }
        }
        [NotMapped]
        protected override object[] Values
        {
            get
            {
                return new object[] { PhoneModelID, ManufacturerID, OperatingSystem, ReleaseDate, LastUpdatedDate };
            }
        }
    }
}
