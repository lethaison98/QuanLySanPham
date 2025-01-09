using QuanLySanPham.Application.Request;
using QuanLySanPham.Application.ViewModel;
using QuanLySanPham.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLySanPham.Application.ViewModel;


namespace QuanLySanPham.Application.Interfaces
{
    public interface IProductService
    {
        Task<ApiResult<int>> InsertUpdate(ProductRequest request);
        Task<ApiResult<bool>> Delete(int productId);
        Task<ApiResult<Product>> GetById(int productId);
        Task<ApiResult<List<Product>>> GetAll();
        Task<ApiResult<PageViewModel<Product>>> GetAllPaging(string keyword, int pageIndex, int pageSize);
    }
}
