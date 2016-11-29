using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 3, TypeName = "tinyint")]
        public byte PhoneVendorPhoneID { get; set; }
        [Column(Order = 4, TypeName = "varchar(64)")]
        public string CarrierID { get; set; }
        [ForeignKey("CarrierID")]
        public Carrier Carrier { get; set; }
        [Column(Order = 5, TypeName = "varchar(32)")]
        public string PaymentType { get; set; }
        [Column(Order = 6, TypeName = "varchar(16)")]
        public string Price { get; set; }
        [Column(Order = 7, TypeName = "varchar(128)")]
        public string Restrictions { get; set; }
        [Column(Order = 8, TypeName = "varchar(2083)")]
        public string URL { get; set; }
        [Column(Order = 9, TypeName = "nvarchar(MAX)")]
        public string Description { get; set; }
        [Column(Order = 10, TypeName = "datetime2")]
        public override DateTime? LastUpdatedDate { get; set; }
        [NotMapped]
        protected override string[] ColumnNames { get
            {
                return new string[] { nameof(VendorID), nameof(PhoneModelID), nameof(PhoneModelVariantID), nameof(PhoneVendorPhoneID), nameof(CarrierID), nameof(PaymentType), nameof(Price), nameof(Restrictions), nameof(URL), nameof(Description), nameof(LastUpdatedDate) };
            }
        }
        [NotMapped]
        protected override object[] Values { get
            {
                return new object[] { VendorID, PhoneModelID, PhoneModelVariantID, PhoneVendorPhoneID, CarrierID, PaymentType, Price, Restrictions, URL, Description, LastUpdatedDate };
            }
        }
        [NotMapped]
        protected override int KeyCount
        {
            get
            {
                return 4;
            }
        }
        
        public string GetColourSpecificImageURL(string firstColour, string newColour, string vendorID, string initialURL)
        {
            if (firstColour == null)
                firstColour = "N/A";
            if (newColour == null)
                newColour = Phone.Colour;
            // If the phone model has different colour choices and does not match the first colour (the phone colour in the initial URL's page)
            if (firstColour != "N/A" && firstColour != newColour) {
                switch (vendorID)
                {
                    case "Bell":
                        if (PhoneModelID.StartsWith("iPhone 7") || PhoneModelID == "iPhone SE")
                        {
                            string pattern = "ac|a|e|il|i|o|u| ";
                            firstColour = Regex.Replace(firstColour, pattern, string.Empty);
                            if (firstColour == "Blk")
                                firstColour = "Mat" + firstColour;
                            if (newColour == "Grey")
                                initialURL = initialURL.Replace(firstColour, "Sp" + firstColour);
                            else if (newColour == "Gold" && PhoneModelID == "iPhone 7 Plus")
                                newColour = "_" + newColour;
                            return initialURL.Replace(firstColour, Regex.Replace(newColour.Replace("jt", "jet"), pattern, string.Empty));
                        } else if (PhoneModelID.StartsWith("iPhone 6")) {
                            if (firstColour == "Grey")
                                firstColour = "space" + firstColour;
                            return initialURL.Replace(firstColour.ToLower().Replace(" ", string.Empty), newColour.Replace(" ", string.Empty));
                        } else if (PhoneModelID.StartsWith("Sony Xperia") || PhoneModelID == "Samsung Galaxy S7")
                        {
                            // Cover a naming inconsistency for the specific phone between different colours
                            if (PhoneModelID == "Sony Xperia X Performance" && firstColour == "Black" && newColour == "White") 
                                initialURL = initialURL.Replace("Lrg2", "Lrg2_en");
                            return initialURL.Replace(firstColour, newColour);
                        } else {
                            switch (PhoneModelID)
                            {
                                case "Pixel, Phone by Google":
                                    return initialURL.Replace((firstColour == "Silver" ? "very_" : string.Empty) + firstColour.ToLower(), (newColour == "Silver" ? "very_" : string.Empty) + newColour.ToLower());
                                case "ALCATEL ONETOUCH Idol X":
                                    return initialURL.Replace("Slate", newColour);
                                default:
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            return initialURL;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}/{2}/{3}/{4}/{5}", GetType().Name, PhoneModelID, PhoneModelVariantID, PhoneVendorPhoneID, CarrierID, PaymentType);
        }
    }
}
