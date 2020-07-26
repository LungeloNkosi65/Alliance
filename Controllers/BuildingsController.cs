using Accommodation.Models;
using Accommodation.Services.Interface;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Accommodation.Controllers
{
    public class BuildingsController : Controller
    {
        private IBuildingService _buildingService;
        public BuildingsController(IBuildingService buildingService)
        {

            _buildingService = buildingService;
        }
        // GET: Buildings
        public ActionResult Index()
        {
            var userName = User.Identity.GetUserName();
            if (User.IsInRole("Landlord"))
            {
                return View(_buildingService.GetBuildings().Where(x=>x.OwnerEmail==userName));

            }
            else
            {
                return View(_buildingService.GetBuildings());

            }
        }

        public ActionResult Index2(string search)
        {
                return View(_buildingService.GetBuildings().Where(x => x.locality.Contains(search)).ToList());
        }
        public ActionResult Index1()
        {
            return View(_buildingService.GetBuildings());

        }

        // GET: Buildings/Details/5
        public ActionResult Details(int id)
        {
            return View(_buildingService.GetBuildings(id));
        }

        // GET: Buildings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Buildings/Create
        [HttpPost]
        public ActionResult Create(Building building, HttpPostedFileBase photoUpload)
        {
            try
            {
                var userName = User.Identity.GetUserName();
                byte[] photo = null;
                photo = new byte[photoUpload.ContentLength];
                photoUpload.InputStream.Read(photo, 0, photoUpload.ContentLength);
                building.BuildingPic = photo;
                building.OwnerEmail = userName;
                building.Address =($"{building.street_number}, {building.route}, {building.locality}, {building.administrative_area_level_1}, {building.country}");
                if (_buildingService.Insert(building))
                {
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return View();
            }
            return View();
        }

        // GET: Buildings/Edit/5
        public ActionResult Edit(int id)
        {
            if (!String.IsNullOrEmpty(id.ToString()))
            {
                try
                {
                    return View(_buildingService.GetBuildings(id));
                }
                catch { }
            }
            return View();
        }

        // POST: Buildings/Edit/5
        [HttpPost]
        public ActionResult Edit(Building building)
        {
            try
            {
                if (_buildingService.Update(building))
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
            return View();
        }

        // GET: Buildings/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Buildings/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
