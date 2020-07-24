using Accommodation.Models;
using Accommodation.Services.Interface;
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
                byte[] photo = null;
                photo = new byte[photoUpload.ContentLength];
                photoUpload.InputStream.Read(photo, 0, photoUpload.ContentLength);
                building.BuildingPic = photo;

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
