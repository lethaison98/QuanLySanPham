using QuanLySanPham.Application.Request;
using QuanLySanPham.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Application.Interfaces
{
    public  interface IUserService
    {
//Quản lý user:
//Quản lý người dùng: Phương thức InsertUpdate(UserRequest request) dùng để thêm mới hoặc cập nhật thông tin người dùng.
//Phương thức GetAllPaging() giúp quản lý danh sách người dùng và phân trang khi hiển thị.
//Phương thức GetById(Guid idTaiKhoan) giúp lấy thông tin chi tiết của một người dùng theo ID.
//Thay đổi mật khẩu người dùng: Phương thức ChangePassByUser(ChangePasswordRequest rq) và ResetPassword(ChangePasswordRequest rq) phục vụ việc thay đổi mật khẩu của người dùng, có thể là thay đổi mật khẩu hiện tại hoặc đặt lại mật khẩu.
       
        
        public Task<ApiResult<UserLoginViewModel>> Authenticate(LoginRequest request);
        public Task<ApiResult<bool>> InsertUpdate(UserRequest request);
        public Task<ApiResult<bool>> Register(RegisterRequest request);
        public Task<ApiResult<bool>> InsertRole(RoleRequest request);
        public Task<ApiResult<bool>> InsertRoleClaims(string roleId, List<Claim> listClaims);
        public Task<ApiResult<bool>> InsertUser_Role(string userId, List<string> listRoles);
        public Task<ApiResult<PageViewModel<UserViewModel>>> GetAllPaging(string keyword, int pageIndex, int pageSize);
        //public Task<ApiResult<PageViewModel<UserViewModel>>> GetRolesForUser(string keyword, int pageIndex, int pageSize);
        public Task<List<string>> GetRolesForUser(string userName);

        public Task<ApiResult<UserViewModel>> GetById(Guid idTaiKhoan);
        public Task<ApiResult<bool>> ChangePassByUser(ChangePasswordRequest rq);
        public Task<ApiResult<bool>> ResetPassword(ChangePasswordRequest rq);
    }
}
