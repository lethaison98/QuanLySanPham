﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Data.Entities
{
    public class Files : BaseEntity
    {
        public int IdFile { get; set; }
        public string LinkFile { get; set; }
        public string TenFile { get; set; }
        public FileObject FileObject { get; set; }
    }
}
