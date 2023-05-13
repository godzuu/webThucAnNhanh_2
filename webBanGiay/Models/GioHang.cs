using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webThucAnNhanh.Models
{
    public class GioHang
    {
        FastfoodServiceEntities db = new FastfoodServiceEntities();
        public int iMaSanPham { get; set; }
        public string sTenSanPham { get; set; }
        public string sHinhAnh { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        //tạo giỏ hàng
        public GioHang(int MaSanPham)
        {
            iMaSanPham = MaSanPham;
            SanPham sanpham = db.SanPhams.Single(n => n.MaSanPham == iMaSanPham);
            sTenSanPham = sanpham.TenSanPham;
            sHinhAnh = sanpham.AnhSanPham;
            dDonGia =double.Parse(sanpham.GiaBan.ToString());
            iSoLuong = 1;

        }
    }
}