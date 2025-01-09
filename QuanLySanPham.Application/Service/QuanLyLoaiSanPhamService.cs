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
    public class QuanLyLoaiSanPhamService : IQuanLyLoaiSanPhamService
    {
        private readonly QuanLySanPhamDbContext _context;
        public IHttpContextAccessor _accessor { get; set; }

        public QuanLyLoaiSanPhamService (QuanLySanPhamDbContext context,  IHttpContextAccessor HttpContextAccessor )
        {
            _context = context;
            _accessor = HttpContextAccessor;
        }

        
        public async Task<ApiResult<PageViewModel<LoaiSanPhamViewModel>>> DanhSachSP(string keyword, int pageIndex, int pageSize)
        {
            // Lấy dữ liệu từ bảng Loại Sản Phẩm
            var query = from a in _context.LoaiSanPham
                        select a;

            // Tìm kiếm theo từ khóa nếu có
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.TenLoaiSanPham.ToLower().Contains(keyword.ToLower()));
            }

            // Lấy dữ liệu phân trang
            var data = query.OrderByDescending(x => x.NgayTao)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Chuyển đổi dữ liệu sang ViewModel
            var listItem = new List<LoaiSanPhamViewModel>();
            foreach (var entity in data)
            {
                var loaiSanPham = new LoaiSanPhamViewModel
                {
                    IdLoaiSanPham = entity.IdLoaiSanPham,
                    TenLoai = entity.TenLoaiSanPham,
                    MoTa = entity.MoTa,
                    NgayTao = entity.NgayTao,
                    NgayCapNhat = entity.NgayCapNhat
                };

                listItem.Add(loaiSanPham);
            }

            // Tạo kết quả phân trang
            var result = new PageViewModel<LoaiSanPhamViewModel>()
            {
                Items = listItem,
                PageIndex = pageIndex,
                TotalRecord = query.Count(),
                PageSize = pageSize
            };

            return new ApiSuccessResult<PageViewModel<LoaiSanPhamViewModel>>() { Data = result };
        }

        public Task<IQuanLyLoaiSanPhamService.ApiResult<LoaiSanPhamViewModel>> LayLoaiSPID(Guid id)
        {
            throw new NotImplementedException();
        }

       
        public async Task<ApiResult<bool>> CreateLSP(LoaiSanPhamRequest request)
        {
            if (string.IsNullOrEmpty(request.TenLoai))
            {
                return new ApiErrorResult<bool>("Tên loại sản phẩm không được để trống");
            }

            if (request.IdLoaiSanPham == Guid.Empty) // Thêm mới
            {
                var loaiSanPhamCheck = await _context.LoaiSanPham
                    .FirstOrDefaultAsync(x => x.TenLoaiSanPham.ToLower() == request.TenLoai.ToLower());
                if (loaiSanPhamCheck != null)
                {
                    return new ApiErrorResult<bool>("Loại sản phẩm đã tồn tại");
                }

                var loaiSanPham = new LoaiSanPham
                {
                //    IdLoaiSanPham = Guid.NewGuid(),
                    TenLoaiSanPham = request.TenLoai,
                    MoTa = request.MoTa,
                    NgayTao = DateTime.Now,
                    NgayCapNhat = null
                };

                _context.LoaiSanPham.Add(loaiSanPham);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiSuccessResult<bool>();
                }
            }
            else // Cập nhật
            {
                var loaiSanPham = await _context.LoaiSanPham.FindAsync(request.IdLoaiSanPham);
                if (loaiSanPham == null)
                {
                    return new ApiErrorResult<bool>("Loại sản phẩm không tồn tại");
                }

                if (!string.Equals(loaiSanPham.TenLoaiSanPham, request.TenLoai, StringComparison.CurrentCultureIgnoreCase))
                {
                    var loaiSanPhamCheck = await _context.LoaiSanPham
                        .FirstOrDefaultAsync(x => x.TenLoaiSanPham.ToLower() == request.TenLoai.ToLower());
                    if (loaiSanPhamCheck != null)
                    {
                        return new ApiErrorResult<bool>("Tên loại sản phẩm đã tồn tại");
                    }
                }

                loaiSanPham.TenLoaiSanPham = request.TenLoai;
                loaiSanPham.MoTa = request.MoTa;
                loaiSanPham.NgayCapNhat = DateTime.Now;

                _context.LoaiSanPham.Update(loaiSanPham);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new ApiSuccessResult<bool>();
                }
            }

            return new ApiErrorResult<bool>("Thao tác không thành công");
        }

        public Task<IQuanLyLoaiSanPhamService.ApiResult<bool>> UpdateLSP(Guid id, LoaiSanPhamRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IQuanLyLoaiSanPhamService.ApiResult<bool>> DeleteLSP(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IQuanLyLoaiSanPhamService.ApiResult<PageViewModel<LoaiSanPhamViewModel>>> TimKiem(string keyword, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
