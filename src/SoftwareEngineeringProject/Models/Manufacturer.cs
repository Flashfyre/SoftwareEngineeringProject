﻿using System;
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
    }
}