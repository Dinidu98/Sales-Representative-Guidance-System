using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sales_Rep.Models;

namespace Sales_Rep.Controllers
{
    public class BrandsController : Controller
    {
        // GET: Brands
        SALES_REPEntities db = new SALES_REPEntities();
        public ActionResult Index()
        {
            return View(db.tblBrands.ToList());
        }

        // GET: Brands/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        [HttpPost]
        public ActionResult Create(tblBrand br)
        {
            try
            {

                tblBrand bd = new tblBrand();
                {
                    bd.Brand = br.Brand;
                    db.tblBrands.Add(bd);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Brands/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.tblBrands.Where(x=>x.id == id).FirstOrDefault());
        }

        // POST: Brands/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblBrand br)
        {
            try
            {
                tblBrand sk = db.tblBrands.Where(x => x.id == id).FirstOrDefault();
                {

                    sk.Brand = br.Brand;

                    db.Entry(sk).State = EntityState.Modified;
                    db.SaveChanges();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Brands/Delete/5
        public ActionResult Delete(int id)
        {
            tblBrand stc1 = db.tblBrands.Where(x => x.id == id).FirstOrDefault();
            db.tblBrands.Remove(stc1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Brands/Delete/5
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
