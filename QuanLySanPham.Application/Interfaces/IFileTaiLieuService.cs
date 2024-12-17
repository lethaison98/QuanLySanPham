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
    public interface IFileObjectService
    {
        public Task<ApiResult<int>> Insert(int idFile, int idObject, string loaiObject);
        public Task<ApiResult<bool>> Delete(int idFileObject);
        //public Task<ApiResult<QuyetDinhMienTienThueDatViewModel>> GetById(int idQuyetDinhMienTienThueDat);
        //public Task<ApiResult<List<QuyetDinhMienTienThueDatViewModel>>> GetAll(int? idDoanhNghiep);
        //public Task<ApiResult<PageViewModel<QuyetDinhMienTienThueDatViewModel>>> GetAllPaging(int? idDoanhNghiep,string keyword, int pageIndex, int pageSize);
    }
}
