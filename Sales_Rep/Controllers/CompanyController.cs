using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sales_Rep.Models;

namespace Sales_Rep.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        SALES_REPEntities db = new SALES_REPEntities();
        public ActionResult Index()
        {
            return View(db.tblCompanies.Where(x=>x.Status ==0).ToList());
        }

        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        [HttpPost]
        public ActionResult Create(tblCompany com)
        {
            try
            {
                tblCompany bd = new tblCompany();
                {
                    bd.CompanyName = com.CompanyName;
                    bd.Address1 = com.Address1;
                    bd.Address2 = com.Address2;
                    bd.Address3 = com.Address3;
                    bd.Tel1 = com.Tel1;
                    bd.Tel2 = com.Tel2;
                    bd.Email = com.Email;
                    bd.Fax = com.Fax;
                    bd.WebSite = com.WebSite;
                    bd.Status = 0;
                    db.tblCompanies.Add(bd);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.tblCompanies.Where(x => x.Id == id).FirstOrDefault());
        }

        // POST: Company/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblCompany com)
        {
            try
            {
                tblCompany sk = db.tblCompanies.Where(x => x.Id == id).FirstOrDefault();
                {

                    sk.CompanyName = com.CompanyName;
                    sk.Address1 = com.Address1;
                    sk.Address2 = com.Address2;
                    sk.Address3 = com.Address3;
                    sk.Tel1 = com.Tel1;
                    sk.Tel2 = com.Tel2;
                    sk.Email = com.Email;
                    sk.Fax = com.Fax;
                    sk.WebSite = com.WebSite;

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

        // GET: Company/Delete/5
        public ActionResult Delete(int id)
        {
            tblCompany stc1 = db.tblCompanies.Where(x => x.Id == id).FirstOrDefault();
            db.tblCompanies.Remove(stc1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Company/Delete/5
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
