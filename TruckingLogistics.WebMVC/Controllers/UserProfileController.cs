using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruckingLogistics.Data;
using TruckingLogistics.Models.User;
using TruckingLogistics.Services;

namespace TruckingLogistics.WebMVC.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserProfileService(userId);
            var model = service.GetUsers();

            return View(model);
        }

        //Get: User/Create
        public ActionResult Create()
        {
            return View();
        }

        //Post: User/Create/Model
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateUser model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateUserProfileService();

            if (service.CreateUser(model))
            {
                ViewBag.SaveResult = "Your User was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "User could not be created.");

            return View(model);
        }

        // Get: User/Details/{Id}
        public ActionResult Details(int id)
        {
            var svc = CreateUserProfileService();
            var model = svc.GetUserById(id);

            return View(model);
        }

        //Get: User/Edit/Id
        // Get id from user
        // Handle if id is null
        //Find restauraunt by id
        //If Restaurant doesn't exist
        //Return Restaurant and the view
        public ActionResult Edit(int id)
        {
            var service = CreateUserProfileService();
            var detail = service.GetUserById(id);
            var model =
                new EditUser
                {
                    CompanyUserId = detail.CompanyUserId,
                    UserName = detail.UserName,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    Email = detail.Email
                };
            return View(model);
        }

        //Post: User/Edit/{Id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditUser model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CompanyUserId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateUserProfileService();

            if (service.EditUser(model))
            {
                TempData["SaveResult"] = "Your user was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your user could not be updated.");
            return View(model);
        }

        //Get: User/Delete/{Id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateUserProfileService();
            var model = svc.GetUserById(id);

            return View(model);
        }

        //Post: User/Delete/{Id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateUserProfileService();

            service.DeleteUser(id);

            TempData["SaveResult"] = "User was deleted";

            return RedirectToAction("Index");
        }
       
        //Private helper method
        private UserProfileService CreateUserProfileService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserProfileService(userId);
            return service;
        }
    }
}