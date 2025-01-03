using Microsoft.EntityFrameworkCore;
using QuanLySanPham.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Data.Configurations
{
    internal class LoaiSanPhamConfiguration : IEntityTypeConfiguration<LoaiSanPham>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<LoaiSanPham> builder)
        {
            builder.ToTable("LoaiSanPham");
            builder.HasKey(x => x.IdLoaiSanPham);
            builder.Property(x => x.TenLoaiSanPham).IsRequired();

        }
    }
}
