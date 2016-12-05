using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.Models;
using SoftwareEngineeringProject.Models.Repositories;
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
            {
                ViewData["Description"] = (GetPhoneModelDescription(PhoneModelID).Value as object[])[1];
                return View(model);
            }
            else
                return Redirect("/Home/Error");
        }

        // POST: /Search/GetPhoneModelDescription
        [HttpPost]
        public JsonResult GetPhoneModelDescription(string PhoneModelID, bool OverviewOnly = false)
        {
            PhoneModel phoneModel = _phoneModels.Find(PhoneModelID);
            bool success = false;
            string message = null;

            if (phoneModel != null)
            {
                if (phoneModel.Phones.Any())
                {
                    Phone phone = phoneModel.Phones.First();
                    if (phone.VendorPhones.Any())
                    {
                        success = true;
                        VendorPhone vendorPhone = phone.VendorPhones.FirstOrDefault(vp => vp.Description != null);
                        message = vendorPhone != null ? vendorPhone.Description : "<p>No description available</p>";
                        if (vendorPhone != null)
                        {
                            switch (vendorPhone.VendorID)
                            {
                                case "Best Buy":
                                    if (OverviewOnly)
                                    {
                                        int startIndex = message.IndexOf("</h4>") + 5;
                                        message = message.Substring(startIndex, message.IndexOf("<h4>", 1) - startIndex);
                                    }
                                    if (message.EndsWith("</h4>"))
                                        message = message.Substring(0, message.LastIndexOf("<h4>"));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                        message = "Error: No phones for the model '" + PhoneModelID + "' are currently available!";
                }
                else
                    message = "Error: No phones for the model '" + PhoneModelID + "' are currently available!";
            }
            else
                message = "Error: No phone model exists with the ID '" + PhoneModelID + "'!";

            return Json(new object[] { success, message });
        }

        // POST: /Search/GetPhoneModelImageURLs
        [HttpPost]
        public JsonResult GetPhoneModelImageURLs(string PhoneModelID)
        {
            PhoneModel phoneModel = _phoneModels.Find(PhoneModelID);
            bool success = false;
            string errorMessage = null;
            string[] imageURLs = null;

            if (phoneModel != null)
            {
                if (phoneModel.Phones.Any())
                {
                    success = true;

                    Phone phone = phoneModel.Phones.First();
                    imageURLs = phone.GetImageURLs();
                }
                else
                    errorMessage = "Error: No phones for the model '" + PhoneModelID + "' are currently available!";
            }
            else
                errorMessage = "Error: No phone model exists with the ID '" + PhoneModelID + "'!";

            return Json(new object[] { success, success ? imageURLs : new string[] { errorMessage } });
        }

        // GET: /Phone/VendorPhonesGrid
        [HttpGet]
        public IActionResult VendorPhonesGrid(string PhoneModelID)
        {
            PhoneModel phoneModel = _phoneModels.Find(PhoneModelID);
            IEnumerable<VendorPhone> vendorPhonesList = phoneModel.Phones.SelectMany(p => p.VendorPhones);

            return PartialView("MvcGrid/_VendorPhonesGrid", vendorPhonesList);
        }
    }
}
