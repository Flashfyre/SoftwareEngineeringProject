using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.Controllers
{
    public class PhoneController : Controller
    {
        private readonly PhoneModelRepository _phoneModels;

        public PhoneController(PhoneModelRepository phoneModels)
        {
            _phoneModels = phoneModels;
        }

        public IActionResult Index(string PhoneModelID)
        {
            PhoneModel model = _phoneModels.Find(PhoneModelID);

            if (model != null)
                return View(model);
            else
                return Redirect("/Home/Error");
        }
    }
}
