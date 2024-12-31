using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sales_Rep.Models;

namespace Sales_Rep.Controllers
{
    public class GRNController : Controller
    {
        // GET: GRN
        SALES_REPEntities db = new SALES_REPEntities();
        public ActionResult Index()
        {
            return View();
        }

        // GET: GRN/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GRN/Create
        public ActionResult Create()
        {
            ViewBag.pcode = new SelectList(db.tblProducts.ToList(), "pcode", "pdesc");
            ViewBag.Vendor = new SelectList(db.tblVendors.ToList(), "id", "vendor");
            return View();
        }

        // POST: GRN/Create
        [HttpPost]
        public ActionResult Create(tblStockIn pr)
        {
            try
            {
                int vendorid = Convert.ToInt32(pr.vendor);

                var vendorDetails = db.tblVendors.Where(x => x.id == vendorid).FirstOrDefault();

                tblStockIn bd = new tblStockIn();
                {
                    bd.refno = db.Proc_INCREMENT_ID("GR").FirstOrDefault();
                    bd.pcode = pr.pcode;
                    bd.qty = pr.qty;
                    bd.sdate = System.DateTime.Now;
                    bd.stockinby = "IN";
                    bd.status = "0";
                    bd.vendor = pr.vendor;
                    bd.InvoiceNo = pr.InvoiceNo;
                    bd.Description = pr.Description;
                    bd.ExpireDate = pr.ExpireDate;
                    bd.Discounts = pr.Discounts;
                    bd.LoyalityPrice = 0;
                    bd.Cost = pr.Cost;
                    bd.TotalCost = pr.Cost*pr.qty;
                    bd.UnitPrice = pr.UnitPrice;
                    bd.OurPrice =  pr.Cost - pr.Discounts;
                   
                    bd.GrossTotal = bd.TotalCost;
                    bd.NetTotal = bd.OurPrice * bd.qty;
                    bd.ContactPerson = vendorDetails.vendor;
                    bd.Address = vendorDetails.address;
                    bd.TelephoneNo = vendorDetails.telephone;
                    bd.FreeQty = pr.FreeQty;
                    db.tblStockIns.Add(bd);
                    db.SaveChanges();
                }

                tblProduct pd = db.tblProducts.Where(x => x.pcode == pr.pcode).FirstOrDefault();
                {

                    pd.price = Convert.ToDecimal(pr.UnitPrice);
                    pd.ourprice =Convert.ToDecimal(pr.OurPrice) - Convert.ToDecimal(pr.Discounts);
                    pd.qty = pd.qty+ pr.qty;
                    pd.LoyalityPrice =Convert.ToDecimal(pr.LoyalityPrice);
                    pd.Cost = Convert.ToDecimal(pr.Cost);

                   

                    db.Entry(pd).State = EntityState.Modified;
                    db.SaveChanges();

                }


                decimal Cos = Convert.ToDecimal(pr.Cost);
                decimal Qt = Convert.ToDecimal(pr.qty);
                decimal Dueamt =Convert.ToDecimal(db.tblStockIns.Where(x => x.InvoiceNo == pr.InvoiceNo).Sum(x => x.NetTotal));

                int Vid = Convert.ToInt32(pr.vendor);

                string supName = db.tblVendors.Where(x => x.id == Vid).FirstOrDefault().vendor;


                tblSupplierPayment sp = new tblSupplierPayment();
                {
                    sp.SupplierName = supName;
                    sp.ReferenceNo = pr.InvoiceNo;
                    sp.InvoiceNo = pr.InvoiceNo;
                    sp.Description = "Supplier Payment";
                    sp.DueAmt = Cos * Qt;
                    
                    db.tblSupplierPayments.Add(sp);
                    db.SaveChanges();
                }



                db.Proc_INCREMENT_ID_Update("GR");
                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }

        // GET: GRN/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GRN/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: GRN/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GRN/Delete/5
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
