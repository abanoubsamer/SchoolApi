using Microsoft.AspNetCore.Identity;
using SchoolWep.Infrustructure.Data;
using SchoolWep.Services.ServicesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Services.AuthorizationServices
{
    public interface IAuthorizationServices
    {

        #region RoleFunction
        public Task<DbServicesResult> AddRoleAsync(IdentityRole Role);
        public Task<DbServicesResult> UpdateRoleAsync(IdentityRole Role);
        public Task<DbServicesResult> DeleteRoleAsync(string Name);
        public Task<IdentityRole> GetRoleByIdAsync(string Id);
        public Task<IdentityRole> GetRoleByNameAsync(string Name);
        public Task<bool> RoleIsExsitsAsync(string Name);
        public Task<DbServicesResult> UpdateRoleClaims(List<RoleClaims> RoleClaims);
        #endregion



        #region UserFunction
        public Task<DbServicesResult> AddUserRoleRengeAsync(ApplicationUser user, List<string> RolesName);
        public Task<List<IdentityRole>> GetRolesUserByNameAsync(ApplicationUser user);
        public Task<DbServicesResult> DeleteUserRoleAsync(ApplicationUser user,string RoleName);
        public Task<bool> RoleIsExsitsInUser(ApplicationUser user, string RoleName);
        public Task<ResultMangeClaimUser> GetUserClaims(ApplicationUser user);
        public Task<DbServicesResult> UpdateUserClaims(string iduser, UserClaims UserClaims);
     
        #endregion







    }
}
