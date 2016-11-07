using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.Models
{
    public class SavedPhoneModel
    {
        [Column(Order = 0, TypeName = "nvarchar(450)")]
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }
        [Column(Order = 1, TypeName = "varchar(64)")]
        public string PhoneModelID { get; set; }
        [ForeignKey("PhoneModelID")]
        public PhoneModel PhoneModel { get; set; }
        [Column(Order = 2, TypeName = "datetime2")]
        public DateTime AddedDate { get; set; }
    }
}
