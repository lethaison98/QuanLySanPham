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

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Authenticate(request);
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        [AllowAnonymous]
        public async Task<IActionResult> InsertUpdate(UserRequest request)
        {
            var result = await _userService.InsertUpdate(request);
            return Ok(result);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Register(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        
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
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var result = await _userService.GetAllPaging(keyword, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string idTaiKhoan)
        {
            var result = await _userService.GetById(new Guid(idTaiKhoan));
            return Ok(result);
        }
  
        [HttpPost("ChangePassByUser")]
        public async Task<IActionResult> ChangePassByUser(ChangePasswordRequest rq)
        {
            var result = await _userService.ChangePassByUser(rq);
            return Ok(result);
        }

    }
}


