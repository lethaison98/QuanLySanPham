using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLySanPham.Application.Interfaces;
using QuanLySanPham.Application.Request;
using System.Security.Claims;

namespace QuanLySanPham.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}


