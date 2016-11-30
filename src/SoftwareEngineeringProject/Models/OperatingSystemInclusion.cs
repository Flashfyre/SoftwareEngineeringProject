using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.Models
{
    public class OperatingSystemInclusion : WebCrawlerManagedObject
    {
        [Key]
        [Column(Order = 0, TypeName = "varchar(64)")]
        public string OperatingSystemID { get; set; }
        [Key]
        [Column(Order = 1, TypeName = "varchar(64)")]
        public string IncludedOperatingSystemID { get; set; }
        [Column(Order = 2, TypeName = "datetime2")]
        public override DateTime? LastUpdatedDate { get; set; }
        [NotMapped]
        protected override string[] ColumnNames
        {
            get
            {
                return new string[] { nameof(OperatingSystemID), nameof(IncludedOperatingSystemID), nameof(LastUpdatedDate) };
            }
        }
        [NotMapped]
        protected override object[] Values
        {
            get
            {
                return new object[] { OperatingSystemID, IncludedOperatingSystemID, LastUpdatedDate };
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
    }
}
