using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sales_Rep.Models;

namespace Sales_Rep.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        SALES_REPEntities db = new SALES_REPEntities();
        public ActionResult Index()
        {
            return View(db.tblProducts.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Brand = new SelectList(db.tblBrands.ToList(), "id", "Brand");
            ViewBag.Category = new SelectList(db.tblCategories.ToList(), "id", "Category");
            ViewBag.Company = new SelectList(db.tblCompanies.Where(x=>x.Status==0).ToList(), "Id", "CompanyName");
            ViewBag.ProductNo = db.Proc_INCREMENT_ID("P").FirstOrDefault();
            ViewBag.Priority= new SelectList(db.PRIOIRTies.ToList(), "PR_ID", "PR_NAME");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(tblProduct pr)
        {
            try
            {
                tblProduct bd = new tblProduct();
                {
                    bd.pcode = pr.pcode;
                    bd.barcode = "0";
                    bd.pdesc = pr.pdesc;
                    bd.price =0;
                    bd.ourprice = 0;
                    bd.qty = 0;
                    bd.reorder = pr.reorder;
                    bd.LoyalityPrice = 0;
                    bd.Cost = 0;
                    bd.Brand_Id = pr.Brand_Id;
                    bd.Category_Id = pr.Category_Id;
                    bd.Brand_Name = db.tblBrands.Where(x=>x.id == pr.Brand_Id).FirstOrDefault().Brand ;
                    bd.Company_Id = pr.Company_Id;
                    bd.Priority = pr.Priority;
                   
                    db.tblProducts.Add(bd);
                    db.SaveChanges();
                }
                db.Proc_INCREMENT_ID_Update("P");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Brand = new SelectList(db.tblBrands.ToList(), "id", "Brand");
            ViewBag.Category = new SelectList(db.tblCategories.ToList(), "id", "Category");
            ViewBag.Company = new SelectList(db.tblCompanies.Where(x => x.Status == 0).ToList(), "Id", "CompanyName");
            ViewBag.Priority = new SelectList(db.PRIOIRTies.ToList(), "PR_ID", "PR_NAME");
            return View(db.tblProducts.Where(x=>x.PId == id).FirstOrDefault());
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblProduct pr)
        {
            try
            {
                tblProduct bd = db.tblProducts.Where(x => x.PId == id).FirstOrDefault();
                {

                    bd.pcode = pr.pcode;
                    bd.barcode = "0";
                    bd.pdesc = pr.pdesc;
                    bd.price = 0;
                    bd.ourprice = 0;
                   
                    bd.reorder = pr.reorder;
                    bd.LoyalityPrice = 0;
                    bd.Cost =0;
                    bd.Brand_Id = pr.Brand_Id;
                    bd.Category_Id = pr.Category_Id;
                    bd.Brand_Name = db.tblBrands.Where(x => x.id == pr.Brand_Id).FirstOrDefault().Brand;
                    bd.Company_Id = pr.Company_Id;
                    bd.Priority = pr.Priority;
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

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            tblProduct stc1 = db.tblProducts.Where(x => x.PId == id).FirstOrDefault();
            db.tblProducts.Remove(stc1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Products/Delete/5
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

        public ActionResult ProductPriorityListIndex(string searchString)
        {

            List<tblProduct> Bedt = new List<tblProduct>();
            if (searchString != null)
            {

                List<tblProduct> OList = db.tblProducts.Where(x => x.pdesc == searchString).OrderBy(z=>z.Priority).ToList();
                foreach (var DbData in OList)
                {


                    tblProduct Wareh = new tblProduct()
                    {
                        pcode = DbData.pcode,
                        pdesc = DbData.pdesc,
                        price = DbData.price,
                        qty = DbData.qty

                    };
                    Bedt.Add(Wareh);
                }
            }
            else
            {
                List<tblProduct> OList = db.tblProducts.OrderBy(z => z.Priority).ToList();
                foreach (var DbData in OList)
                {


                    tblProduct Wareh = new tblProduct()
                    {
                        pcode = DbData.pcode,
                        pdesc = DbData.pdesc,
                        price = DbData.price,
                        qty = DbData.qty

                    };
                    Bedt.Add(Wareh);
                }
            }

            return View(Bedt);
        }

    }
}
