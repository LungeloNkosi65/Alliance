using Accommodation.Models;
using Accommodation.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Accommodation.Controllers
{
    public class RoomsController : Controller
    {
        private IRoomService _roomService;
        private IRoomTypeService _roomTypeService;
        private IBuildingService _buildingService;
        public RoomsController (IRoomService roomService,IRoomTypeService roomTypeService ,IBuildingService buildingService)
        {
            _roomService = roomService;
            _roomTypeService = roomTypeService;
            _buildingService = buildingService;
        }
        // GET: Rooms
        public ActionResult Index()
        {
            return View(_roomService.GetRooms());
        }

        public ActionResult RoomsBooking()
        {
            return View(_roomService.GetRooms());
        }
        public ActionResult RoomsBooking2()
        {
            return View(_roomService.GetRooms());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int id)
        {
            return View(_roomService.GetRooms(id));
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            ViewBag.roomtypeId = new SelectList(_roomTypeService.GetRoomTypes(), "roomtypeId", "Type");
            ViewBag.BuildingId = new SelectList(_buildingService.GetBuildings(), "BuildingId", "BuildingName");
            return View();
        }

        // POST: Rooms/Create
        [HttpPost]
        public ActionResult Create (Room room, HttpPostedFileBase photoUpload)
        {
            try
            {
                
                byte[] photo = null;
                photo = new byte[photoUpload.ContentLength];
                photoUpload.InputStream.Read(photo, 0, photoUpload.ContentLength);
                room.RoomPicture = photo;

                var result = _buildingService.GetBuildings().Where(p => p.BuildingId == room.BuildingId).Select(p => p.BuildingName).FirstOrDefault();
                var result1 = _buildingService.GetBuildings().Where(p => p.BuildingId == room.BuildingId).Select(p => p.TotalNumberOfRooms).FirstOrDefault();

                if (_roomService.ChceckPoeple(room.roomtypeId, room.NoOfPeople))
                {
                    Random a = new Random();

                    room.RoomNumber = result.Substring(0, 3) + a.Next(0, result1);
                    if (_roomService.Insert(room))
                    {


                        ViewBag.roomtypeId = new SelectList(_roomTypeService.GetRoomTypes(), "roomtypeId", "Type");
                        ViewBag.BuildingId = new SelectList(_buildingService.GetBuildings(), "BuildingId", "BuildingName");
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                     ModelState.AddModelError("", "Number of poeple exceeded room type quantity");
                    ViewBag.roomtypeId = new SelectList(_roomTypeService.GetRoomTypes(), "roomtypeId", "Type");
                    ViewBag.BuildingId = new SelectList(_buildingService.GetBuildings(), "BuildingId", "BuildingName");
                    return View(room);
                }

               
            }
            catch
            {
                ViewBag.roomtypeId = new SelectList(_roomTypeService.GetRoomTypes(), "roomtypeId", "Type");
                ViewBag.BuildingId = new SelectList(_buildingService.GetBuildings(), "BuildingId", "BuildingName");
                return View(room);
            }
            ViewBag.roomtypeId = new SelectList(_roomTypeService.GetRoomTypes(), "roomtypeId", "Type");
            ViewBag.BuildingId = new SelectList(_buildingService.GetBuildings(), "BuildingId", "BuildingName");
            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int id)
        {
            if (!String.IsNullOrEmpty(id.ToString()))
            {
                try
                {
                    return View(_roomService.GetRooms(id));
                }
                catch { }
            }
            return View();
        }

        // POST: Rooms/Edit/5
        [HttpPost]
        public ActionResult Edit(Room room)
        {
            try
            {
                if (_roomService.Update(room))
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

        // GET: Rooms/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rooms/Delete/5
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
