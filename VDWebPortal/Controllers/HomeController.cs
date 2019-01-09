using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VDWebPortal.Models;

namespace VDWebPortal.Controllers
{
    public class HomeController : Controller
    {
        private VaishaliDairyDBEntities db = new VaishaliDairyDBEntities();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            string UserID = frm["username"].ToString();
            string Password = frm["password"].ToString();
            if(Resources.VDResources.AdminUser == UserID && Resources.VDResources.P_Admin_User == Password)
            {
                Session["CurrentUser"] = "Admin";
                Session["EmailID"] = UserID;
                return RedirectToAction("Index", "AdminCtrl", null);
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["CurrentUser"] = "";
            Session["EmailID"] = "";
            return RedirectToAction("Index", "Home", null);
        }

        public async Task<ActionResult> eNews()
        {
            return View(await db.T_E_NewsPaper.ToListAsync());
        }

    }
}
