using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SchoolWep.Data.Constans;
using SchoolWep.Infrustructure.Data;
using SchoolWep.Services.ServicesResult;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static SchoolWep.Data.Constans.Permission;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolWep.Services.AuthorizationServices
{
    public class AuthorizationServices : IAuthorizationServices
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IMapper _mapper;

        public AuthorizationServices(IMapper mapper,RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> UserManager)
        {
            _mapper = mapper;
            _roleManager=roleManager;
            _UserManager=UserManager;
        }

        #region RoleFunction   
        public Task<DbServicesResult> AddRoleAsync(IdentityRole Role)
        {
            throw new NotImplementedException();
        }
        public Task<DbServicesResult> DeleteRoleAsync(string Name)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRole> GetRoleByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }
        public Task<IdentityRole> GetRoleByNameAsync(string Name)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> RoleIsExsitsAsync(string Name)
        {
            return await _roleManager.RoleExistsAsync(Name); 
        }
        public Task<DbServicesResult> UpdateRoleAsync(IdentityRole Role)
        {
            throw new NotImplementedException();
        }
        public async Task<DbServicesResult> UpdateRoleClaims(List<RoleClaims> RoleClaims)
        {
            var authModel = new DbServicesResult();


            if (RoleClaims == null)
            {
                authModel.Error = true;
                authModel.Msg = "Invalid model or empty claims.";
                return authModel;
            }
            foreach (var roleClaims in RoleClaims)
            {
                var role = await _roleManager.FindByNameAsync(roleClaims.Name);
                if (role == null)
                {
                    continue;
                }

                var currentRoleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var claim in currentRoleClaims)
                {
                    await _roleManager.RemoveClaimAsync(role, claim);
                }

                foreach (var calim in roleClaims.Calims)
                {
                    var claim = new Claim(calim.Type, calim.Value);
                    await _roleManager.AddClaimAsync(role, claim);
                }

            }
            authModel.Successed = true;
            return authModel;
        }
        #endregion


        #region UserFunction 

        public async Task<DbServicesResult> DeleteUserRoleAsync(ApplicationUser user, string RoleName)
        {
            try
            {
                var result= await  _UserManager.RemoveFromRoleAsync(user, RoleName);
                if (!result.Succeeded)
                {

                    var Errors = string.Empty;
                    foreach (var Error in result.Errors)
                    {
                        Errors += $"{Error.Description} :";
                    }
                    return new DbServicesResult
                    {
                        Error = true,
                        Msg = Errors

                    };
                }

                return new DbServicesResult
                {
                    Successed = true

                };

            }
            catch(Exception ex)
            {
                return new DbServicesResult
                {
                    Error = true,
                    Msg = $"Errors Exception : {ex.Message}"

                };
            }
           
        }
     
        public async Task<List<IdentityRole>> GetRolesUserByNameAsync(ApplicationUser user)
        {
            var Roles = await _UserManager.GetRolesAsync(user);
            var UserRole = new List<IdentityRole>();

            foreach (var role in Roles)
            {
                var Tep = await _roleManager.FindByNameAsync(role);
                UserRole.Add(Tep);
            }

            return UserRole;
        }

        public async Task<DbServicesResult> AddUserRoleRengeAsync(ApplicationUser user, List<string> RolesName)
        {
            var resultModel = new DbServicesResult();

            var currentRoles = await _UserManager.GetRolesAsync(user);

    
            var rolesAlreadyExist = RolesName.Intersect(currentRoles).ToList();
            var rolesToBeAdded = RolesName.Except(currentRoles).ToList();

            // إذا كانت هناك أدوار موجودة بالفعل
            if (rolesAlreadyExist.Any())
            {
                resultModel.Error = true;
                resultModel.Msg = $"Roles [{string.Join(", ", rolesAlreadyExist)}] already exist for the user.";
                return resultModel;
            }

   
            
            foreach (var role in rolesToBeAdded)
            {
                var result = await _UserManager.AddToRoleAsync(user, role);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    resultModel.Error = true;
                    resultModel.Msg = $"Failed to add role [{role}]: {errors}";
                    return resultModel;
                }
            }

   
            resultModel.Successed = true;
            resultModel.Msg = "Roles added successfully.";
            return resultModel;

        }

        public async Task<bool> RoleIsExsitsInUser(ApplicationUser user, string RoleName)
        {
            return await _UserManager.IsInRoleAsync(user, RoleName);
        }

        public async Task<ResultMangeClaimUser> GetUserClaims(ApplicationUser user)
        {
            var AuthModel = new ResultMangeClaimUser();
            var UserRoles = await _UserManager.GetRolesAsync(user);

            // التحقق من وجود أدوار
            if (!UserRoles.Any())
            {
                AuthModel.Error = true;
                AuthModel.Msg = "No roles assigned to this user.";
                return AuthModel;
            }

            // جمع جميع المطالبات بناءً على الأدوار
          

            foreach (var role in UserRoles)
            {
                var roleEntity = await _roleManager.FindByNameAsync(role);
                if (roleEntity != null)
                {
                    var roleClaims = await GetClaimRoleUserAsync(roleEntity);

                    var claims = _mapper.Map<List<Calims>>(roleClaims);
                    var RoleClaims = new RoleClaims()
                    {
                        Calims = claims,
                         Name = role,
                    };
                    AuthModel.RoleClaims.Add(RoleClaims) ;        
                }
            }
          

            var userClaims = await GetClaimUserAsync(user);

            AuthModel.UserId = user.Id;

            AuthModel.UserClaims.Calims = _mapper.Map<List<Calims>>(userClaims);
       
            AuthModel.Successed = true;

            return AuthModel;
        }

        private async Task<List<Claim>> GetClaimUserAsync(ApplicationUser user)
        {
            var claim = await _UserManager.GetClaimsAsync(user);

            return claim.ToList();
        }
        private async Task<List<Claim>> GetClaimRoleUserAsync(IdentityRole role)
        {
            var claim = await _roleManager.GetClaimsAsync(role);

            return claim.ToList();
        }
      
        public async Task<DbServicesResult> UpdateUserClaims(string iduser, UserClaims UserClaims)
        {
            var authModel = new DbServicesResult();

       
            if (UserClaims == null)
            {
                authModel.Error = true;
                authModel.Msg = "Invalid model or empty claims.";
                return authModel;
            }


           var user = await _UserManager.FindByIdAsync(iduser);
            if (user == null)
            {
                authModel.Error = true;
                authModel.Msg = "User not found.";
                return authModel;
            }
               
                var currentUserClaims = await _UserManager.GetClaimsAsync(user);

                foreach (var claim in currentUserClaims)
                {
                    await _UserManager.RemoveClaimAsync(user, claim);
                }

                foreach (var calim in UserClaims.Calims)
                {
                    var claim = new Claim(calim.Type, calim.Value);
                    await _UserManager.AddClaimAsync(user, claim);
                }
           
          
            authModel.Successed = true;
            return authModel;
        }
        #endregion















    }
}
