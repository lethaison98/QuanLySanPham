using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuanLySanPham.Application.Interfaces;
using QuanLySanPham.Application.Request;
using QuanLySanPham.Application.ViewModel;
using System;
using System.Threading.Tasks;

namespace QuanLySanPham.Controllers
{
    public class QuanLyLoaiSanPhamController : Controller
    {
        private readonly IQuanLyLoaiSanPham _quanLyLoaiSanPhamService;

        public QuanLyLoaiSanPhamController(IQuanLyLoaiSanPham quanLyLoaiSanPhamService)
        {
            _quanLyLoaiSanPhamService = quanLyLoaiSanPhamService;
        }

        // Action để hiển thị danh sách loại sản phẩm với phân trang
        [HttpPost]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var result = await _quanLyLoaiSanPhamService.GetAllPagingAsync(keyword, pageNumber, pageSize);

            if (!result.IsSuccessed)
            {
                TempData["ErrorMessage"] = result.Message; // Thông báo lỗi nếu có
                return RedirectToAction("Index");  // Chuyển hướng về trang danh sách
            }

            // Trả về dữ liệu và phân trang cho view
            var viewModel = result.ResultObj; // PageViewModel<LoaiSanPhamViewModel>
            return View("Index", viewModel);  // Trả về view Index.cshtml với dữ liệu
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var result = await _quanLyLoaiSanPhamService.GetAllPagingAsync(keyword, pageNumber, pageSize);

            if (!result.IsSuccessed)
            {
                TempData["ErrorMessage"] = result.Message;
                return View();
            }

            return View(result.ResultObj);
        }

    // Phương thức GET để hiển thị form tạo mới hoặc cập nhật loại sản phẩm
    [HttpGet]
        public async Task<IActionResult> CreateOrUpdate(Guid? id)  // Thay đổi kiểu id thành Guid
        {
            var model = new LoaiSanPhamRequest();

            // Nếu có id, lấy dữ liệu loại sản phẩm để chỉnh sửa
            if (id.HasValue)
            {
                var result = await _quanLyLoaiSanPhamService.GetByIdAsync(id.Value);
                if (result.IsSuccessed)
                {
                    var loaiSanPham = result.ResultObj;
                    model = new LoaiSanPhamRequest
                    {
                        IdLoaiSanPham = loaiSanPham.IdLoaiSanPham,
                        TenLoaiSanPham = loaiSanPham.TenLoaiSanPham,
                        MoTa = loaiSanPham.MoTa
                    };
                }
                else
                {
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        // Phương thức POST để xử lý dữ liệu từ form tạo mới hoặc cập nhật loại sản phẩm
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(LoaiSanPhamRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Nếu dữ liệu không hợp lệ, trả về lỗi
            }

            ApiResult<bool> result;
            if (request.IdLoaiSanPham == Guid.Empty)  // Nếu tạo mới loại sản phẩm
            {
                result = await _quanLyLoaiSanPhamService.CreateAsync(request);
            }
            else  // Nếu cập nhật loại sản phẩm hiện tại
            {
                result = await _quanLyLoaiSanPhamService.UpdateAsync(request.IdLoaiSanPham, request);
            }

            if (result.IsSuccessed)
            {
                TempData["SuccessMessage"] = "Cập nhật loại sản phẩm thành công!";
                return RedirectToAction("Index");  // Chuyển hướng về trang danh sách
            }

            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction("CreateOrUpdate", new { id = request.IdLoaiSanPham });  // Trả lại trang form với lỗi
        }

        // Phương thức xóa loại sản phẩm theo ID
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)  // Thay đổi kiểu id thành Guid
        {
            var result = await _quanLyLoaiSanPhamService.DeleteAsync(id);
            if (result.IsSuccessed)
            {
                TempData["SuccessMessage"] = "Xóa loại sản phẩm thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }

            return RedirectToAction("Index");  // Chuyển hướng về trang danh sách loại sản phẩm
        }

        // Phương thức lấy thông tin chi tiết loại sản phẩm theo ID
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)  // Thay đổi kiểu id thành Guid
        {
            var result = await _quanLyLoaiSanPhamService.GetByIdAsync(id);
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObj);  // Trả về chi tiết loại sản phẩm
            }

            return NotFound(result.Message);  // Trả về lỗi nếu không tìm thấy
        }
    }
}
