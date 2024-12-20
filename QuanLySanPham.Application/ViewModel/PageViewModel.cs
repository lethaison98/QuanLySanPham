using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Application.ViewModel
{
    public class PageViewModel<T>
    {
       //một lớp dùng để đại diện cho dữ liệu phân trang trong ứng dụng của bạn.
       //Lớp này giúp phân chia và quản lý dữ liệu lớn thành nhiều trang nhỏ hơn để hiển thị dễ dàng hơn trong giao diện người dùng
       //như danh sách người dùng, sản phẩm
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }
    }
}
