using QuanLySanPham.Application.Request;
using QuanLySanPham.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLySanPham.Application.Interfaces
{
    public interface IQuanLyLoaiSanPham
    {
        public class ApiResult<T>
        {
            public bool IsSuccessed { get; set; } // Cho biết kết quả có thành công hay không
            public string Message { get; set; }  // Thông báo lỗi hoặc thành công
            public T ResultObj { get; set; }     // Dữ liệu kết quả trả về (nếu có)
        }

        // Lấy danh sách tất cả loại sản phẩm
        Task<ApiResult<List<LoaiSanPhamViewModel>>> GetAllAsync();

        // Lấy loại sản phẩm theo ID
        Task<ApiResult<LoaiSanPhamViewModel>> GetByIdAsync(Guid id);

        // Thêm loại sản phẩm mới
        Task<ApiResult<bool>> CreateAsync(LoaiSanPhamRequest request);

        // Cập nhật loại sản phẩm
        Task<ApiResult<bool>> UpdateAsync(Guid id, LoaiSanPhamRequest request);

        // Xóa loại sản phẩm theo ID
        Task<ApiResult<bool>> DeleteAsync(Guid id);

        // Tìm kiếm loại sản phẩm theo từ khóa và phân trang
        Task<ApiResult<PageViewModel<LoaiSanPhamViewModel>>> GetAllPagingAsync(string keyword, int pageIndex, int pageSize);
    }
}
