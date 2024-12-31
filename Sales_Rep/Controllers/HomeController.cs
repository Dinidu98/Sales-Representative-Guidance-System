using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sales_Rep.Models;

namespace Sales_Rep.Controllers
{
    public class HomeController : Controller
    {
        SALES_REPEntities db = new SALES_REPEntities();
        public ActionResult Index()
        {
            ViewBag.TotalSalesRep = db.tblSalesReps.Where(x => x.Status == 0).Count();
            ViewBag.TotalProducts = db.tblProducts.Count();
            ViewBag.TotalCompanies = db.tblCompanies.Where(x => x.Status == 0).Count();
            ViewBag.TotalReorders = db.tblProducts.Where(x => x.reorder >= x.qty).Count();
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateInsUser(string email,string password,string password_confirmation)
        {
            

            bool save = true;
            try
            {
               

                Login lg = new Login();
                {
                    lg.User_Name = email;
                    lg.Password = password;
                    lg.Confirm_Password = password_confirmation;
                    lg.Status = 0;
                    lg.Role = "Sales";
                    lg.Enter_Date = DateTime.Now;
                    lg.Enter_User = "Sales";
                   

                    db.Logins.Add(lg);
                    db.SaveChanges();

                    save = true;

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

        public ActionResult CheckLog(string username, string password)
        {
            
            int save = 0;

            var user = db.Logins.Where(x => x.User_Name == username && x.Password == password && x.Status == 0).FirstOrDefault();

            //int user_rol_id = Convert.ToInt32(user.Role);

            //var roles = db.Roles.Where(x => x.Id == user_rol_id).FirstOrDefault();

            if (user != null)
            {



                if (user.Role == "1")
                {
                    Session["User_Name"] = user.User_Name;
                    Session["Role"] =user.Role;
                    Session["ID"] = user.ID;

                    save = 1;
                }
                else
                {
                    Session["User_Name"] = user.User_Name;
                    Session["Role"] = user.Role; 
                    Session["ID"] = user.ID;

                    save = 2;
                }



                // return View("Index");

            }
            else
            {
                save = 0;

                //  return View("Login");
            }

            return Json(save, JsonRequestBehavior.AllowGet);
            // return RedirectToAction("Index");
        }
    }
}