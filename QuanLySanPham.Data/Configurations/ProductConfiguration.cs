using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLySanPham.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product"); 

            builder.HasKey(x => x.ProductId);

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.Description);

            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");

            builder.Property(x => x.Stock).IsRequired();

            builder.HasOne(x => x.LoaiSanPham) .WithMany(l => l.Products).HasForeignKey(x => x.LoaiSanPhamId);
            builder.HasOne(x => x.LoaiSanPham)
       .WithMany(l => l.Products)
       .HasForeignKey(x => x.LoaiSanPhamId)
       .OnDelete(DeleteBehavior.Cascade); // Khi xóa LoaiSanPham, các sản phẩm liên quan cũng bị xóa

        }
    }
}
