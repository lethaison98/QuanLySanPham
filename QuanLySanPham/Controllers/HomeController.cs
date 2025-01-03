using Microsoft.AspNetCore.Mvc;
using QuanLySanPham.Application.Request;
using System.Diagnostics;

namespace QuanLySanPham.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        // Hiển thị form đăng ký
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Xử lý dữ liệu gửi lên từ form đăng ký
        [HttpPost]
        public IActionResult Register(RegisterRequest model)
        {
            if (ModelState.IsValid)
            {
                // Xử lý logic lưu user
                // Giả lập lưu thành công
                // TODO: Gọi service để lưu user vào database

                TempData["SuccessMessage"] = "Đăng ký thành công!";
                return RedirectToAction("Index", "Home"); // Redirect về trang chủ hoặc trang khác
            }

            // Nếu dữ liệu không hợp lệ, trả về view kèm lỗi
            return View(model);
        }

        public IActionResult Logout()
        {
            // Xóa thông tin trong Session
            HttpContext.Session.Remove("Username");

            // Xóa cookie nếu có
            if (Request.Cookies["Username"] != null)
            {
                Response.Cookies.Delete("Username");
            }

            return RedirectToAction("Index");
        }

        // GET: Login View
        public IActionResult Login()
        {
            return View();
        }

        // POST: Handle Login Form Submission
        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            // Kiểm tra các trường hợp thiếu thông tin
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                ViewBag.Error = "Username và Password không được để trống.";
                return View();
            }

            // Kiểm tra thông tin đăng nhập (giả sử username: admin, password: 1234)
            if (request.Username == "admin" && request.Password == "1234")
            {
                // Đăng nhập thành công
                ViewBag.Message = "Đăng nhập thành công!";

                // Lưu thông tin đăng nhập vào cookie nếu RememberMe được chọn
                if (request.RememberMe)
                {
                    Response.Cookies.Append("Username", request.Username, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7)
                    });
                }

                HttpContext.Session.SetString("Username", request.Username);
                // Chuyển hướng đến trang chính
                return RedirectToAction("Index");
            }
            else
            {
                // Đăng nhập thất bại
                ViewBag.Error = "Sai thông tin đăng nhập hoặc vai trò không hợp lệ.";
                return View("Index");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}