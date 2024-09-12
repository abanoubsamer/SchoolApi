using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolWep.Data.OptionsConfiguration;
using SchoolWep.Infrustructure.Data;
using SchoolWep.Infrustructure.Encryption;
using SchoolWep.Infrustructure.UnitOfWork;
using SchoolWep.Services.SendEmailServices;
using SchoolWep.Services.ServicesResult;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static SchoolWep.Data.Constans.Permission;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolWep.Services.UserServices
{
    public class UserServices: IUserServices
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly EncryptionService _encryptionService; 
        private readonly ISendEmailServices _SendEmailServices; 
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly ILogger<UserServices> _logger;

        #region Constructor
        public UserServices(
            RoleManager<IdentityRole> roleManager,
            IOptions<JwtOptions> jwtOptions,
            ISendEmailServices sendEmailServices,
           UserManager<ApplicationUser> UserManager
           , EncryptionService encryptionService
           , IUnitOfWork unitOfWork
           , ILogger<UserServices> logger)
        {
            _jwtOptions=jwtOptions;
            _RoleManager= roleManager;
            _SendEmailServices=sendEmailServices;
            _encryptionService = encryptionService;
            _UnitOfWork = unitOfWork;
            _logger = logger;
            _UserManager = UserManager;
         
        }
        #endregion

        #region UserFunction
        public async Task<bool> EmailIsExsitAsync(string Email)
        {
            return await _UserManager.FindByEmailAsync(Email) == null ? false : true;
        }
        
        
        public async Task<bool> UserNameIsExsitAsync(string UserName)
        {
            return await _UserManager.FindByNameAsync(UserName) == null ? false : true;
        }
        
        public async Task<ApplicationUser> FindUserById(string Id)
        {
            return await _UserManager.FindByIdAsync(Id);
        }
        
        public async Task<DbServicesResult> UpdateUserAsync(ApplicationUser User)
        {
            if (User == null) return new DbServicesResult
            {
                Error = true,
                Msg = "User Is Empty"
            };

            try
            {

                var result = await _UserManager.UpdateAsync(User);
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
                        Msg = Errors,
                    };
                }

                return new DbServicesResult
                {
                    Successed = true
                };

            }
            catch (Exception ex)
            {
                return new DbServicesResult
                {
                    Error = true,
                    Msg = $"Exception Error {ex.Message}"
                };
            }
        }

        public async Task<ConfirmEmailResult> CheckEmail(string Email, string Password)
        {
            var user = await _UserManager.FindByEmailAsync(Email);
            if (user == null || !await _UserManager.CheckPasswordAsync(user, Password))
            {
                return new ConfirmEmailResult
                {
                    Error = true,
                    Msg = "Email Or Password Is Invalid"
                };
            }

            return new ConfirmEmailResult
            {
                Successed = true,
                User = user,
            };
        }

        public async Task<List<ApplicationUser>> GetListUsers()
        {
            return await _UserManager.Users.ToListAsync();
        }

        public List<string> GetRolesUser(ApplicationUser user)
        {
            var Roles = _UserManager.GetRolesAsync(user);
           return Roles.Result.ToList();
        }

        public async Task<List<string>> GetRolesUserAsync(ApplicationUser user)
        {
            var Roles =await  _UserManager.GetRolesAsync(user);
            return Roles.ToList();
        }

        public async Task<DbServicesResult> DeleteUserAsync(ApplicationUser User)
        {
            if (User == null) return new DbServicesResult
            {
                Error = true,
                Msg = "User Is Empty"
            };

            try
            {

                var result = await _UserManager.DeleteAsync(User);
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
                        Msg = Errors,
                    };
                }

                return new DbServicesResult
                {
                    Successed = true
                };

            }
            catch (Exception ex)
            {
                return new DbServicesResult
                {
                    Error = true,
                    Msg = $"Exception Error {ex.Message}"
                };
            }
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _UserManager.CheckPasswordAsync(user, password);
        }

        public async Task<DbServicesResult> ChangePasswordAsync(ApplicationUser user, string Newpassword,string oldpassword)
        {
            if (user == null) return new DbServicesResult
            {
                Error = true,
                Msg = "User Is Empty"
            };
            try
            {

                var result = await _UserManager.ChangePasswordAsync(user, oldpassword, Newpassword);
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
                        Msg = Errors,
                    };
                }

                return new DbServicesResult
                {
                    Successed = true
                };

            }
            catch (Exception ex)
            {
                return new DbServicesResult
                {
                    Error = true,
                    Msg = $"Exception Error {ex.Message}"
                };
            }

        
        }

        public async Task<DbServicesResult> ChangePasswordAsync(ApplicationUser user, string Newpassword)
        {
            if (user == null) return new DbServicesResult
            {
                Error = true,
                Msg = "User Is Empty"
            };
            try
            {
                var resultRemove = await _UserManager.RemovePasswordAsync(user);
                if (!resultRemove.Succeeded)
                {
                    var Errors = string.Empty;
                    foreach (var Error in resultRemove.Errors)
                    {
                        Errors += $"{Error.Description} :";
                    }
                    return new DbServicesResult
                    {
                        Error = true,
                        Msg = Errors,
                    };
                }
                var rsuletAdd = await _UserManager.AddPasswordAsync(user, Newpassword);
                if (!rsuletAdd.Succeeded)
                {
                    var Errors = string.Empty;
                    foreach (var Error in resultRemove.Errors)
                    {
                        Errors += $"{Error.Description} :";
                    }
                    return new DbServicesResult
                    {
                        Error = true,
                        Msg = Errors,
                    };
                }
                return new DbServicesResult
                {
                    Successed = true
                };
            }
            catch (Exception ex)
            {
                return new DbServicesResult
                {
                    Error = true,
                    Msg = $"Exception Error {ex.Message}"
                };
            }
            

        }

        public async Task<DbServicesResult> SendOtpResetPassword(string Email)
        {
            // get user 
            var user = await _UserManager.FindByEmailAsync(Email);
            if (user == null) return new DbServicesResult
            {
                Error = true,
                Msg = "User Is Empty"
            };
          
            try
            {
                var Transaction= await _UnitOfWork.BeginTransactionAsync();
                // Generate Otp
                var Randem = new Random().Next(100000).ToString("D6");
                // Insert Otp In db
                user.Code = _encryptionService.Encrypt(Randem);
                var resultUpdate = await _UserManager.UpdateAsync(user);
                if (!resultUpdate.Succeeded)
                {
                    var Errors = string.Empty;
                    foreach (var Error in resultUpdate.Errors)
                    {
                        Errors += $"{Error.Description} :";
                    }
                    return new DbServicesResult
                    {
                        Error = true,
                        Msg = Errors,
                    };
                }

                // Send Otp Email
                var result = await _SendEmailServices.SendOTPResetPassword(user.Email, Randem);
                if(!result) return new DbServicesResult
                {
                    Error = true,
                    Msg = $"Invalid Sender Email"
                };

                await Transaction.CommitAsync();
                return new DbServicesResult
                {
                    Successed = true,

                };

            }
            catch (Exception ex)
            {
                await _UnitOfWork.RollBackAsync();
                return new DbServicesResult
                {
                    Error = true,
                    Msg = $"Exception Error {ex.Message}"
                };
            }
            
          
        }

        public async Task<(string,bool)> ConfirmOtpPassword(string Email, string Otp)
        {
            try
            { // user 
                var Transaction = await _UnitOfWork.BeginTransactionAsync();
                var user = await _UserManager.FindByEmailAsync(Email);
                if (user == null) return ("NotFoundUsner", false);
                if(user.Code==null) return ("NotFoundCode", false);
                var otp = _encryptionService.Decrypt(user.Code);
                // comper 
                if (otp != Otp) return ("Invalid Otp", false);
                //deletefromdb
                user.Code = null;
                var resultupdate= await _UserManager.UpdateAsync(user);
                if (!resultupdate.Succeeded)
                {
                    var Errors = string.Empty;
                    foreach (var Error in resultupdate.Errors)
                    {
                        Errors += $"{Error.Description} :";
                    }
                    return (Errors, false);
                }
                //genrate jwt
                var token = await GenerateJwtWihtResetPassword(user);
                await Transaction.CommitAsync();
                return (token, true);
            }
            catch (Exception ex)
            {
                await _UnitOfWork.RollBackAsync();
                return (ex.Message, false);
            }
        }
        private async Task<string> GenerateJwtWihtResetPassword(ApplicationUser user)
        {

            var claims = await GetUserClaimsAsync(user);

          
            var roleClaims = await GetRoleClaimsAsync(user);

            claims.AddRange(roleClaims);

            var token = CreateJwtToken(claims);

            return token;

        }
        private async Task<List<Claim>> GetUserClaimsAsync(ApplicationUser user)
        {
            var userClaims = await _UserManager.GetClaimsAsync(user);
            var jwtId = Guid.NewGuid().ToString();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(JwtRegisteredClaimNames.Jti, jwtId),
                new Claim("ResetPassword", "true")
             };

            claims.AddRange(userClaims);
            return claims;
        }
        private async Task<List<Claim>> GetRoleClaimsAsync(ApplicationUser user)
        {
            var roles = await _UserManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var roleName in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roleName));

                var role = await _RoleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    var claimsForRole = await _RoleManager.GetClaimsAsync(role);
                    roleClaims.AddRange(claimsForRole);
                }
            }

            return roleClaims;
        }
        private string CreateJwtToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SecretKey));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Value.Audience,
                Issuer = _jwtOptions.Value.Issuer,
                Expires = DateTime.UtcNow.AddDays(_jwtOptions.Value.LiveTimeDay),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var Token=tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(Token);
        }

        public async Task<ApplicationUser> FindUserByEmailAsync(string Email)
        {
            return await _UserManager.FindByEmailAsync(Email);
        }


        #endregion
    }
}
