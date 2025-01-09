using Microsoft.AspNetCore.Mvc;
using QuanLySanPham.Application.Interfaces;
using QuanLySanPham.Application.Request;
using System.Threading.Tasks;
using QuanLySanPham.Application.ViewModel;
using QuanLySanPham.Data.Entities;


namespace QuanLySanPham.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetAll();
            if(result.IsSuccess) {
                return View(result.Data);
            }

            return View(new List<Product>());
        }

        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(ProductRequest request)
        {
            var result = await _productService.InsertUpdate(request);
            return Ok(result);
        }

        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var result = await _productService.GetAllPaging(keyword, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int productId)
        {
            var result = await _productService.Delete(productId);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int productId)
        {
            var result = await _productService.GetById(productId);
            return Ok(result);
        }
    }

}
