using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
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

        public void SetOperatingSystem(string os, ICollection<PhoneModel> allPhoneModels)
        {
            if (os != "Android" && os.Contains("Android"))
            {
                Match versionMatch;
                if (!Regex.IsMatch(os, "^Android [0-9x\\.]+( \\([a-z ]+\\))?$", RegexOptions.IgnoreCase))
                {
                    if (Regex.IsMatch(os, "^Android( [0-9x\\.]+)? [a-z ]+$", RegexOptions.IgnoreCase))
                        os = string.Format("{0} ({1})", os.Substring(0, os.LastIndexOf(" ")), os.Substring(os.LastIndexOf(" ") + 1));
                    else if (Regex.IsMatch(os, "^Android [a-z ]+ [0-9x\\.]+$", RegexOptions.IgnoreCase))
                        os = string.Format("{0}{1} ({2})", os.Substring(0, os.IndexOf(" ")), os.Substring(os.LastIndexOf(" ")), os.Substring(os.IndexOf(" ") + 1, os.LastIndexOf(" ") - (os.IndexOf(" ") + 1)));
                }
                versionMatch = Regex.Match(os, "^Android ([0-9x\\.]+)? ?(\\([a-z ]+\\))?$", RegexOptions.IgnoreCase);
                string versionNumber = versionMatch.Groups[1].Value;
                string versionName = versionMatch.Groups[2].Value;
                if (versionNumber != string.Empty)
                {
                    if (versionNumber.IndexOf('.') > -1)
                        versionNumber = versionNumber.Substring(0, versionNumber.IndexOf("."));
                    else
                        os = os.Replace(versionNumber, string.Format("{0}.0", versionNumber));

                    if (versionName != string.Empty)
                        allPhoneModels.Where(pm => pm.OperatingSystem.StartsWith("Android " + versionNumber) && !pm.OperatingSystem.EndsWith(")")).AsParallel()
                            .ForAll(pm => pm.OperatingSystem = string.Format("{0} ({1})", pm.OperatingSystem, versionName));
                    else
                    {
                        PhoneModel phoneModelWithOSVersionName = allPhoneModels.FirstOrDefault(pm => pm.OperatingSystem.StartsWith("Android " + versionNumber) && pm.OperatingSystem.EndsWith(")"));
                        if (phoneModelWithOSVersionName != null)
                            os += " " + phoneModelWithOSVersionName.OperatingSystem.Substring(phoneModelWithOSVersionName.OperatingSystem.LastIndexOf("("));
                    }
                } else if (versionName != string.Empty)
                {
                    if (!Regex.IsMatch(versionName, "\\(OS\\)"))
                    {
                        PhoneModel phoneModelWithOSVersionNumber = allPhoneModels.FirstOrDefault(pm => Regex.IsMatch(pm.OperatingSystem, string.Format("^Android [0-9x\\.]+ \\({0}\\)$", versionName)));
                        if (phoneModelWithOSVersionNumber != null)
                            os = string.Format("{0} {1}", os.Substring(0, os.IndexOf(" ")), phoneModelWithOSVersionNumber.OperatingSystem.Substring(phoneModelWithOSVersionNumber.OperatingSystem.IndexOf(" ") + 1));
                    } else
                        os = os.Substring(0, os.LastIndexOf(" "));
                }
            }
            else if (os == "Apple")
                os = "iOS";

            OperatingSystem = os;
        }
    }
}
