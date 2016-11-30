using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.Models;
using SoftwareEngineeringProject.Models.SearchViewModels;
using SoftwareEngineeringProject.Helpers;
using SoftwareEngineeringProject.Models.Repositories;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftwareEngineeringProject.Controllers
{
    public class SearchController : Controller
    {
        private readonly PhoneModelRepository _phoneModels;
        private readonly OperatingSystemInclusionRepository _osInclusions;

        public SearchController(PhoneModelRepository phoneModels, OperatingSystemInclusionRepository osInclusions)
        {
            _phoneModels = phoneModels;
            _osInclusions = osInclusions;
        }

        // GET: /Search/Index/(Manufacturers|Models)?query=query
        [HttpGet]
        public IActionResult Index(string Query = "", int Page = 1)
        {
            ViewData["Query"] = Query;
            ViewData["Page"] = Page;
            
            IEnumerable<PhoneModel> results = _phoneModels.GetAll();
            IEnumerable<string> osOptions;

            if (Query != null) {
                Query = Query.ToLower();
                results = results.Where(pm => pm.PhoneModelID.ToLower().Contains(Query) || pm.ManufacturerID.ToLower().Contains(Query));
            }

            ViewData["TotalResults"] = results.Count();
            ViewBag.ManufacturerOptions = results.Select(pm => pm.ManufacturerID).Distinct().OrderBy(m => m);
            ViewBag.CarrierOptions = results.SelectMany(pm => pm.Phones.SelectMany(p => p.VendorPhones.Select(vp => vp.CarrierID))).Distinct().OrderBy(c => c == "No Carrier");
            ViewBag.OSOptions = osOptions = results.Select(pm => pm.OperatingSystem).Distinct().OrderBy(os => os).OrderBy(os => os == "N/A");
            ViewBag.MemoryOptions = results.SelectMany(pm => pm.Phones.Select(p => p.Memory).Distinct()).Distinct().OrderBy(m => FormatHelper.PadNumbers(m)).OrderBy(m => m == "N/A");

            Dictionary<string, string[]> osOptionInclusions = new Dictionary<string, string[]>();
            foreach (string os in osOptions)
                osOptionInclusions.Add(os, _osInclusions.GetAllForIncludedOSID(os).Select(osi => osi.OperatingSystemID).ToArray());
            /*List<string> inclusions;
            string version;
            bool checkVersions;

            foreach (string os in osOptions)
            {
                inclusions = new List<string>();
                version = Regex.Match(os, " ([0-9x\\.]+)?").Groups[1].Value.Replace(".x", string.Empty);
                checkVersions = version != string.Empty;
                foreach (string osInclude in osOptions)
                {
                    if (osInclude != os)
                    {
                        if (os.Contains(osInclude))
                            inclusions.Add(osInclude);
                        else
                        {
                            if (checkVersions) {
                                string includeVersion = Regex.Match(osInclude, " ([0-9x\\.]+)?").Groups[1].Value.Replace(".x", string.Empty);
                                if (includeVersion != string.Empty && version.Contains(includeVersion))
                                    inclusions.Add(osInclude);
                            }
                        }
                        
                    }
                }
                osOptionInclusions.Add(os, inclusions.Any() ? inclusions.ToArray() : new string[0]);
            }*/

            ViewBag.OSOptionInclusions = osOptionInclusions;

            return View(results);
        }

        // POST: /Search/Index
        [HttpPost]
        public IActionResult Index(SearchViewModel Model)
        {
            if (ModelState.IsValid)
                return Redirect("Search?query=" + Model.Query);

            return Redirect("/");
        }
    }
}
