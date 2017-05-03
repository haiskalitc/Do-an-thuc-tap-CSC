using Common.Enum;
using Common.Utilities;
using SystemWebUI.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SystemWebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                return View();
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");
        }
        
    }
}