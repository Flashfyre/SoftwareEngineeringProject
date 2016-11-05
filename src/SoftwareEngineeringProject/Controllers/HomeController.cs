using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.Models.Repositories;

namespace SoftwareEngineeringProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly PhoneModelRepository _phoneModels;

        public HomeController(PhoneModelRepository phoneModels)
        {
            _phoneModels = phoneModels;
        }

        public IActionResult Index()
        {
            ViewBag.FeaturedModels = _phoneModels.GetAll().OrderByDescending(pm => pm.LastUpdatedDate).Take(4);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
