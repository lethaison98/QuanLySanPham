using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using QuanLySanPham.Application.Common.Constant;
using QuanLySanPham.Application.Interfaces;
using QuanLySanPham.Application.Request;
using QuanLySanPham.Application.ViewModel;
using QuanLySanPham.Data.EF;
using QuanLySanPham.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Application.Service
{
    public class DoanhNghiepService : IDoanhNghiepService
    {
        private readonly QuanLySanPhamDbContext _context;
        public IHttpContextAccessor _accessor { get; set; }

        public DoanhNghiepService(QuanLySanPhamDbContext context,
            IHttpContextAccessor HttpContextAccessor )
        {
            _context = context;
            _accessor = HttpContextAccessor;
        }

        public async Task<ApiResult<int>> InsertUpdate(DoanhNghiepRequest rq)
        {
            return new ApiSuccessResult<int>();
        }

        public async Task<ApiResult<bool>> Delete(int idDoanhNghiep)
        {
            return new ApiSuccessResult<bool>();

        }

        public async Task<ApiResult<List<DoanhNghiep>>> GetAll()
        {
            return new ApiSuccessResult<List<DoanhNghiep>>();
        }

        public async Task<ApiResult<PageViewModel<DoanhNghiep>>> GetAllPaging(string keyword, int pageIndex, int pageSize)
        {
            return new ApiSuccessResult<PageViewModel<DoanhNghiep>>();
        }

        public async Task<ApiResult<DoanhNghiep>> GetById(int idDoanhNghiep)
        {
            return new ApiSuccessResult<DoanhNghiep>();
        }
    }
}
