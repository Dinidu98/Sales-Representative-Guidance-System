using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sales_Rep.Models;

namespace Sales_Rep.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        SALES_REPEntities db = new SALES_REPEntities();
        public ActionResult Index()
        {
            return View(db.tblSupplierPayments.Where(x=>x.DueAmt != x.PaidAmount).ToList());
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Paymethod = new SelectList(db.MDCODEs.Where(x=>x.CAT == "S").ToList(), "DESCRIP", "DESCRIP");
        
            return View(db.tblSupplierPayments.Where(x=>x.Id == id).FirstOrDefault());
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblSupplierPayment sp)
        {
            try
            {
                tblSupplierPayment sk = db.tblSupplierPayments.Where(x => x.Id == id).FirstOrDefault();
                {

                    sk.PaymentMethod = sp.PaymentMethod;
                    sk.PaymentDate = System.DateTime.Now;
                    sk.Account = sp.Account;
                    sk.AccountNo = sp.AccountNo;
                    sk.ChequeNo = sp.ChequeNo;
                    sk.ChequeAmount = sp.PayingAmount;
                    sk.ChequeDate = System.DateTime.Now;
                    sk.DueDate = System.DateTime.Now;
                    sk.DueAmt = sp.PayingAmount;

                    sk.PaidAmount = sp.PayingAmount;
                    sk.InvoiceTotal = sp.PayingAmount;
                    sk.PayingAmount = sp.PayingAmount;

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

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Supplier/Delete/5
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
