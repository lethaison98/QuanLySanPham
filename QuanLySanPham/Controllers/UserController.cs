using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLySanPham.Application.Interfaces;
using QuanLySanPham.Application.Request;
using QuanLySanPham.Application.ViewModel;

namespace QuanLySanPham.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Action hiển thị danh sách người dùng với phân trang
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var result = await _userService.GetAllPaging(keyword, pageNumber, pageSize);

            // Trả về dữ liệu và phân trang cho view
            var viewModel = result.Data; // PageViewModel<UserViewModel>

            // thêm role
            foreach (var user in viewModel.Items)
            {
                user.DsRole = await _userService.GetRolesForUser(user.UserName); 
            }

            return View("Index", viewModel);  // Trả về view Index.cshtml thay vì GetAllPaging.cshtml
        }

        [HttpGet]
        public IActionResult CreateOrUpdate()
        {
            // Tạo model rỗng cho form tạo người dùng mới
            var model = new UserRequest();
            return View(model);  // Trả về view CreateOrUpdate.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            // Kiểm tra nếu UserId rỗng, tức là thêm mới người dùng
            if (request.UserId == Guid.Empty)
            {
                // Tạo mới người dùng
                var result = await _userService.InsertUpdate(request);

                if (!result.IsSuccess)
                {
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction("CreateOrUpdate");
                }

                TempData["SuccessMessage"] = "Thêm người dùng thành công!";
                return RedirectToAction("GetAllPaging", "User");
            }
            else
            {
                // Cập nhật người dùng nếu UserId không rỗng
                var result = await _userService.InsertUpdate(request);

                if (!result.IsSuccess)
                {
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction("CreateOrUpdate");
                }

                TempData["SuccessMessage"] = "Cập nhật thông tin người dùng thành công!";
                return RedirectToAction("GetAllPaging", "User");
            }
        }


        //Lấy thông tin chi tiết user theo ID.
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string idTaiKhoan)
        {
            var result = await _userService.GetById(new Guid(idTaiKhoan));
            return Ok(result);
        }

    }
}
