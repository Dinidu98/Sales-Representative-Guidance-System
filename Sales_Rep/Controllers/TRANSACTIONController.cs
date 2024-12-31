using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sales_Rep.Models;

namespace Sales_Rep.Controllers
{
    public class TRANSACTIONController : Controller
    {
        // GET: TRANSACTION
        SALES_REPEntities db = new SALES_REPEntities();
        public ActionResult SalesRepOutstanding()
        {
            List<Sales_Rep_outstandings> Bedt = new List<Sales_Rep_outstandings>();



            List<PROC_SALESREP_OUTSTANDINGS_Result> OList = db.PROC_SALESREP_OUTSTANDINGS().ToList();
            foreach (var DbData in OList)
            {
                int Row = Convert.ToInt32(DbData.row_num);
                int tot = Convert.ToInt32(DbData.total);
                string Rep = DbData.cashier.ToString();

                Sales_Rep_outstandings Wareh = new Sales_Rep_outstandings()
                {
                    Row_Num = Row,
                    Totals = tot,
                    Sales_Rep = Rep


                };
                Bedt.Add(Wareh);
            }
            return View(Bedt);
        }

        // GET: TRANSACTION/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TRANSACTION/Create
        public ActionResult Create()
        {
            ViewBag.REP = new SelectList(db.tblSalesReps.Where(X=>X.Status==0).ToList(), "Id", "Name");
            ViewBag.ROUTE = new SelectList(db.ROUTE_MAP.Where(X=>X.RM_STATUS==0).ToList(), "RM_ID", "RM_ROUTE_NO");
            return View();
        }

        // POST: TRANSACTION/Create
        [HttpPost]
        public ActionResult Create(TRANSACTION tr)
        {
            try
            {
                var haveslaesreid = db.TRANSACTIONs.Where(x => x.SALE_REP_ID == tr.SALE_REP_ID && x.STATUS == 0).FirstOrDefault();
                if(haveslaesreid != null)
                {
                    TRANSACTION pd = db.TRANSACTIONs.Where(x => x.SALE_REP_ID == tr.SALE_REP_ID && x.STATUS == 0).FirstOrDefault();
                    {

                        pd.STATUS = 1;
                        
                        db.Entry(pd).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }

                TRANSACTION bd = new TRANSACTION();
                {
                    bd.SALE_NO = db.Proc_INCREMENT_ID("S").FirstOrDefault();
                    bd.SALE_REP_ID = tr.SALE_REP_ID;
                    bd.ROUTE_ID = tr.ROUTE_ID;
                    bd.STATUS = 0;
                    bd.ENTER_USER= System.Web.HttpContext.Current.Session["User_Name"].ToString();
                    db.TRANSACTIONs.Add(bd);
                    db.SaveChanges();
                }

                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: TRANSACTION/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TRANSACTION/Edit/5
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

        // GET: TRANSACTION/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TRANSACTION/Delete/5
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
