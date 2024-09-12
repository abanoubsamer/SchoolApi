using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SchoolWep.Infrustructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Services.AuthorizationServices.CurrentUserServicse
{
    public class CurrentUserServicse : ICurrentUserServicse
    {


        #region Fields
        public readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        #endregion


        #region Constructor
        public CurrentUserServicse(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _roleManager=roleManager;
            _userManager = userManager;
            _httpContextAccessor= httpContextAccessor;
        }
        #endregion


        #region Implemntation
        public async Task<ApplicationUser>  CurrentUser()
        {
            var UserId = GetCurrentUserId();
            var user =await _userManager.FindByIdAsync(UserId);
            if (user == null) throw new UnauthorizedAccessException();
            return user; 

        }

        public async Task<List<Claim>> GetCurrentUserClaims()
        {
            var user = await CurrentUser();
            var roles = await _userManager.GetRolesAsync(user);
            var UserClaims = new List<Claim>();
            foreach (var role in roles)
            {
                UserClaims.Add(new Claim(ClaimTypes.Role, role));
                var IdentityRole = await _roleManager.FindByNameAsync(role);
                if(IdentityRole!=null)
                UserClaims.AddRange(await _roleManager.GetClaimsAsync(IdentityRole));
            }
            UserClaims.AddRange(await _userManager.GetClaimsAsync(user));

            return UserClaims;
        }

        public string GetCurrentUserId()
        {
            var UserId = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId==null) throw new UnauthorizedAccessException();
            return UserId;
        }

        public async Task<List<string>> GetCurrentUserRoles()
        {
            var user = await CurrentUser();
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        #endregion

    }
}
