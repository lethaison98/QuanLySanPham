using QuanLySanPham.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Application.ViewModel
{
    public class FileObjectViewModel
    {
        public int IdFileObject { get; set; }
        public int IdFile { get; set; }
        public int IdObject { get; set; }
        public int IdLoaiObject { get; set; }
        public string LoaiObject { get; set; }
        public int TrangThai { get; set; }
        public string LinkFile { get; set; }
        public string TenFile { get; set; }
        public string MoTa { get; set; }
        public Files File { get; set; }
    }
}
