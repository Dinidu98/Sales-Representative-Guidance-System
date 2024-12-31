using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sales_Rep.Models;

namespace Sales_Rep.Controllers
{
    public class VendorController : Controller
    {
        SALES_REPEntities db = new SALES_REPEntities();
        // GET: Vendor
        public ActionResult Index()
        {
            return View(db.tblVendors.ToList());
        }

        // GET: Vendor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Vendor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vendor/Create
        [HttpPost]
        public ActionResult Create(tblVendor ve)
        {
            try
            {
                tblVendor bd = new tblVendor();
                {
                    bd.vendor = ve.vendor;
                    bd.address = ve.address;
                    bd.telephone = ve.telephone;
                    bd.email = ve.email;
                    bd.fax = ve.fax;
                   
                    db.tblVendors.Add(bd);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vendor/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.tblVendors.Where(x=>x.id==id).FirstOrDefault());
        }

        // POST: Vendor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblVendor ve)
        {
            try
            {
                tblVendor bd = db.tblVendors.Where(x => x.id == id).FirstOrDefault();
                {

                    bd.vendor = ve.vendor;
                    bd.address = ve.address;
                    bd.telephone = ve.telephone;
                    bd.email = ve.email;
                    bd.fax = ve.fax;

                    db.Entry(bd).State = EntityState.Modified;
                    db.SaveChanges();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vendor/Delete/5
        public ActionResult Delete(int id)
        {
            tblVendor stc1 = db.tblVendors.Where(x => x.id == id).FirstOrDefault();
            db.tblVendors.Remove(stc1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Vendor/Delete/5
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
