using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webThucAnNhanh.Models;

namespace webBanGiay.Controllers
{
    public class ThanhCongController : Controller
    {
        // GET: ThanhCong
        FastfoodServiceEntities db = new FastfoodServiceEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ThanhCongMua()
        {

            List<GioHang> lstGioHang = new List<GioHang>();
            //ktra sp có tồn tại trong session[giohang]
            
            return RedirectToAction("Index", "Home");
        }
    }
    
}