using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sales_Rep.Models;

namespace Sales_Rep.Controllers
{
    public class RouteController : Controller
    {
        // GET: Route
        SALES_REPEntities db = new SALES_REPEntities();
        public ActionResult Index()
        {
            return View(db.ROUTE_MAP.Where(x=>x.RM_STATUS==0).ToList());
        }

        // GET: Route/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Route/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Route/Create
        [HttpPost]
        public ActionResult Create(ROUTE_MAP RM)
        {
            try
            {
                ROUTE_MAP bd = new ROUTE_MAP();
                {
                    bd.RM_ROUTE_NO = RM.RM_ROUTE_NO;
                    bd.RM_DESTINATION_BEGIN = RM.RM_DESTINATION_BEGIN;
                    bd.RM_DESTINATION_END = RM.RM_DESTINATION_END;
                    bd.RM_LENGTH = RM.RM_LENGTH;
                    bd.RM_STATUS = 0;
                   
                    db.ROUTE_MAP.Add(bd);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Route/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.ROUTE_MAP.Where(x=>x.RM_ID == id).FirstOrDefault());
        }

        // POST: Route/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ROUTE_MAP RM)
        {
            try
            {
                ROUTE_MAP sk = db.ROUTE_MAP.Where(x => x.RM_ID == id).FirstOrDefault();
                {

                    sk.RM_ROUTE_NO = RM.RM_ROUTE_NO;
                    sk.RM_DESTINATION_BEGIN = RM.RM_DESTINATION_BEGIN;
                    sk.RM_DESTINATION_END = RM.RM_DESTINATION_END;
                    sk.RM_LENGTH = RM.RM_LENGTH;
                    

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

        // GET: Route/Delete/5
        public ActionResult Delete(int id)
        {
            ROUTE_MAP stc1 = db.ROUTE_MAP.Where(x => x.RM_ID == id).FirstOrDefault();
            db.ROUTE_MAP.Remove(stc1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Route/Delete/5
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
