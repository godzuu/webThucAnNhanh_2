//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace webThucAnNhanh.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }
    
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public Nullable<decimal> GiaBan { get; set; }
        public string AnhSanPham { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        public Nullable<int> SoLuongTon { get; set; }
        public Nullable<int> MaNSX { get; set; }
        public Nullable<int> MaLoai { get; set; }
        public Nullable<int> Moi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual Loai Loai { get; set; }
        public virtual NhaSanXuat NhaSanXuat { get; set; }
    }
}
