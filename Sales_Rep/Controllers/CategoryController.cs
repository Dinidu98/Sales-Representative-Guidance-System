using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sales_Rep.Models;

namespace Sales_Rep.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        SALES_REPEntities db = new SALES_REPEntities();
        public ActionResult Index()
        {
            return View(db.tblCategories.ToList());
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(tblCategory cat)
        {
            try
            {
                tblCategory bd = new tblCategory();
                {
                    bd.Category = cat.Category;
                    db.tblCategories.Add(bd);
                    db.SaveChanges();
                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.tblCategories.Where(x=>x.id == id).FirstOrDefault());
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblCategory cat)
        {
            try
            {
                 tblCategory sk = db.tblCategories.Where(x => x.id == id).FirstOrDefault();
                {

                    sk.Category = cat.Category;

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

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            tblCategory stc1 = db.tblCategories.Where(x => x.id == id).FirstOrDefault();
            db.tblCategories.Remove(stc1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Category/Delete/5
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
