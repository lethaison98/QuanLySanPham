using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuanLySanPham.Application.Common;
using QuanLySanPham.Application.Interfaces;
using QuanLySanPham.Application.Request;
using QuanLySanPham.Application.ViewModel;
using QuanLySanPham.Data.EF;
using QuanLySanPham.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuanLySanPham.Application.Service
{
    public class FileObjectService : IFileObjectService
    {
        private readonly QuanLySanPhamDbContext _context;
        private readonly IFileService _fileService;
        private readonly IDoanhNghiepService _doanhNghiepService;
        public IHttpContextAccessor _accessor;

        public FileObjectService(QuanLySanPhamDbContext context, IFileService fileService, IDoanhNghiepService doanhNghiepService, IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _fileService = fileService;
            _doanhNghiepService = doanhNghiepService;
            _accessor = HttpContextAccessor;
        }

        public async Task<ApiResult<int>> Insert(int idFile, int idObject, string loaiObject)
        {
            return new ApiSuccessResult<int>();
        }

        public async Task<ApiResult<bool>> Delete(int idFileObject)
        {
            return new ApiSuccessResult<bool>();

        }
    }
}
