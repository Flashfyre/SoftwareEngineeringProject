using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.Models
{
    public class Manufacturer
    {
        [Column(Order = 0, TypeName = "varchar(64)")]
        public string ManufacturerID { get; set; }
        public ICollection<PhoneModel> PhoneModels { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", GetType().Name, ManufacturerID);
        }

        public static readonly string[] ManufacturerIDs = new string[]
        {
            "Alcatel",
            "Apple",
            "Blackberry",
            "Doro",
            "Google",
            "Huawei",
            "HTC",
            "Kyocera",
            "LG",
            "Microsoft",
            "Motorola",
            "Nokia",
            "Samsung",
            "Sonim",
            "Sony",
            "ZTE"
        };
    }
}
