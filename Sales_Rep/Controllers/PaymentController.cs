using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Sales_Rep.Models;

namespace Sales_Rep.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        SALES_REPEntities db = new SALES_REPEntities();
        public ActionResult Index()
        {
            ViewBag.INV = db.Proc_INCREMENT_ID("B").FirstOrDefault();

            ViewBag.Item = new SelectList(db.tblProducts.Where(x => x.qty > 0).ToList(), "PId", "pdesc");
            return View(db.tmpcarts.ToList());
        }
        public ActionResult GetRefId(int id)
        {
            //string bacc = db.tblStockIns.Where(x => x.id == id).FirstOrDefault().pcode;

            var data = (from bk in db.tblProducts
                        join br in db.tblCategories on bk.Category_Id equals br.id
                        where bk.PId == id
                        select new { rateId = bk.PId, seq = br.Category, ban = bk.price }).ToList().FirstOrDefault();



            return Json(data, JsonRequestBehavior.AllowGet);



        }
        public JsonResult GetTotla(int qty, decimal Price)
        {
            decimal Total = Price * qty;

            return Json(Total, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Createtmpinvoice(string Item, string category, decimal Price, int Qty, decimal Total, string inv_no)
        {


            bool save = false;
            try
            {
                int it = Convert.ToInt32(Item);

                var qty = db.tblProducts.Where(x => x.PId == it).FirstOrDefault().qty;

                var item_name = db.tblProducts.Where(x => x.PId == it).FirstOrDefault();
                if (qty != null)
                {


                    if (qty >= Qty)
                    {
                        tmpcart lg = new tmpcart();
                        {
                            lg.itm_id = Item;
                            lg.Inv_No = inv_no;
                            lg.item = item_name.pdesc;
                            lg.category = category;
                            lg.price = Convert.ToInt32(Price);
                            lg.qty = Qty;
                            lg.total = Convert.ToInt32(Total);


                            db.tmpcarts.Add(lg);
                            db.SaveChanges();

                            save = true;

                        }
                    }
                    else
                    {
                        save = false;
                    }
                }
                else
                {
                    save = false;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                save = false;
            }


            return Json(save, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Deleteinv(int id, tmpcart tmp)
        {
            //bool save = false;
            try
            {
                tmpcart tmp1 = db.tmpcarts.Where(x => x.id == id).FirstOrDefault();
                db.tmpcarts.Remove(tmp1);
                db.SaveChanges();
                //save = true;

                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                //save = false;
            }


            return View();
        }
        public ActionResult Getdiscount(decimal Discount, decimal subtot)
        {
            decimal grandTotal = (subtot) - Discount;

            return Json(grandTotal, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult ProcessInv(string invno, decimal subtot, decimal Discount, /*decimal vat,*/ decimal grandtot/*, decimal CustMoney, decimal Balance*/)
        {
            bool save = false;
            try
            {
                List<tmpcart> tmpca = db.tmpcarts.Where(x => x.Inv_No == invno).ToList();

                if (grandtot > 0)
                {

                    foreach (var item in tmpca)
                    {
                        var it = db.tmpcarts.Where(x => x.Inv_No == invno).FirstOrDefault();

                        int iid = Convert.ToInt32(it.itm_id);

                        var itmcode = db.tblProducts.Where(x => x.PId == iid).FirstOrDefault();
                        tblCart crt = new tblCart();
                        {
                            crt.transno = item.Inv_No;
                            crt.pcode = itmcode.pcode;
                            crt.price = itmcode.price;
                            crt.ourprice = itmcode.price;
                            crt.qty = item.qty;
                            crt.Linedisc = item.id;
                            crt.total = grandtot;
                            crt.sdate = System.DateTime.Now;
                            crt.status = "0";
                            crt.Description = null;
                            crt.cashier = System.Web.HttpContext.Current.Session["User_Name"].ToString();
                            crt.GrossTotalDiscount = Discount;
                            crt.HiddenOurPrice = itmcode.price;
                            crt.LoyalityPrice = itmcode.price;
                            crt.PaymentMethod = null;
                            crt.CashAmount = grandtot;
                            crt.CreditCardAmount = null;
                            crt.CreditCardInterest = null;
                            crt.CreditAmount = null;
                            crt.PayingAmount = 0;
                            crt.Balance = 0;
                            crt.NetTotal = null;
                            crt.LineDiscPer = null;
                            crt.LineDiscAmt = null;
                            crt.TotalBillDiscPer = null;
                            crt.TotalBillDiscAmount = null;
                            crt.TotalBillDiscPerAmount = null;
                            crt.BillPrice = item.qty * itmcode.price;
                            crt.SinhalaDesc = null;
                            crt.DifferentDisc = 0 - 0;
                            crt.PrintLanguage = null;
                            crt.ItemCount = null;
                            crt.Time = null;
                            crt.TotalDiscounts = Discount;
                            crt.Counter = "1";
                            crt.Cost = itmcode.Cost;
                            crt.TotalCost = item.qty * itmcode.Cost;

                            db.tblCarts.Add(crt);
                            db.SaveChanges();


                            string USERNAME = System.Web.HttpContext.Current.Session["User_Name"].ToString();
                            var salesRepDetails = db.tblSalesReps.Where(x => x.NIC == USERNAME).FirstOrDefault();

                            var CompanyEmail = db.tblOurCompanies.FirstOrDefault();

                            string from = salesRepDetails.Email;
                            string fromname = salesRepDetails.Name;
                            string from_password = salesRepDetails.Email_Password;
                            string to = CompanyEmail.Email;
                            string Toname = CompanyEmail.CompanyName;
                            string subject = "Sell Product";
                            string body = "Sell Date:" + crt.sdate + "Sell Invoice No:" + crt.transno;

                            var fromAddress = new MailAddress(from, fromname);
                            var toAddress = new MailAddress(to, Toname);

                            //using (SmtpClient smtpClient = new SmtpClient("localhost", 465))
                            //{ // <-- note the use of localhost
                            //    NetworkCredential creds = new NetworkCredential(from, from_password);
                            //    smtpClient.Credentials = creds;
                            //    MailMessage msg = new MailMessage(to, to, "Test", body);
                            //    smtpClient.Send(msg);
                            //}
                            var smtp = new SmtpClient
                            {
                                Host = "smtp.gmail.com",
                                Port = 587,
                                EnableSsl = true,
                                DeliveryMethod = SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = false,
                                Credentials = new NetworkCredential(fromAddress.Address, from_password)
                            };
                            using (var message = new MailMessage(fromAddress, toAddress)
                            {
                                Subject = subject,
                                Body = body
                            })
                            {
                                smtp.Send(message);
                            }

                        }

                        tblProduct prduct = db.tblProducts.Where(x => x.PId == iid).FirstOrDefault();
                        {

                            prduct.qty = prduct.qty - item.qty;
                            db.Entry(prduct).State = EntityState.Modified;
                            db.SaveChanges();

                        }

                        

                        tmpcart tcrt1 = db.tmpcarts.Where(x => x.Inv_No == invno).FirstOrDefault();
                        db.tmpcarts.Remove(tcrt1);
                        db.SaveChanges();


                    }

                   






                    db.Proc_INCREMENT_ID_Update("B");
                    save = true;

                    //save = true;
                }
                else
                {
                    save = false;
                }

            }

            catch (Exception ex)
            {
                throw (ex);
                save = false;
            }





            return Json(save, JsonRequestBehavior.AllowGet);
        }
        // GET: Payment/Details/5
       

        // GET: Payment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payment/Create
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

        // GET: Payment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Payment/Edit/5
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

        // GET: Payment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Payment/Delete/5
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

        public ActionResult SaleListIndex(string searchString)
        {
            
            ViewBag.InoviceNos = new SelectList(db.tblCarts.Distinct().ToList(), "transno", "transno");

            List<tblCart> Bedt = new List<tblCart>();
            if (searchString != null)
            {

                List<tblCart> OList = db.tblCarts.Distinct().Where(x=>x.transno== searchString).ToList();
                foreach (var DbData in OList)
                {


                    tblCart Wareh = new tblCart()
                    {
                        id = DbData.id,
                        transno = DbData.transno,
                        cashier = DbData.cashier

                    };
                    Bedt.Add(Wareh);
                }
            }
            else
            {
                List<tblCart> OList = db.tblCarts.Distinct().ToList();
                foreach (var DbData in OList)
                {


                    tblCart Wareh = new tblCart()
                    {
                        id = DbData.id,
                        transno = DbData.transno,
                        cashier = DbData.cashier

                    };
                    Bedt.Add(Wareh);
                }
            }

            return View(Bedt);
        }

        public ActionResult Details(string id)
        {
            List<InvoiceList> Bedt = new List<InvoiceList>();

            List<tblCart> OList = db.tblCarts.Where(x=>x.transno == id).ToList();
            foreach (var DbData in OList)
            {
                string PName = db.tblProducts.Where(x=>x.pcode==DbData.pcode).FirstOrDefault().pdesc;
                double prices = Convert.ToDouble(DbData.price);
                double Discounts = Convert.ToDouble(DbData.GrossTotalDiscount);

                InvoiceList Wareh = new InvoiceList()
                {
                    InvoiceNo = DbData.transno,
                    Pcode = DbData.pcode,
                    Prdouct = PName,
                    Price = prices,
                    Discount = Discounts,
                    Sell_Rep = DbData.cashier,

                };
                Bedt.Add(Wareh);
            }
            return View(Bedt);
        }
    }
}
