using Microsoft.AspNetCore.Http;
using QuanLySanPham.Application.Request;
using QuanLySanPham.Application.ViewModel;
using QuanLySanPham.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Application.Interfaces
{
    public interface IDoanhNghiepService
    {
        public Task<ApiResult<int>> InsertUpdate(DoanhNghiepRequest rq);
        public Task<ApiResult<bool>> Delete(int idDoanhNghiep);
        public Task<ApiResult<DoanhNghiep>> GetById(int idDoanhNghiep);
        public Task<ApiResult<List<DoanhNghiep>>> GetAll();
        public Task<ApiResult<PageViewModel<DoanhNghiep>>> GetAllPaging(string keyword, int pageIndex, int pageSize);
    }
}
