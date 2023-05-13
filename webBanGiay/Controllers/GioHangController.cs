using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webThucAnNhanh.Models;
namespace webThucAnNhanh.Controllers
{
    public class GioHangController : Controller
    {
        //lấy giỏ hàng
        FastfoodServiceEntities db = new FastfoodServiceEntities();
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang == null)
            {
                // nếu chưa tồn tại thì tạo gio hang
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //thêm gio hang
        public ActionResult ThemGioHang(int iMaSanPham,string strUrl)
        {
            SanPham sanphaM = db.SanPhams.SingleOrDefault(n => n.MaSanPham == iMaSanPham);
            if(sanphaM == null)
            {
                Response.StatusCode = 404;
                return null; 
            }
            List<GioHang> lstGioHang = LayGioHang();
            //kiễm tra san pham này đã tồn tại trong session[goihang]
            GioHang sanpham = lstGioHang.Find(n=>n.iMaSanPham ==iMaSanPham);
            if(sanpham == null)
            {
                sanpham = new GioHang(iMaSanPham);
                lstGioHang.Add(sanpham);
                
                return Redirect(strUrl);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strUrl);
            }
        }
        //sũa gio hang
        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            //kiem tra sanpam
            SanPham sanphaM = db.SanPhams.SingleOrDefault(n=>n.MaSanPham == iMaSP);
            if (sanphaM == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng từ session
            List<GioHang> lstGioHang = LayGioHang();
            //ktra sp có tồn tại trong session[giohang]
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSanPham == iMaSP);
            if(sanpham != null)
            {
                //neu ton tai thì cho sua soluong
                sanpham.iSoLuong = int.Parse(   f["txtSoLuong"].ToString());

            }
            TempData["CapNhatThanhCong"] = "Đã cập nhật thành công giỏ hàng";
            return RedirectToAction("GioHang");
        }
        //Xóa gio hang
        public ActionResult XoaGioHang(int iMaSP)
        {
            SanPham sanphaM = db.SanPhams.SingleOrDefault(n => n.MaSanPham == iMaSP);
            if (sanphaM == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            //ktra sp có tồn tại trong session[giohang]
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSanPham == iMaSP);
            if (sanpham != null)
            {
                
                lstGioHang.RemoveAll(n => n.iMaSanPham == iMaSP);

                
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("GioHang");
        }
        //xay dung trang gio hang
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();

            return View(lstGioHang);
        }
        //tinh tong soluong va tong tien
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang !=null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }
        public ActionResult GioHangPartial()
        {
            if(TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //nguoi dùng chỉnh sữa gio hàng
        public ActionResult SuaGiohang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();

            return View(lstGioHang);
        }

        //dat hang
        //[HttpPost]
        //public ActionResult DatHang()
        //{
        //    //kiem tra dang nhap
        //    if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
        //    {
        //        return RedirectToAction("Dangnhap", "NguoiDung");
        //    }
        //    //kiem tra gio hang
        //    if (Session["GioHang"] == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
            
        //    //them don hang
        //    DonHang ddh = new DonHang();
        //    KhachHang kh =(KhachHang) Session["TaiKhoan"];
        //    List<GioHang> gh = LayGioHang();
        //    ddh.MaKH = kh.MaKH;
        //    ddh.NgayDat = DateTime.Now;
        //    db.DonHangs.Add(ddh);
        //    db.SaveChanges();
        //    // them  chi tiet don hang
        //    foreach(var item in gh)
        //    {
        //        ChiTietDonHang ctDonghang = new ChiTietDonHang();
        //        ctDonghang.MaDonHang = ddh.MaDonHang;
        //        ctDonghang.MaSanPham = item.iMaSanPham;
        //        ctDonghang.SoLuong = item.iSoLuong;
        //        ctDonghang.DonGia = item.dDonGia.ToString();
        //        db.ChiTietDonHangs.Add(ctDonghang);
        //        TempData["DatHangThanhCong"] = "Đã đặt hàng thành công, hãy tiếp tục mua sắm. Bạn sẽ thanh toán khi nhận hàng";

        //    }
            
        //    gh.Clear();
        //    return RedirectToAction("Index", "Home");

        //    db.SaveChanges();
        //    /*if (TongSoLuong() == 0)
        //    {
        //        ViewBag.TongSoLuong = TongSoLuong();
        //    }*/
            




        //    return RedirectToAction("Index","Home");
        //}
    }
}