using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLySanPham.Application.Interfaces;
using QuanLySanPham.Application.Request;
using System.Security.Claims;

namespace QuanLySanPham.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //Đăng nhập 
        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Authenticate(request);
            return Ok(result);
        }

        //thêm user
        [HttpPost("InsertUpdate")]
        [AllowAnonymous]
        public async Task<IActionResult> InsertUpdate(UserRequest request)
        {
            var result = await _userService.InsertUpdate(request);
            return Ok(result);
        }

        //đăng ký
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Register(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);


            //if (!ModelState.IsValid)
            //{
            //    // Trả về danh sách lỗi cho client
            //    var errors = ModelState.Values.SelectMany(v => v.Errors)
            //                                  .Select(e => e.ErrorMessage).ToList();

            //    return BadRequest(new { IsSuccess = false, Message = "Validation failed", Errors = errors });
            //}

            //var result = await _userService.Register(request);

            //if (!result.IsSuccess)
            //{
            //    // Trả về lỗi nếu không thành công
            //    return BadRequest(new { IsSuccess = false, Message = result.Message, Errors = result.Errors });
            //}

            //// Trả về thành công
            //return Ok(new { IsSuccess = true, Message = "Registration successful." });
        }
        
        //thêm quyền
        [HttpPost("InsertRole")]
        [AllowAnonymous]
        public async Task<IActionResult> InsertRole([FromBody] RoleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.InsertRole(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        //Thêm quyền cho một vai trò cụ thể
        [HttpPost("InsertRoleClaims")]
        [Authorize]
        public async Task<IActionResult> InsertRoleClaims(string roleId, List<Claim> listClaims)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.InsertRoleClaims(roleId, listClaims);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        //Lấy danh sách user có phân trang và tìm kiếm
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var result = await _userService.GetAllPaging(keyword, pageNumber, pageSize);
            return Ok(result);
        }

        //Lấy thông tin chi tiết user theo ID.
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string idTaiKhoan)
        {
            var result = await _userService.GetById(new Guid(idTaiKhoan));
            return Ok(result);
        }

        // Đổi mật khẩu
        [HttpPost("ChangePassByUser")]
        public async Task<IActionResult> ChangePassByUser(ChangePasswordRequest rq)
        {
            var result = await _userService.ChangePassByUser(rq);
            return Ok(result);
        }

    }
}


