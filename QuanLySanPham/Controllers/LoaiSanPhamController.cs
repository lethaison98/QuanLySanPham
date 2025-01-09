using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySanPham.Data.EF;
using QuanLySanPham.Data.Entities;

namespace QuanLySanPham.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        private readonly QuanLySanPhamDbContext _context;

        public IActionResult Index()
        {
            return View();
        }
        public LoaiSanPhamController(QuanLySanPhamDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetChartData()
        {
            var data = await _context.LoaiSanPhams
                .Include(l => l.Products)
                .Select(l => new
                {
                    LoaiSanPham = l.TenLoaiSanPham,
                    SoLuong = l.Products.Count
                })
                .ToListAsync();

            return Json(data);
        }


        [AllowAnonymous]
        public IActionResult ChartsLoaiSanPham()
        {
            return View();
        }
        
    }
}
