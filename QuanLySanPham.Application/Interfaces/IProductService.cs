using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLySanPham.Application.Request; // Namespace của LoaiSanPhamRequest
using QuanLySanPham.Application.ViewModel; // Namespace của LoaiSanPhamViewModel


namespace QuanLySanPham.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetAllAsync();
        Task<ProductViewModel> GetByIdAsync(int productId);
        Task<bool> CreateAsync(ProductRequest request);
        Task<bool> UpdateAsync(int productId, ProductRequest request);
        Task<bool> DeleteAsync(int productId);
    }
}
