using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webThucAnNhanh.Models;

namespace webThucAnNhanh.Controllers
{
    public class LoaiSPController : Controller
    {
        // GET: LoaiSP
        FastfoodServiceEntities db = new FastfoodServiceEntities();
        public ActionResult Index()
        {
            return View(db.Loais.ToList());
        }
        [HttpGet]
        public ActionResult ThemLoaiSanPham()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemLoaiSanPham(Loai sloai)
        {
            db.Loais.Add(sloai);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult XoaLoaiSanPham(int Maloai)
        {
            Loai ssloai = db.Loais.SingleOrDefault(n => n.MaLoai == Maloai);
            if (ssloai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ssloai);

        }
        [HttpPost, ActionName("XoaLoaiSanPham")]
        public ActionResult XacNhanXoa(int Maloai)
        {
            Loai ssloai = db.Loais.SingleOrDefault(n => n.MaLoai == Maloai);
            if (ssloai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Loais.Remove(ssloai);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}