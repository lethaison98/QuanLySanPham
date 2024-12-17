﻿using Microsoft.EntityFrameworkCore;
using QuanLySanPham.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Data.Configurations
{
    internal class FilesConfiguration : IEntityTypeConfiguration<Files>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Files> builder)
        {
            builder.ToTable("Files");
            builder.HasKey(x => x.IdFile);
        }
    }
}
