﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Application.ViewModel
{
    //model của quản lý user
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<String> DsRole { get; set; }
    }
}
