using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuanLySanPham.Application.Interfaces;
using QuanLySanPham.Application.Request;
using QuanLySanPham.Application.ViewModel;
using QuanLySanPham.Data.EF;
using QuanLySanPham.Data.Entities;
using System.Collections.Generic;
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
        public async Task<ApiResult<List<Product>>> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            return new ApiSuccessResult<List<Product>>(products);
        }



        public async Task<ApiResult<int>> InsertUpdate(ProductRequest request)
        {
            Product product;
            if (request.ProductId == 0)
            {
                product = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Stock = request.Stock,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Product.Add(product);
            }
            else
            {
                product = await _context.Products.FindAsync(request.ProductId);
                if (product == null)
                {
                    return new ApiErrorResult<int>("Product not found");
                }

                product.Name = request.Name;
                product.Description = request.Description;
                product.Price = request.Price;
                product.Stock = request.Stock;
                product.UpdatedAt = DateTime.Now;
                _context.Products.Update(product);
            }

            await _context.SaveChangesAsync();
            return new ApiSuccessResult<int>(product.ProductId);
        }

        public async Task<ApiResult<bool>> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return new ApiErrorResult<bool>("Product not found");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<PageViewModel<Product>>> GetAllPaging(string keyword, int pageIndex, int pageSize)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }

            int totalRecords = await query.CountAsync();
            var data = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var pageViewModel = new PageViewModel<Product>
            {
                Items = data,
                TotalRecords = totalRecords,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            return new ApiSuccessResult<PageViewModel<Product>>(pageViewModel);
        }

        public async Task<ApiResult<Product>> GetById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return new ApiErrorResult<Product>("Product not found");
            }
            return new ApiSuccessResult<Product>(product);
        }
    }
}
