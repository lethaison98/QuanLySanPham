using QuanLySanPham.Application.Interfaces;
using QuanLySanPham.Application.Request;
using QuanLySanPham.Application.ViewModel;
using QuanLySanPham.Data.EF;
using QuanLySanPham.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly QuanLySanPhamDbContext _context;

        public ProductService(QuanLySanPhamDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            return await _context.Products
                .Select(p => new ProductViewModel
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    LoaiSanPhamId = p.LoaiSanPhamId
                })
                .ToListAsync();
        }

        public async Task<ProductViewModel> GetByIdAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return null;

            return new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                LoaiSanPhamId = product.LoaiSanPhamId
            };
        }

        public async Task<bool> CreateAsync(ProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
                LoaiSanPhamId = request.LoaiSanPhamId
            };

            _context.Products.Add(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int productId, ProductRequest request)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;
            product.LoaiSanPhamId = request.LoaiSanPhamId;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
