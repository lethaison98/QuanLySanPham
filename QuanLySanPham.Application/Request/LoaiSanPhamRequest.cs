using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLySanPham.Application.Request
{
    public class LoaiSanPhamRequest
    {
        // Tên loại sản phẩm
        [Required(ErrorMessage = "Tên loại sản phẩm là bắt buộc")]
        [MaxLength(100, ErrorMessage = "Tên loại sản phẩm không được vượt quá 100 ký tự")]
        public string TenLoai { get; set; }

        // Mô tả loại sản phẩm
        [MaxLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        public string MoTa { get; set; }

        // Ngày tạo (nếu cần thiết khi cập nhật)
        public DateTime? NgayTao { get; set; }
        [Key]
        public Guid IdLoaiSanPham { get; set; }
        public object TenLoaiSanPham { get; set; }
    }
}
