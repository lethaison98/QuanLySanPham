using Microsoft.EntityFrameworkCore;
using QuanLySanPham.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Data.Configurations
{
    internal class FileObjectConfiguration : IEntityTypeConfiguration<FileObject>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FileObject> builder)
        {
            builder.ToTable("FileObject");
            builder.HasKey(x => x.IdFileObject);
            builder.HasOne(x => x.File).WithOne(hd => hd.FileObject).HasForeignKey<FileObject>(x=> x.IdFile);

        }
    }
}
