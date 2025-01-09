using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLySanPham.Application.Request; 
using QuanLySanPham.Application.ViewModel; 


namespace QuanLySanPham.Application.Interfaces
{
    public interface ILoaiSanPhamService
    {
        Task<List<LoaiSanPhamViewModel>> GetAllAsync();
        Task<LoaiSanPhamViewModel> GetByIdAsync(int id);
        Task<bool> CreateAsync(LoaiSanPhamRequest request);
        Task<bool> UpdateAsync(int id, LoaiSanPhamRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
