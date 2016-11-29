using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareEngineeringProject.Models
{
    public class MergedPhoneModel : WebCrawlerManagedObject
    {
        [Key]
        [Column(Order = 0, TypeName = "varchar(64)")]
        public string FromPhoneModelID { get; set; }
        [Column(Order = 1, TypeName = "varchar(64)")]
        public string ToPhoneModelID { get; set; }
        [ForeignKey("ToPhoneModelID")]
        public PhoneModel ToPhoneModel { get; set; }

        [Column(Order = 2, TypeName = "datetime2")]
        public override DateTime? LastUpdatedDate { get; set; }
        [NotMapped]
        protected override string[] ColumnNames
        {
            get
            {
                return new string[] { nameof(FromPhoneModelID), nameof(ToPhoneModelID), nameof(LastUpdatedDate) };
            }
        }
        [NotMapped]
        protected override object[] Values
        {
            get
            {
                return new object[] { FromPhoneModelID, ToPhoneModelID, LastUpdatedDate };
            }
        }
        [NotMapped]
        protected override int KeyCount
        {
            get
            {
                return 1;
            }
        }
    }
}
