using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Equipment.Models;

namespace Equipment.Controllers
{
    public class DevicesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: /Devices/
        public ActionResult Index()
        {
            var devices = db.Devices.Include(d => d.DeviceDictionary);
            return View(devices.ToList());
        }
        [HttpPost]
        public ActionResult Index(string User, string Batch)
        {
            var devices = db.Devices.Include(d => d.DeviceDictionary);
            if (!String.IsNullOrEmpty(User))
            {
                devices = devices.Where(model => model.DeviceUser.Equals(User));
            }
            if(!String.IsNullOrEmpty(Batch))
            {
                devices = devices.Where(model => model.Batch.Equals(Batch));
            }
            return View(devices.ToList());
        }

        // GET: /Devices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }
        [HttpPost]
        //public ActionResult Read(object[] data, string DeviceUser, string DeviceId, string number)
        public ActionResult Read(ViewModel m)
        {
            var device = new Device();
            for (int i = 0; i < m.data.Count;i++ )
            {
                device.Batch = m.data[i].Batch;
                device.DeviceSerialNumber = m.data[i].DeviceSerialNumber;
                device.DeviceUser = m.DeviceUser;
                device.DeviceDictionaryId = m.DeviceId;
                if (String.IsNullOrEmpty(m.data[i].StartDate))
                {
                    device.Start = null;
                }
                else
                {
                    device.Start = DateTime.Parse(m.data[i].StartDate);
                }
                if (String.IsNullOrEmpty(m.data[i].EndDate))
                {
                    device.End = null;
                }
                else
                {
                    device.End = DateTime.Parse(m.data[i].EndDate);
                }
                db.Devices.Add(device);
                db.SaveChanges();
            }
                
            
            return Json(new object());
        }

        public ActionResult CreateMany2()
        {
            return View();
        }

        // GET: /Devices/Create
        public ActionResult Create()
        {
            var Ids = db.Dictionaries
               .Select(s => new
               {
                   DeviceDictionaryId = s.DeviceDictionaryId,
                   Description = s.DeviceManufacturer + " " + s.DeviceName
               })
               .ToList();
            ViewBag.DeviceDictionaryId = new SelectList(Ids, "DeviceDictionaryId", "Description");
            return View();
        }

        // POST: /Devices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DeviceId,DeviceSerialNumber,DeviceUser,DeviceDictionaryId,Batch,Start, End")] Device device)
        {
            if (ModelState.IsValid)
            {
                db.Devices.Add(device);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeviceDictionaryId = new SelectList(db.Dictionaries, "DeviceDictionaryId", "DeviceManufacturer", device.DeviceDictionaryId);
            return View(device);
        }

        public ActionResult MyDevices()
        {
            var devices = db.Devices.Include(d => d.DeviceDictionary).Where(d => d.DeviceUser.Equals(HttpContext.User.Identity.Name));
            return View(devices);
        }

        // GET: /Devices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            var Ids = db.Dictionaries
               .Select(s => new
               {
                   DeviceDictionaryId = s.DeviceDictionaryId,
                   Description = s.DeviceManufacturer + " " + s.DeviceName
               })
               .ToList();
            ViewBag.DeviceDictionaryId = new SelectList(Ids, "DeviceDictionaryId", "Description",device.DeviceDictionaryId);
            //ViewBag.DeviceDictionaryId = new SelectList(db.Dictionaries, "DeviceDictionaryId", "DeviceManufacturer", device.DeviceDictionaryId);
            return View(device);
        }

        // POST: /Devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DeviceId,DeviceSerialNumber,DeviceUser,DeviceDictionaryId,Batch,Start,End")] Device device)
        {
            if (ModelState.IsValid)
            {
                db.Entry(device).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var Ids = db.Dictionaries
               .Select(s => new
               {
                   DeviceDictionaryId = s.DeviceDictionaryId,
                   Description = s.DeviceManufacturer + " " + s.DeviceName
               })
               .ToList();
            ViewBag.DeviceDictionaryId = new SelectList(Ids, "DeviceDictionaryId", "Description",device.DeviceDictionaryId);
           // ViewBag.DeviceDictionaryId = new SelectList(db.Dictionaries, "DeviceDictionaryId", "DeviceManufacturer", device.DeviceDictionaryId);
            return View(device);
        }

        // GET: /Devices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // POST: /Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Device device = db.Devices.Find(id);
            db.Devices.Remove(device);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreateMany()
        {
                    
            var Ids = db.Dictionaries
               .Select(s => new
               {
                   DeviceDictionaryId = s.DeviceDictionaryId,
                   Description = s.DeviceManufacturer + " " + s.DeviceName
               })
               .ToList();
            ViewBag.DeviceDictionaryId = new SelectList(Ids, "DeviceDictionaryId", "Description");

            return View();
        }
        [HttpPost]
        public ActionResult CreateMany(int i)
        {
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
