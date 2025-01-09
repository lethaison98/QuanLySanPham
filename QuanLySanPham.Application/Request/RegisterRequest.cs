using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Application.Request
{
    public class RegisterRequest
    {
        public string FullName { get; set; }
        public string DonVi { get; set; }
        public DateTime NgaySinh { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class RoleRequest
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string MoTa { get; set; }
    }

//    3. UserRequest
//Lớp này được sử dụng để thêm mới hoặc cập nhật thông tin người dùng trong hệ thống.
//UserId: ID của người dùng, sử dụng kiểu Guid để đảm bảo mỗi người dùng có một mã duy nhất.
//FullName: Tên đầy đủ của người dùng.
//UserName: Tên người dùng (dùng để đăng nhập).
//Password: Mật khẩu người dùng(có thể là mật khẩu mới hoặc mật khẩu cũ nếu người dùng đang được cập nhật).
//Email: Địa chỉ email của người dùng.
//PhoneNumber: Số điện thoại của người dùng.
//DsRole: Danh sách các vai trò mà người dùng này có, ví dụ như Admin, User, Manager...Một người có thể có nhiều vai trò trong hệ thống.
    public class UserRequest
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<String>DsRole { get; set; }
    }
    public class ChangePasswordRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; } 
    }
}
