using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuanLySanPham.Application.Interfaces;
using QuanLySanPham.Application.Request;
using QuanLySanPham.Application.ViewModel;
using QuanLySanPham.Data.EF;
using QuanLySanPham.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySanPham.Application.Service
{
    public class UserService : IUserService
    {
        private readonly QuanLySanPhamDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        public IHttpContextAccessor _accessor { get; set; }
        public UserService(QuanLySanPhamDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, IConfiguration config, IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _accessor = HttpContextAccessor;
        }
        //thêm mới hoặc cập nhật thông tin user
        public async Task<ApiResult<bool>> InsertUpdate(UserRequest request)
        {
            if (string.IsNullOrEmpty(request.UserName))  
            {
                return new ApiErrorResult<bool>("Tên đăng nhập không được để trống");
            }
            if (request.UserId == new Guid())
            {
                var usercheck = _userManager.FindByNameAsync(request.UserName).Result;
                if (usercheck != null)
                {
                    return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
                }
                if (!String.IsNullOrEmpty(request.Email) && await _userManager.FindByEmailAsync(request.Email) != null)
                {
                    return new ApiErrorResult<bool>("Email đã tồn tại");
                }
                var user = new AppUser()
                {
                    Email = request.Email,
                    FullName = request.FullName,
                    UserName = request.UserName,
                    PhoneNumber = request.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    var u = await _userManager.FindByNameAsync(request.UserName);
                    await _userManager.AddToRolesAsync(u, request.DsRole);
                    return new ApiSuccessResult<bool>();
                }
            }
            else
            {
                var users = await _userManager.FindByIdAsync(request.UserId.ToString());
                if(users != null && users.UserName != request.UserName)
                {
                    var usercheck = _userManager.FindByNameAsync(request.UserName).Result;
                    if (usercheck != null)
                    {
                        return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
                    }
                }
                if(users != null && !String.IsNullOrEmpty(request.Email) && users.Email != request.Email)
                {
                    var usercheck = await _userManager.FindByEmailAsync(request.Email);
                    if (usercheck != null)
                    {
                        return new ApiErrorResult<bool>("Email đã tồn tại");
                    }
                }
                if (users != null)
                {
                    users.Email = request.Email;
                    users.FullName = request.FullName;
                    users.UserName = request.UserName;
                    users.PhoneNumber = request.PhoneNumber;
                    var result = await _userManager.UpdateAsync(users);
                    if (result.Succeeded)
                    {
                        var currentRoles = await _userManager.GetRolesAsync(users);
                        var u = await _userManager.FindByNameAsync(request.UserName);
                        await _userManager.RemoveFromRolesAsync(users, currentRoles);
                        await _userManager.AddToRolesAsync(u, request.DsRole);
                        return new ApiSuccessResult<bool>();
                    }
                }
            }

            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }

        //đăng nhập
        public async Task<ApiResult<UserLoginViewModel>> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null) return new ApiErrorResult<UserLoginViewModel>("Tài khoản không tồn tại"); ;

            var login = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!login.Succeeded)
            {
                return new ApiErrorResult<UserLoginViewModel>("Đăng nhập không thành công");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim("UserName", user.UserName.ToString()),
                new Claim("UserId", user.Id.ToString()),
                //new Claim("ip", ipAddress),
                new Claim("FullName", user.FullName.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            var result = new UserLoginViewModel();
            result.Token = new JwtSecurityTokenHandler().WriteToken(token);
            result.UserName = user.UserName;
            result.FullName = user.FullName;
            result.Roles = string.Join(';', roles);
            return new ApiSuccessResult<UserLoginViewModel>(result);
        }

        //đăng ký
        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }

            user = new AppUser()
            {
                Email = request.Email,
                FullName = request.FullName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }
        public async Task<ApiResult<bool>> InsertRole(RoleRequest rq)
        {
            var role = new AppRole()
            {
                Name = rq.Name,
                NormalizedName = rq.NormalizedName,
                MoTa = rq.MoTa,
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }
        public async Task<ApiResult<bool>> InsertUser_Role(string userId, List<string> listRoleId)
        {
            var claimsIdentity = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            var user = await _userManager.FindByNameAsync(userId);
            var result = await _userManager.AddToRolesAsync(user, listRoleId);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }
        public async Task<ApiResult<bool>> InsertRoleClaims(string roleId, List<Claim> listClaims)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                foreach (var claim in listClaims)
                {
                    await _roleManager.AddClaimAsync(role, claim);
                }

                return new ApiSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>("Cập nhật không thành công");
            }
        }
        //Lấy danh sách người dùng với phân trang (hiển thị toàn bộ danh sách người dùng)
        public async Task<ApiResult<PageViewModel<UserViewModel>>> GetAllPaging(string keyword, int pageIndex, int pageSize)
        {
            var query = from a in _context.AppUser
                        select a ;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => (x.FullName.ToLower().Contains(keyword.ToLower())) || (x.UserName.ToLower().Contains(keyword.ToLower())));
            }

            var data = query.OrderByDescending(x => x.UserName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
            var listItem = new List<UserViewModel>();
            foreach (var entity in data)
            {
                var user = new UserViewModel
                {
                    UserId = entity.Id,
                    FullName = entity.FullName,   
                    UserName = entity.UserName, 
                    PhoneNumber = entity.PhoneNumber,
                    Email = entity.Email,
                };
                var listRole = await _userManager.GetRolesAsync(entity);
                user.DsRole = listRole.ToList();          
                listItem.Add(user);
            }
            var result = new PageViewModel<UserViewModel>()
            {
                Items = listItem,
                PageIndex = pageIndex,
                TotalRecord = query.Count(),
                PageSize = pageSize
            };
            return new ApiSuccessResult<PageViewModel<UserViewModel>>() { Data = result };
        }

        //lấy thông tin role cho từng user
        public async Task<List<string>> GetRolesForUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return new List<string>();

            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        //public async Task<ApiResult<PageViewModel<UserViewModel>>> GetRolesForUser(string keyword, int pageIndex, int pageSize)
        //{
        //    var query = _context.AppUser.AsQueryable();

        //    if (!string.IsNullOrEmpty(keyword))
        //    {
        //        query = query.Where(u => u.UserName.Contains(keyword) || u.FullName.Contains(keyword));
        //    }

        //    var users = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //    var listItem = new List<UserViewModel>();

        //    foreach (var user in users)
        //    {
        //        var roles = await _userManager.GetRolesAsync(user);
        //        listItem.Add(new UserViewModel
        //        {
        //            UserId = user.Id,
        //            FullName = user.FullName,
        //            UserName = user.UserName,
        //            PhoneNumber = user.PhoneNumber,
        //            Email = user.Email,
        //            DsRole = roles.ToList()
        //        });
        //    }

        //    var result = new PageViewModel<UserViewModel>
        //    {
        //        Items = listItem,
        //        PageIndex = pageIndex,
        //        PageSize = pageSize,
        //        TotalRecord = query.Count()
        //    };

        //    return new ApiSuccessResult<PageViewModel<UserViewModel>>(result);
        //}

        //Lấy thông tin người dùng theo ID để cập nhật, sửa
        public async Task<ApiResult<UserViewModel>> GetById(Guid idTaiKhoan)
        {
            var result = new UserViewModel();
            var entity = _context.AppUser.FirstOrDefault(x => x.Id == idTaiKhoan);
            if (entity != null)
            {
                result = new UserViewModel
                {
                    UserId = entity.Id,
                    FullName = entity.FullName,
                    UserName = entity.UserName,
                    PhoneNumber = entity.PhoneNumber,
                    Email = entity.Email,
                };
                var listRole = await _userManager.GetRolesAsync(entity);
                result.DsRole = listRole.ToList();
                return new ApiSuccessResult<UserViewModel>() { Data = result };

            }
            else
            {
                return new ApiErrorResult<UserViewModel>("Không tìm thấy dữ liệu");
            }
        }

        //thay đổi mật khẩu (ChangePassByUser cho phép người dùng thay đổi mật khẩu của mình-> liên quan đến quản lý user)
        public async Task<ApiResult<bool>> ChangePassByUser(ChangePasswordRequest request)
        {
            try
            {
                var claimsIdentity = _accessor.HttpContext.User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.FindFirst("UserId")?.Value;
                var user = await _userManager.FindByIdAsync(userId);
                var x = new object();
                if (request.NewPassword != null && request.OldPassword != null)
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
                    if (changePasswordResult.Succeeded)
                    {
                        await _userManager.UpdateAsync(user);
                        return new ApiSuccessResult<bool>() { };
                    }
                    else
                    {
                        return new ApiErrorResult<bool>(changePasswordResult.Errors.First().Description) { };
                    }
                }
                return new ApiErrorResult<bool>();
            }catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);

            }

        }
        //Đặt lại mật khẩu
        public async Task<ApiResult<bool>> ResetPassword(ChangePasswordRequest request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.OldPassword);
                var x = new object();
                if (request.NewPassword != null && request.OldPassword != null)
                {
                    var removePass = await _userManager.RemovePasswordAsync(user);
                    var changePasswordResult = await _userManager.AddPasswordAsync(user, request.NewPassword);
                    if (changePasswordResult.Succeeded)
                    {
                        await _userManager.UpdateAsync(user);
                        return new ApiSuccessResult<bool>() { };
                    }
                    else
                    {
                        return new ApiErrorResult<bool>(changePasswordResult.Errors.First().Description) { };
                    }
                }
                return new ApiErrorResult<bool>();
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);

            }
        }
    }
}
