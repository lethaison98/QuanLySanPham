using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Application.Request
{
    public class FileUploadRequest
    {
        public string NhomFile { get; set; }
        public IFormFile File { get; set; }

    }
}
