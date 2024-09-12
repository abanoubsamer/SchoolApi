using SchoolWep.Infrustructure.Data;
using SchoolWep.Services.ServicesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Services.UserServices
{
    public interface IUserServices
    {
        public Task<ApplicationUser> FindUserById(string Id);
        public Task<ApplicationUser> FindUserByEmailAsync(string Email);
        public Task<DbServicesResult> DeleteUserAsync(ApplicationUser User);
        public Task<DbServicesResult> UpdateUserAsync(ApplicationUser User);
        public Task<ConfirmEmailResult> CheckEmail(string Email, string Password);
        public Task<bool> EmailIsExsitAsync(string Email);
        public Task<bool> UserNameIsExsitAsync(string UserName);
        public List<string> GetRolesUser(ApplicationUser user);
        public Task<List<string>>  GetRolesUserAsync(ApplicationUser user);
        public Task<List<ApplicationUser>> GetListUsers();
        public Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        public Task<DbServicesResult> SendOtpResetPassword(string Email);
        public Task<(string,bool)> ConfirmOtpPassword(string Email, string Otp);
        public Task<DbServicesResult> ChangePasswordAsync(ApplicationUser user, string Newpassword, string oldpassword);
        public Task<DbServicesResult> ChangePasswordAsync(ApplicationUser user, string Newpassword);

    }
}
