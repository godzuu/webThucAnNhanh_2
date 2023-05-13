using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webThucAnNhanh.Models;
using PagedList.Mvc;
using PagedList;
namespace webThucAnNhanh.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        //FormCollection 
        FastfoodServiceEntities db = new FastfoodServiceEntities();
        [HttpPost]
        public ActionResult KetQuaTimKiem( FormCollection f, int? page)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = sTuKhoa;
            if(sTuKhoa.ToString() == "dangnhapadmin")
            {
                return RedirectToAction("DangNhap", "QuanLySP");
            }
            //Contains phuoc thức tìm kiem
            List<SanPham> lstKQ = db.SanPhams.Where(n=>n.TenSanPham.Contains(sTuKhoa)).ToList();
            //phan trang
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            if(lstKQ.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm";
                return View(db.SanPhams.OrderBy(n=>n.TenSanPham).ToPagedList(pageNumber,pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy " + lstKQ.Count + " kết quả";
            return View(lstKQ.OrderBy(n=>n.TenSanPham).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult KetQuaTimKiem(string sTuKhoa, int? page)
        {

            ViewBag.TuKhoa = sTuKhoa;
            //Contains phuoc thức tìm kiem
            List<SanPham> lstKQ = db.SanPhams.Where(n => n.TenSanPham.Contains(sTuKhoa)).ToList();
            //phan trang
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            if (lstKQ.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                
                return View(db.SanPhams.OrderBy(n => n.TenSanPham).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy " + lstKQ.Count + " kết quả";
            return View(lstKQ.OrderBy(n => n.TenSanPham).ToPagedList(pageNumber, pageSize));
        }
    }
}