using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.Models;
using SoftwareEngineeringProject.Models.SearchViewModels;
using SoftwareEngineeringProject.Helpers;
using System;
using System.Text.RegularExpressions;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftwareEngineeringProject.Controllers
{
    public class SearchController : Controller
    {
        private readonly PhoneModelRepository _phoneModels;

        public SearchController(PhoneModelRepository phoneModels)
        {
            _phoneModels = phoneModels;
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
            ViewBag.OSOptions = osOptions = results.Select(pm => pm.OperatingSystem).Distinct().OrderBy(os => os);
            ViewBag.MemoryOptions = results.SelectMany(pm => pm.Phones.Select(p => p.Memory).Distinct()).Distinct().OrderBy(m => FormatHelper.PadNumbers(m));

            Dictionary<string, string[]> osOptionInclusions = new Dictionary<string, string[]>();
            List<string> inclusions;
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
            }

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
