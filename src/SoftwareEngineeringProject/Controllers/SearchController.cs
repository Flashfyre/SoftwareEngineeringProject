using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.Models;
using SoftwareEngineeringProject.Models.SearchViewModels;
using SoftwareEngineeringProject.Helpers;
using System;

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

        // GET: /SearchController/Index/(Manufacturers|Models)?query=query
        [HttpGet]
        public IActionResult Index(string SearchType = "Models", string Query = "", int Page = 1)
        {
            ViewData["SearchType"] = SearchType;
            ViewData["Query"] = Query;
            ViewData["Page"] = Page;

            Query = Query.ToLower();

            IEnumerable<PhoneModel> results = _phoneModels.GetAll();
            
            if (Query != "")
                results = results.Where(pm => pm.PhoneModelID.ToLower().Contains(Query) || pm.ManufacturerID.ToLower().Contains(Query));

            ViewData["TotalResults"] = results.Count();
            ViewBag.ManufacturerOptions = results.Select(pm => pm.ManufacturerID).Distinct().OrderBy(m => m);
            ViewBag.OSOptions = results.Select(pm => pm.OperatingSystem).Distinct().OrderBy(os => os);
            ViewBag.MemoryOptions = results.SelectMany(pm => pm.Phones.Select(p => p.Memory).Distinct()).Distinct().OrderBy(m => FormatHelper.PadNumbers(m));

            return View(results);
        }

        // POST: /SearchController/Index
        [HttpPost]
        public IActionResult Index(SearchViewModel Model)
        {
            if (ModelState.IsValid) {
                if (Model.SearchType == null)
                    Model.SearchType = "Models";

                return Redirect("Search/" + Model.SearchType + "?query=" + Model.Query);
            }

            return Redirect("/");
        }
    }
}
