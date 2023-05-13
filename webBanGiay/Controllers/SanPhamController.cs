using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webThucAnNhanh.Models;

namespace webThucAnNhanh.Controllers
{
    public class SanPhamController : Controller
    {
        FastfoodServiceEntities db = new FastfoodServiceEntities();
        // GET: Giay
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult SanphammoiPartial()
        {
            var sanphammoi = db.SanPhams.Where(m => m.Moi ==1).ToList();
            return PartialView(sanphammoi);

        }
        public PartialViewResult SanphamPartial()
        {
            var sanphammoi = db.SanPhams.Where(m => m.Moi != 1).ToList();
            return PartialView(sanphammoi);

        }
        public PartialViewResult LoaiSanPhamPartial()
        {
            var SPTheoLoai = db.Loais.ToList();
            return PartialView(SPTheoLoai);

        }
        
        public ActionResult SanPhamTheoLoai(int MaLoai = 0)
        {
            //kiễm tra xem sản phẩm có tồn tại ko
            Loai sloai = db.Loais.SingleOrDefault(n => n.MaLoai == MaLoai);
            if (sloai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<SanPham> listgiay = db.SanPhams.Where(n => n.MaLoai == MaLoai).OrderBy(n => n.GiaBan).ToList();
            if (listgiay.Count == 0)
            {
                ViewBag.Giay = "Không còn sản phẩm nào thuộc loại này";
            }
            return View(listgiay);
        }
        //xem chi tiet
        public ActionResult XemChiTiet(int MaGiay=0)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n=>n.MaSanPham == MaGiay);
            if(sanpham == null)
            {
                //trả về  trang báo lỗi 
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.NhaSX = db.NhaSanXuats.Single(n=>n.MaNSX== sanpham.MaNSX).TenNSX;
            ViewBag.LoaiSanPham = db.Loais.Single(n => n.MaLoai == sanpham.MaLoai).TenLoai;
            return View(sanpham);
        }

    }
}