using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Application.ViewModel
{
    public class LoaiSanPhamViewModel
    {
        public Guid IdLoaiSanPham { get; set; }

        public string TenLoai { get; set; }

        public string MoTa { get; set; }

        public DateTime NgayTao { get; set; }

        public DateTime? NgayCapNhat { get; set; }
        public object TenLoaiSanPham { get; set; }
    }
}
