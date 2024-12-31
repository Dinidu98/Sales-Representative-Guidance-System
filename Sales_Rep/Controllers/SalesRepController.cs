using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sales_Rep.Models;

namespace Sales_Rep.Controllers
{
    public class SalesRepController : Controller
    {
        // GET: SalesRep
        SALES_REPEntities db = new SALES_REPEntities();
        public ActionResult Index()
        {
            return View(db.tblSalesReps.Where(x=>x.Status ==0).ToList());
        }

        // GET: SalesRep/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalesRep/Create
        public ActionResult Create()
        {
            ViewBag.Gender = new SelectList(db.MDCODEs.Where(x=>x.CAT =="G").ToList(), "DESCRIP", "DESCRIP");
            return View();
        }

        // POST: SalesRep/Create
        [HttpPost]
        public ActionResult Create(tblSalesRep SP)
        {
            try
            {
                tblSalesRep bd = new tblSalesRep();
                {
                    bd.NIC = SP.NIC;
                    bd.Name = SP.Name;
                    bd.Email = "repsale020@gmail.com" /*SP.Email*/;
                    bd.Phone_No = SP.Phone_No;
                    bd.Address = SP.Address;
                    bd.Gender = SP.Gender;
                    bd.Status =0;
                    bd.Email_Password = "trdmnytpehkcsimp";



                    db.tblSalesReps.Add(bd);
                    db.SaveChanges();
                }
                Login lg = new Login();
                {
                   lg.User_Name = SP.NIC;
                   lg.Password = SP.NIC;
                   lg.Confirm_Password = SP.NIC;
                   lg.Status =0;
                   lg.Role = "Sales";
                   lg.Enter_Date = System.DateTime.Now;
                   lg.Enter_User = System.Web.HttpContext.Current.Session["User_Name"].ToString();
                    lg.Login_Id = bd.Id;


                    db.Logins.Add(lg);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SalesRep/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Gender = new SelectList(db.MDCODEs.Where(x => x.CAT == "G").ToList(), "DESCRIP", "DESCRIP");
            return View(db.tblSalesReps.Where(X=>X.Id == id).FirstOrDefault());
        }

        // POST: SalesRep/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblSalesRep SP)
        {
            try
            {
                tblSalesRep bd = db.tblSalesReps.Where(x => x.Id == id).FirstOrDefault();
                {

                    bd.Name = SP.Name;
                    //bd.Email = SP.Email;
                    bd.Phone_No = SP.Phone_No;
                    bd.Address = SP.Address;
                    bd.Gender = SP.Gender;

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

        // GET: SalesRep/Delete/5
        public ActionResult Delete(int id)
        {
            tblSalesRep stc1 = db.tblSalesReps.Where(x => x.Id == id).FirstOrDefault();
            db.tblSalesReps.Remove(stc1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: SalesRep/Delete/5
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
