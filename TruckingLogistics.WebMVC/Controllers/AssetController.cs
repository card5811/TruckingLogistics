using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruckingLogistics.Data;
using TruckingLogistics.Models.RosterAsset;
using TruckingLogistics.Services;

namespace TruckingLogistics.WebMVC.Controllers
{
    public class AssetController : Controller
    {
        // GET: Details/Assets
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AssetRosterServices(userId);
            var model = service.GetAssetLists();

            return View(model);
        }

        //Get: Create/Asset
        public ActionResult Create()
        {
            return View();
        }

        //Post: Create/Asset
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAssetList model)
        {
            if (!ModelState.IsValid) return View(model);

                var service = CreateAssetRoster();

            if (service.AddToRoster(model))
            {
                TempData["SaveResult"] = "Your asset was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "It could not be created.");

            return View(model);
        }

        //Get: Details/AssetRoster
        public ActionResult Details()
        {
            var svc = CreateAssetRoster();
            var model = svc.GetAssetLists();

            return View(model);
        }

        //Get: Edit/Asset/Id
        // Get id from AssetRoster
        // Handle if id is null
        //Find Asset by id
        //If Asset doesn't exist
        //Return Asset and the view  
        public ActionResult Edit(int id)
        {

            var service = CreateAssetRoster();
            var detail = service.GetAssetById(id);
            var model =
                new EditAssetList
                {
                    FirstName = detail.FirstName,
                    TruckNumber = detail.TruckNumber,
                    TrailerNumber = detail.TrailerNumber
                };
            return View(model);
        }

        //Post: Edit/Asset/Id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditAssetList model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.RosterId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateAssetRoster();

            if (service.UpdateAsset(model))
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //Get: Delete/Assset/Id
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateAssetRoster();
            var model = svc.DeleteFromRoster(id);

            return View(model);
        }

        //Post: Delete/Asset/Id
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateAssetRoster();

            service.DeleteFromRoster(id);

            TempData["SaveResult"] = "Truck was deleted";

            return RedirectToAction("Index");
        }

        //Private: Helper Method
        private AssetRosterServices CreateAssetRoster()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AssetRosterServices(userId);
            return service;
        }
    }
}