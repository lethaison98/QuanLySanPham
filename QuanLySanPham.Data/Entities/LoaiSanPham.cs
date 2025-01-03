using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Data.Entities
{
    public class LoaiSanPham
    {
        public int IdLoaiSanPham { get; set; }       // ID loại sản phẩm
        public string TenLoaiSanPham { get; set; }  // Tên loại sản phẩm
        public string MoTa { get; set; }            // Mô tả loại sản phẩm
        public DateTime NgayTao { get; set; }       // Ngày tạo
        public DateTime? NgayCapNhat { get; set; }  // Ngày cập nhật (optional)
        public bool IsDeleted { get; set; }
    }
}
