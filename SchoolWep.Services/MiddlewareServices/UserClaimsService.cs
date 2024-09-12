using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using SchoolWep.Infrustructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace SchoolWep.Services.MiddlewareServices
{
    public class UserClaimsService : IUserClaimsService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public UserClaimsService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IMemoryCache cache, IMapper mapper)
        {
            _roleManager=roleManager;
            _userManager = userManager;
            _cache = cache;
            _mapper = mapper;
        }
        public async Task<bool> ValidateTokenClaimsAsync(string token)
        {
            var tokenClaims = ExtractClaimsFromToken(token);
            if (tokenClaims == null || !tokenClaims.Any())
                return false;

            var userIdClaim = tokenClaims.FirstOrDefault(c => c.Type == "nameid")?.Value;
            if (userIdClaim == null)
                return false;

            // محاولة استرجاع Claims من In-Memory Cache
            if (!_cache.TryGetValue($"UserClaims_{userIdClaim}", out CachedClaimsData cachedData))
            {
                // إذا لم يتم العثور على Claims في Cache، استرجاعها من قاعدة البيانات
                var user = await _userManager.FindByIdAsync(userIdClaim);
                if (user == null)
                    return false;

                var userClaimsList = (await _userManager.GetClaimsAsync(user)).ToList();
                var userRolesList = (await _userManager.GetRolesAsync(user)).Select(role => new Claim("role", role)).ToList();
                var RolesCliamsList = new List<Claim>();
                foreach (var item in userRolesList)
                {
                    var Role = await _roleManager.FindByNameAsync(item.Value);
                    var Tep = await _roleManager.GetClaimsAsync(Role);
                    RolesCliamsList.AddRange(Tep);
                }
                

              
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                };

                _cache.Set($"UserClaims_{userIdClaim}", new CachedClaimsData
                {
                    Claims = userClaimsList,
                    Roles = userRolesList,
                    permission = RolesCliamsList,
                }, cacheOptions);

              
                cachedData = new CachedClaimsData
                {
                    Claims = userClaimsList,
                    Roles = userRolesList,
                     permission = RolesCliamsList,
                };
            }

            var cachedUserClaims = cachedData.Claims;
            var cachedUserRoles = cachedData.Roles;
            var cachepermissionUser = cachedData.permission;
            
            var permissionToken = tokenClaims.Where(x => x.Type == "Permission");
           
            bool claimsMatch = cachedUserClaims.All(dbClaim =>
                tokenClaims.Any(c => c.Type == dbClaim.Type && c.Value == dbClaim.Value));

            if (!claimsMatch)
                return false;

           
            bool permissionsMatch = permissionToken.All(permissionClaim =>
                cachepermissionUser.Any(c => c.Type == permissionClaim.Type && c.Value == permissionClaim.Value));

            if (!permissionsMatch)
                return false;
            
            var tokenRoles = tokenClaims.Where(c => c.Type == "role").ToList();
           
            bool rolesMatch = tokenRoles.All(cachedRole =>
                cachedUserRoles.Any(t => t.Type == cachedRole.Type && t.Value == cachedRole.Value));

            if (!rolesMatch)
                return false;

            return true; 
        }

        private List<Claim> ExtractClaimsFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            return jwtToken?.Claims.ToList();
        }

        private class CachedClaimsData
        {
            public List<Claim> Claims { get; set; }
            public List<Claim> Roles { get; set; }
            public List<Claim> permission { get; set; }
        }
    }
}