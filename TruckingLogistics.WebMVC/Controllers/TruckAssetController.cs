using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruckingLogistics.Data;
using TruckingLogistics.Models.Truck;
using TruckingLogistics.Services;

namespace TruckingLogistics.WebMVC.Controllers
{
    [Authorize]
    public class TruckAssetController : Controller
    {
        // GET: Details/Truck
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TruckAssetService(userId);
            var model = service.GetTrucks();

            return View(model);
        }

        //Get: Create/Truck
        public ActionResult Create()
        {
            return View();
        }

        //Post: Create/Truck/Model
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTruck model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreateTruckService();

            if (service.TruckCreate(model))
            {
                TempData["SaveResult"] = "Your truck was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Truck could not be created.");

            return View(model);
        }

        //Get: Details/Truck/Id
        public ActionResult Details(int id)
        {
            var svc = CreateTruckService();
            var model = svc.GetTruckById(id);

            return View(model);
        }

        //Get: Edit/Truck/Id
        // Get id from user
        // Handle if id is null
        //Find restauraunt by id
        //If Restaurant doesn't exist
        //Return Restaurant and the view
        public ActionResult Edit(int id)
        {
            var service = CreateTruckService();
            var detail = service.GetTruckById(id);
            var model =
                new EditTruck
                {
                    TruckNumber = detail.TruckNumber,
                    Make = detail.Make,
                    Model = detail.Model,
                    Mileage = detail.Mileage,
                    Comment = detail.Comment
                };
            return View(model);
        }

        //Post: Edit/Truck/Id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditTruck model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.TruckId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateTruckService();

            if (service.UpdateTruck(model))
            {
                TempData["SaveResult"] = "Your truck was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your truck could not be updated.");
            return View(model);
        }


        //Get: Delete/Truck/Id
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateTruckService();
            var model = svc.GetTruckById(id);

            return View(model);
        }

        //Post: Delete/Truck/Id
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateTruckService();

            service.DeleteTruckById(id);

            TempData["SaveResult"] = "Truck was deleted";

            return RedirectToAction("Index");
        }

        //Private: Helper Method
        private TruckAssetService CreateTruckService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TruckAssetService(userId);
            return service;
        }
    }
}