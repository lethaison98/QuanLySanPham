using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySanPham.Data.EF;

namespace QuanLySanPham.Controllers
{
    public class ProductController : Controller
    {
        private readonly QuanLySanPhamDbContext _context;

        public ProductController(QuanLySanPhamDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetTotalProductCount()
        {
            var totalProductCount = await _context.Products.CountAsync();
            return Json(totalProductCount);
        }

        [HttpGet]
        public IActionResult ChartsTotalProducts()
        {
            return View();
        }

    }
}
