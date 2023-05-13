using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using webThucAnNhanh.Models;

namespace webThucAnNhanh.Controllers
{
    public class HomeController : Controller
    {
        FastfoodServiceEntities db = new FastfoodServiceEntities();
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult abc()
        {
            return View();
        }

    }
}