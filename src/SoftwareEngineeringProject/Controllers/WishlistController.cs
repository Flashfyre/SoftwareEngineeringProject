using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SoftwareEngineeringProject.Models;
using SoftwareEngineeringProject.Models.Repositories;

namespace SoftwareEngineeringProject.Controllers
{
    public class WishlistController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PhoneModelRepository _phoneModels;
        private readonly SavedPhoneModelRepository _savedPhoneModels;

        public WishlistController(UserManager<ApplicationUser> userManager, PhoneModelRepository phoneModels, SavedPhoneModelRepository savedPhoneModels)
        {
            _userManager = userManager;
            _phoneModels = phoneModels;
            _savedPhoneModels = savedPhoneModels;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ApplicationUser user = _userManager.Users.SingleOrDefault(u => u.Email == Request.HttpContext.User.Identity.Name);

            if (user == null)
                return Redirect("/");

            return View(_savedPhoneModels.GetAllForUserID(user.Id).OrderByDescending(spm => spm.AddedDate));
        }

        [HttpPost]
        public JsonResult AddToWishlist(string phoneModelID)
        {
            ApplicationUser user = _userManager.Users.SingleOrDefault(u => u.Email == Request.HttpContext.User.Identity.Name);
            PhoneModel phoneModel;
            bool success = false;
            string errorMessage = null;

            if (user != null)
            {
                phoneModel = _phoneModels.Find(phoneModelID);
                if (phoneModel != null)
                {
                    success = true;
                    if (!_savedPhoneModels.GetAllForUserID(user.Id).Any(spm => spm.PhoneModelID == phoneModel.PhoneModelID))
                        _savedPhoneModels.Add(
                            new SavedPhoneModel()
                            {
                                UserID = user.Id,
                                PhoneModelID = phoneModel.PhoneModelID,
                                AddedDate = DateTime.Now
                            }
                        );
                    else
                        errorMessage = "This phone is already part of your wishlist.";
                } else
                    errorMessage = string.Format("Sorry, no phone model with the name \"{0}\" exists in our database. It may have been removed.", phoneModelID);
            }
            else
                errorMessage = "You may not add this item to your wishlist since you are not logged in.";

            return Json(new object[] { success, errorMessage });
        }

        [HttpPost]
        public JsonResult RemoveFromWishlist(string phoneModelID)
        {
            ApplicationUser user = _userManager.Users.SingleOrDefault(u => u.Email == Request.HttpContext.User.Identity.Name);
            PhoneModel phoneModel;
            bool success = false;
            string errorMessage = null;

            if (user != null)
            {
                phoneModel = _phoneModels.Find(phoneModelID);
                if (phoneModel != null)
                {
                    success = true;

                    SavedPhoneModel savedPhoneModel = _savedPhoneModels.GetAllForUserID(user.Id).SingleOrDefault(spm => spm.PhoneModelID == phoneModel.PhoneModelID);

                    if (savedPhoneModel != null)
                        _savedPhoneModels.Remove(savedPhoneModel);
                    else
                        errorMessage = "This phone is not part of your wishlist.";
                }
                else
                    errorMessage = string.Format("Sorry, no phone model with the name \"{0}\" exists in our database. It may have been removed.", phoneModelID);
            }
            else
                errorMessage = "You may not remove this item from your wishlist since you are not logged in.";

            return Json(new object[] { success, errorMessage });
        }
    }
}