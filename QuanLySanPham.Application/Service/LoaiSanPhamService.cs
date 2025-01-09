using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySanPham.Application.Interfaces;
using QuanLySanPham.Application.Request;
using QuanLySanPham.Application.ViewModel;
using QuanLySanPham.Data.EF;
using QuanLySanPham.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Application.Service
{
    public class LoaiSanPhamService : ILoaiSanPhamService
    {
        private readonly QuanLySanPhamDbContext _context;

        public LoaiSanPhamService(QuanLySanPhamDbContext context)
        {
            _context = context;
        }

        public async Task<List<LoaiSanPhamViewModel>> GetAllAsync()
        {
            return await _context.LoaiSanPhams
                .Select(l => new LoaiSanPhamViewModel
                {
                    IdLoaiSanPham = l.IdLoaiSanPham,
                    TenLoaiSanPham = l.TenLoaiSanPham,
                    MoTa = l.MoTa
                })
                .ToListAsync();
        }

        public async Task<LoaiSanPhamViewModel> GetByIdAsync(int id)
        {
            var loaiSanPham = await _context.LoaiSanPhams.FindAsync(id);
            if (loaiSanPham == null) return null;

            return new LoaiSanPhamViewModel
            {
                IdLoaiSanPham = loaiSanPham.IdLoaiSanPham,
                TenLoaiSanPham = loaiSanPham.TenLoaiSanPham,
                MoTa = loaiSanPham.MoTa
            };
        }

        public async Task<bool> CreateAsync(LoaiSanPhamRequest request)
        {
            var loaiSanPham = new LoaiSanPham
            {
                TenLoaiSanPham = request.TenLoaiSanPham,
                MoTa = request.MoTa
            };

            _context.LoaiSanPhams.Add(loaiSanPham);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, LoaiSanPhamRequest request)
        {
            var loaiSanPham = await _context.LoaiSanPhams.FindAsync(id);
            if (loaiSanPham == null) return false;

            loaiSanPham.TenLoaiSanPham = request.TenLoaiSanPham;
            loaiSanPham.MoTa = request.MoTa;

            return await _context.SaveChangesAsync() > 0;
        }



        public async Task<bool> DeleteAsync(int id)
        {
            var loaiSanPham = await _context.LoaiSanPhams.FindAsync(id);
            if (loaiSanPham == null) return false;

            _context.LoaiSanPhams.Remove(loaiSanPham);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
