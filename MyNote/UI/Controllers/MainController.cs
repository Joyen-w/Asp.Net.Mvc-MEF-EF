using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult MyView()
        {
            return View();
        }
    }
}