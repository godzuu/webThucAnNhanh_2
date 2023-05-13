using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webThucAnNhanh.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace webThucAnNhanh.Controllers
{
    public class QuanLySPController : Controller
    {
        FastfoodServiceEntities db = new FastfoodServiceEntities();
        // GET: QuanLySP
        public ActionResult Index(int? page)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("DangNhap", "QuanLySP");
            }
           
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            return View(db.SanPhams.ToList().OrderBy(n => n.MaSanPham).ToPagedList(pageNumber, pageSize));
                
        }
        // thêm 
        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaLoai = new SelectList( db.Loais.ToList().OrderBy(n=>n.TenLoai), "MaLoai","TenLoai");
            ViewBag.MaNSX =new SelectList( db.NhaSanXuats.ToList().OrderBy(n=>n.TenNSX), "MaNSX", "TenNSX");
            return View();

        }
        [HttpPost]
        public ActionResult ThemMoi(SanPham sanpham,HttpPostedFileBase fileUpload)
        {
           
            // dưa dữ liệu dropdown list
            ViewBag.MaLoai = new SelectList(db.Loais.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            //ktra anh giay
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Chọn hình ảnh";
                return View();
            }
            // thêm vào csdl, ModelState.IsValid nếu thảo mảng tất cả các dk 
            if (ModelState.IsValid)
            {
                //luu tên file
                var fileName = Path.GetFileName(fileUpload.FileName);
                //luu đường dẫn
                var path = Path.Combine(Server.MapPath("~/AnhSanPham"), fileName);
                //ktra hinh anh ton tai chua
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                sanpham.AnhSanPham = fileUpload.FileName;
                db.SanPhams.Add(sanpham);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //chỉnh sua sp
        [HttpGet]
        public ActionResult ChinhSua( int MaSanPham, HttpPostedFileBase fileUpload)
        {
            //lấy đối tượng san pham theo mã
            SanPham giay = db.SanPhams.SingleOrDefault(n=> n.MaSanPham == MaSanPham);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLoai = new SelectList(db.Loais.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai",giay.MaLoai);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX",giay.MaNSX);

            return View(giay);
        }
        [HttpPost]
        public ActionResult ChinhSua(SanPham sanpham)
        {
           
            
           
            // thêm vào csdl, ModelState.IsValid nếu thảo mảng tất cả các dk 
            if (ModelState.IsValid)
            {
                //cap nhật trong model
                db.Entry(sanpham).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            // dưa dữ liệu dropdown list
            ViewBag.MaLoai = new SelectList(db.Loais.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai", sanpham.MaLoai);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", sanpham.MaNSX);
            return RedirectToAction("Index");
        }
        public ActionResult HienThi(int MaSanPham)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSanPham == MaSanPham);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            

            return View(sanpham);
        }
        [HttpGet]
        public ActionResult XoaSp(int MaSanPham)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSanPham == MaSanPham);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            return View(sanpham);  
        }
        [HttpPost,ActionName("XoaSp")]
        public ActionResult XacNhanXoa(int MaSanPham)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSanPham == MaSanPham);
            if(sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SanPhams.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string user, string password)
        {
            if (user.ToLower() == "admin" && password == "123")
            {
                Session["User"] = "admin";
                return RedirectToAction("Index", "QuanLySP");
            }
            else
            {
                return View();
            }

        }
        public ActionResult Logout()
        {
            Session.Remove("user");
            FormsAuthentication.SignOut();
            return RedirectToAction("DangNhap","QuanLySP");

        }
    }
}