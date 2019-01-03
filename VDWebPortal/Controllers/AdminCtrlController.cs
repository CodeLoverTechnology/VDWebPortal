using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VDWebPortal.App_Code;

namespace VDWebPortal.Controllers
{
    public class AdminCtrlController : Controller
    {
        // GET: AdminCtrl
        public ActionResult Index()
        {
            if (!CommonFunctionVD.CheckUserAuthentication())
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}