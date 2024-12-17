
using Microsoft.AspNetCore.Mvc;
using QuanLySanPham.Application.Interfaces;
using QuanLySanPham.Application.Request;
using QuanLySanPham.Application.ViewModel;

namespace QuanLySanPham.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadRequest req)
        {
            var result = await _fileService.Insert(req);
            return Ok(result);
        }
        
    }
}
