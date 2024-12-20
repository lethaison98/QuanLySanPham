using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Data.Entities
{
    public class AppUser: IdentityUser<Guid>
    {
        //còn nhiều thuộc tính nữa
        public string FullName { get; set; }



    }
}
