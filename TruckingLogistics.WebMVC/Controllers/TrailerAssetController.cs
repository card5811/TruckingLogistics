using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruckingLogistics.Data;
using TruckingLogistics.Models.Trailer;
using TruckingLogistics.Services;

namespace TruckingLogistics.WebMVC.Controllers
{
    public class TrailerAssetController : Controller
    {
        // GET: Trailer
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TrailerAssetServices(userId);
            var model = service.GetTrailer();

            return View(model);
        }

        //Get: Create/Trailer
        public ActionResult Create()
        {
            // var service = CreateTrailerService();
            // ViewBag.TrailerId = new SelectList(service.GetTrailer(), "TrailerID", "TrailerName");
            return View();
        }

        //Post: Create/Trailer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTrailer model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreateTrailerService();

            if (service.TrailerCreate(model))
            {
                TempData["SaveResult"] = "Your trailer was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Trailer could not be created.");

            return View(model);
        }

        //Get: Trailer/Details/Id
        public ActionResult TrailerDetails(int id)
        {
            var svc = CreateTrailerService();
            var model = svc.GetTrailerById(id);

            return View(model);
        }

        //Get: Edit/Trailer/Id
        // Get id from user
        // Handle if id is null
        //Find restauraunt by id
        //If Restaurant doesn't exist
        //Return Restaurant and the view
        public ActionResult Edit(int id)
        {
            var service = CreateTrailerService();
            var detail = service.GetTrailerById(id);
            var model =
                new EditTrailer
                {
                    TrailerNumber = detail.TrailerNumber,
                    TrailerMileage = detail.TrailerMileage,
                    TrailerType = detail.TrailerType,
                    Comment = detail.Comment
                };
            return View(model);
        }

        //Post: Edit/Trailer/Id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditTrailer model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.TrailerId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateTrailerService();

            if (service.UpdateTrailer(model))
            {
                TempData["SaveResult"] = "Your trailer was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your trailer could not be updated.");
            return View(model);
        }

        //Get: Delete/Trailer/Id
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateTrailerService();
            var model = svc.GetTrailerById(id);

            return View(model);
        }
        //Post: Delete/Trailer/Id
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateTrailerService();

            service.DeleteTrailerById(id);

            TempData["SaveResult"] = "Truck was deleted";

            return RedirectToAction("Index");
        }

        //private: Helper Method
        private TrailerAssetServices CreateTrailerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TrailerAssetServices(userId);
            return service;
        }
    }
}