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
    public interface IFileService
    {
        public Task<ApiResult<int>> Insert(FileUploadRequest req);
        public Task<ApiResult<bool>> Delete(int idFile);
    }
}
