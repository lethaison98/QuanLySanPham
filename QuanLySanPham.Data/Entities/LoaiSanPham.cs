using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Data.Entities
{
    public class LoaiSanPham : BaseEntity 
    {
        public int IdLoaiSanPham { get; set; }
        public string TenLoaiSanPham { get; set; }
        public string MoTa { get; set; }
        public ICollection<Product> Products { get; set; } 
    }
}
